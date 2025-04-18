using SpartaDungeon.GameDungeon;
using SpartaDungeon.Managers;

namespace SpartaDungeon.Scene
{
    public class DungeonClearScene : BaseScene
    {
        private const string SUCCESS_TITLE = "던전 클리어";
        private const string FAIL_TITLE = "던전 클리어";
        private const string DUNGEON_CLEAR_RESULT = "\n[탐험 결과]";


        protected override void RegisterMenu()
        {
            AddMenuAction(0, MenuType.Exit, () => { SceneManager.Instance.ChangeScene(SceneType.MainMenu); });
        }

        protected override void DisplayScene()
        {
            DungeonResult? result = GameManager.Instance.DungeonResult;
            Console.Clear();
            string description;

            if (result.isClear == true)
            {
                description = $"축하합니다!!\n{Utils.GetDescription(result.level)}을 클리어 하였습니다.";
                Console.WriteLine(SUCCESS_TITLE);
            }
            else
            {
                description = $"안타깝네요!!\n{Utils.GetDescription(result.level)}을 클리어 하지 못하였습니다.";
                Console.WriteLine(FAIL_TITLE);


            }

            Console.WriteLine(description);
            Console.WriteLine(DUNGEON_CLEAR_RESULT);
            Console.WriteLine($"체력 {result.previousHealth} -> {result.previousHealth - result.HealthLost}");
            Console.WriteLine($"Gold {result.previousGold} G -> {result.previousGold + result.reward} G");
            Console.WriteLine();
            ViewMenu();
        }
    }
}
