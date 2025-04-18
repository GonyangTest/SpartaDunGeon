namespace SpartaDungeon.Item
{
    class ItemDataBase
    {
        private static ItemDataBase _instance;

        private Dictionary<int, BaseItem> items;
        private const string ITEM_lIST_FILE = "C:\\Users\\park\\source\\repos\\SpartaDungeon\\SpartaDungeon\\Resource\\Items.csv";

        public static ItemDataBase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemDataBase();
                }
                return _instance;
            }
        }

        public ItemDataBase()
        {
            items = new Dictionary<int, BaseItem>();
            LoadItemListFromCSV();
        }

        private void LoadItemListFromCSV()
        {
            if(!File.Exists(ITEM_lIST_FILE))
            {
                return;
            }

            string[] lines = File.ReadAllLines(ITEM_lIST_FILE);
            bool isHeader = true;
            foreach (string line in lines)
            {
                if(isHeader)
                {
                    isHeader = false;
                    continue;
                }

                try
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 4)
                    {
                        ItemOptions options = new ItemOptions();
                        int id = int.Parse(parts[0]);
                        string name = parts[1];
                        string description = parts[2];
                        int price = int.Parse(parts[3]);
                        ItemType itemType = (ItemType)Enum.Parse(typeof(ItemType), parts[4]);
                        options.AddOption(ItemOptionType.Power, int.Parse(parts[5]));
                        options.AddOption(ItemOptionType.Defense, int.Parse(parts[6]));
                        options.AddOption(ItemOptionType.Health, int.Parse(parts[7]));
                        AddItem(id, name, description, price, itemType, options);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public BaseItem CreateItem(int id)
        {
            if (items.ContainsKey(id))
            {
                return (BaseItem)items[id].Clone();
            }
            else
            {
                Console.WriteLine($"Item {id} not found in the database.");
                return null;
            }
        }

        private void AddItem(int id, string name, string description, int price, ItemType itemType, ItemOptions options)
        {
            if (!items.ContainsKey(id))
            {
                switch (itemType)
                {
                    case ItemType.Weapon:
                        items[id] = new Weapon(id, name, description, price, options);
                        break;
                    case ItemType.Armor:
                        items[id] = new Armor(id, name, description, price, options);
                        break;
                    case ItemType.Ring:
                        items[id] = new Ring(id, name, description, price, options);
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Item {id} already exists in the database.");
            }
        }
    }
}
