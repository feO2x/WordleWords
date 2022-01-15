using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Synnotech.Xunit;
using Xunit;

namespace WordleWords.Tests;

public static class QueryTests
{
    [SkippableFact]
    public static async Task QueryCharacter()
    {
        Skip.IfNot(TestSettings.Configuration.GetValue<bool>("runQueryTest"));
        
        using var session = RavenDb.CreateSession();

        var words = await session.Query<FiveLetterWord>()
                                 .Where(word => word.Character1 == 'b')
                                 .ToListAsync();

        words.Should().NotBeEmpty();
    }
}