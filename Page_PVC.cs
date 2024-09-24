using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using Library_GlobalMethods;
using Page_Menu;
using Page_Options;
using Page_Ranking;
using static System.Net.Mime.MediaTypeNames;

namespace Page_PVC {
    public class PVC {
        public static int page_ID = 0;
        public static bool isPage = false;
        /*
        public static bool isUser = false;   // Pętla poświęcona: wybieraniu użytkowanika
        public static bool isSetting = false;   // Pętla poświęcona: ustawianiu statków
        public static bool isBattle = false;   // Pętla poświęcona: bitwie
        public static bool isSummary = false;   // Pętla poświęcona: podsumowaniu
        */
        public static string part = "user";
        public static int pageLineLength = 80;
        public static int PVC_mode = 0;
        public static string PVC_filePath = "players_PVC.txt";
        public static string dataFormat = "#0#0#?#0#0#0%";
        public static string user = "";
        public static string[] buttons = new string[Ranking.modePlayersInfo[PVC_mode].Count];
        public static bool isEmpty = false;
        public static int currentButton = 0;
        public static List<ConsoleKey> usingKeys_ERROR = new List<ConsoleKey> { ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_USER_STANDARD = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_USER_TOP = new List<ConsoleKey> { ConsoleKey.S, ConsoleKey.DownArrow, ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_USER_DOWN = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.UpArrow, ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_USER_ONE = new List<ConsoleKey> { ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_USER_ZERO = new List<ConsoleKey> { ConsoleKey.C, ConsoleKey.P, ConsoleKey.Backspace };
        public void RenderPage() {   // Wyświetlenie strony PVC i zarazem panel kontrolny tej strony.
            bool isCorrect = false;
            isCorrect = Error.CheckRankingValid(isCorrect, PVC_mode);
            if (isCorrect) {
                ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolna niewłaściwa wartość.
                while (isPage == true) {
                    Console.Clear();
                    if (isEmpty) Error.EmptyMessage();   // Po utworzeniu użytkowania: isEmpty = false
                    Part.Content();
                    Part.Action(key);   // Miejsce na właściwą funkcję - SWITCH na odpowiedni "button".
                    key = Part.KeysControl(key);
                    Part.MoveCursor(key);
                }
            }
        }
        public class Error {
            public static bool CheckRankingValid(bool isCorrect, int mode) {
                if (Ranking.isFile[mode]) {
                    if (Ranking.isCorrectContent[mode]) {
                        isCorrect = true;
                    } else {
                        if (Ranking.errorCorrectContent[mode] == Ranking.errorEmpty) { isCorrect = true; isEmpty = true; }   // W przypadku pustego pliku danych, nie wyświetl błędu, a pozwól na tworzenie użytkowaników.
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
            public static void EmptyMessage() {
                Console.WriteLine("\nThere isn't any user to play this game mode. Create new user and play game.\n");
            }
        }
        public static void RenderTitle() {
            Console.WriteLine("BBBBBBB   BB    BB   BBBBBBB");
            Console.WriteLine("BB    BB  BB    BB  BB      ");
            Console.WriteLine("BB    BB  BB    BB  BB      ");
            Console.WriteLine("BBBBBBB   BB    BB  BB      ");
            Console.WriteLine("BB         BB  BB   BB      ");
            Console.WriteLine("BB          BBBB    BB      ");
            Console.WriteLine("BB           BB      BBBBBBB");
            GlobalMethod.Page.RenderDottedLine(pageLineLength);
            Console.WriteLine("PVC MODE: | Moving: arrows/[W][S] | Click: [ENTER] | Create player: [C] | Delete player: [P] | Back to menu: [BACKSPACE]\n");
        }
        public static void GetButtons(int mode) {
            buttons = new string[Ranking.modePlayersInfo[PVC_mode].Count];   // Ten trik rozwiązuje upierdliwy problem, który trzebaby było inaczej rozwiązać konwersją "List<string>" na "string[]".
            for (int i = 0; i < Ranking.modePlayersInfo[mode].Count; i++) {  // ^ Ale na szczęście zamiast tracić na to czas za każdym wywołaniem tej metody nadpisuję zmienną nową inicjacją zmiennej
                buttons[i] = Ranking.modePlayersInfo[mode][i][0];            // ^ "string[]" z nową aktualną długością, zaktualizowaną "statycznie" w klasie Ranking.
            }
        }
        public class Part {
            public static void Content() {
                switch (part) {
                    case "user":
                        RenderTitle();
                        GetButtons(PVC_mode);
                        Console.WriteLine("Select: [" + Ranking.modePlayersInfo[PVC_mode][currentButton][0] + "]");
                        GlobalMethod.Page.RenderButtons(buttons, currentButton);
                        GlobalMethod.Page.RenderDottedLine(pageLineLength);
                        break;
                    case "setting":
                        break;
                    case "battle":
                        break;
                    case "summary":
                        break;
                }
            }
            public static void Action(ConsoleKeyInfo key) {
                switch (part) {
                    case "user":
                        switch (key.Key) {
                            case ConsoleKey.Enter: User.SelectUser(); break;
                            case ConsoleKey.C: User.AddUser(); break;
                            case ConsoleKey.P: User.DeleteUser(); break;
                        }
                        break;
                    case "setting":
                        break;
                    case "battle":
                        break;
                    case "summary":
                        break;
                }
            }
            public static ConsoleKeyInfo KeysControl(ConsoleKeyInfo key) {
                switch (part) {
                    case "user":
                        if (buttons.Length == 0) key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_USER_ZERO);
                        else key = GlobalMethod.Page.SelectUsingKeys(currentButton, page_ID, key, buttons, usingKeys_USER_STANDARD, usingKeys_USER_TOP, usingKeys_USER_DOWN, usingKeys_USER_ONE);
                        break;
                    case "setting":
                        break;
                    case "battle":
                        break;
                    case "summary":
                        break;
                }
                return key;
            }
            public static void MoveCursor(ConsoleKeyInfo key) {
                switch (part) {
                    case "user": currentButton = GlobalMethod.Page.MoveButtons(buttons, currentButton, key); break;
                    case "setting":  break;
                    case "battle":  break;
                    case "summary":  break;
                }
            }
            public class User {
                public static void SelectUser() {
                    user = Ranking.modePlayersInfo[PVC_mode][currentButton][0];
                    Console.WriteLine("Chosed user: " + user);
                }
                public static void AddUser() {
                    Console.WriteLine("GUIDE: Write new user name -> [ENTER]");
                    string fileContent = GlobalMethod.StringPlayersInfo(Ranking.modePlayersInfo[PVC_mode]);
                    string name = "";
                    bool isBad = false;
                    bool isLoop = true;
                    while (isLoop) {
                        Console.CursorVisible = true;
                        Console.Write("\nName: ");
                        name = Console.ReadLine();

                        // Walidacja

                        if (isBad == false) {
                            isLoop = false;
                            Console.CursorVisible = false;
                            fileContent += (buttons.Length > 0) ? "*" + name + dataFormat : name + dataFormat;
                            File.WriteAllText(PVC_filePath, fileContent);
                            UpdateGameData();
                            PVC pvc = new PVC();
                            pvc.RenderPage();
                        }
                    }
                }
                public static void DeleteUser() {
                    Console.WriteLine("DeleteUser");
                }
                public static void UpdateGameData() {   // METODA DO RANKINGU: UpdateGameUsersData()
                    string uploadContent = File.ReadAllText(PVC_filePath);
                    List<List<string>> playersInfo = new List<List<string>>();
                    List<string> players = new List<string>(uploadContent.Split('*'));
                    for (int i = 0; i < players.Count; i++) {
                        playersInfo.Add(new List<string>(players[i].Split('#')));
                    }
                    Ranking.modePlayersInfo[PVC_mode] = playersInfo;
                }
                public static string ValidSigns() {
                    return "qwertyuioplkjhgfdsazxcvbnm" +
                           "QWERTYUIOPLKJHGFDSAZXCVBNM" +
                           "ąćęłńóśżź" +
                           "ĄĆĘŁŃÓŚŻŹ" +
                           "0123456789";
                }
            }
            public class Setting {

            }
            public class Battle {

            }
            public class Summary {

            }
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
