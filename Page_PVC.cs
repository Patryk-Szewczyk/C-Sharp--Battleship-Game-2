using System;
using System.Collections.Generic;
using System.IO;
using Library_GlobalMethods;
using Page_Menu;
using Page_Options;
using Page_Ranking;

namespace Page_PVC {
    public class PVC {
        public static int page_ID = 0;
        public static bool isPage = false;
        public static bool isUserSelect = false;   // Pętla poświęcona: wybieraniu użytkowanika
        public static bool isShipPositing = false;   // Pętla poświęcona: ustawianiu statków
        public static bool isBattle = false;   // Pętla poświęcona: bitwie
        public static bool isSubmit = false;   // Pętla poświęcona: podsumowaniu
        public static int pageLineLength = 80;
        public static int PVC_mode = 0;
        public static string[] buttons = new string[Ranking.modePlayersInfo[PVC_mode].Count];
        public static int currentButton = 0;
        public static List<ConsoleKey> usingKeys_STANDARD = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_TOP = new List<ConsoleKey> { ConsoleKey.S, ConsoleKey.DownArrow, ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_DOWN = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.UpArrow, ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_ONE = new List<ConsoleKey> { ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_ZERO = new List<ConsoleKey> { ConsoleKey.C, ConsoleKey.P, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_ERROR = new List<ConsoleKey> { ConsoleKey.Backspace };
        public void RenderPage() {   // Wyświetlenie strony PVC i zarazem panel kontrolny tej strony.
            bool isCorrect = false;
            isCorrect = Error.CheckRankingValid(isCorrect, PVC_mode);
            if (isCorrect) {
                ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolna niewłaściwa wartość.
                while (isPage == true) {
                    Console.Clear();
                    RenderTitle();
                    GetButtons(PVC_mode);
                    GlobalMethod.Page.RenderButtons(buttons, currentButton);
                    GlobalMethod.Page.RenderDottedLine(pageLineLength);
                    //ShowInstruction(currentButton);   // Miejsce na właściwą funkcję - SWITCH na odpowiedni "button".
                    if (buttons.Length == 0) key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_ZERO);
                    else key = GlobalMethod.Page.SelectUsingKeys(currentButton, page_ID, key, buttons, usingKeys_STANDARD, usingKeys_TOP, usingKeys_DOWN, usingKeys_ONE);   // Pętla ta uniemożliwia przeładowanie strony kiedy kliknie się niewłaściwy klawisz.
                    currentButton = GlobalMethod.Page.MoveButtons(buttons, currentButton, key);   // Poruszanie się po przyciskach (obliczenia).
                }
            }
        }
        public class Error {
            public static bool CheckRankingValid(bool isCorrect, int mode) {
                if (Ranking.isFile[mode]) {
                    if (Ranking.isCorrectContent[mode]) {
                        isCorrect = true;
                    } else {
                        if (Ranking.errorCorrectContent[mode] == Ranking.errorEmpty) isCorrect = true;   // W przypadku pustego pliku danych, nie wyświetl błędu, a pozwól na tworzenie użytkowaników.
                        else isError(Ranking.errorCorrectContent[mode]);
                    }
                } else {
                    isError(Ranking.errorFile[mode]);
                }
                return isCorrect;
            }
            public static void isError(string error) {
                Console.Clear();
                Console.WriteLine(error);
                Console.WriteLine("\nFor this reason, you can't play in PVC mode." +
                    "\n\n\nClick [BACKSPACE] to back to menu.");
                ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);
                GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_ERROR);
            }
        }
        public void RenderTitle() {
            Console.WriteLine("BBBBBBB   BB    BB   BBBBBBB");
            Console.WriteLine("BB    BB  BB    BB  BB      ");
            Console.WriteLine("BB    BB  BB    BB  BB      ");
            Console.WriteLine("BBBBBBB   BB    BB  BB      ");
            Console.WriteLine("BB         BB  BB   BB      ");
            Console.WriteLine("BB          BBBB    BB      ");
            Console.WriteLine("BB           BB      BBBBBBB");
            GlobalMethod.Page.RenderDottedLine(pageLineLength);
            Console.WriteLine("PVC MODE: | Moving: arrows/[W][S] | Click = [ENTER] | Create player: [C] | Delete player: [P] | Back to menu: [BACKSPACE]\n");
        }
        public static void GetButtons(int mode) {
            for (int i = 0; i < Ranking.modePlayersInfo[mode].Count; i++) {
                buttons[i] = Ranking.modePlayersInfo[mode][i][0];
            }
        }
        public void addUser(List<List<string>> playersDetails_PARTS) {
            char[] valid_CHAR_AR = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm',
                                  'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M',
                                  'ą', 'ć', 'ę', 'ł', 'ń', 'ó', 'ś', 'ż', 'ź',
                                  'Ą', 'Ć', 'Ę', 'Ł', 'Ń', 'Ó', 'Ś', 'Ż', 'Ź',
                                  '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        }
        public void SetShips_PLAYER(System.ConsoleKeyInfo key, string userName) {
            Console.Clear();
            // Walidacja poruszania się po planszy:
            Console.WriteLine("Player: [" + userName + "]");
            int setVal = SetShipsPVC.CursorNavigate(key);
            Console.WriteLine("Ship initial coordinate: " + setVal);
        }
        public class SetShipsPVC {
            public static int cursor = 0;
            public static int CursorNavigate(System.ConsoleKeyInfo key) {
                System.ConsoleKey[] direction_1 = new ConsoleKey[4] { System.ConsoleKey.UpArrow, System.ConsoleKey.DownArrow, System.ConsoleKey.RightArrow, System.ConsoleKey.LeftArrow };
                System.ConsoleKey[] direction_2 = new ConsoleKey[4] { System.ConsoleKey.W, System.ConsoleKey.S, System.ConsoleKey.D, System.ConsoleKey.A };
                int[] add = new int[4] { -10, 10, 1, -1 };
                bool[] stop = new bool[4] { false, false, false, false };
                int[,] valNum = new int[4, 10] {
                    /*Up:*/    { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                    /*Down:*/  { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 },
                    /*Right:*/ { 9, 19, 29, 39, 49, 59, 69, 79, 89, 99 },
                    /*Left:*/  { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90 }
                };
                for (int i = 0; i < valNum.GetLength(0); i++) {
                    stop[i] = false;
                    for (int j = 0; j < valNum.GetLength(1); j++) {
                        if (cursor == valNum[i,j]) stop[i] = true;
                    }
                    if (!stop[i]) {
                        if (key.Key == direction_1[i] || key.Key == direction_2[i]) {
                            cursor += add[i];
                        }
                    }
                }
                return cursor;
            }
        }
    }
}
