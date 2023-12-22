using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hypixel_Skyblock_shop.Types
{
    public class User
    {
        [JsonRequired]
        public string Username { get; set; } = string.Empty;

        [JsonRequired]
        public string UUID { get; set; } = string.Empty;

        [JsonRequired]
        public object Transactions { get; set; } = null;
    }

    public class Users
    {
        [JsonRequired]
        public List<User> users = new List<User>();
    }
}
