using System.Configuration;

namespace Services.Infrastructure
{
    public interface IConfigurationFinder
    {
        string Find(string key);
    }

    public class ConfigurationFinder : IConfigurationFinder
    {
        public string Find(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}