// Te przestrzenie nazw są tu POTRZEBNE, ponieważ metoda MENU odwołuje się do klas tych przestrzeni nazw.
using Page_Instructions;
using Page_Credits;
using Page_PVP;
using Page_PVC;
using Page_Ranking;
using Page_Options;

namespace Page_Menu
{
    public class MenuPage
    {
        public static bool isMenuButtonLoop = true;
        public static string[] menuButtons = { "PVP Mode", "PVC Mode", "Instruction", "Ranking", "Options", "Credits", "Exit" };
        public static int menuButtNum = menuButtons.Length;   // Zawsze ostatni, bo chcę mieć kursor na górze!

        public static void Menu()
        {
            while (isMenuButtonLoop == true)   // MENU
            {
                Console.Clear();
                Console.WriteLine("BBBBBBB     BBBB    BBBBBBBB  BBBBBBBB  BB        BBBBBBBB   BBBBBBB  BB    BB  BB  BBBBBBB      BBBBBB");
                Console.WriteLine("BB    BB   BB  BB      BB        BB     BB        BB        BB        BB    BB  BB  BB    BB    BB    BB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB        BB        BB    BB  BB  BB    BB         BB");
                Console.WriteLine("BBBBBBBB  BBBBBBBB     BB        BB     BB        BBBBBBBB   BBBBBB   BBBBBBBB  BB  BBBBBBB        BBB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB            BB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB           BB");
                Console.WriteLine("BBBBBBB   BB    BB     BB        BB     BBBBBBBB  BBBBBBBB  BBBBBBB   BB    BB  BB  BB          BBBBBBBB");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("MENU: (navigation = arrows/wsad, click = ENTER/[E])\n");
                for (int i = 0, j = menuButtons.Length; i < menuButtons.Length; i++, j--)
                {
                    if (j == menuButtNum)
                    {
                        Console.WriteLine("> " + menuButtons[i]);
                    }
                    else
                    {
                        Console.WriteLine("  " + menuButtons[i]);
                    }
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.E)   // ENTER / [E]
                {
                    isMenuButtonLoop = false;
                    switch (menuButtNum)
                    {
                        case 7:   // PVP
                            PagePVP.isPVPShipPositingLoop = true;
                            PagePVP pvp = new PagePVP();
                            pvp.PVP();
                            break;
                        case 6:   // PVC
                            PagePVC.isPVCShipPositingLoop = true;
                            PagePVC pvc = new PagePVC();
                            pvc.PVC();
                            break;
                        case 5:   // Instruction
                            PageInstructions.isInstructionButtonLoop = true;
                            PageInstructions instruction = new PageInstructions();
                            instruction.Instructions();
                            break;
                        case 4:   // Ranking
                            PageRanking.isRankingButtonLoop = true;
                            PageRanking ranking = new PageRanking();
                            ranking.Ranking();
                            break;
                        case 3:   // Options
                            PageOptions.isOptionsButtonLoop = true;
                            PageOptions options = new PageOptions();
                            options.Options();
                            break;
                        case 2:   // Credits
                            PageCredits.isCreditsLoop = true;
                            PageCredits credits = new PageCredits();
                            credits.Credits();
                            break;
                        case 1:   // Exit
                            isMenuButtonLoop = false;
                            break;
                    }
                }
                // Poruszanie się po przyciskach (obliczenia):
                if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                {
                    menuButtNum = (menuButtNum < menuButtons.Length) ? menuButtNum += 1 : menuButtNum;
                }
                else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                {
                    menuButtNum = (menuButtNum > 1) ? menuButtNum -= 1 : menuButtNum;
                }
            }
        }
    }
}
