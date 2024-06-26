using System;
using Page_Menu;

namespace Page_Ranking
{
    // KIEDY SKOŃCZYSZ GRĘ - SPRÓBUJ ZAAPLICOWAĆ "static" DO INTERFEJSÓW STRON!
    interface IPageRanking   // Mogłem opuścić interfejs, aby mieć metody statyczne, ale używam go ponieważ chcę mieć widoczne na górze nazwy wszystkich metody danej klasy:
    {
        void Ranking();   // Wyświetlenie strony rankingu.
        //void Scores_PVP();   // Wyświetlenie wyników trybu gry: PVP.
        void Scores_PVC();   // Wyświetlenie wyników trybu gry: PVC.
    }
    public class PageRanking : IPageRanking
    {
        public static bool isRankingButtonLoop = true;
        public static bool isCorrectSign = false;
        public static string[] rankingButtons = { "PVC" };
        public static int rankingButtNum = rankingButtons.Length;
        public void Ranking()
        {
            System.ConsoleKey key = System.ConsoleKey.Backspace;   // Dowolny niewłaściwy klawisz.
            System.ConsoleKeyInfo corr_key;
            while (isRankingButtonLoop == true)
            {
                Console.Clear();
                Console.WriteLine("BBBBBBB     BBBB    BBBB  BB  BB    BB  BB  BBBB  BB   BBBBBB ");
                Console.WriteLine("BB    BB   BB  BB   BB BB BB  BB   BB   BB  BB BB BB  BB    BB");
                Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB  BB    BB  BB BB BB  BB      ");
                Console.WriteLine("BBBBBBB   BBBBBBBB  BB BB BB  BBBBB     BB  BB BB BB  BB  BBB ");
                Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB  BB    BB  BB BB BB  BB  B BB");
                Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB   BB   BB  BB BB BB  BB    BB");
                Console.WriteLine("BB    BB  BB    BB  BB  BBBB  BB    BB  BB  BB  BBBB   BBBBBB ");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("Choose game mode: (arrows/[W][S]) | Back to menu: [Q]\n");
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
                    /*case 2:
                        PageRanking pvp = new PageRanking();
                        pvp.Scores_PVP();
                        break;*/
                    case 1:
                        PageRanking pvc = new PageRanking();
                        pvc.Scores_PVC();
                        break;
                }
                while (isCorrectSign == false)   // Pętla ta uniemożliwia przeładowanie strony kiedy kliknie się niewłaściwy klawisz.
                {
                    corr_key = Console.ReadKey(true);
                    if (corr_key.Key == System.ConsoleKey.W || corr_key.Key == System.ConsoleKey.S || corr_key.Key == System.ConsoleKey.UpArrow || corr_key.Key == System.ConsoleKey.DownArrow || corr_key.Key == System.ConsoleKey.Q)
                    {
                        isCorrectSign = true;
                        key = corr_key.Key;
                    }
                }
                isCorrectSign = false;
                // Poruszanie się po przyciskach (obliczenia):
                if (key == System.ConsoleKey.UpArrow || key == System.ConsoleKey.W)
                {
                    rankingButtNum = (rankingButtNum < rankingButtons.Length) ? rankingButtNum += 1 : rankingButtNum;
                }
                else if (key == System.ConsoleKey.DownArrow || key == System.ConsoleKey.S)
                {
                    rankingButtNum = (rankingButtNum > 1) ? rankingButtNum -= 1 : rankingButtNum;
                }
                else if (key == System.ConsoleKey.Q)
                {
                    isRankingButtonLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
        /*public void Scores_PVP()  // Anulowany tryb:
        {
            System.Console.WriteLine("Page_PVP");
        }*/
        public void Scores_PVC()  // Tryb gracz vs komputer:
        {
            Console.WriteLine("Page_PVC");
        }
    }
}
