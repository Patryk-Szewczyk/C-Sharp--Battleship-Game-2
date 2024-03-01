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
            while (isPVCShipPositingLoop == true)
            {
                System.Console.Clear();
                System.Console.WriteLine("BBBBBBB   BB    BB   BBBBBBB");
                System.Console.WriteLine("BB    BB  BB    BB  BB      ");
                System.Console.WriteLine("BB    BB  BB    BB  BB      ");
                System.Console.WriteLine("BBBBBBB   BB    BB  BB      ");
                System.Console.WriteLine("BB         BB  BB   BB      ");
                System.Console.WriteLine("BB          BBBB    BB      ");
                System.Console.WriteLine("BB           BB      BBBBBBB");
                System.Console.WriteLine("\n- - - - - - - - - - - - - -\n");
                System.Console.WriteLine("Back to menu: [Q]\n");
                System.ConsoleKeyInfo key = System.Console.ReadKey(true);
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
