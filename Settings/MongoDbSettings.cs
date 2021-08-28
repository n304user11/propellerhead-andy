namespace Propellerhead_Andy.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }

        public string ConnectionString 
        {
            get
            {
                return $"{Host}";
            }
            
        }
    }
}