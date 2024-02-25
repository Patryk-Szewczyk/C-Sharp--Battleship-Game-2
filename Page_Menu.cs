namespace Page_Menu
{
    interface IManuPage
    {
        public static void Menu() { }
    }
    public class MenuPage : IManuPage
    {
        public static bool isMenuButtonLoop = true;
        public static string[] menuButtons = { "PVP Mode", "PVC Mode", "Ranking", "Options", "Credits", "Exit" };
        public static int menuButtNum = 6;
        
        public static void Menu()
        {
            while (isMenuButtonLoop == true)
            {
                Console.Clear();
                Console.WriteLine("BBBBBBB     BBBB    BBBBBBBB  BBBBBBBB  BB        BBBBBBBB   BBBBBBB  BB    BB  BB  BBBBBBB      BBBBBB");
                Console.WriteLine("BB    BB   BB  BB      BB        BB     BB        BB        BB        BB    BB  BB  BB    BB    BB    BB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB        BB        BB    BB  BB  BB    BB         BB");
                Console.WriteLine("BBBBBBBB  BBBBBBBB     BB        BB     BB        BBBBBBBB   BBBBBB   BBBBBBBB  BB  BBBBBBB        BBB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB            BB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB           BB");
                Console.WriteLine("BBBBBBB   BB    BB     BB        BB     BBBBBBBB  BBBBBBBB  BBBBBBB   BB    BB  BB  BB          BBBBBBBB");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("To continue choose some option: (navigation = arrows/wsad, press = ENTER)");
                for (int i = 0, j = 6; i < menuButtons.Length; i++, j--)
                {
                    if (j == menuButtNum)
                    {
                        Console.WriteLine("> " + menuButtons[i]);
                    }
                    else
                    {
                        Console.WriteLine("  " + menuButtons[i]);
                    }
                }
                int asciiValue = 0;
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Enter)
                {
                    Console.Write("");
                    asciiValue = (int)key.Key;
                }
                else
                {
                    isMenuButtonLoop = false;   // Wybranie danej opcji.
                }
                if (asciiValue == 87 || asciiValue == 38)
                {
                    menuButtNum = (menuButtNum < 6) ? menuButtNum += 1 : menuButtNum;
                    Console.WriteLine(menuButtNum);
                }
                else if (asciiValue == 83 || asciiValue == 40)
                {
                    menuButtNum = (menuButtNum > 1) ? menuButtNum -= 1 : menuButtNum;
                    Console.WriteLine(menuButtNum);
                }
            }
        }
    }
}