using Page_Menu;

namespace Page_Instructions
{
    // KIEDY SKOŃCZYSZ GRĘ - SPRÓBUJ ZAAPLICOWAĆ "static" DO INTERFEJSÓW STRON!
    interface IPageInstructions   // Mogłem opuścić interfejs, aby mieć metody statyczne, ale używam go ponieważ chcę mieć widoczne na górze nazwy wszystkich metody danej klasy:
    {
        void Instructions();   // Wyświetlenie strony instrukcji.
        void Page_Game();   // Wyświetlenie instrukcji obsługi gry.
        void Page_Ships();   // Wyświetlenie informacji odnośnie statków.
        void Page_Board();   // Wyświetlenie instrukcji postępowania z planszą w trakcie gry.
    }
    public class PageInstructions : IPageInstructions
    {
        public static bool isInstructionButtonLoop = true;
        public static bool isCorrectSign = false;
        public static string[] instructionButtons = { "Game", "Ships", "Board"};
        public static int instructionButtNum = instructionButtons.Length;
        public void Instructions()
        {
            while (isInstructionButtonLoop == true)
            {
                System.Console.Clear();
                System.Console.WriteLine("BB  BBBB  BB   BBBBBBB  BBBBBBBB  BBBBBBB   BB    BB   BBBBBBB  BBBBBBBB  BB   BBBBBB   BBBB  BB");
                System.Console.WriteLine("BB  BB BB BB  BB           BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
                System.Console.WriteLine("BB  BB BB BB  BB           BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
                System.Console.WriteLine("BB  BB BB BB   BBBBBB      BB     BBBBBBB   BB    BB  BB           BB     BB  BB    BB  BB BB BB");
                System.Console.WriteLine("BB  BB BB BB        BB     BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
                System.Console.WriteLine("BB  BB BB BB        BB     BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
                System.Console.WriteLine("BB  BB  BBBB  BBBBBBB      BB     BB    BB   BBBBBB    BBBBBBB     BB     BB   BBBBBB   BB  BBBB");
                System.Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                System.Console.WriteLine("Choose instruction page: (arrows/wsad) | Back to menu: [Q]\n");
                for (int i = 0, j = instructionButtons.Length; i < instructionButtons.Length; i++, j--)
                {
                    if (j == instructionButtNum)
                    {
                        System.Console.WriteLine("> " + instructionButtons[i]);
                    }
                    else
                    {
                        System.Console.WriteLine("  " + instructionButtons[i]);
                    }
                }
                System.Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                switch (instructionButtNum)
                {
                    case 3:
                        PageInstructions game = new PageInstructions();
                        game.Page_Game();
                        break;
                    case 2:
                        PageInstructions ships = new PageInstructions();
                        ships.Page_Ships();
                        break;
                    case 1:
                        PageInstructions board = new PageInstructions();
                        board.Page_Board();
                        break;
                }
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
                    instructionButtNum = (instructionButtNum < instructionButtons.Length) ? instructionButtNum += 1 : instructionButtNum;
                }
                else if (key == System.ConsoleKey.DownArrow || key == System.ConsoleKey.S)
                {
                    instructionButtNum = (instructionButtNum > 1) ? instructionButtNum -= 1 : instructionButtNum;
                }
                else if (key == System.ConsoleKey.Q)
                {
                    isInstructionButtonLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
        public void Page_Game()
        {
            System.Console.WriteLine("1. Every player have a 7 ships.");
            System.Console.WriteLine("2. Every player must set our every ship on our board.");
            System.Console.WriteLine("3. Each ship has a certain length and direction.");
            System.Console.WriteLine("4. Ships can be placed on board in two types of positions: horizontal and vertical.");
            System.Console.WriteLine("5. The player boards have dimensions of 10 x 10 fields.");
            System.Console.WriteLine("6. When placing ships, ships canNOT leave the board and canNOT overlap each other.");
            System.Console.WriteLine("7. After placing the ships on the boards, you can start the game.");
            System.Console.WriteLine("8. At the beginning, all squares of the board are marked with the sign \"~\" on the board.");
            System.Console.WriteLine("9. A hit ship is marked with a series of number from 1 to 7 according to ship number on the board.");
            System.Console.WriteLine("10. The hit is marked with an \"X\" on the board.");
            System.Console.WriteLine("11. The miss is marked with an \"O\" on the board.");
            System.Console.WriteLine("12. If you sink enemy ship, this ship coordinates emerge under boards in players ships info.");
            System.Console.WriteLine("13. To attack an opponent, the player must move your cursor {  } on area,");
            System.Console.WriteLine("    which you wan attack by arrows or \"wsad\" keys.");
            System.Console.WriteLine("14. Players shooting together by turns.");
            System.Console.WriteLine("15. If some player hits your enemy, this player can shoot again. This activity repeat as much");
            System.Console.WriteLine("    times how many this player hits your enemy's ships.");
            System.Console.WriteLine("16. The winner is this one who defeats his enemy.");
            System.Console.WriteLine("17. After battle, players see your scores and next they see appropriate game mode score ranking.");
            System.Console.WriteLine("18. After game players can play game again or get in game credits.");
        }
        public void Page_Ships()
        {
            System.Console.WriteLine("Page_Ships");
        }
        public void Page_Board()
        {
            System.Console.WriteLine("Page_Board");
        }
    }
}
