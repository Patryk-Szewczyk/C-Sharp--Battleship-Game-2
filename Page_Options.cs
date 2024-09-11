using System;
using System.Collections.Generic;
using System.Net;
using Library_GlobalMethods;
using Page_Menu;
using Page_PVC;
using Page_Ranking;

namespace Page_Options {    // DO£¥CZ DO OPCJI ODDZIELNY PLIK TEKSTOWY, W KTÓRYM ZAPISUJESZ I ZAMIENIASZ DANE ODNOŒNIE OPCJI!!!
    public class Options {
        public static bool isPage = true;
        public static bool isCorrSign = false;
        /*public static string[] options = new string[6] {
            "[ON]",
            "[ON]",
            "[ON]",
            "[OFF]",
            "[2,2,2,3,3,4,5]",
            "[DATA]"
        };*/
        public static string[] guide = new string[6] {
            "ON = [E], OFF = [D]",
            "ON = [E], OFF = [D]",
            "ON = [E], OFF = [D]",
            "ON = [E], OFF = [D]",
            "change = Write new value and click [E]",
            "delete = [D]"
        };
        public static string[] buttons = { 
            "Music:                                 ",
            "Sound effects:                         ",
            "Equal ships direction for AI:          ",
            "Show only top 10 players in ranking:   ",
            "Change ships in battle:                ",
            "Delete PVC ranking data:               "
        };
        public static int currentButton = 0;   // Zawsze pierwszy, bo chcê mieæ kursor na górze!
        public void RenderPage() {
            System.ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolny niew³aœciwy klawisz.
            //PrepareButtons();
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.RenderButtons(buttons, currentButton);
                GlobalMethod.RenderDottedLine(90);
                RenderContent(currentButton);
                key = LoopCorrectKey(key);
                currentButton = GlobalMethod.MoveButtons(buttons, currentButton, key);
            }
        }
        public class Upload {
            /*public static void ShowOptions() {   // Panel kontrolny
                (bool, string) isFile = GlobalMethod.UploadFile("options.txt");
                if (isFile.Item1 == true) {
                    string[] options = UploadData(isFile.Item2);
                    Sort(options);
                    Render(options);
                }
            }*/
            /*public static string[] UploadData() {

            }
            public static void PrepareButtons() {
                for (int i = 0; i < buttons.Length; i++) {
                    buttons[i] += options[i];
                }
            }*/
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
        public static void RenderContent(int currentButton) {
            //Console.WriteLine(buttons[currentButton]);
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
        public static void MenuReturn() {
            isPage = false;
            MenuPage.isPage = true;
            MenuPage.Menu();
        }
    }
}