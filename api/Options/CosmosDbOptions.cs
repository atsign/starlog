using System.Collections.Generic;

namespace StarLog.Options
{
    public class CosmosDbOptions
    {
        public string DatabaseName { get; set; }
        public List<ContainerInfo> ContainerNames {get ; set; }

        public void Deconstruct(out string databaseName, out List<ContainerInfo> containerNames)
        {
            databaseName = DatabaseName;
            containerNames = ContainerNames;
        }
    }

    public class ContainerInfo
    {
        public string Name { get; set; }
        public string PartitionKey { get; set; }
    }
}