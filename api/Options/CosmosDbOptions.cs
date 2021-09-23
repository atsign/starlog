using System.Collections.Generic;

namespace StarLog.Options
{
    public class CosmosDbOptions
    {
        public string DatabaseName { get; set; }

        public void Deconstruct(out string databaseName)
        {
            databaseName = DatabaseName;
        }
    }
}