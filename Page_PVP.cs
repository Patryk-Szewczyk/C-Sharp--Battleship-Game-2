using System;
using Page_Menu;

namespace Page_PVP
{

    // KIEDY SKOŃCZYSZ GRĘ - SPRÓBUJ ZAAPLICOWAĆ "static" DO INTERFEJSÓW STRON!
    interface IPagePVP   // Mogłem opuścić interfejs, aby mieć metody statyczne, ale używam go ponieważ chcę mieć widoczne na górze nazwy wszystkich metody danej klasy:
    {
        void PVP();   // Wyświetlenie strony PVP.
    }
    public class PagePVP : IPagePVP
    {
        public static bool isPVPShipPositingLoop = true;
        public void PVP()
        {
            System.ConsoleKeyInfo key;
            while (isPVPShipPositingLoop == true)
            {
                MenuPage.currentSoundtrack.Stop();   // Test poprwności zamykania i ponownego odtwierania ścieżki dźwiękowej | ?
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
                Console.WriteLine("PVP MODE: | Back to menu: [Q] | Create player: [C] | Delete player: [P]\n");
                key = Console.ReadKey(true);
                if (key.Key == System.ConsoleKey.Q)
                {
                    isPVPShipPositingLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
    }
}
