using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hypixel_Skyblock_shop.Functions
{
    public static class SaveSettings
    {
        public static void Load()
        {
            string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Hypixel Skyblock Shop");
            if(!Directory.Exists(AppData))
            {
                Directory.CreateDirectory(AppData);
            }

            if(File.Exists(AppData + "\\settings.json"))
            {
                Globals.settings = JsonSerializer.Deserialize<Types.Settings>(File.ReadAllText(AppData + "\\settings.json"));
            } else
            {
                Globals.settings = new Types.Settings();
            }
        }

        public static void Save()
        {
            string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Hypixel Skyblock Shop");
            if (!Directory.Exists(AppData))
            {
                Directory.CreateDirectory(AppData);
            }

            File.WriteAllText(AppData + "\\settings.json", JsonSerializer.Serialize(Globals.settings));
        }
    }
}
