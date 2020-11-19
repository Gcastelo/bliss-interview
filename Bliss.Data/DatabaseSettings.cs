using System;

namespace Bliss.Data
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string QuestionsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string QuestionsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
