using System;
using System.IO;
using Newtonsoft.Json;

namespace FtpGazApp
{
    public static class IoClass
    {
        private const string SettingsFileName = "settings.json";

        /// <summary>
        /// Serializes settings object to settings.json file in .exe directory or reads it if it exists
        /// </summary>
        /// <returns></returns>
        public static Settings ReadSettingsFile()
        {
            string directory = Directory.GetCurrentDirectory();
            string path = Path.Combine(directory, SettingsFileName);

            if (File.Exists(path))
            {
                Console.WriteLine($"{SettingsFileName} found! Continue...");
            }
            else
            {
                Console.WriteLine($"{SettingsFileName} not found! Creating {SettingsFileName}!");

                // explicit  for visibility
                Settings settings = new Settings()
                {
                    UserName = default,
                    Password = default,

                    HostUrl = default,
                    RemotePath = default,

                    LocalPath = default,
                    LocalPathUploaded = default,
                };
                
                string settingsJson = JsonConvert.SerializeObject(settings);
                File.WriteAllText(path, settingsJson);
                
                Console.WriteLine($"{SettingsFileName} created successfully! Fill in values and relaunch exe to continue!");
                
                return null;
            }

            string settingsSerialized = File.ReadAllText(path);
            Settings newSettings = JsonConvert.DeserializeObject<Settings>(settingsSerialized);
            return newSettings;
        }
        
        public static string GetFilePathOrNull(string localPath)
        {
            string[] filePaths = Directory.GetFiles(localPath);

            if (filePaths.Length == 0)
            {
                return null;
            }

            return filePaths[0];
        }
    }
}