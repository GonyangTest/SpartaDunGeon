using System.ComponentModel;
using System.Numerics;
using System.Text.Json;
using SpartaDungeon.Core.Equipment.Interface;
using SpartaDungeon.Core.Inventory.Interface;
using SpartaDungeon.Core.Shop;
using SpartaDungeon.Core.Shop.Interface;
using SpartaDungeon.User;

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

        public void SaveShopData(IItemShop shop)
        {
            // Save shop data to file
            string path = "shop.json";
            string json = JsonSerializer.Serialize(shop);
            File.WriteAllText(path, json);
        }

        public Player? LoadPlayerData()
        {
            // 팩토리 메서드로 의존성 생성
            IPlayerInventory inventory = DependencyContainer.CreateInventory();
            IPlayerEquipmentSlot equipmentSlot = DependencyContainer.CreateEquipmentSlot();
            
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
        public IItemShop? LoadShopData()
        {
            // 상점 데이터 로드
            string path = "shop.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                IItemShop? shop = JsonSerializer.Deserialize<ItemShop>(json);
                return shop;
            }
            return null;
        }
    }
}
