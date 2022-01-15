using System.Threading.Tasks;
using FluentAssertions;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Xunit;

namespace WordleWords.Tests;

public static class QueryTests
{
    [Fact]
    public static async Task QueryCharacter()
    {
        using var session = RavenDb.CreateSession();

        var words = await session.Query<FiveLetterWord>()
                                 .Where(word => word.Character1 == 'b')
                                 .ToListAsync();

        words.Should().NotBeEmpty();
    }
}