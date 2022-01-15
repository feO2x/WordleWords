using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Session;
using Synnotech.Xunit;

namespace WordleWords.Tests;

public static class RavenDb
{
    private static object LockObject { get; } = new ();
    
    private static IDocumentStore? DocumentStore { get; set; }

    public static IAsyncDocumentSession CreateSession() =>
        GetOrCreateDocumentStore().OpenAsyncSession();

    private static IDocumentStore GetOrCreateDocumentStore()
    {
        var documentStore = DocumentStore;
        if (documentStore != null)
            return documentStore;

        lock (LockObject)
        {
            documentStore = DocumentStore;
            if (documentStore != null)
                return documentStore;

            documentStore = DocumentStore = new DocumentStore
            {
                Urls = new[] { TestSettings.Configuration["ravenDbUrl"] },
                Database = TestSettings.Configuration["databaseName"]
            }.Initialize();
        }

        return documentStore;
    }
}