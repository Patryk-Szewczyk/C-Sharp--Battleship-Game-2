// Te przestrzenie nazw sπ tu POTRZEBNE, poniewaø metoda MENU odwo≥uje siÍ do klas tych przestrzeni nazw.
using System;
using System.Media;
using Page_Instructions;
using Page_Credits;
using Page_PVC;
using Page_Ranking;
using Page_Options;
using Library_GlobalMethods;

namespace Page_Menu {
    public class MenuPage {
        public static SoundPlayer currSound = new SoundPlayer();   // WEè TO SPR”BUJ PRZENIEå∆ DO MENU Z try/catch
        public static bool PLAY_menu = false;
        public static bool PLAY_credits = false;
        public static bool isPage = true;
        public static bool isCorrSign = false;
        public static string[] buttons = { "PVC Mode", "Instruction", "Ranking", "Options", "Credits", "Exit" };
        public static int currentButton = 0;   // Zawsze pierwszy, bo chcÍ mieÊ kursor na gÛrze!
        public static void Menu() {
            Tuple<PVC, Instructions, Ranking, Options, Credits> pages = new Tuple<PVC, Instructions, Ranking, Options, Credits>(
                new PVC(),
                new Instructions(),
                new Ranking(),
                new Options(),
                new Credits()
            );
            SoundSwitch();
            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolna niew≥aúciwa wartoúÊ.
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.RenderButtons(buttons, currentButton);
                key = LoopCorrectKey(key);   // PÍtla ta uniemoøliwia prze≥adowanie strony kiedy kliknie siÍ niew≥aúciwy klawisz.
                RenderPage(key, pages);
                currentButton = GlobalMethod.MoveButtons(buttons, currentButton, key);   // Poruszanie siÍ po przyciskach (obliczenia):
            }
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
            GlobalMethod.RenderDottedLine(105);
            Console.WriteLine("MENU: | Moving: arrows/[W][S] | Click = ENTER\n");
        }
        public static ConsoleKeyInfo LoopCorrectKey(ConsoleKeyInfo key) {
            while (isCorrSign == false) {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.S || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.Enter) {
                    isCorrSign = true;
                }
            }
            isCorrSign = false;
            return key;
        }
        public static void RenderPage(ConsoleKeyInfo key, Tuple<PVC, Instructions, Ranking, Options, Credits> pages) {
            if (key.Key == ConsoleKey.Enter) {
                isPage = false;
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