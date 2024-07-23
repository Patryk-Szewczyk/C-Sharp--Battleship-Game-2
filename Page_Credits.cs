using System;
using System.Diagnostics;
using System.IO;
using Page_Menu;

namespace Page_Credits
{

    // KIEDY SKOÑCZYSZ GRÊ - SPRÓBUJ ZAAPLICOWAÆ "static" DO INTERFEJSÓW STRON!
    interface IPageCredits   // Mog³em opuœciæ interfejs, aby mieæ metody statyczne, ale u¿ywam go poniewa¿ chcê mieæ widoczne na górze nazwy wszystkich metody danej klasy:
    {
        void Credits();   // Wyœwietlenie strony napisów koñcowych.
    }
    public class PageCredits
    {
        public static bool isCreditsLoop = true;
        public static bool isCorrectSign = false;
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
                Console.WriteLine("CREDITS: | Back to menu: [Backspace]\n");

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
                string htmlFilePath = Path.Combine(projectDirectory, "credits.html");
                //Console.WriteLine("Œcie¿ka do pliku HTML: " + htmlFilePath);
                if (File.Exists(htmlFilePath))
                {
                    //Console.WriteLine("Plik credits.html znaleziony. Otwieranie przegl¹darki...");
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
                    Console.WriteLine("Plik credits.html nie zosta³ znaleziony.");
                }*/
                
                // Pêtla ta uniemo¿liwia prze³adowanie strony kiedy kliknie siê niew³aœciwy klawisz.
                while (isCorrectSign == false)
                {
                    key = System.Console.ReadKey(true);
                    if (key.Key == System.ConsoleKey.Backspace)
                    {
                        isCorrectSign = true;
                        isCreditsLoop = false;
                        MenuPage.isMenuButtonLoop = true;
                        MenuPage.Menu();
                    }
                }
                isCorrectSign = false;
            }
        }
    }
}