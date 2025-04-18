using System.Text;
using SpartaDungeon.Item;
using SpartaDungeon.Managers;
namespace SpartaDungeon
{

    public class DependencyContainer
    {
        // 간단한 예시 - 실제로는 더 복잡한 DI 컨테이너를 사용할 수 있음
        public static GameManager CreateGameManager()
        {
            // IPlayerService playerService = new PlayerService();
            // IShopService shopService = new ShopService();
            // ISaveService saveService = new SaveService();
            // ISceneService sceneService = new SceneService();
            
            // return new GameManager(playerService, shopService, saveService, sceneService);
             return new GameManager();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //Item sword = new Item("Sword", "A sharp blade.", 100);
            //Item shield = new Item("Shield", "A sturdy shield.", 150);
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(140, 60);

            // 아이템 데이터베이스 초기화
            ItemDataBase itemDataBase = ItemDataBase.Instance;
            
            // 게임 시작 - 게임 루프 시작
            GameManager.Instance.StartGame();
        }
    }
}
