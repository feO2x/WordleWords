using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Synnotech.Xunit;
using Xunit;

namespace WordleWords.Tests;

public static class RavenDbImport
{
    [SkippableFact]
    public static async Task ImportWordsToRavenDb()
    {
        Skip.IfNot(TestSettings.Configuration.GetValue<bool>("runImport"));

        var currentDirectory = Path.GetDirectoryName(typeof(RavenDbImport).Assembly.Location)!;
        using var streamReader = new StreamReader(Path.Combine(currentDirectory, "five-letter-words.txt"));
        using var session = RavenDb.CreateSession();
        string? currentLine;
        while ((currentLine = await streamReader.ReadLineAsync()) != null)
        {
            if (currentLine.Length != 5)
                continue;

            var fiveLetterWord = new FiveLetterWord
            {
                Id = currentLine,
                Character1 = currentLine[0],
                Character2 = currentLine[1],
                Character3 = currentLine[2],
                Character4 = currentLine[3],
                Character5 = currentLine[4]
            };
            await session.StoreAsync(fiveLetterWord);
        }
        
        await session.SaveChangesAsync();
    }
}