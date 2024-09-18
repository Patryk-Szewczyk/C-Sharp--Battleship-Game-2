using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Library_GlobalMethods;
using Page_Menu;
using Page_PVC;
using Page_Ranking;

namespace Page_Options {    // DO��CZ DO OPCJI ODDZIELNY PLIK TEKSTOWY, W KT�RYM ZAPISUJESZ I ZAMIENIASZ DANE ODNO�NIE OPCJI!!!
    public class Options {
        public static int page_ID = 3;   // ZMIE� "static" NA "const" i sprawd� w dokumentacji jej zasi�g w kontek�cie KLASY !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public static bool isPage = false;
        public static int pageLineLength = 64;
        public static int maxShipsLengthScore = 25;
        public const int buttonsAmount = 7;   // Musia�em ustawi� const, aby zadeklarowa� d�ugo�� tablicy.
        public static string[] buttons = new string[buttonsAmount];
        public static string[] buttonsTitle = { 
            "Music:                                 ",
            "Sound effects:                         ",
            "Equal ships direction for AI:          ",
            "Show only top 10 players in ranking:   ",
            "Change ships in battle - PVC mode:     ",
            "Reset ranking data - - - PVC mode:     ",   // [DATA], [CLEAN], [EMPTY]
            "Delete users - - - - - - PVC mode:     "    // [CONTENT]. [EMPTY]   
        };
        public static string[] guide = new string[buttonsAmount] {
            "ON = [E] | OFF = [D]",
            "ON = [E] | OFF = [D]",
            "ON = [E] | OFF = [D]",
            "ON = [E] | OFF = [D]",
            "change = [C] -> Write value -> Write \"yes\" or \"no\" -> [ENTER]\n\nCorrect keys are numbers and comma. | Example: 2,2,3,4,5\n\nAdditional the sum of lenght all ships can be max: " + maxShipsLengthScore + ".",
            "reset = [R] -> Write \"yes\" or \"no\" -> [ENTER]",
            "delete = [P] -> Write \"yes\" or \"no\" -> [ENTER]"
        };
        public static int currentButton = 0;   // Zawsze pierwszy, bo chc� mie� kursor na g�rze!
        //public static List<ConsoleKey> usingKeys = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_DEFAULT = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.E, ConsoleKey.D };
        public static List<ConsoleKey> usingKeys_CHANGE = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.C };
        public static List<ConsoleKey> usingKeys_RESET_IS = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.R };
        public static List<ConsoleKey> usingKeys_RESET_NOT = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace};
        public static List<ConsoleKey> usingKeys_DELETE_IS = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.P };
        public static List<ConsoleKey> usingKeys_DELETE_NOT = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace };
        public const string optionsPath = "options.txt";   // Zmienna ta jest u�ywana w klasie "Program"
        public static bool isFile = true;
        public static bool isCorrectContent = true;
        public static string errorFile = "";  // b��d odczutu bie��cego pliku = index
        public static string errorCorrectContent = "";  // b��d odczutu bie��cego pliku = index
        public static List<string> options = new List<string>();
        public static bool isUpdate = false;
        public void RenderPage() {
            System.ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolny niew�a�ciwy klawisz.
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                // Dlaczego nie ma kontroli walidacji b��d�w? Poniewa� jest w klasie "Program" przy pierwszym pobraniu danych.
                GlobalMethod.Page.RenderButtons(buttons, currentButton);
                GlobalMethod.Page.RenderDottedLine(pageLineLength);
                ShowOption(currentButton, key);
                key = SelectLoopCorrectKey(currentButton, page_ID, key);
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
            Console.WriteLine("OPTIONS: | Moving: arrows/[W][S] | Back to menu: [Backspace]\n");
        }
        public static void ShowOption(int currentButton, ConsoleKeyInfo key) {

            switch (currentButton) {
                case 4: 
                    Data.DetermineShips(currentButton, key, "PVC");
                    Console.WriteLine("GUIDE: " + guide[currentButton]);
                    break;
                case 5:
                    if (Ranking.isFile[0] == true) {   // Walidacja na zawarto�� pliku danego rankingu | 0 = PVC mode
                        if (Ranking.isCorrectContent[0] == true) {
                            Console.WriteLine("GUIDE: " + guide[currentButton]);
                            Data.ResetRanking(currentButton, key, "PVC", 0);
                        } else {
                            Console.WriteLine(Ranking.errorCorrectContent[0]);
                        }
                    } else {
                        Console.WriteLine(Ranking.errorFile[0]);
                    }
                    break;
                case 6:
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
                    Data.EnableDisable(currentButton, key);
                    Console.WriteLine("GUIDE: " + guide[currentButton]);
                    break;
            }
        }
        public static ConsoleKeyInfo SelectLoopCorrectKey(int currentButton, int page_ID, ConsoleKeyInfo key) {
            switch (currentButton) {
                case 4: key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_CHANGE); break;   // ostatni argument = zestaw odpowiednich przycisk�w dla: metody "DetermineShips"
                case 5: key = (Ranking.isFile[0] && Ranking.isCorrectContent[0] == true) ? GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_RESET_IS) : GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_RESET_NOT); break;   // ostatni argument = zestaw odpowiednich przycisk�w dla: metody "DetermineShips"
                case 6: key = (Ranking.isFile[0] && Ranking.isCorrectContent[0] == true) ? GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_DELETE_IS) : GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_DELETE_NOT); break;
                default: key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_DEFAULT); break;   // ostatni argument = zestaw odpowiednich przycisk�w dla: metody "EnableDisable"
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
                string errorMessage = "The data format in \"" + filePath + "\" is not correct. It should be: data*data*data*data*data*data";
                string content = File.ReadAllText(filePath);
                string fileContent = GlobalMethod.TrimAllContent(content);
                List<string> info = new List<string>();
                if (fileContent == "") {
                    isCorrectContent = false;
                    errorCorrectContent = errorMessage;
                } else if (fileContent != "") {
                    try {
                        info = new List<string>(fileContent.Split('*')); // Rozk�ad danych.
                        for (int i = 0; i < info.Count; i++) {   // Sprawdzenie czy kt�ra� z informacji ka�dego gracza jest pusta.
                            if (info[i] == "") {
                                isCorrectContent = false;
                                errorCorrectContent = errorMessage;
                                break;
                            }
                        }
                        if (info.Count != buttonsAmount) {   // Sprawdzenie czy ilo�� danych opcji jest odpowiednia.
                            isCorrectContent = false;
                            errorCorrectContent = errorMessage;
                        }
                        if (isCorrectContent == true) { options = info; FillButtons(); }

                    }
                    catch {   // Nie podaje parametru b��du, poniewa� chc� jedynie poinformaowa� o nieprawid�owym formacie danych.
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
        }
        public class Data {
            public static void EnableDisable(int option, ConsoleKeyInfo key) {
                // Jako, �e tak fajnie si� sk�ada, �e te metody odpalaj� si� po walidacji przycisk�w, przekazujesz przycisk tutaj i robisz robot� z w�a�ciwymi funkcjami :)
                if (key.Key == ConsoleKey.E) {
                    options[option] = "ON";
                    isUpdate = true;
                    Update();
                    if (isUpdate) Update();
                } else if (key.Key == ConsoleKey.D) {
                    options[option] = "OFF";
                    isUpdate = true;
                    if (isUpdate) Update();
                }
            }
            public static void DetermineShips(int option, ConsoleKeyInfo key, string mode) {
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
                                };
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
                                    for (int i = 0; i < splitValue.Count; i++) {   // Walidacja na tylko jedn� cyfr� w sektorze.
                                        if (splitValue[i].Length != 1) {
                                            isBad = true;
                                            dtrmError = "This value can only contain one number in field (1,2,3).\nWrite correct value.";
                                            break;
                                        }
                                    }
                                }
                                if (isBad == false) {
                                    int totalLength = 0;
                                    for (int i = 0; i < splitValue.Count; i++) {   // Walidacja na maksymalne ��czne miejsce zaj�te przez statki, domy�lnie: 30.
                                        totalLength += int.Parse(splitValue[i]);
                                    }
                                    if (totalLength > maxShipsLengthScore) {
                                        isBad = true;
                                        dtrmError = "The total ships lenght is more than max limit: " + maxShipsLengthScore + ".\nYour total length: " + totalLength + ". Write correct value.";
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
                            Console.CursorVisible = false;
                            //ResetRanking(option, new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false), mode);
                            options[option] = SortShips(newValue);
                            if (options[5] == "EMPTY") options[5] = "EMPTY";
                            else if (options[5] == "DATA") options[5] = "CLEAN";
                            Update();
                        }
                        
                        
                    }
                }
            }
            public static string SortShips(string newValue) {
                List<int> values = new List<string>(newValue.Split(',')).Select(int.Parse).ToList();
                bool isChange = true;
                int smaller = 0;
                while (isChange == true) {   // Algroytm sortowania b�belkowego. Z�o�ono�� obliczeniowa maksymalna: O(n^2)
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
                    Console.WriteLine("\nDo you want reset " + modeText + " ranking data?\n");
                    List<List<string>> playersInfo = Ranking.modePlayersInfo[modeNum];
                    // Reset warto�ci
                    // Reszta podobnie jak w kasowaniu, z pewnymi r�nicami.
                }
            }
            public static void DeleteUsers(int option, ConsoleKeyInfo key, string modeText, int modeNum) {
                if (key.Key == ConsoleKey.P) {
                    Console.CursorVisible = true;
                    Console.WriteLine("\nDo you want delete " + modeText + " users?\n");
                    string answer = "";
                    bool isLoop = true;
                    while (isLoop) {
                        answer = Console.ReadLine();
                        if (answer == "yes") {
                            isLoop = false;
                            Console.CursorVisible = false;
                            File.WriteAllText("players_" + modeText + ".txt", "");   // Kasowanie u�ytkownik�w.
                            Ranking.modePlayersInfo[modeNum] = Ranking.Data.UpdateData("players_" + modeText + ".txt");   // Ranking.modePlayersInfo[modeNum] = 0 = PVC mode
                            Ranking.isCorrectContent[modeNum] = false;
                            Ranking.errorCorrectContent[modeNum] = "This data file is empty. Create new user and play game.";
                            options[option] = "EMPTY";
                            options[5] = "EMPTY";   // Warto�� opcji od resetowania. | Kiedy pojawi/wi� si� gracze z pocz�tkowymi danymi, albo zostanie aktywowana metoda resetu = [CLEAN] | Kiedy dane kt�egokolwiek z graczy zostan� uzupe�nione = [DATA]
                            Update();
                        } else if (answer == "no") {   /// wwaliduj to dobrze
                            isLoop = false;
                            Console.CursorVisible = false;
                            Console.WriteLine("\n\nYou can move to other options.");
                        } else {
                            Console.WriteLine("\n\nBad value. Write correct value.\n");
                        }
                    }
                }
            }
            public static void Update() {
                isUpdate = false;
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
