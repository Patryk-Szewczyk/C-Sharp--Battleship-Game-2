// Te przestrzenie nazw sπ tu POTRZEBNE, poniewaø metoda MENU odwo≥uje siÍ do klas tych przestrzeni nazw.
using System;
using System.Media;
using Page_Instructions;
using Page_Credits;
using Page_PVC;
using Page_Ranking;
using Page_Options;

namespace Page_Menu {
    public class MenuPage {
        public static SoundPlayer currSound = new SoundPlayer();   // WEè TO SPR”BUJ PRZENIEå∆ DO MENU Z try/catch
        public static bool PLAY_menu = false;
        public static bool PLAY_credits = false;
        public static bool isMenu = true;
        public static bool isCorrectSign = false;
        public static string[] buttons = { "PVC Mode", "Instruction", "Ranking", "Options", "Credits", "Exit" };
        public static int buttNum = buttons.Length;   // Zawsze ostatni, bo chcÍ mieÊ kursor na gÛrze!
        public static void Menu() {
            PVC pvc = new PVC();
            Instructions instruction = new Instructions();
            Ranking ranking = new Ranking();
            Options options = new Options();
            Credits credits = new Credits();

            System.ConsoleKey key = System.ConsoleKey.Backspace;   // Dowolny niew≥aúciwy klawisz.
            System.ConsoleKeyInfo corr_key;
            if (PLAY_menu == false && PLAY_credits == false) {
                MenuPage.Sound("Soundtracks/Menu/473915__xhale303__synthwave-loop.wav", true);
                PLAY_menu = true;
            } else if (PLAY_menu == false && PLAY_credits == true) {
                currSound.Stop();
                PLAY_credits = false;
                MenuPage.Sound("Soundtracks/Menu/473915__xhale303__synthwave-loop.wav", true);
                PLAY_menu = true;
            }
            while (isMenu == true) {
                Console.Clear();
                Console.WriteLine("BBBBBBB     BBBB    BBBBBBBB  BBBBBBBB  BB        BBBBBBBB   BBBBBBB  BB    BB  BB  BBBBBBB      BBBBBB");
                Console.WriteLine("BB    BB   BB  BB      BB        BB     BB        BB        BB        BB    BB  BB  BB    BB    BB    BB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB        BB        BB    BB  BB  BB    BB         BB");
                Console.WriteLine("BBBBBBBB  BBBBBBBB     BB        BB     BB        BBBBBBBB   BBBBBB   BBBBBBBB  BB  BBBBBBB        BBB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB            BB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB           BB");
                Console.WriteLine("BBBBBBB   BB    BB     BB        BB     BBBBBBBB  BBBBBBBB  BBBBBBB   BB    BB  BB  BB          BBBBBBBB");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("MENU: | Moving: arrows/[W][S] | Click = ENTER\n");
                for (int i = 0, j = buttons.Length; i < buttons.Length; i++, j--) {
                    if (j == buttNum) {
                        Console.WriteLine("> " + buttons[i]);
                    } else {
                        Console.WriteLine("  " + buttons[i]);
                    }
                }
                while (isCorrectSign == false)  {
                    corr_key = Console.ReadKey(true);
                    if (corr_key.Key == System.ConsoleKey.W || corr_key.Key == System.ConsoleKey.S || corr_key.Key == System.ConsoleKey.UpArrow || corr_key.Key == System.ConsoleKey.DownArrow || corr_key.Key == System.ConsoleKey.Enter) {
                        isCorrectSign = true;
                        key = corr_key.Key;
                    }
                }
                isCorrectSign = false;
                if (key == System.ConsoleKey.Enter) {
                    isMenu = false;
                    switch (buttNum) {
                        case 6:
                            PVC.isPVCLoop = true;
                            pvc.RenderPage();
                            break;
                        case 5:
                            Instructions.isInstructionLoop = true;
                            instruction.RenderPage();
                            break;
                        case 4:
                            Ranking.isRankingLoop = true;
                            ranking.RenderPage();
                            break;
                        case 3:
                            Options.isOptionsLoop = true;
                            options.RenderPage();
                            break;
                        case 2:
                            Credits.isCreditsLoop = true;
                            credits.RenderPage();
                            break;
                        case 1:
                            isMenu = false;
                            break;
                    }
                }
                // Poruszanie siÍ po przyciskach (obliczenia):
                if (key == System.ConsoleKey.UpArrow || key == System.ConsoleKey.W) {
                    buttNum = (buttNum < buttons.Length) ? buttNum += 1 : buttNum;
                } else if (key == System.ConsoleKey.DownArrow || key == System.ConsoleKey.S) {
                    buttNum = (buttNum > 1) ? buttNum -= 1 : buttNum;
                }
            }
        }
        public static void Sound(string filepath, bool isLoop) {
            try {   // W przypadku braku úcieøki døwiÍkowej, wyjπtek zostanie z≥apany, przez co program nie zakoÒczy dzia≥ania. 
                MenuPage.currSound.SoundLocation = filepath;
                if (isLoop) MenuPage.currSound.PlayLooping();
                //Console.WriteLine("STAN MUZYKI: Znaleziono\n\n" + filepath);
            } catch (Exception error) {
                Console.WriteLine("STAN MUZYKI: Nie znaleziono\n\n" + error);
            }
        }
    }
}