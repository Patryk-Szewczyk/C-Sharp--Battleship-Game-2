using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Library_GlobalMethods;
using Page_Options;
using Page_Ranking;

namespace Page_PVC {
    public class PVC {
        public static int page_ID = 0;
        public static bool isPage = false;
        public static string part = "user";
        public static int pageLineLength = 80;
        public static int PVC_mode = 0;
        public static string PVC_filePath = "players_PVC.txt";
        public static string userStr = "";
        public static int userInt = 0;
        public static string[] users = new string[Ranking.modePlayersInfo[PVC_mode].Count];
        public static int currentUser = 0;
        public static int positioningCursor = 0;
        public static string positUsingKeys_BOARD = "all";   // all, top, down, left, right
        public static string positUsingKeys_SHIPS = "all";   // all, left, right, one, zero
        public static bool isPositReset = false;
        public static bool isEnterPart = false;
        public static List<ConsoleKey> usingKeys_ERROR = new List<ConsoleKey> { ConsoleKey.Backspace };
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
            public static void EmptyMessage() {
                Console.WriteLine("There isn't any user to play this game mode. Create new user and play game.");
            }
        }
        public class Part {
            public static void Content() {
                switch (part) {
                    case "user":
                        User.RenderTitle();
                        User.GetUsers(PVC_mode);
                        Console.WriteLine(User.SelectUserInfo());   // Wyprubuj: buttons.Length == 0
                        if (users.Length > 0) GlobalMethod.Page.RenderButtons(users, currentUser);   // Options.options[Options.optDelete_PVC] != "EMPTY"
                        if (users.Length == 0) Error.EmptyMessage();   // Options.options[Options.optDelete_PVC] == "EMPTY"
                        GlobalMethod.Page.RenderDottedLine(pageLineLength);
                        break;
                    case "positioning":
                        Positioning.RenderTitle();
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
                    case "positioning":
                        switch (key.Key) {
                            case ConsoleKey.R: Positioning.Reset(); break;
                            case ConsoleKey.Enter: Positioning.Set(); break;
                        }
                        break;
                    case "battle":
                        break;
                    case "summary":
                        break;
                }
            }
            public static ConsoleKeyInfo KeysControl(ConsoleKeyInfo key) {
                List<ConsoleKey> USER_standard = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
                List<ConsoleKey> USER_top = new List<ConsoleKey> { ConsoleKey.S, ConsoleKey.DownArrow, ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
                List<ConsoleKey> USER_down = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.UpArrow, ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
                List<ConsoleKey> USER_one = new List<ConsoleKey> { ConsoleKey.C, ConsoleKey.P, ConsoleKey.Enter, ConsoleKey.Backspace };
                List<ConsoleKey> USER_zero = new List<ConsoleKey> { ConsoleKey.C, ConsoleKey.Backspace };
                //List<ConsoleKey> usingKeys_POSITIONING_ALL = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Q, ConsoleKey.E, ConsoleKey.R, ConsoleKey.Enter };
                List<ConsoleKey> POSIT_fusion = new List<ConsoleKey>();
                List<ConsoleKey> POSIT_BOARD_all = new List<ConsoleKey>() { ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow };
                List<ConsoleKey> POSIT_BOARD_top = new List<ConsoleKey>() { ConsoleKey.S, ConsoleKey.A, ConsoleKey.D, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow };
                List<ConsoleKey> POSIT_BOARD_down = new List<ConsoleKey>() { ConsoleKey.W, ConsoleKey.A, ConsoleKey.D, ConsoleKey.UpArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow };
                List<ConsoleKey> POSIT_BOARD_left = new List<ConsoleKey>() { ConsoleKey.W, ConsoleKey.S, ConsoleKey.D, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.RightArrow };
                List<ConsoleKey> POSIT_BOARD_right = new List<ConsoleKey>() { ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow };
                List<ConsoleKey> POSIT_BOARD_topLeft = new List<ConsoleKey>() { ConsoleKey.S, ConsoleKey.D, ConsoleKey.DownArrow, ConsoleKey.RightArrow };
                List<ConsoleKey> POSIT_BOARD_topRight = new List<ConsoleKey>() { ConsoleKey.S, ConsoleKey.A, ConsoleKey.DownArrow, ConsoleKey.LeftArrow };
                List<ConsoleKey> POSIT_BOARD_downLeft = new List<ConsoleKey>() { ConsoleKey.W, ConsoleKey.D, ConsoleKey.UpArrow, ConsoleKey.RightArrow };
                List<ConsoleKey> POSIT_BOARD_downRight = new List<ConsoleKey>() { ConsoleKey.W, ConsoleKey.A, ConsoleKey.UpArrow, ConsoleKey.LeftArrow };
                List<ConsoleKey> POSIT_SHIPS_all = new List<ConsoleKey>() { ConsoleKey.Q, ConsoleKey.E };
                List<ConsoleKey> POSIT_SHIPS_left = new List<ConsoleKey>() { ConsoleKey.E };
                List<ConsoleKey> POSIT_SHIPS_right = new List<ConsoleKey>() { ConsoleKey.Q };
                switch (part) {
                    case "user":
                        if (users.Length == 0) key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, USER_zero);
                        else key = GlobalMethod.Page.SelectUsingKeys(currentUser, page_ID, key, users, USER_standard, USER_top, USER_down, USER_one);
                        break;
                    case "positioning":
                        Positioning.DetermineCursorUsingKeys(positioningCursor);
                        POSIT_fusion.Clear();
                        if (isPositReset) POSIT_fusion.Add(ConsoleKey.R);
                        POSIT_fusion.Add(ConsoleKey.Enter);
                        switch (positUsingKeys_BOARD) {
                            case "all":
                                for (int i = 0; i < POSIT_BOARD_all.Count; i++) {
                                    POSIT_fusion.Add(POSIT_BOARD_all[i]);
                                }
                                break;
                            case "top":
                                for (int i = 0; i < POSIT_BOARD_top.Count; i++) {
                                    POSIT_fusion.Add(POSIT_BOARD_top[i]);
                                }
                                break;
                            case "down":
                                for (int i = 0; i < POSIT_BOARD_down.Count; i++) {
                                    POSIT_fusion.Add(POSIT_BOARD_down[i]);
                                }
                                break;
                            case "left":
                                for (int i = 0; i < POSIT_BOARD_left.Count; i++) {
                                    POSIT_fusion.Add(POSIT_BOARD_left[i]);
                                }
                                break;
                            case "right":
                                for (int i = 0; i < POSIT_BOARD_right.Count; i++) {
                                    POSIT_fusion.Add(POSIT_BOARD_right[i]);
                                }
                                break;
                            case "top-left":
                                for (int i = 0; i < POSIT_BOARD_topLeft.Count; i++) {
                                    POSIT_fusion.Add(POSIT_BOARD_topLeft[i]);
                                }
                                break;
                            case "top-right":
                                for (int i = 0; i < POSIT_BOARD_topRight.Count; i++) {
                                    POSIT_fusion.Add(POSIT_BOARD_topRight[i]);
                                }
                                break;
                            case "down-left":
                                for (int i = 0; i < POSIT_BOARD_downLeft.Count; i++) {
                                    POSIT_fusion.Add(POSIT_BOARD_downLeft[i]);
                                }
                                break;
                            case "down-right":
                                for (int i = 0; i < POSIT_BOARD_downRight.Count; i++) {
                                    POSIT_fusion.Add(POSIT_BOARD_downRight[i]);
                                }
                                break;
                        }
                        switch (positUsingKeys_SHIPS) {
                            case "all":
                                for (int i = 0; i < POSIT_SHIPS_all.Count; i++) {
                                    POSIT_fusion.Add(POSIT_SHIPS_all[i]);
                                }
                                break;
                            case "left":
                                for (int i = 0; i < POSIT_SHIPS_left.Count; i++) {
                                    POSIT_fusion.Add(POSIT_SHIPS_left[i]);
                                }
                                break;
                            case "right":
                                for (int i = 0; i < POSIT_SHIPS_right.Count; i++) {
                                    POSIT_fusion.Add(POSIT_SHIPS_right[i]);
                                }
                                break;
                        }
                        /*if (positUsingKeys_BOARD == "zero" && (positUsingKeys_SHIPS == "one" || positUsingKeys_SHIPS == "zero")) {
                            usingKeys_POSITIONING.Add(ConsoleKey.R);
                            usingKeys_POSITIONING.Add(ConsoleKey.Enter);
                        }*/
                        (bool, ConsoleKeyInfo) positCorr = GlobalMethod.Page.LoopCorrectKey_GameMode(isEnterPart, key, POSIT_fusion);
                        isEnterPart = positCorr.Item1;
                        key = positCorr.Item2;
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
                    case "user": currentUser = GlobalMethod.Page.MoveButtons(users, currentUser, key); break;
                    case "positioning": positioningCursor = Positioning.CursorNavigate(positioningCursor, key); break;
                    case "battle":  break;
                    case "summary":  break;
                }
            }
            public class User {
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
                public static void GetUsers(int mode) {
                    users = new string[Ranking.modePlayersInfo[PVC_mode].Count];   // Ten trik rozwiązuje upierdliwy problem, który trzebaby było inaczej rozwiązać konwersją "List<string>" na "string[]".
                    for (int i = 0; i < Ranking.modePlayersInfo[mode].Count; i++) {  // ^ Ale na szczęście zamiast tracić na to czas za każdym wywołaniem tej metody nadpisuję zmienną nową inicjacją zmiennej
                        users[i] = Ranking.modePlayersInfo[mode][i][0];            // ^ "string[]" z nową aktualną długością, zaktualizowaną "statycznie" w klasie Ranking.
                    }
                }
                public static string SelectUserInfo() {
                    string selected = "Selected: [";
                    if (users.Length > 0) selected += Ranking.modePlayersInfo[PVC_mode][currentUser][0];
                    selected += "]\n";
                    return selected;
                }
                public static void SelectUser() {
                    userStr = Ranking.modePlayersInfo[PVC_mode][currentUser][0];
                    userInt = currentUser;
                    part = "positioning";
                    isEnterPart = true;
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
                            UpdateOptions("addUser");
                            PVC pvc = new PVC();
                            pvc.RenderPage();
                        } else {
                            Console.WriteLine("\n" + validError);
                        }
                    }
                }
                public static void DeleteUser() {
                    Console.WriteLine("GUIDE: Selected [user] -> Write \"yes\" or \"no\" -> [ENTER]");
                    string name = Ranking.modePlayersInfo[PVC_mode][currentUser][0];
                    Console.WriteLine("\n\nDo you want delete [" + name + "] user?:\n");
                    bool isDelete = Valid.ValidDelete();
                    if (isDelete) {
                        Ranking.modePlayersInfo[PVC_mode].RemoveAt(currentUser);
                        File.WriteAllText(PVC_filePath, GlobalMethod.StringPlayersInfo(Ranking.modePlayersInfo[PVC_mode]));
                        if (currentUser > Ranking.modePlayersInfo[PVC_mode].Count - 1) currentUser--;
                        if (Ranking.modePlayersInfo[PVC_mode].Count == 0) currentUser = 0;   // Resetowanie wartości kursora na 0, inaczej jest -1 i wywala błąd, w sytuacji kiedy: usuniemy wszystkich użytkowników za pomocą [P] i utworzymy nowego użytkownika.
                        UpdateOptions("deleteUser");
                        PVC pvc = new PVC();
                        if (Ranking.modePlayersInfo[PVC_mode].Count == 0) Console.WriteLine(users.Length);
                        pvc.RenderPage();
                    }
                }
                public static void UpdateOptions(string activityContext) {
                    if (activityContext == "addUser") {
                        Options.options[Options.optDelete_PVC] = "CONTENT";
                        if (Options.options[Options.optReset_PVC] == "EMPTY") {
                            Options.options[Options.optReset_PVC] = "CLEAN";
                        }
                        Options.Upload.FillButtons();
                        string fileContent = "";
                        for (int i = 0; i < Options.buttonsAmount; i++) {
                            fileContent += Options.options[i];
                            if (i < Options.buttonsAmount - 1) fileContent += "*";
                        }
                        File.WriteAllText(Options.optionsPath, fileContent);
                        Options.isCorrectContent = true;   // Dzięki temu mogę zdjąć blokady na funkcję resetu danych użytkowników i usunięcia użytkowników, aby umożliwić te funkcje, gdyż wyłączam je w "else if" tej metody.
                        Ranking.isCorrectContent[PVC_mode] = true;   // Dodatkowo opcja resetu danych z sytuacją [CLEAN] jest obsłużona w opcjach, więc nie trzeba robić tutaj jej obsługi i psuć estetykę kodu - tzn chodzi o to, że jak mam [CLEAN] na resecie, to aby nie dało się ponownie czyścić danych użytkowników, bo już są wyczyszczone - to jest już obsłużone, więc nie musze tu (w tej metodzie) tego robić.
                    } else if (activityContext == "deleteUser") {
                        if (Ranking.modePlayersInfo[PVC_mode].Count == 0) {
                            Options.options[Options.optDelete_PVC] = "EMPTY";
                            Options.options[Options.optReset_PVC] = "EMPTY";
                        }
                        Options.Upload.FillButtons();
                        string fileContent = "";
                        for (int i = 0; i < Options.buttonsAmount; i++) {
                            fileContent += Options.options[i];
                            if (i < Options.buttonsAmount - 1) fileContent += "*";
                        }
                        File.WriteAllText(Options.optionsPath, fileContent);
                        Options.isCorrectContent = false;   // Bez dego nie będą wyświetlone komunikaty o pustym pliku w Opcjach i tym samym nie będzie blokady na funkcję resetu danych użytkowników i usunięcia użytkowników.
                        Options.errorCorrectContent = Ranking.errorEmpty;
                        Ranking.isCorrectContent[PVC_mode] = false;
                        Ranking.errorCorrectContent[PVC_mode] = Ranking.errorEmpty;
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
                }
            }
            public class Positioning {   // WAŻNE!!! NIE ZAPOMNIJ ZRESETOWAĆ ZMIENNEJ "positioningCursor" DO ZERA (0), PO ZAKOŃCZENIU OPERACJI W TEJ KLASIE! (ustawienie wszystkcich statków przez gracza)
                public static void RenderTitle() {
                    Console.WriteLine("User: [" + userStr + "] | [" + userInt + "]\n");
                    GlobalMethod.Page.RenderDottedLine(pageLineLength);
                    Console.WriteLine("Ships: " + "{ 2  2  2  3  3  4  5 }"); // KONCEPT
                    Console.WriteLine("                    " + "  ^");
                    Console.WriteLine("Position: " + positioningCursor);   // TYMCZASOWO
                }
                public static int CursorNavigate(int cursor, ConsoleKeyInfo key) {
                    if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow) {
                        if (cursor > 9) cursor -= 10;
                    } else if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow) {
                        if (cursor < 90) cursor += 10;
                    } else if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow) {
                        for (int i = 0, j = 10; i < 100; i=i+10, j=j+10) {
                            if (cursor > i && cursor < j) cursor -= 1;
                        }
                    } else if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow) {
                        for (int i = 0, j = 9; i < 100; i=i+10, j=j+10) {
                            if (cursor >= i && cursor < j) cursor += 1;
                        }
                    }
                    return cursor;
                    /*if (cursor > 0 && cursor < 10) cursor -= 1;  // Jeżeli kursor jest w przedziale {1,2,3,4,5,6,7,8,9}
                    if (cursor > 10 && cursor < 20) cursor -= 1;   // Jeżeli kursor jest w przedziale {11,12,13,14,15,16,17,18,19}
                    if (cursor > 20 && cursor < 30) cursor -= 1;   // Jeżeli kursor jest w przedziale {21,22,23,...}
                    if (cursor > 30 && cursor < 40) cursor -= 1;*/ // ...
                    /*if (cursor >= 0 && cursor < 9) cursor += 1;   // Jeżeli kursor jest w przedziale {0,1,2,3,4,5,6,7,8}
                    if (cursor >= 10 && cursor < 19) cursor += 1;   // Jeżeli kursor jest w przedziale {10,11,12,13,14,15,16,17,18}
                    if (cursor >= 20 && cursor < 29) cursor += 1;   // Jeżeli kursor jest w przedziale {20,21,22...}
                    if (cursor >= 30 && cursor < 39) cursor += 1;*/ // ...
                }
                public static void DetermineCursorUsingKeys(int cursor) {   // MIXED: top-left, top-right, down-left, top-right
                    int[] top = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    int[] down = new int[10] { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 };
                    int[] left = new int[10] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90 };
                    int[] right = new int[10] { 9, 19, 29, 39, 49, 59, 69, 79, 89, 99 };
                    int allCounter = 0;
                    for (int i = 0; i < top.Length; i++) {
                        //if (cursor)
                    }
                    if (allCounter == 4) positUsingKeys_BOARD = "all";
                }
                public static void Reset() {
                    positioningCursor = 0;
                }
                public static void Set() {
                    // Ustawienie wybranego statku
                }
            }
            public class Battle {

            }
            public class Summary {

            }
        }
    }
}
