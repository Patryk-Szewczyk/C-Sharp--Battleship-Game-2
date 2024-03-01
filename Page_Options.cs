using Page_Menu;

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
        public static string[] optionsButtons = { "Music:                            ON = [E], OFF = [D]",
                                                  "Sound effects:                    ON = [E], OFF = [D]",
                                                  "AI voice:                         ON = [E], OFF = [D]",
                                                  "AI subtitles:                     ON = [E], OFF = [D]",
                                                  "Game Credits: website / console:  website = [E], console = [D]",
                                                  "Delete ranking data:              moving = [O][L], choose = [I], delete = [C]",
                                                  "Delete Machine Learning data:     moving = [O][L], choose = [I], delete = [C]" };
        public static int optionsButtNum = optionsButtons.Length;
        public void Options()
        {
            while (isOptionsButtonLoop == true)
            {
                System.Console.Clear();
                System.Console.WriteLine(" BBBBBB   BBBBBBB   BBBBBBBB  BB   BBBBBB   BBBB  BB   BBBBBBB");
                System.Console.WriteLine("BB    BB  BB    BB     BB     BB  BB    BB  BB BB BB  BB      ");
                System.Console.WriteLine("BB    BB  BB    BB     BB     BB  BB    BB  BB BB BB  BB      ");
                System.Console.WriteLine("BB    BB  BBBBBBB      BB     BB  BB    BB  BB BB BB   BBBBBB ");
                System.Console.WriteLine("BB    BB  BB           BB     BB  BB    BB  BB BB BB        BB");
                System.Console.WriteLine("BB    BB  BB           BB     BB  BB    BB  BB BB BB        BB");
                System.Console.WriteLine(" BBBBBB   BB           BB     BB   BBBBBB   BB  BBBB  BBBBBBB ");
                System.Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                System.Console.WriteLine("Options: (arrows/wsad) | Back to menu: [Q]\n");
                for (int i = 0, j = optionsButtons.Length; i < optionsButtons.Length; i++, j--)
                {
                    if (j == optionsButtNum)
                    {
                        System.Console.WriteLine("> " + optionsButtons[i]);
                    }
                    else
                    {
                        System.Console.WriteLine("  " + optionsButtons[i]);
                    }
                }
                System.Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");

                /*switch (optionsButtNum)
                {
                    // INFORMACJE O STANIE POSZCZEGÓLNYCH OPCJI, np. Music: OFF
                }*/
                System.ConsoleKey key = System.ConsoleKey.Backspace;   // Dowolny niewłaściwy klawisz.
                while (isCorrectSign == false)   // Pętla ta uniemożliwia przeładowanie strony kiedy kliknie się niewłaściwy klawisz.
                {
                    System.ConsoleKeyInfo corr_key = System.Console.ReadKey(true);
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
