using Page_Menu;

namespace Page_Ranking
{
    // KIEDY SKOŃCZYSZ GRĘ - SPRÓBUJ ZAAPLICOWAĆ "static" DO INTERFEJSÓW STRON!
    interface IPageRanking   // Mogłem opuścić interfejs, aby mieć metody statyczne, ale używam go ponieważ chcę mieć widoczne na górze nazwy wszystkich metody danej klasy:
    {
        public void Ranking();   // Wyświetlenie strony rankingu.
        public void Scores_PVP();   // Wyświetlenie wyników trybu gry: PVP.
        public void Scores_PVC();   // Wyświetlenie wyników trybu gry: PVC.
    }
    public class PageRanking : IPageRanking
    {
        public static bool isRankingButtonLoop = true;
        public static string[] rankingButtons = { "PVP", "PVC" };
        public static int rankingButtNum = rankingButtons.Length;
        public void Ranking()
        {
            while (isRankingButtonLoop == true)
            {
                Console.Clear();
                Console.WriteLine("BBBBBBB   ");
                Console.WriteLine("BB    BB  ");
                Console.WriteLine("BB    BB  ");
                Console.WriteLine("BBBBBBB   ");
                Console.WriteLine("BB    BB  ");
                Console.WriteLine("BB    BB  ");
                Console.WriteLine("BB    BB  ");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("Choose game mode: (arrows/wsad) | Back to menu: [Q]\n");
                for (int i = 0, j = rankingButtons.Length; i < rankingButtons.Length; i++, j--)
                {
                    if (j == rankingButtNum)
                    {
                        Console.WriteLine("> " + rankingButtons[i]);
                    }
                    else
                    {
                        Console.WriteLine("  " + rankingButtons[i]);
                    }
                }
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                switch (rankingButtNum)
                {
                    case 2:
                        PageRanking pvp = new PageRanking();
                        pvp.Scores_PVP();
                        break;
                    case 1:
                        PageRanking pvc = new PageRanking();
                        pvc.Scores_PVC();
                        break;
                }
                // Poruszanie się po przyciskach (obliczenia):
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                {
                    rankingButtNum = (rankingButtNum < rankingButtons.Length) ? rankingButtNum += 1 : rankingButtNum;
                }
                else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                {
                    rankingButtNum = (rankingButtNum > 1) ? rankingButtNum -= 1 : rankingButtNum;
                }
                else if (key.Key == ConsoleKey.Q)
                {
                    isRankingButtonLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
        public void Scores_PVP()
        {
            Console.WriteLine("Page_PVP");
        }
        public void Scores_PVC()
        {
            Console.WriteLine("Page_PVC");
        }
    }
}
