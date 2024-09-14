using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Library_GlobalMethods;
using Page_Menu;
using Page_PVC;
using Page_Ranking;

namespace Page_Options {    // DO£¥CZ DO OPCJI ODDZIELNY PLIK TEKSTOWY, W KTÓRYM ZAPISUJESZ I ZAMIENIASZ DANE ODNOŒNIE OPCJI!!!
    public class Options {
        public static int page_ID = 3;   // ZMIEÑ "static" NA "const" i sprawdŸ w dokumentacji jej zasiêg w kontekœcie KLASY !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public static bool isPage = false;
        public static int maxShipsLengthScore = 25;
        public const int buttonsAmount = 6;   // Musia³em ustawiæ const, aby zadeklarowaæ d³ugoœæ tablicy.
        public static string[] buttons = new string[buttonsAmount];
        public static string[] buttonsTitle = { 
            "Music:                                 ",
            "Sound effects:                         ",
            "Equal ships direction for AI:          ",
            "Show only top 10 players in ranking:   ",
            "Change ships in battle:                ",
            "Delete PVC mode ranking data:          "
        };
        public static string[] guide = new string[buttonsAmount] {
            "ON = [E] | OFF = [D]",
            "ON = [E] | OFF = [D]",
            "ON = [E] | OFF = [D]",
            "ON = [E] | OFF = [D]",
            "change = [C] -> Write new numeric value -> [ENTER]\n\nCorrect keys are numbers and comma. | Example: 2,2,3,4,5\n\nAdditional the sum of lenght all ships can be max: " + maxShipsLengthScore + ".",
            "delete = [P] -> Write \"yes\" or \"no\" -> [ENTER]"
        };
        public static int currentButton = 0;   // Zawsze pierwszy, bo chcê mieæ kursor na górze!
        //public static List<ConsoleKey> usingKeys = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_DEFAULT = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.E, ConsoleKey.D };
        public static List<ConsoleKey> usingKeys_CHANGE = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.C };
        public static List<ConsoleKey> usingKeys_DELETE = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.P };
        public const string optionsPath = "options.txt";   // Zmienna ta jest u¿ywana w klasie "Program"
        public static bool isFile = true;
        public static bool isCorrectContent = true;
        public static string errorFile = "";  // b³¹d odczutu bie¿¹cego pliku = index
        public static string errorCorrectContent = "";  // b³¹d odczutu bie¿¹cego pliku = index
        public static List<string> options = new List<string>();
        public static bool isUpdate = false;
        public void RenderPage() {
            System.ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);   // Dowolny niew³aœciwy klawisz.
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                // Dlaczego nie ma kontroli walidacji b³êdów? Poniewa¿ jest w klasie "Program" przy pierwszym pobraniu danych.
                GlobalMethod.Page.RenderButtons(buttons, currentButton);
                GlobalMethod.Page.RenderDottedLine(64);
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
            GlobalMethod.Page.RenderDottedLine(64);
            Console.WriteLine("OPTIONS: | Moving: arrows/[W][S] | Back to menu: [Backspace]\n");
        }
        public static void ShowOption(int currentButton, ConsoleKeyInfo key) {
            Console.WriteLine("GUIDE: " + guide[currentButton]);
            switch (currentButton) {
                case 4: Data.DetermineShips(currentButton, key); break;
                case 5: Data.DeleteRanking(currentButton, key, "PVC"); break;
                default: Data.EnableDisable(currentButton, key); break;
            }
        }
        public static ConsoleKeyInfo SelectLoopCorrectKey(int currentButton, int page_ID, ConsoleKeyInfo key) {
            switch (currentButton) {
                case 4: key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_CHANGE); break;   // ostatni argument = zestaw odpowiednich przycisków dla: metody "DetermineShips"
                case 5: key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_DELETE); break;   // ostatni argument = zestaw odpowiednich przycisków dla: metody "DeleteRanking"
                default: key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_DEFAULT); break;   // ostatni argument = zestaw odpowiednich przycisków dla: metody "EnableDisable"
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
                        info = new List<string>(fileContent.Split('*')); // Rozk³ad danych.
                        ValidateData(info, errorMessage);   // Walidacja danych.

                    }
                    catch {   // Nie podaje parametru b³êdu, poniewa¿ chcê jedynie poinformaowaæ o nieprawid³owym formacie danych.
                        isCorrectContent = false;
                        errorCorrectContent = errorMessage;
                    }
                }
            }
            public static void ValidateData(List<string> info, string errorMessage) {
                for (int i = 0; i < info.Count; i++) {   // Sprawdzenie czy któraœ z informacji ka¿dego gracza jest pusta.
                    if (info[i] == "") {
                        isCorrectContent = false;
                        errorCorrectContent = errorMessage;
                        break;
                    }
                }
                if (info.Count != buttonsAmount) {   // Sprawdzenie czy iloœæ danych opcji jest odpowiednia.
                    isCorrectContent = false;
                    errorCorrectContent = errorMessage;
                }
                if (isCorrectContent == true) { options = info; FillButtons(); }
            }
            public static void FillButtons() {
                for (int i = 0; i < buttonsAmount; i++) {
                    buttons[i] = buttonsTitle[i] + "[" + options[i] + "]";
                }
            }
        }
        public class Data {
            public static void EnableDisable(int option, ConsoleKeyInfo key) {
                // Jako, ¿e tak fajnie siê sk³ada, ¿e te metody odpalaj¹ siê po walidacji przycisków, przekazujesz przycisk tutaj i robisz robotê z w³aœciwymi funkcjami :)
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
            public static void DetermineShips(int option, ConsoleKeyInfo key) {
                if (key.Key == ConsoleKey.C) {
                    string dtrmError = "";
                    string newValue = "";
                    string[] corrSigns = new string[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "," };
                    bool isChangeLoop = true;
                    bool isBad = false;
                    while (isChangeLoop) {
                        isBad = false;
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
                                    for (int i = 0; i < splitValue.Count; i++) {   // Walidacja na tylko jedn¹ cyfrê w sektorze.
                                        if (splitValue[i].Length != 1) {
                                            isBad = true;
                                            dtrmError = "This value can only contain one number in field (1,2,3).\nWrite correct value.";
                                            break;
                                        }
                                    }
                                }
                                if (isBad == false) {
                                    int totalLength = 0;
                                    for (int i = 0; i < splitValue.Count; i++) {   // Walidacja na maksymalne ³¹czne miejsce zajête przez statki, domyœlnie: 30.
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
                            isChangeLoop = false;
                            options[option] = SortShips(newValue);
                            Update();
                        }
                    }
                }
            }
            public static string SortShips(string newValue) {
                List<int> values = new List<string>(newValue.Split(',')).Select(int.Parse).ToList();
                bool isChange = true;
                int smaller = 0;
                while (isChange == true) {   // Algroytm sortowania b¹belkowego. Z³o¿onoœæ obliczeniowa maksymalna: O(n*2)
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
            public static void DeleteRanking(int option, ConsoleKeyInfo key, string name) {
                Console.WriteLine("\nDo you want delete " + name + " ranking data ?");
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