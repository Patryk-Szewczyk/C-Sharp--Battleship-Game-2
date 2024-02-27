using Page_Instructions;

namespace Page_Menu
{
    interface IManuPage
    {
        public static void Menu() { }   // Wyświetelenie strony menu.
    }
    public class MenuPage : IManuPage
    {
        public static bool isMenuButtonLoop = true;
        public static string[] menuButtons = { "PVP Mode", "PVC Mode", "Instruction", "Ranking", "Options", "Credits", "Exit" };
        public static int menuButtNum = menuButtons.Length;   // Zawsze ostatni, bo chcę mieć kursor na górze!
        
        public static void Menu()
        {

            while (isMenuButtonLoop == true)   // MENU
            {
                Console.Clear();
                Console.WriteLine("BBBBBBB     BBBB    BBBBBBBB  BBBBBBBB  BB        BBBBBBBB   BBBBBBB  BB    BB  BB  BBBBBBB      BBBBBB");
                Console.WriteLine("BB    BB   BB  BB      BB        BB     BB        BB        BB        BB    BB  BB  BB    BB    BB    BB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB        BB        BB    BB  BB  BB    BB         BB");
                Console.WriteLine("BBBBBBBB  BBBBBBBB     BB        BB     BB        BBBBBBBB   BBBBBB   BBBBBBBB  BB  BBBBBBB        BBB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB            BB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB           BB");
                Console.WriteLine("BBBBBBB   BB    BB     BB        BB     BBBBBBBB  BBBBBBBB  BBBBBBB   BB    BB  BB  BB          BBBBBBBB");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("MENU: (navigation = arrows/wsad, click = ENTER)\n");
                for (int i = 0, j = menuButtons.Length; i < menuButtons.Length; i++, j--)
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
                // ASCII:
                int asciiValue = 0;
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Enter)
                {
                    Console.Write("");
                    asciiValue = (int)key.Key;
                } 
                else
                {
                    isMenuButtonLoop = false;
                }
                // Poruszanie się po przyciskach (obliczenia):
                if (asciiValue == 87 || asciiValue == 38)
                {
                    menuButtNum = (menuButtNum < menuButtons.Length) ? menuButtNum += 1 : menuButtNum;
                    Console.WriteLine(menuButtNum);
                }
                else if (asciiValue == 83 || asciiValue == 40)
                {
                    menuButtNum = (menuButtNum > 1) ? menuButtNum -= 1 : menuButtNum;
                    Console.WriteLine(menuButtNum);
                }
            }
            switch (menuButtNum)
            {
                case 7:   // PVP
                    break;
                case 6:   // PVC
                    break;
                case 5:   // Instruction
                    PageInstructions.Instruction();
                    break;
                case 4:   // Ranking
                    break;
                case 3:   // Options
                    break;
                case 2:   // Credits
                    break;
                case 1:   // Exit
                    isMenuButtonLoop = false;
                    break;
            }
        }
    }
}
