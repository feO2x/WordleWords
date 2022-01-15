# Wordle Words
Test project that imports 5-letter words into RavenDB for easy querying.

To import data into RavenDB, open testsettings.json and set `runImport` to `true` (you only need to execute this test once). You can adjust the RavenDB instance by using `ravenDbUrl` and `databaseName`.

Afterwards, you can run the failing query test by setting `runQueryTest` to `true`.

Instead of using testsettings.json, you can also create a separate testsettings.Development.json file which is automatically ignored by git. Microsoft.Extensions.Configuration is used under the covers, so you can overwrite the settings of testsettings.json in this file.
