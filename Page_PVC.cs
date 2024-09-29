using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Library_GlobalMethods;
using Page_Options;
using Page_Ranking;
using static Library_GlobalMethods.GlobalMethod;

namespace Page_PVC {
    public class PVC {
        public static int page_ID = 0;
        public static bool isPage = false;
        public static string part = "user";
        public static int pageLineLength = 80;
        public static int PVC_mode = 0;
        public static string PVC_filePath = "players_PVC.txt";
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - Przenieś później do klasy "Part":
        public static string userStr = "";
        public static int userInt = 0;
        public static string[] users = new string[Ranking.modePlayersInfo[PVC_mode].Count];
        public static int currentUser = 0;
        public static int positBoardCursor = 0;
        public static int positShipsCursor = 0;
        public static string coorA0 = "A0";
        public static string positDirection = "horizontal";
        public static string positUsingKeys_BOARD = "";   // all, top, down, left, right
        public static string positUsingKeys_SHIPS = "";   // all, left, right, one, zero
        public static bool isPositReset = false;
        public static List<string> positShips = new List<string>();
        public static List<int> positBoard = new List<int>();
        public static List<int> positShipCoor = new List<int>();
        public static List<List<int>> userShipsCoor = new List<List<int>>();
        public static bool isEnterPart = false;
        public static int counterEnter = 0;
        public static List<ConsoleKey> usingKeys_ERROR = new List<ConsoleKey> { ConsoleKey.Backspace };
        // - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void RenderPage() {   // Wyświetlenie strony PVC i zarazem panel kontrolny tej strony.
            bool isCorrect = false;
            isCorrect = Error.CheckRankingValid(isCorrect, PVC_mode);

            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);
            if (isCorrect) {   // Dowolna niewłaściwa wartość.
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
                        Positioning.User.RenderTitle();
                        break;
                    case "battle":
                        break;
                    case "summary":
                        break;
                }
            }
            public static void Action(ConsoleKeyInfo key) {
                //Console.WriteLine(key.Key);
                //Console.ReadKey();
                switch (part) {
                    case "user":
                        switch (key.Key) {
                            case ConsoleKey.Enter: User.SelectUser(); break;
                            case ConsoleKey.C: User.AddUser(); break;
                            case ConsoleKey.P: User.DeleteUser(); break;
                        }
                        break;
                    case "positioning":
                        if (positShips.Count == 0) {   // Wyświetl statki:
                            Positioning.User.ShowUserShips();
                        }
                        switch (key.Key) {
                            case ConsoleKey.P: Positioning.User.Reset(); break;
                            case ConsoleKey.C: Positioning.User.ChangeDirection(); break;
                            case ConsoleKey.Enter: Positioning.User.SetShip(); break;
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
                        Positioning.User.DetermineBoardUsingKeys();
                        Positioning.User.DetermineShipsUsingKeys();
                        POSIT_fusion.Clear();
                        if (isPositReset) POSIT_fusion.Add(ConsoleKey.P);
                        POSIT_fusion.Add(ConsoleKey.Enter);
                        //POSIT_fusion.Add(ConsoleKey.Backspace);   // TYLKO DLA TESTÓW!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        if (positShips.Count > 0) {
                            POSIT_fusion.Add(ConsoleKey.C);
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
                        }
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
                    case "positioning": Positioning.User.CursorBoard(key); Positioning.User.CursorShips(key); break;
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
                    //Console.WriteLine(++counterEnter);
                    //Console.ReadLine();
                    userStr = Ranking.modePlayersInfo[PVC_mode][currentUser][0];
                    userInt = currentUser;
                    part = "positioning";
                    isEnterPart = true;
                    Positioning.User.Reset();   // Ustawienie listy dynamicznej statków z opcji jeden raz, kolejne, tylko przy resecie.
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
                public class User {
                    public static void RenderTitle() {
                        Console.WriteLine("User: [" + userStr + "] | [" + userInt + "]\n");
                        Console.WriteLine("POSITIONING: | Choose ship: [Q][E] -> [ENTER] | Change direction: [C] | Set ship: [W][S][A][D]/arrows -> [ENTER] | Reset: [P]");
                        GlobalMethod.Page.RenderDottedLine(pageLineLength);
                        Console.WriteLine("Ships: " + "{" + RenderShipsInfo() + " }"); // KONCEPT
                        if (positShips.Count > 0) Console.WriteLine("       " + RenderShipSpace() + "  ^");
                        if (positShips.Count == 0) Console.WriteLine();
                        Console.WriteLine("Direction: " + positDirection + " | Position: " + positBoardCursor + " / " + ConvertTo_A0(positBoardCursor) + "\n\n");   // TYMCZASOWO
                        //ShowUserShips();   // do testów!
                        Board.RenderBoard(positBoardCursor, userShipsCoor);
                    }
                    public static string RenderShipsInfo() {
                        string result = "";
                        for (int i = 0; i < positShips.Count; i++) {
                            result += " " + positShips[i];
                        }
                        return result;
                    }
                    public static void CursorBoard(ConsoleKeyInfo key) {
                        int cursor = positBoardCursor;
                        if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow) {
                            if (cursor > 9) cursor -= 10;
                        } else if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow) {
                            if (cursor < 90) cursor += 10;
                        } else if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow) {
                            for (int i = 0, j = 10; i < 100; i = i + 10, j = j + 10) {
                                if (cursor > i && cursor < j) cursor -= 1;
                            }
                        } else if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow) {
                            for (int i = 0, j = 9; i < 100; i = i + 10, j = j + 10) {
                                if (cursor >= i && cursor < j) cursor += 1;
                            }
                        }
                        positBoardCursor = cursor;
                        /*if (cursor > 0 && cursor < 10) cursor -= 1;  // Jeżeli kursor jest w przedziale {1,2,3,4,5,6,7,8,9}
                        if (cursor > 10 && cursor < 20) cursor -= 1;   // Jeżeli kursor jest w przedziale {11,12,13,14,15,16,17,18,19}
                        if (cursor > 20 && cursor < 30) cursor -= 1;   // Jeżeli kursor jest w przedziale {21,22,23,...}
                        if (cursor > 30 && cursor < 40) cursor -= 1;*/ // ...
                        /*if (cursor >= 0 && cursor < 9) cursor += 1;   // Jeżeli kursor jest w przedziale {0,1,2,3,4,5,6,7,8}
                        if (cursor >= 10 && cursor < 19) cursor += 1;   // Jeżeli kursor jest w przedziale {10,11,12,13,14,15,16,17,18}
                        if (cursor >= 20 && cursor < 29) cursor += 1;   // Jeżeli kursor jest w przedziale {20,21,22...}
                        if (cursor >= 30 && cursor < 39) cursor += 1;*/ // ...
                    }
                    public static string ConvertTo_A0(int coordinate) {
                        string corrForm = Convert.ToString(coordinate);
                        switch (corrForm[0]) {
                            case '0': corrForm = "A" + corrForm[0]; break;
                            case '1': corrForm = "A" + corrForm[0]; break;
                            case '2': corrForm = "A" + corrForm[0]; break;
                            case '3': corrForm = "A" + corrForm[0]; break;
                            case '4': corrForm = "A" + corrForm[0]; break;
                            case '5': corrForm = "A" + corrForm[0]; break;
                            case '6': corrForm = "A" + corrForm[0]; break;
                            case '7': corrForm = "A" + corrForm[0]; break;
                            case '8': corrForm = "A" + corrForm[0]; break;
                            case '9': corrForm = "A" + corrForm[0]; break;
                        }
                        if (coordinate > 9) {   // "34"   [0] = 3 | [1] = 4
                            corrForm = Convert.ToString(coordinate);
                            switch (corrForm[0]) {
                                case '1': corrForm = "B" + corrForm[1]; break;
                                case '2': corrForm = "C" + corrForm[1]; break;
                                case '3': corrForm = "D" + corrForm[1]; break;
                                case '4': corrForm = "E" + corrForm[1]; break;
                                case '5': corrForm = "F" + corrForm[1]; break;
                                case '6': corrForm = "G" + corrForm[1]; break;
                                case '7': corrForm = "H" + corrForm[1]; break;
                                case '8': corrForm = "I" + corrForm[1]; break;
                                case '9': corrForm = "J" + corrForm[1]; break;
                            }
                        }
                        return corrForm;
                    }
                    public static void CursorShips(ConsoleKeyInfo key) {
                        int cursor = positShipsCursor;
                        if (key.Key == ConsoleKey.Q) {
                            if (cursor > 0) cursor -= 1;
                        } else if (key.Key == ConsoleKey.E) {
                            if (cursor < positShips.Count - 1) cursor += 1;
                        }
                        positShipsCursor = cursor;
                    }
                    public static void DetermineBoardUsingKeys() {   // MIXED: top-left, top-right, down-left, top-right
                        int cursor = positBoardCursor;
                        int[] top = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                        int[] down = new int[10] { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 };
                        int[] left = new int[10] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90 };
                        int[] right = new int[10] { 9, 19, 29, 39, 49, 59, 69, 79, 89, 99 };
                        int all = 0;
                        int topLeft = 0;
                        int topRight = 0;
                        int downLeft = 0;
                        int downRight = 0;
                        all++;
                        for (int i = 0; i < top.Length; i++) {
                            if (cursor == top[i]) {
                                positUsingKeys_BOARD = "top";
                                topLeft++;
                                topRight++;
                                all--;
                                break;
                            }
                        }
                        all++;
                        for (int i = 0; i < down.Length; i++) {
                            if (cursor == down[i]) {
                                positUsingKeys_BOARD = "down";
                                downLeft++;
                                downRight++;
                                all--;
                                break;
                            }
                        }
                        all++;
                        for (int i = 0; i < left.Length; i++) {
                            if (cursor == left[i]) {
                                positUsingKeys_BOARD = "left";
                                topLeft++;
                                downLeft++;
                                all--;
                                break;
                            }
                        }
                        all++;
                        for (int i = 0; i < right.Length; i++) {
                            if (cursor == right[i]) {
                                positUsingKeys_BOARD = "right";
                                topRight++;
                                downRight++;
                                all--;
                                break;
                            }
                        }
                        if (topLeft == 2) positUsingKeys_BOARD = "top-left";
                        if (topRight == 2) positUsingKeys_BOARD = "top-right";
                        if (downLeft == 2) positUsingKeys_BOARD = "down-left";
                        if (downRight == 2) positUsingKeys_BOARD = "down-right";
                        if (all == 4) positUsingKeys_BOARD = "all";
                    }
                    public static void DetermineShipsUsingKeys() {   // NAPRAW TO!
                        int cursor = positShipsCursor;
                        int ships = positShips.Count - 1;
                        if (cursor > 0 && cursor < ships - 1) positUsingKeys_SHIPS = "all";
                        if (cursor == 0) positUsingKeys_SHIPS = "left";
                        if (cursor == ships) positUsingKeys_SHIPS = "right";
                        if (ships <= 1) positUsingKeys_SHIPS = "";
                    }
                    public static string RenderShipSpace() {
                        string space = "";
                        for (int i = 0; i < positShipsCursor; i++) {
                            space += "  ";
                        }
                        return space;
                    }
                    public static void Reset() {   // RESET ma znajdować się jeszcze po bitwie, kiedy przejdzie się do podsumowania.  // Zrób potem z tego dwie metody!
                        // - - - - - - - - - - - - Statki:
                        positShipsCursor = 0;
                        positShips.Clear();
                        string[] ships = Options.options[Options.optShips].Split(',');   // TUTAJ!!!!!!!!!   Trzeba pobrać aktualne dane z pliku
                        List<string> result = new List<string>();
                        for (int i = 0; i < ships.Length; i++) {
                            result.Add(ships[i]);
                        }
                        positShips = result;
                        // - - - - - - - - - - - Plansza:
                        coorA0 = "A0";
                        positBoardCursor = 0;
                        positBoard.Clear();
                        for (int i = 0; i < 100; i++) {
                            positBoard.Add(i);
                        }
                        positShipCoor.Clear();
                        userShipsCoor.Clear();
                        // - - - - - - - - - - - 
                        if (isPositReset) {
                            isPositReset = false;
                            ReloadPage();
                        }
                    }
                    public static void ChangeDirection() {
                        positDirection = (positDirection == "horizontal") ? "vertical" : "horizontal";
                        ReloadPage();
                    }
                    public static void SetShip() {   // Ustawianie wybranego statku
                        if (counterEnter < 3) counterEnter++;   // Dlaczego w ogóle coś takiego jest? Wyjasnienie w pliku txt. pod tytułem "counterEnter - Dlaczego". 
                        if (counterEnter > 1) {   // Bez blokady na zmienną "counterEnter" użytkownik mógłby tyle razy go nacisnąć w jednej sesji uruchomienia programu, że zakres wartości typu "int" wykraczałby poza zasięg typu "int".
                            if (positShips.Count > 0) {
                                List<int> shipCoor = new List<int>();
                                bool isBad = false;
                                string error = "";
                                (List<int>, bool, string) valid = SetShipValidate();
                                shipCoor = valid.Item1;
                                isBad = valid.Item2;
                                error = valid.Item3;
                                if (isBad) {
                                    Console.WriteLine("\n" + error + "\n");
                                } else {
                                    for (int i = 0; i < shipCoor.Count; i++) {   // Właściwe usuwanie pól zajętych przez dany statek:
                                        positBoard.RemoveAt(GlobalMethod.SearchRemoveAt(positBoard, shipCoor[i]));
                                    }
                                    userShipsCoor.Add(shipCoor);
                                    positShips.RemoveAt(positShipsCursor);   // Usuwanie ustawionego statku
                                    // DO TESTÓW: - - - - - - - - - - - - - - - - - -
                                    /*Console.WriteLine("\nWspółrzędne statku:");
                                    for (int i = 0; i < shipCoor.Count; i++) {
                                        Console.Write(shipCoor[i] + ", ");
                                    }
                                    Console.WriteLine("\nPo kasacji:");
                                    for (int i = 0; i < positBoard.Count; i++) {
                                        Console.Write(positBoard[i] + ", ");
                                    }
                                    // - - - - - - - - - - - - - - - - - - - - - - -
                                    Console.ReadKey();*/
                                    if (positShipsCursor > positShips.Count - 1) positShipsCursor -= 1;
                                    isPositReset = true;
                                    ReloadPage();
                                }
                            } else if (positShips.Count == 0) {
                                // 1. Przejdź do losowania statków dla komputera i zablokuj [ENTER]!!
                                // 2. Wówczas pojawia się strona na której jest informacja o ładowaniu statków dla komputera z napisem "Loading..."
                                // 3. Po wyslosowaniu statków program przechodzi dalej, czyści tą stronę i pojawia się strona bitwy.
                            }
                        }
                    }
                    public static (List<int>, bool, string) SetShipValidate() {   // positBoard | shipCoorList
                        int ship = int.Parse(positShips[positShipsCursor]);
                        string positVal = Convert.ToString(positBoardCursor);
                        List<int> board = positBoard;
                        int init = positBoardCursor;
                        string dirVal = positDirection;
                        int shipDist = 0, limit = 0;
                        List<int> shipCoor = new List<int>();
                        bool isBad = false;
                        string error = "";
                        int rem = GlobalMethod.SearchRemoveAt(board, init);
                        if (rem == -1) {
                            isBad = true;
                            error = "Ship can't overlap another ship.";
                            shipCoor.Clear();
                        } else {
                            shipCoor.Add(init);
                            if (dirVal == "horizontal") {
                                shipDist = init + (ship - 1);
                                limit = (Convert.ToString(init).Length == 1) ? 9 : 9 + (10 * (int)char.GetNumericValue(Convert.ToChar(Convert.ToString(init)[0])));
                                if (shipDist > limit) {
                                    isBad = true;
                                    error = "The ship can't leave the board. | " + shipDist + " > " + limit + " = " + (shipDist > limit);
                                    shipCoor.Clear();
                                }
                                if (!isBad) {   // Czy  statek nakłada się na inny ustawiony na planszy statek?
                                    if (ship > 1) {
                                        for (int i = 1; i < ship; i++) {
                                            rem = GlobalMethod.SearchRemoveAt(board, init + i);
                                            if (rem == -1) {   // Jeżeli nie ma kolizji statku
                                                isBad = true;
                                                error = "Ship can't overlap another ship.";
                                                shipCoor.Clear();
                                                break;
                                            } else {
                                                shipCoor.Add(init + i);
                                            }
                                        }
                                    }
                                }
                            } else if (dirVal == "vertical") {
                                shipDist = init + ((ship * 10) - 10);
                                limit = (Convert.ToString(init).Length == 1) ? 90 : 90 + (1 * (int)char.GetNumericValue(Convert.ToChar(Convert.ToString(init)[1])));
                                if (shipDist > limit) {
                                    isBad = true;
                                    error = "The ship can't leave the board.";
                                    shipCoor.Clear();
                                }
                                if (!isBad) {
                                    if (ship > 1) {
                                        for (int i = 10; i < ship * 10; i=i+10) {
                                            rem = GlobalMethod.SearchRemoveAt(board, init + i);
                                            if (rem == -1) {
                                                isBad = true;
                                                error = "Ship can't overlap another ship.";
                                                shipCoor.Clear();
                                                break;
                                            } else {
                                                shipCoor.Add(init + i);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        return (shipCoor, isBad, error);
                    }
                    public static void ShowUserShips() {
                        Console.WriteLine();
                        for (int i = 0; i < userShipsCoor.Count; i++) {
                            Console.Write((i + 1) + ". { ");
                            for (int j = 0; j < userShipsCoor[i].Count; j++) {
                                if (j < userShipsCoor[i].Count - 1) Console.Write(ConvertTo_A0(userShipsCoor[i][j]) + ", ");
                                else Console.Write(ConvertTo_A0(userShipsCoor[i][j]));
                            }
                            Console.Write(" }");
                            Console.WriteLine();
                        }
                    }
                    public static void ReloadPage() {
                        PVC pvc = new PVC();
                        pvc.RenderPage();
                    }
                    public class Board {
                        public static void RenderBoard(int cursor, List<List<int>> ships) {
                            string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                            int cursorCnt = -1;
                            bool isCursor = false;
                            int shipCnt = -1;
                            bool isShip = false;
                            string sign = "";
                            Top();
                            SpaceVertical();
                            for (int i = 0; i < 10; i++) {
                                SpaceVertical();
                                Left(letters[i]);
                                for (int j = 0; j < 10; j++) {
                                    cursorCnt++;
                                    isCursor = (cursorCnt == cursor) ? true : false;
                                    sign = "~";
                                    shipCnt++;
                                    for (int k = 0; k < ships.Count; k++) {
                                        for (int l = 0; l < ships[k].Count; l++) {
                                            isShip = (shipCnt == ships[k][l]) ? true : false;
                                            if (isShip) {
                                                switch (ships[k].Count) {
                                                    case 1: sign = "1"; break;
                                                    case 2: sign = "2"; break;
                                                    case 3: sign = "3"; break;
                                                    case 4: sign = "4"; break;
                                                    case 5: sign = "5"; break;
                                                    case 6: sign = "6"; break;
                                                    case 7: sign = "7"; break;
                                                    case 8: sign = "8"; break;
                                                    case 9: sign = "9"; break;
                                                }
                                            }
                                        }
                                    }
                                    Sign(sign, isCursor);
                                }
                                Right();
                            }
                            SpaceVertical();
                            Bottom();
                        }
                        public static void Top() {
                            GlobalMethod.Color("          0   1   2   3   4   5   6   7   8   9      \n", ConsoleColor.Green);
                            GlobalMethod.Color("     _______________________________________________ \n", ConsoleColor.Green);
                        }
                        public static void Bottom() {
                            GlobalMethod.Color("    |_______________________________________________| \n", ConsoleColor.Green);
                        }
                        public static void SpaceVertical() {
                            GlobalMethod.Color("    |                                               | \n", ConsoleColor.Green);
                        }
                        public static void Left(string letter) {
                            GlobalMethod.Color(" " + letter + "  |    ", ConsoleColor.Green);
                        }
                        public static void Right() {
                            GlobalMethod.Color("   |\n", ConsoleColor.Green);
                        }
                        public static void Sign(string sign, bool isCursor) {
                            ConsoleColor signColor = ConsoleColor.White;
                            if (sign == "~") signColor = ConsoleColor.Blue;
                            else signColor = ConsoleColor.Yellow;
                            string text = "";
                            if (isCursor) {
                                GlobalMethod.Color("{", ConsoleColor.White);
                                GlobalMethod.Color(sign, signColor);
                                GlobalMethod.Color("} ", ConsoleColor.White);
                            } else {
                                text = " " + sign + "  ";
                                GlobalMethod.Color(text, signColor);
                            }
                        }
                    }
                }
                public class Computer {
                    
                }
            }
            public class Battle {

            }
            public class Summary {

            }
        }
    }
}
