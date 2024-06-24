using Page_Menu;
using System;
using System.Diagnostics;
using System.IO;

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
            MenuPage.menuSoundtrack.Stop();   // Test poprwności zamykania i ponownego odtwierania ścieżki dźwiękowej | ?
            MenuPage.menuSoundtrack_PLAY = false;

            System.ConsoleKeyInfo key;
            while (isCreditsLoop == true)
            {
                System.Console.Clear();
                System.Console.WriteLine(" BBBBBB     BBBB    BBBBBBBB  BBBBBBBB     BBBBBBB  BBBBBBB   BBBBBBBB  BBBBBB    BB  BBBBBBBB   BBBBBBB");
                System.Console.WriteLine("BB    BB   BB  BB   BB BB BB  BB          BB        BB    BB  BB        BB   BB   BB     BB     BB      ");
                System.Console.WriteLine("BB        BB    BB  BB BB BB  BB          BB        BB    BB  BB        BB    BB  BB     BB     BB      ");
                System.Console.WriteLine("BB  BBBB  BBBBBBBB  BB BB BB  BBBBBBBB    BB        BBBBBBB   BBBBBBBB  BB    BB  BB     BB      BBBBBB ");
                System.Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB          BB        BB    BB  BB        BB    BB  BB     BB           BB");
                System.Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB          BB        BB    BB  BB        BB   BB   BB     BB           BB");
                System.Console.WriteLine(" BBBBBB   BB    BB  BB BB BB  BBBBBBBB     BBBBBBB  BB    BB  BBBBBBBB  BBBBBB    BB     BB     BBBBBBB ");
                System.Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                System.Console.WriteLine("Back to menu: [Q]\n");

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

                key = System.Console.ReadKey(true);
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
