using System;
using Page_Menu;
using Library_GlobalMethods;

namespace Page_Instructions
{
    // KIEDY SKOÑCZYSZ GRÊ - SPRÓBUJ ZAAPLICOWAÆ "static" DO INTERFEJSÓW STRON!
    interface IPageInstructions   // Mog³em opuœciæ interfejs, aby mieæ metody statyczne, ale u¿ywam go poniewa¿ chcê mieæ widoczne na górze nazwy wszystkich metody danej klasy:
    {
        void Instructions();   // Wyœwietlenie strony instrukcji.
        void Page_Game();   // Wyœwietlenie instrukcji obs³ugi gry.
        void Page_Ships();   // Wyœwietlenie informacji odnoœnie statków.
        void Page_Board();   // Wyœwietlenie instrukcji postêpowania z plansz¹ w trakcie gry.
    }
    public class PageInstructions : IPageInstructions
    {
       // public static GlobalMethod globalMethod = new GlobalMethod();
        public static bool isInstructionLoop = true;
        public static bool isCorrectSign = false;
        public static string[] instructionButtons = { "Game", "Ships", "Board"};
        public static int instructionButtNum = instructionButtons.Length;
        public void Instructions()
        {
            System.ConsoleKey key = System.ConsoleKey.Backspace;   // Dowolny niew³aœciwy klawisz.
            System.ConsoleKeyInfo corr_key;
            while (isInstructionLoop == true)
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
                Console.WriteLine("INSTRUCTION: | Moving: arrows/[W][S] | Back to menu: [Backspace]\n");
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
                while (isCorrectSign == false)   // Pêtla ta uniemo¿liwia prze³adowanie strony kiedy kliknie siê niew³aœciwy klawisz.
                {
                    corr_key = System.Console.ReadKey(true);
                    if (corr_key.Key == System.ConsoleKey.W || corr_key.Key == System.ConsoleKey.S || corr_key.Key == System.ConsoleKey.UpArrow || corr_key.Key == System.ConsoleKey.DownArrow || corr_key.Key == System.ConsoleKey.Backspace)
                    {
                        isCorrectSign = true;
                        key = corr_key.Key;
                    }
                }
                isCorrectSign = false;
                // Poruszanie siê po przyciskach (obliczenia):
                if (key == System.ConsoleKey.UpArrow || key == System.ConsoleKey.W)
                {
                    instructionButtNum = (instructionButtNum < instructionButtons.Length) ? instructionButtNum += 1 : instructionButtNum;
                }
                else if (key == System.ConsoleKey.DownArrow || key == System.ConsoleKey.S)
                {
                    instructionButtNum = (instructionButtNum > 1) ? instructionButtNum -= 1 : instructionButtNum;
                }
                else if (key == System.ConsoleKey.Backspace)
                {
                    isInstructionLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
        public void Page_Game()
        {
            Console.WriteLine("Game instruction:" +
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
                "\n18. After game players can play game again or get in game credits.");
        }
        public void Page_Ships()
        {
            Console.WriteLine("You have 5 ships, about these length:" +
                "\n1 - patrol boat" +
                "\n22 - frigate" +
                "\n333 - submarine" +
                "\n4444 - destroyer" +
                "\n55555 - aircraft carrier");
            Console.WriteLine("\nDestruction all ships indicate win these player who do it the opposite player.");
        }
        public void Page_Board()  // NA PODSTAWIE PONI¯SZEGO ZAPISU ZAIMPLEMENTUJ DWIE PÊTLE, KTÓRE BÊD¥ ODPOWIEDZIALNE ZA WK£ADANIE ODPOWIEDNICH DANYCH Z IF'óW DO METODY "GlobalMethod.Color()"!!!
        {
            Console.Write("Cursor: ");
            GlobalMethod.Color("{ }", ConsoleColor.White);
            Console.Write(" | Sea area: ");
            GlobalMethod.Color("~", ConsoleColor.Blue);
            Console.Write(" | Hit: ");
            GlobalMethod.Color("X", ConsoleColor.Red);
            Console.Write(" | Miss: ");
            GlobalMethod.Color("O", ConsoleColor.DarkYellow);
            Console.Write(" | Sunken ships: (length numbers) - ");
            GlobalMethod.Color("22", ConsoleColor.Gray);
            Console.Write(", ");
            GlobalMethod.Color("4444", ConsoleColor.Gray);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            GlobalMethod.Color(" _______________________________________________       _______________________________________________ ", ConsoleColor.Green);
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - WIERSZ 1:
            // - - - - - - - - - - - - - - - - - - - - - - - LEWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);  // Ka¿dy tego typu element bêdzie mia³ dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.Write("     ");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - PRAWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" 5  ", ConsoleColor.Gray);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - WIERSZ 2:
            // - - - - - - - - - - - - - - - - - - - - - - - LEWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka¿dy tego typu element bêdzie mia³ dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" 2  ", ConsoleColor.Gray);
            GlobalMethod.Color(" 2  ", ConsoleColor.Gray);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.Write("     ");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - PRAWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" 5  ", ConsoleColor.Gray);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - WIERSZ 3:
            // - - - - - - - - - - - - - - - - - - - - - - - LEWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka¿dy tego typu element bêdzie mia³ dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.Write("     ");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - PRAWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" X  ", ConsoleColor.Red);
            GlobalMethod.Color(" X  ", ConsoleColor.Red);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" 5  ", ConsoleColor.Gray);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" 1  ", ConsoleColor.Gray);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - WIERSZ 4:
            // - - - - - - - - - - - - - - - - - - - - - - - LEWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka¿dy tego typu element bêdzie mia³ dwie wersje. Pierwsza: " O  ". Druga: "O", gdzie przed jest "{", a po "} ".
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" 3  ", ConsoleColor.Gray);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.Write("     ");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - PRAWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" 5  ", ConsoleColor.Gray);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - WIERSZ 5:
            // - - - - - - - - - - - - - - - - - - - - - - - LEWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka¿dy tego typu element bêdzie mia³ dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
            GlobalMethod.Color(" X  ", ConsoleColor.Red);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" 3  ", ConsoleColor.Gray);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.Write("     ");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - PRAWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" 5  ", ConsoleColor.Gray);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - WIERSZ 6:
            // - - - - - - - - - - - - - - - - - - - - - - - LEWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka¿dy tego typu element bêdzie mia³ dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
            GlobalMethod.Color(" X  ", ConsoleColor.Red);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" 3  ", ConsoleColor.Gray);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.Write("     ");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - PRAWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - ELSE IF: (Druga opcja, Pierwsza: to co zwykle [jedna linia "GlobalMethod()"])
            GlobalMethod.Color("{", ConsoleColor.White);
            GlobalMethod.Color("~", ConsoleColor.Blue);
            GlobalMethod.Color("} ", ConsoleColor.White);
            // - - - - - - - - - - - - - - - - - - - - -
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" 5  ", ConsoleColor.Gray);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - WIERSZ 7:
            // - - - - - - - - - - - - - - - - - - - - - - - LEWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka¿dy tego typu element bêdzie mia³ dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
            GlobalMethod.Color(" X  ", ConsoleColor.Red);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.Write("     ");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - PRAWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" 2  ", ConsoleColor.Gray);
            GlobalMethod.Color(" 2  ", ConsoleColor.Gray);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - WIERSZ 8:
            // - - - - - - - - - - - - - - - - - - - - - - - LEWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka¿dy tego typu element bêdzie mia³ dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" 4  ", ConsoleColor.Gray);
            GlobalMethod.Color(" 4  ", ConsoleColor.Gray);
            GlobalMethod.Color(" 4  ", ConsoleColor.Gray);
            GlobalMethod.Color(" 4  ", ConsoleColor.Gray);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.Write("     ");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - PRAWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - WIERSZ 9:
            // - - - - - - - - - - - - - - - - - - - - - - - LEWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka¿dy tego typu element bêdzie mia³ dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.Write("     ");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - PRAWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - WIERSZ 10:
            // - - - - - - - - - - - - - - - - - - - - - - - LEWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka¿dy tego typu element bêdzie mia³ dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.Write("     ");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - -
            // - - - - - - - - - - - - - - - - - - - - - - - PRAWO:
            GlobalMethod.Color("|    ", ConsoleColor.Green);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~  ", ConsoleColor.Blue);
            GlobalMethod.Color(" ~ ", ConsoleColor.Blue);
            GlobalMethod.Color("    |", ConsoleColor.Green);
            // - - - - - - - - - - - - - - - - - - - - - - - DODATEK:
            Console.WriteLine();
            GlobalMethod.Color("|                                               |     |                                               |", ConsoleColor.Green);
            Console.WriteLine();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            GlobalMethod.Color("|_______________________________________________|     |_______________________________________________|", ConsoleColor.Green);


            //"\n _______________________________________________ " + "     " + " _______________________________________________ " +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|     O   ~   ~   ~   ~   ~   ~   O   ~   ~     |" + "     " + "|     ~   ~   ~   ~   ~   O   5   O   ~   ~     |" +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|     ~   ~   ~   ~   ~   O   2   2   O   ~     |" + "     " + "|     ~   O   ~   ~   ~   ~   5   ~   ~   ~     |" +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|     ~   ~   ~   O   ~   ~   ~   ~   ~   ~     |" + "     " + "|     ~   ~   ~   ~   ~   ~   5   ~   1   ~     |" +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|     ~   ~   O   3   ~   ~   ~   O   ~   ~     |" + "     " + "|     ~   ~   ~   ~   ~   ~   5   ~   ~   ~     |" +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|     ~   ~   ~   3   ~   ~   ~   O   ~   ~     |" + "     " + "|     ~   ~   O   ~   ~   ~   5   ~   ~   ~     |" +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|     ~   X   ~   3   ~   ~   ~   ~   ~   ~     |" + "     " + "|    {~}  X   X   O   ~   ~   ~   ~   ~   ~     |" +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|     ~   X   ~   ~   ~   O   ~   ~   ~   ~     |" + "     " + "|     ~   O   ~   ~   ~   2   2   O   ~   ~     |" +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|     ~   ~   ~   ~   O   4   4   4   4   ~     |" + "     " + "|     ~   ~   ~   ~   ~   ~   ~   ~   ~   ~     |" +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|     ~   O   ~   ~   ~   ~   ~   ~   ~   ~     |" + "     " + "|     ~   ~   ~   ~   ~   ~   O   ~   ~   ~     |" +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|     ~   ~   ~   ~   ~   ~   ~   ~   ~   ~     |" + "     " + "|     ~   ~   ~   ~   ~   ~   ~   ~   ~   ~     |" +
            //"\n|                                               |" + "     " + "|                                               |" +
            //"\n|_______________________________________________|" + "     " + "|_______________________________________________|");
        }
    }
}