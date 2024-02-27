using Page_Menu;

namespace Page_Options
{
    // KIEDY SKOŃCZYSZ GRĘ - SPRÓBUJ ZAAPLICOWAĆ "static" DO INTERFEJSÓW STRON!
    interface IPageOptions   // Mogłem opuścić interfejs, aby mieć metody statyczne, ale używam go ponieważ chcę mieć widoczne na górze nazwy wszystkich metody danej klasy:
    {
        public void Options();   // Wyświetlenie strony rankingu.
    }
    public class PageOptions : IPageOptions
    {
        public static bool isOptionsButtonLoop = true;
        // EJJJJJJJJJJJ!!! Weź dodaj w pętli FOR po elementach poniższej tablicy odpowiednią informację o włączonych i wyłączonych "checkbox'ach"!
        public static string[] optionsButtons = { "Music", "Sound effects", "AI voice", "Game Credits Presentation", "Delete ranking data: moving = [O][L], choose = [I], delete = [C]", "Delete Machine Learning data: moving = [O][L], choose = [I), delete = [C]" };
        public static int optionsButtNum = optionsButtons.Length;
        public void Options()
        {
            while (isOptionsButtonLoop == true)
            {
                Console.Clear();
                Console.WriteLine(" BBBBBB   ");
                Console.WriteLine("BB    BB  ");
                Console.WriteLine("BB    BB  ");
                Console.WriteLine("BB    BB  ");
                Console.WriteLine("BB    BB  ");
                Console.WriteLine("BB    BB  ");
                Console.WriteLine(" BBBBBB   ");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("Options: (arrows/wsad) | Back to menu: [Q]\n");
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
                switch (optionsButtNum)
                {
                    //
                }
                // Poruszanie się po przyciskach (obliczenia):
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                {
                    optionsButtNum = (optionsButtNum < optionsButtons.Length) ? optionsButtNum += 1 : optionsButtNum;
                }
                else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                {
                    optionsButtNum = (optionsButtNum > 1) ? optionsButtNum -= 1 : optionsButtNum;
                }
                else if (key.Key == ConsoleKey.Q)
                {
                    isOptionsButtonLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
    }
}
