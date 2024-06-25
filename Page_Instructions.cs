using Page_Menu;
using System;

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
            System.ConsoleKey key = System.ConsoleKey.Backspace;   // Dowolny niewłaściwy klawisz.
            System.ConsoleKeyInfo corr_key;
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
                System.Console.WriteLine("Choose instruction page: (arrows/[W][S]) | Back to menu: [Q]\n");
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
                while (isCorrectSign == false)   // Pętla ta uniemożliwia przeładowanie strony kiedy kliknie się niewłaściwy klawisz.
                {
                    corr_key = System.Console.ReadKey(true);
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
            System.Console.WriteLine("Game instruction:" +
                "\n1. Every player have a 7 ships." +
                "\n2. Every player must set our every ship on our board." +
                "\n3. Each ship has a certain length and direction." +
                "\n4. Ships can be placed on board in two types of positions: horizontal and vertical." +
                "\n5. The player boards have dimensions of 10 x 10 fields." +
                "\n6. When placing ships, ships canNOT leave the board and canNOT overlap each other." +
                "\n7. After placing the ships on the boards, you can start the game." +
                "\n8. At the beginning, all squares of the board are marked with the sign \"~\" on the board." +
                "\n9. A hit ship is marked with a series of number from 1 to 7 according to ship number on the board." +
                "\n10. The hit is marked with an \"X\" on the board." +
                "\n11. The miss is marked with an \"O\" on the board." +
                "\n12. If you sink enemy ship, this ship coordinates emerge under boards in players ships info." +
                "\n13. To attack an opponent, the player must move your cursor {  } on area," +
                "\n    which you wan attack by arrows or \"[W][S][A][D]\" keys." +
                "\n14. Players shooting together by turns." +
                "\n15. If some player hits your enemy, this player can shoot again. This activity repeat as much" +
                "\n    times how many this player hits your enemy's ships." +
                "\n16. The winner is this one who defeats his enemy." +
                "\n17. After battle, players see your scores and next they see appropriate game mode score ranking." +
                "\n18. After game players can play game again or get in game credits.");// Ustawienie koloru tekstu na zielony
            
            
            
            
            
            Console.ForegroundColor = ConsoleColor.Green;
            string text = "This is a green text!";
            Console.WriteLine(text);

            // Przywrócenie domyślnego koloru tekstu
            Console.ResetColor();

            // Wyświetlenie kolejnego tekstu w domyślnym kolorze
            string defaultColorText = "This is a text in the default color.";
            Console.WriteLine(defaultColorText);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("I'am blue!");
            Console.ResetColor();





        }
        public void Page_Ships()
        {
            System.Console.WriteLine("You have 5 ships, about these length:" +
                "\nO - patrol boat" +
                "\nOO - frigate" +
                "\nOOO - submarine" +
                "\nOOOO - destroyer" +
                "\nOOOOO - aircraft carrier");
            System.Console.WriteLine("\nDestruction all ships indicate win these player who do it the opposite player.");
        }
        public void Page_Board()
        {
            System.Console.WriteLine("| Sea area: ~ | Cursor: { } | Hit: X | Miss: O | Sunken ships: (length numbers) - 22, 4444 |" +
                "\n __________________________________________________________ " + "     " + " __________________________________________________________ " +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|      O    ~    ~    ~    ~    ~    ~    O    ~    ~      |" + "     " + "|      ~    ~    ~    ~    ~    O    5    O    ~    ~      |" +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|      ~    ~    ~    ~    ~    O    2    2    O    ~      |" + "     " + "|      ~    O    ~    ~    ~    ~    5    ~    ~    ~      |" +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|      ~    ~    ~    O    ~    ~    ~    ~    ~    ~      |" + "     " + "|      ~    ~    ~    ~    ~    ~    5    ~    1    ~      |" +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|      ~    ~    O    3    ~    ~    ~    O    ~    ~      |" + "     " + "|      ~    ~    ~    ~    ~    ~    5    ~    ~    ~      |" +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|      ~    ~    ~    3    ~    ~    ~    O    ~    ~      |" + "     " + "|      ~    ~    O    ~    ~    ~    5    ~    ~    ~      |" +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|      ~    X    ~    3    ~    ~    ~    ~    ~    ~      |" + "     " + "|    { ~ }  X    X    O    ~    ~    ~    ~    ~    ~      |" +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|      ~    ~    ~    ~    ~    O    ~    ~    ~    ~      |" + "     " + "|      ~    O    ~    ~    ~    2    2    O    ~    ~      |" +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|      ~    ~    ~    ~    O    4    4    4    4    ~      |" + "     " + "|      ~    ~    ~    ~    ~    ~    ~    ~    ~    ~      |" +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|      ~    O    ~    ~    ~    ~    ~    ~    ~    ~      |" + "     " + "|      ~    ~    ~    ~    ~    ~    O    ~    ~    ~      |" +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|      ~    ~    ~    ~    ~    ~    ~    ~    ~    ~      |" + "     " + "|      ~    ~    ~    ~    ~    ~    ~    ~    ~    ~      |" +
                "\n|                                                          |" + "     " + "|                                                          |" +
                "\n|__________________________________________________________|" + "     " + "|__________________________________________________________|");
        }
    }
}
