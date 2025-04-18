using SpartaDungeon.Managers;

namespace SpartaDungeon.Scene
{
    public class IntroScene : BaseScene
    {
        private const string TITLE = @"
            ███████╗██████╗  █████╗ ██████╗ ████████╗ █████╗         ██████╗ ██╗   ██╗███╗   ██╗ ██████╗ ███████╗ ██████╗ ███╗   ██╗
            ██╔════╝██╔══██╗██╔══██╗██╔══██╗╚══██╔══╝██╔══██╗        ██╔══██╗██║   ██║████╗  ██║██╔════╝ ██╔════╝██╔═══██╗████╗  ██║
            ███████╗██████╔╝███████║██████╔╝   ██║   ███████║        ██║  ██║██║   ██║██╔██╗ ██║██║  ███╗█████╗  ██║   ██║██╔██╗ ██║
            ╚════██║██╔═══╝ ██╔══██║██╔══██╗   ██║   ██╔══██║        ██║  ██║██║   ██║██║╚██╗██║██║   ██║██╔══╝  ██║   ██║██║╚██╗██║
            ███████║██║     ██║  ██║██║  ██║   ██║   ██║  ██║        ██████╔╝╚██████╔╝██║ ╚████║╚██████╔╝███████╗╚██████╔╝██║ ╚████║
            ╚══════╝╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝  ╚═╝        ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═══╝
    
                             
                                       ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                       ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣒⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                       ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣪⠀⡀⣀⢀⢀⡀⡀⡀⣀⢀⢀⢀⡀⡀⡀⣀⢀⢀⢀⡀⡀⡀⣀⢀⢀⢀⡀⡀⡀⣀⢀⢀⢀⡀⡀⡀⠀⠀⠀
                                       ⠐⠲⠇⠷⠸⠽⠨⠷⠪⠗⣅⠖⡿⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⣭⢽⠽⣟⠯⠆⠀
                                       ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢵⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠀⠀⠀⠀
                                       ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠣⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        ";

        protected override void DisplayScene()
        {
            Console.Clear();
            Console.WriteLine(TITLE);
            ViewMenu();
        }

        protected override void RegisterMenu()
        {
            AddMenuAction(1, MenuType.NewGame, () => {
                GameManager.Instance.NewGame();
                SceneManager.Instance.ChangeScene(SceneType.MainMenu); 
            });
            AddMenuAction(2, MenuType.LoadGame, () => {
                // TODO: 불러오기 기능 아직 구현되지 않음
                if(GameManager.Instance.LoadGame())
                {
                    SceneManager.Instance.ChangeScene(SceneType.MainMenu); 
                }
            });
            AddMenuAction(3, MenuType.GameExit, () => { Environment.Exit(0); });
        }
    }
}
