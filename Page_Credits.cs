using System;
using System.Diagnostics;
using System.IO;
using Library_GlobalMethods;
using Page_Menu;
using static System.Net.Mime.MediaTypeNames;

namespace Page_Credits {
    public class Credits {
        public static bool isPage = true;
        public static bool isCorrSign = false;
        public void RenderPage() {
            SoundSwitch();
            RenderTitle();
            System.ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                OpenHTML();
                LoopCorrectKey(key);   // Pêtla ta uniemo¿liwia prze³adowanie strony kiedy kliknie siê niew³aœciwy klawisz.
            }
        }
        public static void SoundSwitch() {
            if (MenuPage.PLAY_menu == true && MenuPage.PLAY_credits == false) {
                MenuPage.currSound.Stop();
                MenuPage.PLAY_credits = true;
                MenuPage.PLAY_menu = false;
            }
            if (MenuPage.PLAY_menu == false && MenuPage.PLAY_credits == true) {
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
            GlobalMethod.RenderDottedLine(105);
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
        public static void LoopCorrectKey(System.ConsoleKeyInfo key) {
            while (isCorrSign == false) {
                key = System.Console.ReadKey(true);
                if (key.Key == System.ConsoleKey.Backspace) break;
            }
            if (key.Key == System.ConsoleKey.Backspace) MenuReturn();
        }
        public static void MenuReturn() {
            isPage = false;
            MenuPage.isPage = true;
            MenuPage.Menu();
        }
    }
}