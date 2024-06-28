using System;
using System.Diagnostics;
using System.IO;
using Page_Menu;

namespace Page_Credits
{

    // KIEDY SKOŃCZYSZ GRĘ - SPRÓBUJ ZAAPLICOWAĆ "static" DO INTERFEJSÓW STRON!
    interface IPageCredits   // Mogłem opuścić interfejs, aby mieć metody statyczne, ale używam go ponieważ chcę mieć widoczne na górze nazwy wszystkich metody danej klasy:
    {
        void Credits();   // Wyświetlenie strony napisów końcowych.
    }
    public class PageCredits
    {
        public static bool isCreditsLoop = true;
        public void Credits()
        {
            if (MenuPage.menuSoundtrack_PLAY == true && MenuPage.creditsSoundtrack_PLAY == false)
            {
                MenuPage.currentSoundtrack.Stop();
                MenuPage.creditsSoundtrack_PLAY = true;
                MenuPage.menuSoundtrack_PLAY = false;
            }
            if (MenuPage.menuSoundtrack_PLAY == false && MenuPage.creditsSoundtrack_PLAY == true)
            {
                MenuPage.Soundtrack("Soundtracks/Credits/stay-retro-124958.wav");
            }

            System.ConsoleKeyInfo key;
            while (isCreditsLoop == true)
            {
                Console.Clear();
                Console.WriteLine(" BBBBBB     BBBB    BBBBBBBB  BBBBBBBB     BBBBBBB  BBBBBBB   BBBBBBBB  BBBBBB    BB  BBBBBBBB   BBBBBBB");
                Console.WriteLine("BB    BB   BB  BB   BB BB BB  BB          BB        BB    BB  BB        BB   BB   BB     BB     BB      ");
                Console.WriteLine("BB        BB    BB  BB BB BB  BB          BB        BB    BB  BB        BB    BB  BB     BB     BB      ");
                Console.WriteLine("BB  BBBB  BBBBBBBB  BB BB BB  BBBBBBBB    BB        BBBBBBB   BBBBBBBB  BB    BB  BB     BB      BBBBBB ");
                Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB          BB        BB    BB  BB        BB    BB  BB     BB           BB");
                Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB          BB        BB    BB  BB        BB   BB   BB     BB           BB");
                Console.WriteLine(" BBBBBB   BB    BB  BB BB BB  BBBBBBBB     BBBBBBB  BB    BB  BBBBBBBB  BBBBBB    BB     BB     BBBBBBB ");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("CREDITS: | Back to menu: [Q]\n");

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
                string htmlFilePath = Path.Combine(projectDirectory, "credits.html");
                //Console.WriteLine("Ścieżka do pliku HTML: " + htmlFilePath);
                if (File.Exists(htmlFilePath))
                {
                    //Console.WriteLine("Plik credits.html znaleziony. Otwieranie przeglądarki...");
                    var process = new Process();
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = htmlFilePath,
                        UseShellExecute = true
                    };
                    process.Start();
                }
                /*else
                {
                    Console.WriteLine("Plik credits.html nie został znaleziony.");
                }*/

                key = Console.ReadKey(true);
                if (key.Key == System.ConsoleKey.Q)
                {
                    isCreditsLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
    }
}
