using System;
using System.Collections.Generic;
using System.IO;
using Library_GlobalMethods;
using Page_PVC;
using Page_Ranking;

namespace Page_Options {    // DOŁĄCZ DO OPCJI ODDZIELNY PLIK TEKSTOWY, W KTÓRYM ZAPISUJESZ I ZAMIENIASZ DANE ODNOŚNIE OPCJI!!!
    public class Options {
        public static int page_ID = 3;   // ZMIEŃ "static" NA "const" i sprawdź w dokumentacji jej zasięg w kontekście KLASY !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public static bool isPage = false;
        public static int pageLineLength = 64;
        public static int maxShipsLengthScore = 30;
        public const int buttonsAmount = 6;   // Musiałem ustawić const, aby zadeklarować długość tablicy.
        public static int optMusic = 0;  // W razie zmiany pozycji tego przycisku odpowiedzialnego za włącznie i wyłączanie muzyki.
        public static int optEqualShipsAI = 1;
        public static int optTopPlayers = 2;
        public static int optReset_PVC = 4;   // Metoda "DetermineShips" i "DeleteUsers" odwołuje się do danych opcji resetu, a przy dodawaniu opcji nowego trybu, opcję tą mogę przenieść np. do dołu, jeżeli zajdzie taka potrzeba.
        public static int optDelete_PVC = 5;
        public static string[] buttons = new string[buttonsAmount];
        public static string[] buttonsTitle = { 
            "Music:                                   ",   // OK
            "Equal ships direction for AI:            ",   // "RandomShips"
            "Show only top 10 players in ranking:     ",   // "RenderRanking"
            "Change ships in battle - PVC mode:       ",   // OK
            "Reset ranking data - - - PVC mode:       ",   // [DATA], [CLEAN], [EMPTY] | [DATA] = kiedy gracz ma wpisywany wynik do pliku
            "Delete users - - - - - - PVC mode:       "    // [CONTENT], [EMPTY] | [CONTENT] = kiedy jest dodawany nowy gracz
        };
        public static string[] guide = new string[buttonsAmount] {
            "ON = [E] | OFF = [D]",
            "ON = [E] | OFF = [D]",
            "ON = [E] | OFF = [D]",
            "change = [C] -> Write value -> Write \"yes\" or \"no\" -> [ENTER]" +
                "\n\nCorrect keys are numbers and comma. | Example: 2,2,3,4,5 " +
                "\n\nAdditional the sum of lenght all ships can be max: " + maxShipsLengthScore + "." +
                "\n\nWARNING: When you set new ships, you cause reset PVC ranking data!",
            "reset = [R] -> Write \"yes\" or \"no\" -> [ENTER]",
            "delete = [P] -> Write \"yes\" or \"no\" -> [ENTER]"
        };
        public static int currentButton = 0;   // Zawsze pierwszy, bo chcę mieć kursor na górze!
        public static List<ConsoleKey> usingKeys_ENABLE = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.E };
        public static List<ConsoleKey> usingKeys_ENABLE_FIRST = new List<ConsoleKey> { ConsoleKey.S, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.E };
        public static List<ConsoleKey> usingKeys_DISABLE = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.D };
        public static List<ConsoleKey> usingKeys_DISABLE_FIRST = new List<ConsoleKey> { ConsoleKey.S, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.D };
        public static List<ConsoleKey> usingKeys_CHANGE = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.C };
        public static List<ConsoleKey> usingKeys_RESET_IS = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.R };
        public static List<ConsoleKey> usingKeys_RESET_NOT = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace};
        public static List<ConsoleKey> usingKeys_DELETE_IS = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.UpArrow, ConsoleKey.Backspace, ConsoleKey.P };
        public static List<ConsoleKey> usingKeys_DELETE_NOT = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.UpArrow, ConsoleKey.Backspace };
        public const string optionsPath = "options.txt";   // Zmienna ta jest używana w klasie "Program"
        public static bool isFile = true;
        public static bool isCorrectContent = true;
        public static string errorFile = "";  // błąd odczutu bieżącego pliku = index
        public static string errorCorrectContent = "";  // błąd odczutu bieżącego pliku = index
        public static List<string> options = new List<string>();
        public static bool isDisable = false;
        public void RenderPage() {
            System.ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolny niewłaściwy klawisz.
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                // Dlaczego nie ma kontroli walidacji błędów? Ponieważ jest w klasie "Program" przy pierwszym pobraniu danych.
                GlobalMethod.Page.RenderButtons(buttons, currentButton);
                GlobalMethod.Page.RenderDottedLine(pageLineLength);
                ShowOption(currentButton, key);
                key = SelectUsingKeys(currentButton, page_ID, key);
                //key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys);
                currentButton = GlobalMethod.Page.MoveButtons(buttons, currentButton, key);
            }
        }
        public static void RenderTitle() {
            Console.WriteLine(" BBBBBB   BBBBBBB   BBBBBBBB  BB   BBBBBB   BBBB  BB   BBBBBBB");
            Console.WriteLine("BB    BB  BB    BB     BB     BB  BB    BB  BB BB BB  BB      ");
            Console.WriteLine("BB    BB  BB    BB     BB     BB  BB    BB  BB BB BB  BB      ");
            Console.WriteLine("BB    BB  BBBBBBB      BB     BB  BB    BB  BB BB BB   BBBBBB ");
            Console.WriteLine("BB    BB  BB           BB     BB  BB    BB  BB BB BB        BB");
            Console.WriteLine("BB    BB  BB           BB     BB  BB    BB  BB BB BB        BB");
            Console.WriteLine(" BBBBBB   BB           BB     BB   BBBBBB   BB  BBBB  BBBBBBB ");
            GlobalMethod.Page.RenderDottedLine(pageLineLength);
            Console.WriteLine("OPTIONS: | Moving: arrows/[W][S] | Back to menu: [BACKSPACE]\n");
        }
        public static void ShowOption(int currentButton, ConsoleKeyInfo key) {
            switch (currentButton) {
                case 3:
                    Console.WriteLine("GUIDE: " + guide[currentButton]);
                    Data.DetermineShips(currentButton, key, "PVC", 0);
                    break;
                case 4:
                    if (Ranking.isFile[0] == true) {   // Walidacja na zawartość pliku danego rankingu | 0 = PVC mode
                        if (Ranking.isCorrectContent[0] == true) {
                            if (options[currentButton] == "DATA") {   // Jeżeli zawartość danego rankingu została już wyczyszczona, nie ma potrzeby czyścić jej ponownie.
                                Console.WriteLine("GUIDE: " + guide[currentButton]);
                                Data.ResetRanking(currentButton, key, "PVC", 0);
                            } else {
                                Console.WriteLine("The PVC ranking content has been cleared.");
                            }
                        } else {
                            Console.WriteLine(Ranking.errorCorrectContent[0]);
                        }
                    } else {
                        Console.WriteLine(Ranking.errorFile[0]);
                    }
                    break;
                case 5:
                    if (Ranking.isFile[0] == true) {
                        if (Ranking.isCorrectContent[0] == true) {
                            Console.WriteLine("GUIDE: " + guide[currentButton]);
                            Data.DeleteUsers(currentButton, key, "PVC", 0);
                        } else {
                            Console.WriteLine(Ranking.errorCorrectContent[0]);
                        }
                    } else {
                        Console.WriteLine(Ranking.errorFile[0]);
                    }
                    break;
                default:
                    Console.WriteLine("GUIDE: " + guide[currentButton]);
                    Data.EnableDisable(currentButton, key);
                    break;
            }
        }
        public static ConsoleKeyInfo SelectUsingKeys(int currentButton, int page_ID, ConsoleKeyInfo key) {
            switch (currentButton) {
                case 0: key = (isDisable == true) ? GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_DISABLE_FIRST) : GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_ENABLE_FIRST); break;
                case 3: key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_CHANGE); break;   // ostatni argument = zestaw odpowiednich przycisków dla: metody "DetermineShips"
                case 4: key = (Ranking.isFile[0] && Ranking.isCorrectContent[0] == true) ? GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_RESET_IS) : GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_RESET_NOT); break;   // ostatni argument = zestaw odpowiednich przycisków dla: metody "DetermineShips"
                case 5: key = (Ranking.isFile[0] && Ranking.isCorrectContent[0] == true) ? GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_DELETE_IS) : GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_DELETE_NOT); break;
                default: key = (isDisable == true) ? GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_DISABLE) : GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_ENABLE); break;
            }
            return key;
        }
        public class Upload {
            public static void SearchFile(string filePath) {   // Panel kontrolny
                (bool, string, string) fileInfo = GlobalMethod.UploadFile(filePath);
                isFile = fileInfo.Item1;
                errorFile = fileInfo.Item3;
                if (isFile == true) {
                    UploadData(fileInfo.Item2);
                }
            }
            public static void UploadData(string filePath) {
                isCorrectContent = true;
                errorCorrectContent = "";
                string errorMessage = "The data format in \"" + filePath + "\" is not correct. It should be: " + GenerateFormat(buttonsAmount);
                string content = File.ReadAllText(filePath);
                string fileContent = GlobalMethod.TrimAllContent(content);
                List<string> info = new List<string>();
                if (fileContent == "") {
                    isCorrectContent = false;
                    errorCorrectContent = errorMessage;
                } else if (fileContent != "") {
                    try {
                        info = new List<string>(fileContent.Split('*')); // Rozkład danych.
                        for (int i = 0; i < info.Count; i++) {   // Sprawdzenie czy któraś z informacji każdego gracza jest pusta.
                            if (info[i] == "") {
                                isCorrectContent = false;
                                errorCorrectContent = errorMessage;
                                break;
                            }
                        }
                        if (info.Count != buttonsAmount) {   // Sprawdzenie czy ilość danych opcji jest odpowiednia.
                            isCorrectContent = false;
                            errorCorrectContent = errorMessage;
                        }
                        if (isCorrectContent == true) { options = info; FillButtons(); }

                    }
                    catch {   // Nie podaje parametru błędu, ponieważ chcę jedynie poinformaować o nieprawidłowym formacie danych.
                        isCorrectContent = false;
                        errorCorrectContent = errorMessage;
                    }
                }
            }
            public static void FillButtons() {
                for (int i = 0; i < buttonsAmount; i++) {
                    buttons[i] = buttonsTitle[i] + "[" + options[i] + "]";
                }
            }
            public static string GenerateFormat(int optionsNum) {
                string format = "";
                for (int i = 0; i < optionsNum; i++) {
                    format += "data";
                    if (i < optionsNum - 1) format += "*";
                }
                return format;
            }
        }
        public class Data {
            public static void EnableDisable(int option, ConsoleKeyInfo key) {
                // Jako, że tak fajnie się składa, że te metody odpalają się po walidacji przycisków, przekazujesz przycisk tutaj i robisz robotę z właściwymi funkcjami :)
                string prev = options[option];   // Zabezpieczenie, mające na celu zablokowanie akcji klawisza [E] i [D] klikniętego więcej niż jeden raz podczas bycia na tej samej opcji - np: [E] -> [E], aby nie przeładowała się niepotrzebnie strona za drugim razem.
                string next = "";
                isDisable = (options[option] == "ON") ? isDisable = true : isDisable = false;
                if (key.Key == ConsoleKey.E) {
                    options[option] = "ON";
                    next = options[option];
                    if (option == optMusic && prev != next) GlobalMethod.SoundControl.ResumeSound();
                    if (prev != next) PageUpdate();
                } else if (key.Key == ConsoleKey.D) {
                    options[option] = "OFF";
                    next = options[option];
                    if (option == optMusic && prev != next) GlobalMethod.SoundControl.StopSound();
                    if (prev != next) PageUpdate();
                }
            }
            public static void DetermineShips(int option, ConsoleKeyInfo key, string modeText, int modeNum) {
                if (key.Key == ConsoleKey.C) {
                    string dtrmError = "";
                    string newValue = "";
                    string[] corrSigns = new string[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "," };
                    bool isLoop = true;
                    bool isBad = false;
                    while (isLoop) {
                        isBad = false;
                        Console.CursorVisible = true;
                        Console.Write("\n\nNew value: ");
                        newValue = GlobalMethod.TrimAllContent(Console.ReadLine());
                        if (newValue != "") {
                            bool idxSign = true;
                            for (int i = 0; i < newValue.Length; i++) {   // Walidacja na poprawne znaki.
                                idxSign = true;
                                for (int j = 0; j < corrSigns.Length; j++) {
                                    if (newValue[i] == Convert.ToChar(corrSigns[j])) {
                                        idxSign = false;
                                        break;
                                    }
                                }
                                if (idxSign) {
                                    isBad = true;
                                    dtrmError = "This value can only contain characters from 1 to 9 and a comma [,].\nWrite correct value.";
                                    break;
                                }
                            }
                            if (isBad == false) {
                                List<string> splitValue = new List<string>(newValue.Split(','));
                                for (int i = 0; i < splitValue.Count; i++) {   // Walidacja na puste miejsce. '0' - odpowiednik pustego stringa.
                                    if (splitValue[i] == "" || splitValue[i] == " ") {
                                        isBad = true;
                                        dtrmError = "No field can be empty. Write correct value.";
                                        break;
                                    }
                                }
                                if (isBad == false) {
                                    for (int i = 0; i < splitValue.Count; i++) {   // Walidacja na tylko jedną cyfrę w sektorze.
                                        if (splitValue[i].Length != 1) {
                                            isBad = true;
                                            dtrmError = "This value can only contain one number in field (1,2,3).\nWrite correct value.";
                                            break;
                                        }
                                    }
                                }
                                if (isBad == false) {
                                    int totalLength = 0;
                                    for (int i = 0; i < splitValue.Count; i++) {   // Walidacja na maksymalne łączne miejsce zajęte przez statki, domyślnie: 30.
                                        totalLength += int.Parse(splitValue[i]);
                                    }
                                    if (totalLength > maxShipsLengthScore) {
                                        isBad = true;
                                        dtrmError = "The total ships lenght is more than max limit: " + maxShipsLengthScore + ".\nYour total length: " + totalLength + ". Write correct value.";
                                    }
                                }
                                if (isBad == false) {
                                    newValue = SortShips(newValue);   // Sortowanie statków. | Sortowanie jest przeniesione do walidacji na takie same statki, aby nie wywoływać tej metody dwukrotnie.
                                    if (options[option] == newValue) {   // Walidacja na takie same statki jak poprzednio.
                                        isBad = true;
                                        dtrmError = "The ships fleet is the same like previous ship fleet. You must change them.";
                                    }
                                }
                            }
                        } else {
                            isBad = true;
                            dtrmError = "This value is empty. Write correct value.";
                        }
                        if (isBad) {
                            Console.WriteLine("\n" + dtrmError + "\n");
                        } else {
                            isLoop = false;
                            Console.CursorVisible = true;
                            bool isReset = false;
                            if (options[optReset_PVC] == "DATA") {
                                Console.WriteLine("\nDo you want create new ship fleet in " + modeText + " mode?" +
                                "\nYou will cause RESET all " + modeText + " mode ranking data." +
                                "\n\nWrite \"yes\" or \"no\".");
                                string answer = "";
                                bool confirmLoop = true;
                                while (confirmLoop) {
                                    answer = Console.ReadLine();
                                    if (answer == "yes") {
                                        confirmLoop = false;
                                        isReset = true;
                                    } else if (answer == "no") {
                                        confirmLoop = false;
                                        Console.WriteLine("\n\nYou can now move to other options.");
                                    } else {
                                        Console.WriteLine("\n\nBad value. Write correct value.\n");
                                    }
                                }
                            }
                            Console.CursorVisible = false;
                            if (options[optReset_PVC] == "DATA" && isReset == true) {
                                if (isReset) options[option] = newValue;   // Sortowanie jest przeniesione do walidacji na takie same statki, aby nie wywoływać tej metody dwukrotnie.
                                options[optReset_PVC] = "CLEAN";
                            }
                            if (options[optReset_PVC] == "CLEAN" || options[optReset_PVC] == "EMPTY") options[option] = newValue;
                            if (isReset) ResetProper(modeText, modeNum);
                            PageUpdate();
                        }
                    }
                }
            }
            public static string SortShips(string newValue) {
                List<string> content = new List<string>(newValue.Split(','));
                List<int> values = new List<int>();
                for (int i = 0; i < content.Count; i++) {
                    values.Add(int.Parse(content[i]));
                }
                bool isChange = true;
                int smaller = 0;
                while (isChange == true) {   // Algroytm sortowania bąbelkowego. Złożoność obliczeniowa maksymalna: O(n^2)
                    isChange = false;
                    for (int i = 0; i < values.Count - 1; i++) {
                        if (values[i + 1] < values[i]) {
                            smaller = values[i + 1];
                            values[i + 1] = values[i];
                            values[i] = smaller;
                            isChange = true;
                        }
                    }
                }
                newValue = "";
                for (int i = 0; i < values.Count; i++) {
                    newValue += values[i];
                    if (i < values.Count - 1) newValue += ",";
                }
                return newValue;
            }
            public static void ResetRanking(int option, ConsoleKeyInfo key, string modeText, int modeNum) {
                Console.WriteLine();
                if (key.Key == ConsoleKey.R) {
                    Console.CursorVisible = true;
                    Console.WriteLine("\nDo you want reset " + modeText + " ranking data?\n");
                    string answer = "";
                    bool isLoop = true;
                    while (isLoop) {
                        answer = Console.ReadLine();
                        if (answer == "yes") {
                            Console.CursorVisible = false;
                            ResetProper(modeText, modeNum);
                            options[option] = "CLEAN";
                            PageUpdate();
                        } else if (answer == "no") {
                            isLoop = false;
                            Console.CursorVisible = false;
                            Console.WriteLine("\n\nYou can now move to other options.");
                        } else {
                            Console.WriteLine("\n\nBad value. Write correct value.\n");
                        }
                    }
                }
            }
            public static void ResetProper(string modeText, int modeNum) {   // Zrobiłem tą metodę, aby metoda "DetermineShips" miała dostęp do metody "WYŁĄCZNIE" kasującej.
                List<List<string>> playersInfo = Ranking.modePlayersInfo[modeNum];
                const int user = 0;
                const int battle = 3;
                const int accurate = 6;
                for (int i = 0; i < playersInfo.Count; i++) {
                    for (int j = 0; j < playersInfo[i].Count; j++) {
                        switch(j) {
                            case user: break;
                            case battle: playersInfo[i][j] = "?"; break;
                            case accurate: playersInfo[i][playersInfo[i].Count - 1] = "0%"; break;
                            default: playersInfo[i][j] = "0"; break;
                        }
                    }
                }
                Ranking.modePlayersInfo[modeNum] = playersInfo;
                File.WriteAllText("players_" + modeText + ".txt", GlobalMethod.StringPlayersInfo(playersInfo));
            }
            public static void DeleteUsers(int option, ConsoleKeyInfo key, string modeText, int modeNum) {
                if (key.Key == ConsoleKey.P) {
                    Console.CursorVisible = true;
                    Console.WriteLine("\n\nDo you want delete " + modeText + " users?\n");
                    string answer = "";
                    bool isLoop = true;
                    while (isLoop) {
                        answer = Console.ReadLine();
                        if (answer == "yes") {
                            isLoop = false;
                            Console.CursorVisible = false;
                            File.WriteAllText("players_" + modeText + ".txt", "");   // Kasowanie użytkowników.
                            Ranking.modePlayersInfo[modeNum].Clear();   // Czyszczenie danych w programie.   // Inaczej kiedy usuniemy wszystkich użytkowników w opcjach i wrócimy do danego trybu aby utworzyć nowego użytkownika - (wówczas) pojawi się błąd. Tablica "buttons" Będzie miała długość równą 1, gdyż jakimś cudem (nie mam pojęcia dlaczego) do tablicy użytkowników danego trybu zostanie dodane jedno miejce. Dlatego trzeba to skasować to skasować za pomocą metody ".Clear()" listy dynamicznej.
                            Ranking.isCorrectContent[modeNum] = false;
                            Ranking.errorCorrectContent[modeNum] = "This data file is empty. Create new user and play game.";
                            options[option] = "EMPTY";
                            options[optReset_PVC] = "EMPTY";   // Wartość opcji od resetowania. | Kiedy pojawi/wią się gracze z początkowymi danymi, albo zostanie aktywowana metoda resetu = [CLEAN] | Kiedy dane któegokolwiek z graczy zostaną uzupełnione = [DATA]
                            PVC.currentButton = 0;   // Resetowanie wartości kursora klasy "PVC" na 0, inaczej jest -1 i wywala błąd, w sytuacji kiedy: usuniemy wszystkich użytkowników za pomocą [P] i utworzymy nowego użytkownika.
                            PageUpdate();
                        } else if (answer == "no") {
                            isLoop = false;
                            Console.CursorVisible = false;
                            Console.WriteLine("\n\nYou can now move to other options.");
                        } else {
                            Console.WriteLine("\n\nBad value. Write correct value.\n");
                        }
                    }
                }
            }
            public static void PageUpdate() {
                Upload.FillButtons();
                string fileContent = "";
                for (int i = 0; i < buttonsAmount; i++) {
                    fileContent += options[i];
                    if (i < buttonsAmount - 1) fileContent += "*";
                }
                File.WriteAllText(optionsPath, fileContent);
                Options page = new Options();
                page.RenderPage();
            }
        }
    }
}
