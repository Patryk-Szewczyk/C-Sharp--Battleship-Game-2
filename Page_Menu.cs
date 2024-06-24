// Te przestrzenie nazw są tu POTRZEBNE, ponieważ metoda MENU odwołuje się do klas tych przestrzeni nazw.
using Page_Instructions;
using Page_Credits;
using Page_PVP;
using Page_PVC;
using Page_Ranking;
using Page_Options;
using System.Media;
using System;

namespace Page_Menu
{
    public class MenuPage
    {
        public static SoundPlayer menuSoundtrack = new SoundPlayer();   // WEŹ TO SPRÓBUJ PRZENIEŚĆ DO MENU Z try/catch
        public static bool menuSoundtrack_PLAY = false;
        public static bool isMenuButtonLoop = true;
        public static bool isCorrectSign = false;
        public static string[] menuButtons = { "PVC Mode", "Instruction", "Ranking", "Options", "Credits", "Exit" };
        public static int menuButtNum = menuButtons.Length;   // Zawsze ostatni, bo chcę mieć kursor na górze!

        public static void Menu()
        {
            System.ConsoleKey key = System.ConsoleKey.Backspace;   // Dowolny niewłaściwy klawisz.
            System.ConsoleKeyInfo corr_key;
            if (menuSoundtrack_PLAY != true)   // Jeżeli ścieżka dżwiękowa nie jest włączona, włącz ją.
            {
                MenuPage.Soundtrack("Soundtracks/Menu/473915__xhale303__synthwave-loop.wav");
                menuSoundtrack_PLAY = true;
            }   // Włączenieścieżki dźwiękowej musi znajdować się w tym warunku, gdyż jeżeli wyjdziemy z danej podstrony i wrócimy do menu (menu wówczas wywołuje się), to ścieżka dżwiękowa wywoła się z menu.
            while (isMenuButtonLoop == true)   // MENU
            {
                System.Console.Clear();
                System.Console.WriteLine("BBBBBBB     BBBB    BBBBBBBB  BBBBBBBB  BB        BBBBBBBB   BBBBBBB  BB    BB  BB  BBBBBBB      BBBBBB");
                System.Console.WriteLine("BB    BB   BB  BB      BB        BB     BB        BB        BB        BB    BB  BB  BB    BB    BB    BB");
                System.Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB        BB        BB    BB  BB  BB    BB         BB");
                System.Console.WriteLine("BBBBBBBB  BBBBBBBB     BB        BB     BB        BBBBBBBB   BBBBBB   BBBBBBBB  BB  BBBBBBB        BBB");
                System.Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB            BB");
                System.Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB           BB");
                System.Console.WriteLine("BBBBBBB   BB    BB     BB        BB     BBBBBBBB  BBBBBBBB  BBBBBBB   BB    BB  BB  BB          BBBBBBBB");
                System.Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                System.Console.WriteLine("MENU: (navigation = arrows/[W][S], click = ENTER/[E])\n");
                for (int i = 0, j = menuButtons.Length; i < menuButtons.Length; i++, j--)
                {
                    if (j == menuButtNum)
                    {
                        System.Console.WriteLine("> " + menuButtons[i]);
                    }
                    else
                    {
                        System.Console.WriteLine("  " + menuButtons[i]);
                    }
                }
                while (isCorrectSign == false)   // Pętla ta uniemożliwia przeładowanie strony kiedy kliknie się niewłaściwy klawisz.
                {
                    corr_key = System.Console.ReadKey(true);
                    if (corr_key.Key == System.ConsoleKey.W || corr_key.Key == System.ConsoleKey.S || corr_key.Key == System.ConsoleKey.UpArrow || corr_key.Key == System.ConsoleKey.DownArrow || corr_key.Key == System.ConsoleKey.E || corr_key.Key == System.ConsoleKey.Enter)
                    {
                        isCorrectSign = true;
                        key = corr_key.Key;
                    }
                }
                isCorrectSign = false;
                if (key == System.ConsoleKey.Enter || key == System.ConsoleKey.E)   // ENTER / [E]
                {
                    isMenuButtonLoop = false;
                    switch (menuButtNum)
                    {
                        /*case 7:   // PVP
                            PagePVP.isPVPShipPositingLoop = true;
                            PagePVP pvp = new PagePVP();
                            pvp.PVP();
                            //GC.Collect();   // Usuwanie obiektu z pamięci, aby nie zaśmiecać jej dodatkowymi kopiami tego opiektu. (wiem, że on sam się włącza, a chcę mieć 100% pewność, że jednak obiekt ten znika z pamięci)
                            //GC.WaitForPendingFinalizers();
                            break;*/
                        case 6:   // PVC
                            PagePVC.isPVCShipPositingLoop = true;
                            PagePVC pvc = new PagePVC();
                            pvc.PVC();
                            //GC.Collect();
                            //GC.WaitForPendingFinalizers();
                            break;
                        case 5:   // Instruction
                            PageInstructions.isInstructionButtonLoop = true;
                            PageInstructions instruction = new PageInstructions();
                            instruction.Instructions();
                            //GC.Collect();
                            //GC.WaitForPendingFinalizers();
                            break;
                        case 4:   // Ranking
                            PageRanking.isRankingButtonLoop = true;
                            PageRanking ranking = new PageRanking();
                            ranking.Ranking();
                            //GC.Collect();
                            //GC.WaitForPendingFinalizers();
                            break;
                        case 3:   // Options
                            PageOptions.isOptionsButtonLoop = true;
                            PageOptions options = new PageOptions();
                            options.Options();
                            //GC.Collect();
                            //GC.WaitForPendingFinalizers();
                            break;
                        case 2:   // Credits
                            PageCredits.isCreditsLoop = true;
                            PageCredits credits = new PageCredits();
                            credits.Credits();
                            //GC.Collect();
                            //GC.WaitForPendingFinalizers();
                            break;
                        case 1:   // Exit
                            isMenuButtonLoop = false;
                            break;
                    }
                }
                // Poruszanie się po przyciskach (obliczenia):
                if (key == System.ConsoleKey.UpArrow || key == System.ConsoleKey.W)
                {
                    menuButtNum = (menuButtNum < menuButtons.Length) ? menuButtNum += 1 : menuButtNum;
                }
                else if (key == System.ConsoleKey.DownArrow || key == System.ConsoleKey.S)
                {
                    menuButtNum = (menuButtNum > 1) ? menuButtNum -= 1 : menuButtNum;
                }
            }
        }
        public static void Soundtrack(string filepath)
        {
            try   // W przypadku braku ścieżki dżwiękowej, wyjątek zostanie złapany, przez co program nie zakończy działania.
            {
                menuSoundtrack.SoundLocation = filepath;
                menuSoundtrack.PlayLooping();
            }
            catch { }
        }
    }
}
