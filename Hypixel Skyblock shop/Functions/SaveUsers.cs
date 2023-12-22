using Hypixel_Skyblock_shop.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hypixel_Skyblock_shop.Functions
{
    public static class SaveUsers
    {
        public static void Save(Users users)
        {
            if(users == null) users = new Users(); 

            string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Hypixel Skyblock Shop", "Users");
            if (!Directory.Exists(AppData))
            {
                Directory.CreateDirectory(AppData);
            }

            users.users.ForEach(u =>
            {
                if (!Directory.Exists(Path.Combine(AppData, u.UUID)))
                {
                    Directory.CreateDirectory(Path.Combine(AppData, u.UUID));
                }
            });

            File.WriteAllText(Path.Combine(AppData, ".users"), JsonSerializer.Serialize(users));
        }

        public static Users Load()
        {
            string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Hypixel Skyblock Shop", "Users");
            if (!Directory.Exists(AppData))
            {
                Directory.CreateDirectory(AppData);
            }

            if(File.Exists(Path.Combine(AppData, ".users")))
            {
                return JsonSerializer.Deserialize<Users>(File.ReadAllText(Path.Combine(AppData, ".users")));
            } else
            {
                return new Users();
            }
        }
    }
}
