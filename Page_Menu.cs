// Te przestrzenie nazw są tu POTRZEBNE, ponieważ metoda MENU odwołuje się do klas tych przestrzeni nazw.
using System;
using System.Media;
using System.Collections.Generic;
using Library_GlobalMethods;
using Page_PVC;
using Page_Instructions;
using Page_Ranking;
using Page_Options;
using Page_Credits;

namespace Page_Menu {
    public class Menu {
        public static int page_ID = 99;   // Musiałem dla wywołania metody "LoopCorrectKey", ponieważ wymaga przekazania ID strony.
        public static int pageLineLength = 105;
        public static SoundPlayer currentSound = new SoundPlayer();
        public static bool PLAY_menu = false;
        public static bool PLAY_credits = false;
        public static bool isPage = true;
        public static string[] buttons = { "PVC Mode", "Instruction", "Ranking", "Options", "Credits", "Exit" };
        public static int currentButton = 0;   // Zawsze pierwszy, bo chcę mieć kursor na górze!
        public static List<ConsoleKey> usingKeys_STANDARD = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Enter };
        public static List<ConsoleKey> usingKeys_TOP = new List<ConsoleKey> { ConsoleKey.S, ConsoleKey.DownArrow, ConsoleKey.Enter };
        public static List<ConsoleKey> usingKeys_DOWN = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.UpArrow, ConsoleKey.Enter };
        public static List<ConsoleKey> usingKeys_ONE = new List<ConsoleKey> { ConsoleKey.Enter };
        public static Tuple<PVC, Instructions, Ranking, Options, Credits> pages = new Tuple<PVC, Instructions, Ranking, Options, Credits>(
                new PVC(),
                new Instructions(),
                new Ranking(),
                new Options(),
                new Credits()
        );
        public static void RenderPage() {
            SoundSwitch();
            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolna niewłaściwa wartość.
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.Page.RenderButtons(buttons, currentButton);
                key = GlobalMethod.Page.SelectUsingKeys(currentButton, page_ID, key, buttons, usingKeys_STANDARD, usingKeys_TOP, usingKeys_DOWN, usingKeys_ONE);   // Pętla ta uniemożliwia przeładowanie strony kiedy kliknie się niewłaściwy klawisz.
                RenderPage(key, pages);
                currentButton = GlobalMethod.Page.MoveButtons(buttons, currentButton, key);   // Poruszanie się po przyciskach (obliczenia)
            }
            Environment.Exit(0);   // Użyłem tej metody, ponieważ po zapisie danych w klasie "Options" pojawia się problem z wyjściem z pragramu. Strasznie długo się zamyka.
        }
        public static void SoundSwitch() {
            if (PLAY_menu == false && PLAY_credits == false) {
                GlobalMethod.SoundControl.PlaySound("Soundtracks/Menu/473915__xhale303__synthwave-loop.wav");
                PLAY_menu = true;
            } else if (PLAY_menu == false && PLAY_credits == true) {
                currentSound.Stop();
                PLAY_credits = false;
                GlobalMethod.SoundControl.PlaySound("Soundtracks/Menu/473915__xhale303__synthwave-loop.wav");
                PLAY_menu = true;
            }
        }
        public static void RenderTitle() {
            Console.WriteLine("BBBBBBB     BBBB    BBBBBBBB  BBBBBBBB  BB        BBBBBBBB   BBBBBBB  BB    BB  BB  BBBBBBB      BBBBBB");
            Console.WriteLine("BB    BB   BB  BB      BB        BB     BB        BB        BB        BB    BB  BB  BB    BB    BB    BB");
            Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB        BB        BB    BB  BB  BB    BB         BB");
            Console.WriteLine("BBBBBBBB  BBBBBBBB     BB        BB     BB        BBBBBBBB   BBBBBB   BBBBBBBB  BB  BBBBBBB        BBB");
            Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB            BB");
            Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB           BB");
            Console.WriteLine("BBBBBBB   BB    BB     BB        BB     BBBBBBBB  BBBBBBBB  BBBBBBB   BB    BB  BB  BB          BBBBBBBB");
            GlobalMethod.Page.RenderDottedLine(pageLineLength);
            Console.WriteLine("MENU: | Moving: arrows/[W][S] | Click = [ENTER]\n");
        }
        public static void RenderPage(ConsoleKeyInfo key, Tuple<PVC, Instructions, Ranking, Options, Credits> pages) {
            if (key.Key == ConsoleKey.Enter) {
                switch (currentButton) {
                    case 0:
                        PVC.isPage = true;
                        pages.Item1.RenderPage();
                        break;
                    case 1:
                        Instructions.isPage = true;
                        pages.Item2.RenderPage();
                        break;
                    case 2:
                        Ranking.isPage = true;
                        pages.Item3.RenderPage();
                        break;
                    case 3:
                        Options.isPage = true;
                        pages.Item4.RenderPage();
                        break;
                    case 4:
                        Credits.isPage = true;
                        pages.Item5.RenderPage();
                        break;
                    case 5:
                        isPage = false;
                        break;
                }
            }
        }
    }
}
