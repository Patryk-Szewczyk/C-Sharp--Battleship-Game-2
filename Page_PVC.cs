using System;
using Page_Menu;

namespace Page_PVC
{

    // KIEDY SKOŃCZYSZ GRĘ - SPRÓBUJ ZAAPLICOWAĆ "static" DO INTERFEJSÓW STRON!
    interface IPagePVC   // Mogłem opuścić interfejs, aby mieć metody statyczne, ale używam go ponieważ chcę mieć widoczne na górze nazwy wszystkich metody danej klasy:
    {
        void PVC();   // Wyświetlenie strony PVC.
    }
    public class PagePVC : IPagePVC
    {
        public static bool isPVCShipPositingLoop = true;
        public void PVC()
        {
            System.ConsoleKeyInfo key;
            while (isPVCShipPositingLoop == true)
            {
                MenuPage.currentSoundtrack.Stop();   // Test poprwności zamykania i ponownego odtwierania ścieżki dźwiękowej | ?
                MenuPage.menuSoundtrack_PLAY = false;

                //

                Console.Clear();
                Console.WriteLine("BBBBBBB   BB    BB   BBBBBBB");
                Console.WriteLine("BB    BB  BB    BB  BB      ");
                Console.WriteLine("BB    BB  BB    BB  BB      ");
                Console.WriteLine("BBBBBBB   BB    BB  BB      ");
                Console.WriteLine("BB         BB  BB   BB      ");
                Console.WriteLine("BB          BBBB    BB      ");
                Console.WriteLine("BB           BB      BBBBBBB");
                Console.WriteLine("\n- - - - - - - - - - - - - -\n");
                Console.WriteLine("Back to menu: [Q]\n");
                key = Console.ReadKey(true);
                if (key.Key == System.ConsoleKey.Q)
                {
                    isPVCShipPositingLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
    }
}
