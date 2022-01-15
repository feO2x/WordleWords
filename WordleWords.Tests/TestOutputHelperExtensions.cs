using System.Collections.Generic;
using Light.GuardClauses;
using Xunit.Abstractions;

namespace WordleWords.Tests;

public static class TestOutputHelperExtensions
{
    public static void OutputWords(this ITestOutputHelper output, List<string> words)
    {
        if (words.IsNullOrEmpty())
        {
            output.WriteLine("No words found");
            return;
        }

        for (var i = 0; i < words.Count; i++)
        {
            output.WriteLine(words[i]);
        }
    }
}