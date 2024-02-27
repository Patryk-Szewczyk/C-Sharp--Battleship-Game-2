using Page_Menu;

namespace Page_Credits
{

    // KIEDY SKOŃCZYSZ GRĘ - SPRÓBUJ ZAAPLICOWAĆ "static" DO INTERFEJSÓW STRON!
    interface IPageCredits   // Mogłem opuścić interfejs, aby mieć metody statyczne, ale używam go ponieważ chcę mieć widoczne na górze nazwy wszystkich metody danej klasy:
    {
        public void Credits();   // Wyświetlenie strony napisów końcowych.
    }
    public class PageCredits : IPageCredits
    {
        public static bool isCreditsLoop = true;
        public void Credits()
        {
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
                Console.WriteLine("Back to menu: [Q]\n");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q)
                {
                    isCreditsLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
    }
}
