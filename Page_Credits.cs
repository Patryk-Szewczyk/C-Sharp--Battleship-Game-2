using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Library_GlobalMethods;
using Page_Menu;

namespace Page_Credits {
    public class Credits {
        public static int page_ID = 4;
        public static bool isPage = false;
        public static List<ConsoleKey> usingKeys = new List<ConsoleKey> { ConsoleKey.Backspace };
        public void RenderPage() {
            SoundSwitch();
            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                OpenHTML();
                key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys);   // Pêtla ta uniemo¿liwia prze³adowanie strony kiedy kliknie siê niew³aœciwy klawisz.
            }
        }
        public static void SoundSwitch() {
            if (Menu.PLAY_menu == true && Menu.PLAY_credits == false) {
                Menu.currSound.Stop();
                Menu.PLAY_credits = true;
                Menu.PLAY_menu = false;
            }
            if (Menu.PLAY_menu == false && Menu.PLAY_credits == true) {
                GlobalMethod.PlaySound("Soundtracks/Credits/stay-retro-124958.wav", true);
            }
        }
        public static void RenderTitle() {
            Console.WriteLine(" BBBBBB     BBBB    BBBBBBBB  BBBBBBBB     BBBBBBB  BBBBBBB   BBBBBBBB  BBBBBB    BB  BBBBBBBB   BBBBBBB");
            Console.WriteLine("BB    BB   BB  BB   BB BB BB  BB          BB        BB    BB  BB        BB   BB   BB     BB     BB      ");
            Console.WriteLine("BB        BB    BB  BB BB BB  BB          BB        BB    BB  BB        BB    BB  BB     BB     BB      ");
            Console.WriteLine("BB  BBBB  BBBBBBBB  BB BB BB  BBBBBBBB    BB        BBBBBBB   BBBBBBBB  BB    BB  BB     BB      BBBBBB ");
            Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB          BB        BB    BB  BB        BB    BB  BB     BB           BB");
            Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB          BB        BB    BB  BB        BB   BB   BB     BB           BB");
            Console.WriteLine(" BBBBBB   BB    BB  BB BB BB  BBBBBBBB     BBBBBBB  BB    BB  BBBBBBBB  BBBBBB    BB     BB     BBBBBBB ");
            GlobalMethod.Page.RenderDottedLine(105);
            Console.WriteLine("CREDITS: | Back to menu: [Backspace]\n");
        }
        public static void OpenHTML() {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
            string htmlFilePath = Path.Combine(projectDirectory, "credits.html");
            if (File.Exists(htmlFilePath)) {
                var process = new Process();
                process.StartInfo = new ProcessStartInfo {
                    FileName = htmlFilePath,
                    UseShellExecute = true
                };
                process.Start();
            } else {
                Console.WriteLine("Plik credits.html nie zosta³ znaleziony.");
            }
        }
    }
}