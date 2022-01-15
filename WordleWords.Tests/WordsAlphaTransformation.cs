using System.IO;
using Microsoft.Extensions.Configuration;
using Synnotech.Xunit;
using Xunit;

namespace WordleWords.Tests;

public static class WordsAlphaTransformation
{
    /* This test reads a text file with the name words_alpha.txt line-by-line. Each line
     * should contain only one word. The test then checks if the word has five characters,
     * if yes, it will be written to a file called five-letter-words.txt.
     * You can find the default file in the following repo: https://github.com/dwyl/english-words
     * It is not included in this repository.
     */
    [SkippableFact]
    public static void ExtractWordsWithFiveLetters()
    {
        Skip.IfNot(TestSettings.Configuration.GetValue<bool>("runExtractWordsWithFiveLetters"));

        var currentDirectory = Path.GetDirectoryName(typeof(WordsAlphaTransformation).Assembly.Location)!;
        using var streamReader = new StreamReader(Path.Combine(currentDirectory, "words_alpha.txt"));
        using var streamWriter = new StreamWriter(Path.Combine(currentDirectory, "five-letter-words.txt"));
        string? currentLine;
        while ((currentLine = streamReader.ReadLine()) != null)
        {
            if (currentLine.Length == 5)
                streamWriter.WriteLine(currentLine);
        }
    }
}