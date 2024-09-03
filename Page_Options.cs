using System;
using Page_Menu;
using Page_PVP;
using Page_PVC;
using Page_Ranking;

namespace Page_Options {
    public class PageOptions {
        public static bool isOptionsLoop = true;
        public static bool isCorrectSign = false;
        public static string[] optionsButtons = 
        { 
            "Music: - - - - - - - - - - - - - - - - [ON] - - - - - - ON = [E], OFF = [D]",
            "Sound effects: - - - - - - - - - - - - [ON] - - - - - - ON = [E], OFF = [D]",
            "Equal ships direction for AI:  - - - - [ON] - - - - - - ON = [E], OFF = [D]",
            "Show only top 10 players in ranking: - [OFF]  - - - - - ON = [E], OFF = [D]",
            "Delete PVC ranking data: - - - - - - - [DATA] - - - - - delete = [P]"
            //"AI voice:                    [ON]                ON = [E], OFF = [D]",
            //"AI subtitles:                [ON]                ON = [E], OFF = [D]",
            //"Delete PVP ranking data:       [DATA]              delete = [P]",
            //"Delete Machine Learning data:                moving = [O][L], choose = [I], delete = [P]" 
        };
        public static string[] toRankingButtons = { "PVP", "PVC" };   // EJJJJJJJJJJ!!! JESTEŒ NA TYM JAKBY CO!!!
        public static int optionsButtNum = optionsButtons.Length;
        public void Options() {
            System.ConsoleKey key = System.ConsoleKey.Backspace;   // Dowolny niew³aœciwy klawisz.
            while (isOptionsLoop == true) {
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
                for (int i = 0, j = optionsButtons.Length; i < optionsButtons.Length; i++, j--) {
                    if (j == optionsButtNum) {
                        Console.WriteLine("> " + optionsButtons[i]);
                    } else {
                        Console.WriteLine("  " + optionsButtons[i]);
                    }
                }
                //Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");

                /*switch (optionsButtNum)
                {
                    // INFORMACJE O STANIE POSZCZEGÓLNYCH OPCJI, np. Music: OFF
                }*/
                while (isCorrectSign == false) {  // Pêtla ta uniemo¿liwia prze³adowanie strony kiedy kliknie siê niew³aœciwy klawisz.
                    System.ConsoleKeyInfo corr_key = Console.ReadKey(true);
                    if (corr_key.Key == System.ConsoleKey.W || corr_key.Key == System.ConsoleKey.S || corr_key.Key == System.ConsoleKey.UpArrow || corr_key.Key == System.ConsoleKey.DownArrow || corr_key.Key == System.ConsoleKey.Backspace) {
                        isCorrectSign = true;
                        key = corr_key.Key;
                    }
                }
                isCorrectSign = false;
                // Poruszanie siê po przyciskach (obliczenia):
                if (key == System.ConsoleKey.UpArrow || key == System.ConsoleKey.W) {
                    optionsButtNum = (optionsButtNum < optionsButtons.Length) ? optionsButtNum += 1 : optionsButtNum;
                } else if (key == System.ConsoleKey.DownArrow || key == System.ConsoleKey.S) {
                    optionsButtNum = (optionsButtNum > 1) ? optionsButtNum -= 1 : optionsButtNum;
                } else if (key == System.ConsoleKey.Backspace) {
                    isOptionsLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
    }
}