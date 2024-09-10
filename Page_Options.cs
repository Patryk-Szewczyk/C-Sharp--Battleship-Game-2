using System;
using Library_GlobalMethods;
using Page_Menu;
using Page_PVC;
using Page_Ranking;

namespace Page_Options {    // DO£¥CZ DO OPCJI ODDZIELNY PLIK TEKSTOWY, W KTÓRYM ZAPISUJESZ I ZAMIENIASZ DANE ODNOŒNIE OPCJI!!!
    public class Options {
        public static bool isPage = true;
        public static bool isCorrSign = false;
        public static string[] buttons = { 
            "Music:                                 [ON]              ON = [E], OFF = [D]",
            "Sound effects:                         [ON]              ON = [E], OFF = [D]",
            "Equal ships direction for AI:          [ON]              ON = [E], OFF = [D]",
            "Show only top 10 players in ranking:   [OFF]             ON = [E], OFF = [D]",
            "Change ships in battle:                [2,2,2,3,3,4,5]   change = [C]",
            "Delete PVC ranking data:               [DATA]            delete = [P]"
        };
        public static int currentButton = buttons.Length;
        public void RenderPage() {
            System.ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolny niew³aœciwy klawisz.
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.RenderButtons(buttons, currentButton);
                GlobalMethod.RenderDottedLine(90);
                key = LoopCorrectKey(key);
                MoveButtons(key);
            }
        }
        public static void RenderTitle() {
            Console.WriteLine(" BBBBBB   BBBBBBB   BBBBBBBB  BB   BBBBBB   BBBB  BB   BBBBBBB");
            Console.WriteLine("BB    BB  BB    BB     BB     BB  BB    BB  BB BB BB  BB      ");
            Console.WriteLine("BB    BB  BB    BB     BB     BB  BB    BB  BB BB BB  BB      ");
            Console.WriteLine("BB    BB  BBBBBBB      BB     BB  BB    BB  BB BB BB   BBBBBB ");
            Console.WriteLine("BB    BB  BB           BB     BB  BB    BB  BB BB BB        BB");
            Console.WriteLine("BB    BB  BB           BB     BB  BB    BB  BB BB BB        BB");
            Console.WriteLine(" BBBBBB   BB           BB     BB   BBBBBB   BB  BBBB  BBBBBBB ");
            GlobalMethod.RenderDottedLine(90);
            Console.WriteLine("OPTIONS: | Moving: arrows/[W][S] | Back to menu: [Backspace]\n");
        }
        public static ConsoleKeyInfo LoopCorrectKey(ConsoleKeyInfo key) {
            while (isCorrSign == false) {   // Pêtla ta uniemo¿liwia prze³adowanie strony kiedy kliknie siê niew³aœciwy klawisz.
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.S || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.Backspace) {
                    isCorrSign = true;
                }
            }
            isCorrSign = false; 
            if (key.Key == ConsoleKey.Backspace) MenuReturn();
            return key;
        }
        public static void MoveButtons(ConsoleKeyInfo key) {
            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W) {   // Poruszanie siê po przyciskach (obliczenia):
                currentButton = (currentButton < buttons.Length) ? currentButton += 1 : currentButton;
            } else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S) {
                currentButton = (currentButton > 1) ? currentButton -= 1 : currentButton;
            }
        }
        public static void MenuReturn() {
            isPage = false;
            MenuPage.isPage = true;
            MenuPage.Menu();
        }
    }
}