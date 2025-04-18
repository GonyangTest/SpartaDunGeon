using System.ComponentModel;
using System.Numerics;
using System.Text.Json;
using SpartaDungeon.GamePlayer;

namespace SpartaDungeon.Managers
{
    class SaveManager
    {
        private static SaveManager? _instance = null;
        public static SaveManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SaveManager();
                }
                return _instance;
            }
        }

        public void SavePlayerData(Player player)
        {
            // Save player data to file
            string path = "playerData.json";
            string json = JsonSerializer.Serialize(player);
            File.WriteAllText(path, json);
        }

        public void SaveShopData(Shop shop)
        {
            // Save shop data to file
            string path = "shop.json";
            string json = JsonSerializer.Serialize(shop);
            File.WriteAllText(path, json);
        }

        public Player? LoadPlayerData()
        {
            // Load player data from file
            string path = "playerData.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Player? player = JsonSerializer.Deserialize<Player>(json);
                return player;
            }
            return null;
        }
        public Shop? LoadShopData()
        {
            // Load player shop from file
            string path = "shop.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Shop? shop = JsonSerializer.Deserialize<Shop>(json);
                return shop;
            }
            return null;
        }
    }
}
