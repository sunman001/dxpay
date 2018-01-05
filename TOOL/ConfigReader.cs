using System;
using System.Configuration;

namespace TOOL
{
    public class ConfigReader
    {
        public static string GetSettingValueByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("配置项的键为空");
            }
            return ConfigurationManager.AppSettings[key];
        }

    }
}
