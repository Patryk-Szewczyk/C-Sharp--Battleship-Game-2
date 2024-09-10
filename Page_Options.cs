using System;
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
        public static int buttNum = buttons.Length;
        public void RenderPage() {
            System.ConsoleKey key = System.ConsoleKey.Backspace;   // Dowolny niew³aœciwy klawisz.
            while (isPage == true) {
                Console.Clear();
                Console.WriteLine(" BBBBBB   BBBBBBB   BBBBBBBB  BB   BBBBBB   BBBB  BB   BBBBBBB");
                Console.WriteLine("BB    BB  BB    BB     BB     BB  BB    BB  BB BB BB  BB      ");
                Console.WriteLine("BB    BB  BB    BB     BB     BB  BB    BB  BB BB BB  BB      ");
                Console.WriteLine("BB    BB  BBBBBBB      BB     BB  BB    BB  BB BB BB   BBBBBB ");
                Console.WriteLine("BB    BB  BB           BB     BB  BB    BB  BB BB BB        BB");
                Console.WriteLine("BB    BB  BB           BB     BB  BB    BB  BB BB BB        BB");
                Console.WriteLine(" BBBBBB   BB           BB     BB   BBBBBB   BB  BBBB  BBBBBBB ");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("OPTIONS: | Moving: arrows/[W][S] | Back to menu: [Backspace]\n");
                for (int i = 0, j = buttons.Length; i < buttons.Length; i++, j--) {
                    if (j == buttNum) {
                        Console.WriteLine("> " + buttons[i]);
                    } else {
                        Console.WriteLine("  " + buttons[i]);
                    }
                }
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");

                /*switch (optionsButtNum)
                {
                    // INFORMACJE O STANIE POSZCZEGÓLNYCH OPCJI, np. Music: OFF
                }*/

                while (isCorrSign == false) {  // Pêtla ta uniemo¿liwia prze³adowanie strony kiedy kliknie siê niew³aœciwy klawisz.
                    System.ConsoleKeyInfo corr_key = Console.ReadKey(true);
                    if (corr_key.Key == System.ConsoleKey.W || corr_key.Key == System.ConsoleKey.S || corr_key.Key == System.ConsoleKey.UpArrow || corr_key.Key == System.ConsoleKey.DownArrow || corr_key.Key == System.ConsoleKey.Backspace) {
                        isCorrSign = true;
                        key = corr_key.Key;
                    }
                }
                isCorrSign = false;
                // Poruszanie siê po przyciskach (obliczenia):
                if (key == System.ConsoleKey.UpArrow || key == System.ConsoleKey.W) {
                    buttNum = (buttNum < buttons.Length) ? buttNum += 1 : buttNum;
                } else if (key == System.ConsoleKey.DownArrow || key == System.ConsoleKey.S) {
                    buttNum = (buttNum > 1) ? buttNum -= 1 : buttNum;
                } else if (key == System.ConsoleKey.Backspace) {
                    isPage = false;
                    MenuPage.isPage = true;
                    MenuPage.Menu();
                }
            }
        }
    }
}