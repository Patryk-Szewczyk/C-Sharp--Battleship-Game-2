using System;
using Page_Menu;
using Library_GlobalMethods;
using System.Collections.Generic;

namespace Page_Instructions {
    public class Instructions {
        public static int page_ID = 1;
        public static bool isPage = false;
        public static int pageLineLength = 97;
        public static string[] buttons = { "Game", "Ships", "Board"};
        public static int currentButton = 0;   // Zawsze pierwszy, bo chc� mie� kursor na g�rze!
        public static List<ConsoleKey> usingKeys = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace };
        public void RenderPage() {
            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolna niew�a�ciwa warto��.
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.Page.RenderButtons(buttons, currentButton);
                GlobalMethod.Page.RenderDottedLine(pageLineLength);
                ShowInstruction(currentButton);
                key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys);   // P�tla ta uniemo�liwia prze�adowanie strony kiedy kliknie si� niew�a�ciwy klawisz.
                currentButton = GlobalMethod.Page.MoveButtons(buttons, currentButton, key);   // Poruszanie si� po przyciskach (obliczenia).
            }
        }
        public static void RenderTitle() {
            Console.WriteLine("BB  BBBB  BB   BBBBBBB  BBBBBBBB  BBBBBBB   BB    BB   BBBBBBB  BBBBBBBB  BB   BBBBBB   BBBB  BB");
            Console.WriteLine("BB  BB BB BB  BB           BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
            Console.WriteLine("BB  BB BB BB  BB           BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
            Console.WriteLine("BB  BB BB BB   BBBBBB      BB     BBBBBBB   BB    BB  BB           BB     BB  BB    BB  BB BB BB");
            Console.WriteLine("BB  BB BB BB        BB     BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
            Console.WriteLine("BB  BB BB BB        BB     BB     BB    BB  BB    BB  BB           BB     BB  BB    BB  BB BB BB");
            Console.WriteLine("BB  BB  BBBB  BBBBBBB      BB     BB    BB   BBBBBB    BBBBBBB     BB     BB   BBBBBB   BB  BBBB");
            GlobalMethod.Page.RenderDottedLine(pageLineLength);
            Console.WriteLine("INSTRUCTION: | Moving: arrows/[W][S] | Back to menu: [Backspace]\n");
        }
        public static void ShowInstruction(int option) {   // Dlaczego "internal"? Poniewa� chc� ograniczy� wykonywanie tej metody i innych w tej klasie do wy��cznie tego "namespace" (przestrze� nazw) w kt�rym si� znajduje ("Page_Instructions"), aby nie wykona� jej przypadkiem w innych namespacach z plik�w do��czonych za pomoc� s�owa kluczowego "using". Mo�na si� sprzecza�, �e jest to niepotrzebne, gdy� klasa ma ustawiony modyfikator dost�pu "internal", ale dla ostro�no�ci i czytelno�ci kodu lepiej trzyma� si� estetyki.
            switch (option) {
                case 0: Guide.Game(); break;
                case 1: Guide.Ships(); break;
                case 2: Guide.Board(); break;
            }
        }
        public class Guide {
            public static void Game() {
                Console.WriteLine("Game instruction:" +
                    "\n1. Each player has as many ships as are set for a specific game mode (from options)." +
                    "\n2. Each player must set our every ship on our board." +
                    "\n3. Each ship has a certain length and direction." +
                    "\n4. Ships can be placed on board in two types of positions: horizontal and vertical." +
                    "\n5. The player boards have dimensions of 10 x 10 fields." +
                    "\n6. When placing ships, ships canNOT leave the board and canNOT overlap each other." +
                    "\n7. After placing the ships on the boards, you can start the game." +
                    "\n8. At the beginning, all squares of the board are marked with the sign \"~\" on the board." +
                    "\n9. A hit ship is marked with a series of number from 1 to 9 according to according to the " + 
                    "\n   established list of ships in the options for specific game mode." +
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
                    "\n18. After game players can play game again or back to menu.");
            }
            public static void Ships() {
                Console.WriteLine("You have 5 ships, about these length:" +
                    "\n1 - patrol boat" +
                    "\n22 - frigate" +
                    "\n333 - submarine" +
                    "\n4444 - destroyer" +
                    "\n55555 - aircraft carrier");
                Console.WriteLine("\nDestruction all ships indicate win these player who do it the opposite player.");
            }
            public static void Board() { // NA PODSTAWIE PONI�SZEGO ZAPISU ZAIMPLEMENTUJ DWIE P�TLE, KT�RE B�D� ODPOWIEDZIALNE ZA WK�ADANIE ODPOWIEDNICH DANYCH Z IF'�W DO METODY "GlobalMethod.Color()"!!!
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
                GlobalMethod.Color(" O  ", ConsoleColor.DarkYellow);  // Ka�dy tego typu element b�dzie mia� dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
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
                GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka�dy tego typu element b�dzie mia� dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
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
                GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka�dy tego typu element b�dzie mia� dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
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
                GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka�dy tego typu element b�dzie mia� dwie wersje. Pierwsza: " O  ". Druga: "O", gdzie przed jest "{", a po "} ".
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
                GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka�dy tego typu element b�dzie mia� dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
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
                GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka�dy tego typu element b�dzie mia� dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
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
                GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka�dy tego typu element b�dzie mia� dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
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
                GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka�dy tego typu element b�dzie mia� dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
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
                GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka�dy tego typu element b�dzie mia� dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
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
                GlobalMethod.Color(" ~  ", ConsoleColor.Blue);  // Ka�dy tego typu element b�dzie mia� dwie wersje. Pierwsza: " O  ". Druga: "{O} ".
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
            }
        }
    }
}

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
