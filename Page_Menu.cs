// Te przestrzenie nazw s� tu POTRZEBNE, poniewa� metoda MENU odwo�uje si� do klas tych przestrzeni nazw.
using System;
using System.Media;
using System.Collections.Generic;
using Page_Instructions;
using Page_Credits;
using Page_PVC;
using Page_Ranking;
using Page_Options;
using Library_GlobalMethods;

namespace Page_Menu {
    public class Menu {
        public static int page_ID = 99;   // Musia�em dla wywo�ania metody "LoopCorrectKey", poniewa� wymaga przekazania ID strony.
        public static int pageLineLength = 105;
        public static SoundPlayer currSound = new SoundPlayer();   // WE� TO SPR�BUJ PRZENIE�� DO MENU Z try/catch
        public static bool PLAY_menu = false;
        public static bool PLAY_credits = false;
        public static bool isPage = true;
        public static string[] buttons = { "PVC Mode", "Instruction", "Ranking", "Options", "Credits", "Exit" };
        public static int currentButton = 0;   // Zawsze pierwszy, bo chc� mie� kursor na g�rze!
        public static List<ConsoleKey> usingKeys = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Enter };
        public static Tuple<PVC, Instructions, Ranking, Options, Credits> pages = new Tuple<PVC, Instructions, Ranking, Options, Credits>(
                new PVC(),
                new Instructions(),
                new Ranking(),
                new Options(),
                new Credits()
        );
        public static void RenderPage() {
            SoundSwitch();
            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolna niew�a�ciwa warto��.
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.Page.RenderButtons(buttons, currentButton);
                key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys);   // P�tla ta uniemo�liwia prze�adowanie strony kiedy kliknie si� niew�a�ciwy klawisz.
                RenderPage(key, pages);
                currentButton = GlobalMethod.Page.MoveButtons(buttons, currentButton, key);   // Poruszanie si� po przyciskach (obliczenia):
            }
            Environment.Exit(0);   // U�y�em tej metody, poniewa� po zapisie danych w klasie "Options" pojawia si� problem z wyj�ciem z pragramu. Strasznie d�ugo si� zamyka.
        }
        public static void SoundSwitch() {
            if (PLAY_menu == false && PLAY_credits == false) {
                GlobalMethod.PlaySound("Soundtracks/Menu/473915__xhale303__synthwave-loop.wav", true);
                PLAY_menu = true;
            } else if (PLAY_menu == false && PLAY_credits == true) {
                currSound.Stop();
                PLAY_credits = false;
                GlobalMethod.PlaySound("Soundtracks/Menu/473915__xhale303__synthwave-loop.wav", true);
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
            Console.WriteLine("MENU: | Moving: arrows/[W][S] | Click = ENTER\n");
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
