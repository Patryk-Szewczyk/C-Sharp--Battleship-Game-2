using System;
using System.Collections.Generic;
using System.IO;
using Library_GlobalMethods;
using Page_Ranking;

namespace Page_PVC {
    public class PVC {
        public static int page_ID = 0;
        public static bool isPage = false;
        public static string part = "user";
        public static int pageLineLength = 80;
        public static int PVC_mode = 0;
        public static string PVC_filePath = "players_PVC.txt";
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
                Console.WriteLine("\nThere isn't any user to play this game mode. Create new user and play game.");
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
                        Console.WriteLine(User.SelectUserInfo());
                        GlobalMethod.Page.RenderButtons(buttons, currentButton);
                        if (isEmpty) Error.EmptyMessage();   // Po utworzeniu użytkowania: isEmpty = false
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
                public static string SelectUserInfo() {
                    string selected = "Selected: [";
                    if (Ranking.modePlayersInfo[PVC_mode].Count > 0) selected += Ranking.modePlayersInfo[PVC_mode][currentButton][0];
                    selected += "]";
                    return selected;
                }
                public static void SelectUser() {
                    user = Ranking.modePlayersInfo[PVC_mode][currentButton][0];
                    Console.WriteLine("Chosed user: [" + user + "]");
                    // part = "setting"
                }
                public static void AddUser() {
                    Console.WriteLine("GUIDE: Write new user name -> [ENTER]");
                    string name = "";
                    string validError = "";
                    bool isBad = false;
                    bool isLoop = true;
                    while (isLoop) {
                        isBad = false;
                        Console.CursorVisible = true;
                        Console.Write("\n\nName: ");
                        name = Console.ReadLine();
                        (string, bool, string) valid = Valid.ValidControlAdd(name);
                        name = valid.Item1;
                        isBad = valid.Item2;
                        validError = valid.Item3;
                        if (isBad == false) {
                            isLoop = false;
                            Console.CursorVisible = false;
                            Ranking.modePlayersInfo[PVC_mode].Add(new List<string>() { name, "0", "0", "?", "0", "0", "0%" } );
                            File.WriteAllText(PVC_filePath, GlobalMethod.StringPlayersInfo(Ranking.modePlayersInfo[PVC_mode]));
                            isEmpty = false;
                            PVC pvc = new PVC();
                            pvc.RenderPage();
                        } else {
                            Console.WriteLine("\n" + validError);
                        }
                    }
                }
                public static void DeleteUser() {
                    Console.WriteLine("GUIDE: Selected [user] -> Write \"yes\" or \"no\" -> [ENTER]");
                    string name = Ranking.modePlayersInfo[PVC_mode][currentButton][0];
                    Console.WriteLine("\n\nDo you want delete [" + name + "] user?:\n");
                    bool isDelete = Valid.ValidDelete();
                    if (isDelete) {
                        Ranking.modePlayersInfo[PVC_mode].RemoveAt(currentButton);
                        File.WriteAllText(PVC_filePath, GlobalMethod.StringPlayersInfo(Ranking.modePlayersInfo[PVC_mode]));
                        if (currentButton > Ranking.modePlayersInfo[PVC_mode].Count - 1) currentButton--;
                        if (Ranking.modePlayersInfo[PVC_mode].Count == 0) isEmpty = true;
                        PVC pvc = new PVC();
                        pvc.RenderPage();
                    }
                }
                public class Valid {   // Do globalnych metod, kiedy będziesz robił tryb PVP
                    public static bool ValidDelete() {
                        bool isDelete = false;
                        Console.CursorVisible = true;
                        string answer;
                        bool confirmLoop = true;
                        while (confirmLoop) {
                            answer = Console.ReadLine();
                            if (answer == "yes") {
                                isDelete = true;
                                confirmLoop = false;
                            } else if (answer == "no") {
                                confirmLoop = false;
                                Console.WriteLine("\n\nYou can now move to other options.");
                            } else {
                                Console.WriteLine("\n\nBad value. Write correct value.\n");
                            }
                        }
                        Console.CursorVisible = false;
                        return isDelete;
                    }
                    public static (string, bool, string) ValidControlAdd(string name) {
                        bool isBad = false;
                        string validError = "";
                        name = TrimNameOneSpace(name);
                        if (name == "" || name == " ") {
                            isBad = true;
                            validError = "This value is empty. Write correct value.";
                        } else {
                            if (isIdx0Number(name)) {
                                isBad = true;
                                validError = "Name can't begin on number sign.";
                            } else {
                                if (BadSign(name)) {
                                    isBad = true;
                                    validError = "This value can only contain word characters and signs from 0 to 9. Write correct value.";
                                }
                                else {
                                    if (SameName(name)) {
                                        isBad = true;
                                        validError = "This user exists. Create another user.";
                                    }
                                }
                            }
                        }
                        return (name, isBad, validError);
                    }
                    public static string TrimNameOneSpace(string name) {
                        string result = "";
                        int space = 0;
                        int start = 0;
                        int end = 0;
                        for (int i = 0; i < name.Length; i++) {
                            if (name[i] != ' ') { start = i; break; }
                        }
                        for (int i = name.Length - 1; i >= 0; i--) {
                            if (name[i] != ' ') { end = i; break; }
                        }
                        if (name.Length > 0) {
                            for (int i = start; i <= end; i++) {
                                if (name[i] == ' ' && space < 2) { space++; result += name[i]; } else space++;
                                if (name[i] != ' ') { space = 1; result += name[i]; }
                            }
                        }
                        return result;
                    }
                    public static bool isIdx0Number(string name) {
                        string numbers = "0123456789";
                        bool isNumber = false;
                        for (int i = 0; i < numbers.Length; i++) {
                            if (name[0] == numbers[i]) { isNumber = true; break; }
                        }
                        return isNumber;
                    }
                    public static bool BadSign(string name) {
                        bool isBadSign = false;
                        string signs = "qwertyuioplkjhgfdsazxcvbnm" +
                                       "QWERTYUIOPLKJHGFDSAZXCVBNM" +
                                       "ąćęłńóśżź" +
                                       "ĄĆĘŁŃÓŚŻŹ" +
                                       "0123456789" +
                                       " ";
                        int counter = 0;
                        for (int i = 0; i < name.Length; i++) {
                            counter = 0;
                            for (int j = 0; j < signs.Length; j++) {
                                if (name[i] != signs[j]) {
                                    counter++;
                                }
                            }
                            if (counter != signs.Length - 1) { isBadSign = true; break; }
                        }
                        return isBadSign;
                    }
                    public static bool SameName(string name) {
                        bool isSameName = false;
                        for (int i = 0; i < Ranking.modePlayersInfo[PVC_mode].Count; i++) {
                            if (name == Ranking.modePlayersInfo[PVC_mode][i][0]) isSameName = true;
                        }
                        return isSameName;
                    }
                    public static bool NotFound(string name) {
                        bool isNotFound = true;
                        for (int i = 0; i < Ranking.modePlayersInfo[PVC_mode].Count; i++) {
                            if (name == Ranking.modePlayersInfo[PVC_mode][i][0]) isNotFound = false;
                        }
                        return isNotFound;
                    }
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
