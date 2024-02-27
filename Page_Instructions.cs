namespace Page_Instructions
{
    interface IPageInstructions
    {
        public static void Instruction() { }   // Wyœwietlenie strony instrukcji.
        protected static void Page_Game() { }   // Wyœwietlenie instrukcji obs³ugi gry.
        protected static void Page_Ships() { }   // Wyœwietlenie informacji odnoœnie statków.
        protected static void Page_Board() { }   // Wyœwietlenie instrukcji postêpowania z plansz¹ w trakcie gry.
    }
    public class PageInstructions : IPageInstructions
    {
        public static bool isInstructionButtonLoop = true;
        public static string[] instructionButtons = { "Game", "Ships", "Board"};
        public static int instructionButtNum = instructionButtons.Length;
        public static void Instruction()
        {
            while (isInstructionButtonLoop == true)
            {
                Console.Clear();
                Console.WriteLine("BB  BBBB  BB   BBBBBBB  BBBBBBBB  BBBBBBB   BB    BB   BBBBBBB  BBBBBBBB  BB   BBBBBB   BBBB  BB");
                Console.WriteLine("BB  BB BB BB  BB           BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
                Console.WriteLine("BB  BB BB BB  BB           BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
                Console.WriteLine("BB  BB BB BB   BBBBBB      BB     BBBBBBB   BB    BB  BB           BB     BB  BB    BB  BB BB BB");
                Console.WriteLine("BB  BB BB BB        BB     BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
                Console.WriteLine("BB  BB BB BB        BB     BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
                Console.WriteLine("BB  BB  BBBB  BBBBBBB      BB     BB    BB   BBBBBB    BBBBBBB     BB     BB   BBBBBB   BB  BBBB");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("Choose instruction page: (arrows/wsad) | Back to menu: [E]\n");
                for (int i = 0, j = instructionButtons.Length; i < instructionButtons.Length; i++, j--)
                {
                    if (j == instructionButtNum)
                    {
                        Console.WriteLine("> " + instructionButtons[i]);
                    }
                    else
                    {
                        Console.WriteLine("  " + instructionButtons[i]);
                    }
                }
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                switch (instructionButtNum)
                {
                    case 3:
                        PageInstructions.Page_Game();
                        break;
                    case 2:
                        PageInstructions.Page_Ships();
                        break;
                    case 1:
                        PageInstructions.Page_Board();
                        break;
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
                    isInstructionButtonLoop = false;
                }
                // Poruszanie siê po przyciskach (obliczenia):
                if (asciiValue == 87 || asciiValue == 38)
                {
                    instructionButtNum = (instructionButtNum < instructionButtons.Length) ? instructionButtNum += 1 : instructionButtNum;
                    Console.WriteLine(instructionButtNum);
                }
                else if (asciiValue == 83 || asciiValue == 40)
                {
                    instructionButtNum = (instructionButtNum > 1) ? instructionButtNum -= 1 : instructionButtNum;
                    Console.WriteLine(instructionButtNum);
                }
            }
        }
        protected static void Page_Game()
        {
            Console.WriteLine("1. Every player have a 7 ships.");
            Console.WriteLine("2. Every player must set our every ship on our board.");
            Console.WriteLine("3. Each ship has a certain length and direction.");
            Console.WriteLine("4. Ships can be placed on board in two types of positions: horizontal and vertical.");
            Console.WriteLine("5. The player boards have dimensions of 10 x 10 fields.");
            Console.WriteLine("6. When placing ships, ships canNOT leave the board and canNOT overlap each other.");
            Console.WriteLine("7. After placing the ships on the boards, you can start the game.");
            Console.WriteLine("8. At the beginning, all squares of the board are marked with the sign \"~\" on the board.");
            Console.WriteLine("9. A hit ship is marked with a series of number from 1 to 7 according to ship number on the board.");
            Console.WriteLine("10. The hit is marked with an \"X\" on the board.");
            Console.WriteLine("11. The miss is marked with an \"O\" on the board.");
            Console.WriteLine("12. If you sink enemy ship, this ship coordinates emerge under boards in players ships info.");
            Console.WriteLine("13. To attack an opponent, the player must move your cursor {  } on area,");
            Console.WriteLine("    which you wan attack by arrows or \"wsad\" keys.");
            Console.WriteLine("14. Players shooting together by turns.");
            Console.WriteLine("15. If some player hits your enemy, this player can shoot again. This activity repeat as much");
            Console.WriteLine("    times how many this player hits your enemy's ships.");
            Console.WriteLine("16. The winner is this one who defeats his enemy.");
            Console.WriteLine("17. After battle, players see your scores and next they see appropriate game mode score ranking.");
            Console.WriteLine("18. After game players can play game again or get in game credits.");
        }
        protected static void Page_Ships()
        {
            Console.WriteLine("Page_Ships");
        }
        protected static void Page_Board()
        {
            Console.WriteLine("Page_Board");
        }
    }
}