namespace StarLog
{
    public static class Constants
    {
        public static class Configuration
        {
            public const string ConnectionStringOptions = "ConnectionStringOptions";
            public const string CosmosDbOptions = "CosmosDbOptions";
        }

        public static class ContainerNames
        {
            public const string CelestialObjects = "CelestialObjects";
        }

        public static class QueryStrings
        {
            public const string GetCelestialObjectsBySearchTerm = "SELECT TOP 5 * FROM c WHERE CONTAINS(UPPER(c.Name), @term) OR CONTAINS(UPPER(c[\"Common names\"]), @term)";
        }
    }
}