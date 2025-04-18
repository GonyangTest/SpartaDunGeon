using SpartaDungeon.Scene;
using SpartaDungeon.Item;
using SpartaDungeon.GamePlayer;
using SpartaDungeon.GameDungeon;

namespace SpartaDungeon.Managers
{
    public class GameManager()
    {
        public static GameManager? _Instance = null;
        private Player? _player = null;
        private Shop? _shop = null;
        private bool _isGameRunning = false;
        BaseScene? _currentScene = null;
        DungeonResult? _dungeonResult = null;

        public static GameManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GameManager();
                }
                return _Instance;
            }
        }

        public DungeonResult? DungeonResult
        {
            get { return _dungeonResult; }
        }

        public void StartGame()
        {
            // 씬 로드
            LoadScene();

            // 게임 시작
            GameLoop();
        }

        public void NewGame()
        {
            _player = new Player();
            _shop = new Shop();

            // 플레이어 초기화
            _player.Init();
            _shop.Init();
        }

        public bool LoadGame()
        {
            _player = SaveManager.instance.LoadPlayerData();
             _shop = SaveManager.instance.LoadShopData();
            return true;
        }

        public bool SaveGame()
        {
            if (_player == null || _shop == null)
            {
                Console.WriteLine("플레이어나 상점 정보가 없습니다.");
                return false;
            }

            SaveManager.instance.SavePlayerData(_player);
            SaveManager.instance.SaveShopData(_shop);
            return true;    
        }

        public string GetPlayerStatus()
        {
            if(_player == null)
            {
                Console.WriteLine("플레이어 정보가 없습니다.");
            }

            return _player.GetStatusAsString();
        }

        public void UnEquipItem(ItemType itemType)
        {
            if (_player == null)
            {
                Console.WriteLine("플레이어 정보가 없습니다.");
                return;
            }
            _player.UnEquipItem(itemType);
        }

        public void EquipItem(int index)
        {
            if (_player == null)
            {
                Console.WriteLine("플레이어 정보가 없습니다.");
                return;
            }
            _player.EquipItem(index);
        }

        private void GameLoop()
        {
            _isGameRunning = true;
            SceneManager.Instance.ChangeScene(SceneType.Intro);

            while (_isGameRunning)
            {
                // 현재 씬 업데이트
                BaseScene? newScene = SceneManager.Instance.GetCurrentScene();
                if (newScene == null)
                {
                    Console.WriteLine("현재 씬이 없습니다.");
                    _isGameRunning = false;
                    return;
                }

                if (_currentScene != newScene)
                {
                    _currentScene = newScene;
                    _currentScene.Update();
                }

                // 사용자 입력 처리
                ProcessInput();
            }
        }
        private void ProcessInput()
        {
            // 사용자 입력 처리
            BaseScene? currentScene = SceneManager.Instance.GetCurrentScene();
            if(currentScene == null)
            {
                Console.WriteLine("현재 씬이 없습니다.");
                return;
            }

            Console.WriteLine(Gobal.INPUT_PROMPT);
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                currentScene.Update();
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            // 현재 씬에 입력 전달
            if (currentScene != null)
            {
                currentScene.HandleInput(input);
            }
        }
        public string GetPlayerGold()
        {
            return _player.PlayerStatus.Gold.ToString();
        }

        public List<BaseItem> GetInventoryItmes()
        {
            if (_player == null)
            {
                Console.WriteLine("플레이어 정보가 없습니다.");
                return new List<BaseItem>();
            }
            return _player.Inventory.GetItemList();
        }

        public string GetInventoryAsString(bool isEquipping)
        {
            string result = "";
            
            if(_player == null)
            {
                Console.WriteLine("플레이어 정보가 없습니다.");
                return result;
            }
            List<BaseItem> items = _player.Inventory.GetItemList();
            for (int i = 0; i < items.Count; i++)
            {
                string line = "";
                line += "- ";
                if (isEquipping == true)
                {
                    line += $"{i + 1 + _player.EquipmentSlot.GetItemCount()}. ";
                }

                line += $"{items[i].ToString()}";
                if (i != items.Count - 1)
                {
                    line += "\n";
                }
                result += line;
            }
            return result;
        }

        public List<BaseItem> GetEquipmentItmes()
        {
            if (_player == null)
            {
                Console.WriteLine("플레이어 정보가 없습니다.");
                return new List<BaseItem>();
            }
            return _player.EquipmentSlot.GetItemList();
        }

        public string GetEquipmentAsString(bool isEquipping)
        {
            string result = "";

            if (_player == null)
            {
                Console.WriteLine("플레이어 정보가 없습니다.");
                return result;
            }
            List<BaseItem> items = _player.EquipmentSlot.GetItemList();
            for (int i = 0; i < items.Count; i++)
            {
                string line = "";
                line += "- ";
                if (isEquipping == true)
                {
                    line += $"{i + 1}. ";
                }

                line += $"[E]{items[i].ToString()}";
                if(i != items.Count - 1)
                {
                    line += "\n";
                }
                result += line;
            }
            return result;
        }

        public List<BaseItem> GetShopItems()
        {
            if (_shop == null)
            {
                Console.WriteLine("상점 정보가 없습니다.");
                return new List<BaseItem>();
            }
            return _shop.GetShopItemList();
        }

        public string GetShopItemsAsString(bool isBuying)
        {
            string result = "";

            if (_shop == null)
            {
                Console.WriteLine("상점 정보가 없습니다.");
                return result;
            }
            List<BaseItem> items = _shop.GetShopItemList();
            for (int i = 0; i < items.Count; i++)
            {
                string line = "";
                line += "- ";
                if (isBuying == true)
                {
                    line += $"{i + 1}. ";
                }

                if (_shop.HasStock(i) == false)
                {


                    line += $"{items[i].ToString()} | 구매완료";
                }
                else
                {
                    line += $"{items[i].ToString()} | {items[i].Price}";
                }
                if (i != items.Count - 1)
                {
                    line += "\n";
                }
                result += line;
            }
            return result;
        }

        public string BuyItem(int index)
        {
            if (_shop == null || _player == null)
            {
                
                return "상점 또는 플레이어 정보가 없습니다.";
            }

            if(_shop.HasStock(index))
            {
                BaseItem item = _shop.GetItem(index);
                BaseItem clone = ItemDataBase.Instance.CreateItem(item.Id);
                if (_player.BuyItem(clone))
                {
                    _shop.DecreaseStock(index);
                }
                else
                {
                    return "골드가 부족합니다.";
                }
            }
            else
            {
                return "이미 구매한 아이템입니다.";
            }

            return "구매를 완료했습니다.";
        }

        public string SellItem(int index, BaseItem item, bool isEquipped)
        {
            if (_shop == null || _player == null)
            {

                return "상점 또는 플레이어 정보가 없습니다.";
            }

            BaseItem? removedItem = null;

            if (isEquipped)
            {
                removedItem = _player.EquipmentSlot.RemoveItem(item.ItemType);
            }
            else
            {
                removedItem = _player.Inventory.RemoveItem(index);
            }

            if (removedItem == null)
            {
                return "잘못된 인덱스입니다.";
            }

            _player.AddGold((int)(removedItem.Price * 0.85));
            _shop.IncreaseStock(removedItem);
            return "판매를 완료했습니다.";
        }

        public string Rest()
        {
            if (_player == null)
            {
                return "플레이어 정보가 없습니다.";
            }
            if (_player.PlayerStatus.Gold < 500)
            {
                return "골드가 부족합니다.";
            }
            _player.AddGold(-500);
            _player.Recover();
            return "휴식이 완료되었습니다.";
        }

        public void EnterDungeon(DungeonType level)
        {
            if(_player == null)
            {
                Console.WriteLine("플레이어 정보가 없습니다.");
                return;
            }

            Dungeon dungeon = new Dungeon(level);
            DungeonResult result = dungeon.Enter(_player);
            if (result.isClear == false)
            {
                _player.AddHealth(-result.HealthLost);
                if (_player.PlayerStatus.Health <= 0)
                {
                    Console.WriteLine("플레이어가 사망했습니다.");
                    _isGameRunning = false;
                }
            }
            else
            {
                _player.AddHealth(-result.HealthLost);
                _player.AddGold(result.reward);
                _player.AddExp(1);
                Console.WriteLine($"던전 클리어 보상: {result.reward} G");
            }
            _dungeonResult = result;
        }

        private void LoadScene()
        {
            SceneManager.Instance.AddScene(SceneType.Intro, new IntroScene());
            SceneManager.Instance.AddScene(SceneType.MainMenu, new MainScene());
            SceneManager.Instance.AddScene(SceneType.Shop, new ShopScene());
            SceneManager.Instance.AddScene(SceneType.Inventory, new InventoryScene());
            SceneManager.Instance.AddScene(SceneType.Equipment, new EquipmentScene());
            SceneManager.Instance.AddScene(SceneType.Status, new StatusScene());
            SceneManager.Instance.AddScene(SceneType.Buy, new BuyScene());
            SceneManager.Instance.AddScene(SceneType.Sell, new SellScene());
            SceneManager.Instance.AddScene(SceneType.Dungeon, new DungeonScene());
            SceneManager.Instance.AddScene(SceneType.Rest, new RestScene());
            SceneManager.Instance.AddScene(SceneType.DungeonClear, new DungeonClearScene());
        }
    }
}
