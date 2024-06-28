using System;
using Page_Menu;
using Page_PVP;
using Page_PVC;
using Page_Ranking;

namespace Page_Options
{
    // KIEDY SKOŃCZYSZ GRĘ - SPRÓBUJ ZAAPLICOWAĆ "static" DO INTERFEJSÓW STRON!
    interface IPageOptions   // Mogłem opuścić interfejs, aby mieć metody statyczne, ale używam go ponieważ chcę mieć widoczne na górze nazwy wszystkich metody danej klasy:
    {
        void Options();   // Wyświetlenie strony rankingu.
    }
    public class PageOptions : IPageOptions
    {
        public static bool isOptionsButtonLoop = true;
        public static bool isCorrectSign = false;
        public static string[] optionsButtons = 
        { 
            "Music:                            ON = [E], OFF = [D]",
            "Sound effects:                    ON = [E], OFF = [D]",
            "AI voice:                         ON = [E], OFF = [D]",
            "AI subtitles:                     ON = [E], OFF = [D]",
            "Delete ranking data:              moving = [O][L], choose = [I], delete = [P]",   // PVP || PVC
            //"Delete Machine Learning data:     moving = [O][L], choose = [I], delete = [P]" 
        };
        public static string[] toRankingButtons = { "PVP", "PVC" };   // EJJJJJJJJJJ!!! JESTEŚ NA TYM JAKBY CO!!!
        public static int optionsButtNum = optionsButtons.Length;
        public void Options()
        {
            System.ConsoleKey key = System.ConsoleKey.Backspace;   // Dowolny niewłaściwy klawisz.
            while (isOptionsButtonLoop == true)
            {
                Console.Clear();
                Console.WriteLine(" BBBBBB   BBBBBBB   BBBBBBBB  BB   BBBBBB   BBBB  BB   BBBBBBB");
                Console.WriteLine("BB    BB  BB    BB     BB     BB  BB    BB  BB BB BB  BB      ");
                Console.WriteLine("BB    BB  BB    BB     BB     BB  BB    BB  BB BB BB  BB      ");
                Console.WriteLine("BB    BB  BBBBBBB      BB     BB  BB    BB  BB BB BB   BBBBBB ");
                Console.WriteLine("BB    BB  BB           BB     BB  BB    BB  BB BB BB        BB");
                Console.WriteLine("BB    BB  BB           BB     BB  BB    BB  BB BB BB        BB");
                Console.WriteLine(" BBBBBB   BB           BB     BB   BBBBBB   BB  BBBB  BBBBBBB ");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("OPTIONS: | Moving: arrows/[W][S] | Back to menu: [Q]\n");
                for (int i = 0, j = optionsButtons.Length; i < optionsButtons.Length; i++, j--)
                {
                    if (j == optionsButtNum)
                    {
                        Console.WriteLine("> " + optionsButtons[i]);
                    }
                    else
                    {
                        Console.WriteLine("  " + optionsButtons[i]);
                    }
                }
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");

                /*switch (optionsButtNum)
                {
                    // INFORMACJE O STANIE POSZCZEGÓLNYCH OPCJI, np. Music: OFF
                }*/
                while (isCorrectSign == false)   // Pętla ta uniemożliwia przeładowanie strony kiedy kliknie się niewłaściwy klawisz.
                {
                    System.ConsoleKeyInfo corr_key = Console.ReadKey(true);
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
                    optionsButtNum = (optionsButtNum < optionsButtons.Length) ? optionsButtNum += 1 : optionsButtNum;
                }
                else if (key == System.ConsoleKey.DownArrow || key == System.ConsoleKey.S)
                {
                    optionsButtNum = (optionsButtNum > 1) ? optionsButtNum -= 1 : optionsButtNum;
                }
                else if (key == System.ConsoleKey.Q)
                {
                    isOptionsButtonLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
    }
}
