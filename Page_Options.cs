using System;
using System.Collections.Generic;
using System.Net;
using Library_GlobalMethods;
using Page_Menu;
using Page_PVC;
using Page_Ranking;

namespace Page_Options {    // DO£¥CZ DO OPCJI ODDZIELNY PLIK TEKSTOWY, W KTÓRYM ZAPISUJESZ I ZAMIENIASZ DANE ODNOŒNIE OPCJI!!!
    public class Options {
        public static int page_ID = 3;
        public static bool isPage = false;
        /*public static string[] options = new string[6] {
            "[ON]",
            "[ON]",
            "[ON]",
            "[OFF]",
            "[2,2,2,3,3,4,5]",
            "[DATA]"
        };*/
        public static string[] buttons = { 
            "Music:                                 ",
            "Sound effects:                         ",
            "Equal ships direction for AI:          ",
            "Show only top 10 players in ranking:   ",
            "Change ships in battle:                ",
            "Delete PVC ranking data:               "
        };
        public static string[] guide = new string[6] {
            "ON = [E], OFF = [D]",
            "ON = [E], OFF = [D]",
            "ON = [E], OFF = [D]",
            "ON = [E], OFF = [D]",
            "change = Write new value and click [E]",
            "delete = [D]"
        };
        public static int currentButton = 0;   // Zawsze pierwszy, bo chcê mieæ kursor na górze!
        public static List<ConsoleKey> usingKeys = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace };
        public void RenderPage() {
            System.ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolny niew³aœciwy klawisz.
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.Page.RenderButtons(buttons, currentButton);
                GlobalMethod.Page.RenderDottedLine(90);
                ShowOption(currentButton);
                key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys);
                currentButton = GlobalMethod.Page.MoveButtons(buttons, currentButton, key);
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
            GlobalMethod.Page.RenderDottedLine(90);
            Console.WriteLine("OPTIONS: | Moving: arrows/[W][S] | Back to menu: [Backspace]\n");
        }
        public static void ShowOption(int currentButton) {

        }
        public class Upload : Options {
            
        }
        public class Data : Options {
            
        }
    }
}