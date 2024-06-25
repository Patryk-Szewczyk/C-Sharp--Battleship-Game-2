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

                //

                System.Console.Clear();
                System.Console.WriteLine("BBBBBBB   BB    BB  BBBBBBB ");
                System.Console.WriteLine("BB    BB  BB    BB  BB    BB");
                System.Console.WriteLine("BB    BB  BB    BB  BB    BB");
                System.Console.WriteLine("BBBBBBB   BB    BB  BBBBBBB ");
                System.Console.WriteLine("BB         BB  BB   BB      ");
                System.Console.WriteLine("BB          BBBB    BB      ");
                System.Console.WriteLine("BB           BB     BB      ");
                System.Console.WriteLine("\n- - - - - - - - - - - - - -\n");
                System.Console.WriteLine("Back to menu: [Q]\n");
                key = System.Console.ReadKey(true);
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
