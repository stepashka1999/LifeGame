using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Windows.UI.Xaml;

namespace LifeGame.Core.Models
{
    public static class ConfigLoader
    {
        private static string defaultDirectory = Directory.GetCurrentDirectory() + @"\configuration.json";
        public static GameConfiguration Load()
        {
            if (!File.Exists(defaultDirectory))
                return new GameConfiguration();
            else
            {
                var json = File.ReadAllText(defaultDirectory);
                return JsonSerializer.Deserialize<GameConfiguration>(json);
            }
        }

        public static void Save(GameConfiguration configuration)
        {
            var config = JsonSerializer.Serialize<GameConfiguration>(configuration);
            File.WriteAllText(defaultDirectory, config);
        }
    }
}
