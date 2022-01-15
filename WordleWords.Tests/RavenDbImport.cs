using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Raven.Client.Documents;
using Synnotech.Core.Entities;
using Synnotech.Xunit;
using Xunit;

namespace WordleWords.Tests;

public static class RavenDbImport
{
    [SkippableFact]
    public static void ImportWordsToRavenDb()
    {
        Skip.IfNot(TestSettings.Configuration.GetValue<bool>("runImport"));

        var currentDirectory = Path.GetDirectoryName(typeof(RavenDbImport).Assembly.Location)!;
        using var streamReader = new StreamReader(Path.Combine(currentDirectory, "five-letter-words.txt"));
        using var session = RavenDb.CreateSession();
        string? currentLine;
        while ((currentLine = streamReader.ReadLine()) != null)
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
            session.Store(fiveLetterWord);
        }
        
        session.SaveChanges();
    }
}

public sealed class FiveLetterWord : StringEntity
{
    public char Character1 { get; init; }
    public char Character2 { get; init; }
    public char Character3 { get; init; }
    public char Character4 { get; init; }
    public char Character5 { get; init; }
}