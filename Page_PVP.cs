using System;
using Page_Menu;

namespace Page_PVP
{

    // KIEDY SKOÑCZYSZ GRÊ - SPRÓBUJ ZAAPLICOWAÆ "static" DO INTERFEJSÓW STRON!
    interface IPagePVP   // Mog³em opuœciæ interfejs, aby mieæ metody statyczne, ale u¿ywam go poniewa¿ chcê mieæ widoczne na górze nazwy wszystkich metody danej klasy:
    {
        void PVP();   // Wyœwietlenie strony PVP.
    }
    public class PagePVP : IPagePVP
    {
        public static bool isPVPShipPositingLoop = true;
        public void PVP()
        {
            System.ConsoleKeyInfo key;
            while (isPVPShipPositingLoop == true)
            {
                MenuPage.currentSoundtrack.Stop();   // Test poprwnoœci zamykania i ponownego odtwierania œcie¿ki dŸwiêkowej | ?
                MenuPage.menuSoundtrack_PLAY = false;

                Console.Clear();
                Console.WriteLine("BBBBBBB   BB    BB  BBBBBBB ");
                Console.WriteLine("BB    BB  BB    BB  BB    BB");
                Console.WriteLine("BB    BB  BB    BB  BB    BB");
                Console.WriteLine("BBBBBBB   BB    BB  BBBBBBB ");
                Console.WriteLine("BB         BB  BB   BB      ");
                Console.WriteLine("BB          BBBB    BB      ");
                Console.WriteLine("BB           BB     BB      ");
                Console.WriteLine("\n- - - - - - - - - - - - - -\n");
                Console.WriteLine("PVC MODE: | Moving: arrows/[W][S] | Click = ENTER | Create player: [C] | Delete player: [P] | Back to menu: [Backspace]\n");
                key = Console.ReadKey(true);
                if (key.Key == System.ConsoleKey.Backspace)
                {
                    isPVPShipPositingLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
    }
}