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
            "change = [C] => Write new numeric value => [ENTER]\n\nCorrect keys are numbers and comma | Example: 2,2,3,4,5",
            "delete = [P] => Write \"yes\" or \"no\" => [ENTER]"
        };
        public static int currentButton = 0;   // Zawsze pierwszy, bo chcê mieæ kursor na górze!
        //public static List<ConsoleKey> usingKeys = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace };
        public static List<ConsoleKey> usingKeys_DEFAULT = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.E, ConsoleKey.D };
        public static List<ConsoleKey> usingKeys_CHANGE = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.C };
        // Ustaw maksymaln¹ d³ugoœæ zajêtych pól (w kontekœcie d³ugoœci statków) na max. 25!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public static List<ConsoleKey> usingKeys_SHIPS = new List<ConsoleKey> { ConsoleKey.OemComma, ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Enter };
        public static List<ConsoleKey> usingKeys_DELETE = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace, ConsoleKey.P };
        public static List<ConsoleKey> usingKeys_CONFIRM = new List<ConsoleKey> { ConsoleKey.Y, ConsoleKey.E, ConsoleKey.S, ConsoleKey.N, ConsoleKey.O, ConsoleKey.Enter };
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
                GlobalMethod.Page.RenderDottedLine(90);
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
            GlobalMethod.Page.RenderDottedLine(90);
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
                    string newValue = "";
                    List<string> splitValue = new List<string>();
                    bool isChangeLoop = true;
                    bool isBad = false;
                    while (isChangeLoop) {
                        isBad = false;
                        Console.WriteLine("\nNew value: ");
                        //GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_SHIPS);  -- pomyœl nad tym
                        newValue = Console.ReadLine();
                        if (GlobalMethod.TrimAllContent(newValue) != "") {
                            for (int i = 0; i < newValue.Length; i++) {
                                if (newValue[i] == '\0') {   // '0' - odpowiednik pustego stringa
                                    isBad = true;
                                    break;
                                } else {
                                    splitValue = new List<string>(newValue.Split(','));
                                    
                                    // Walidacja na tylko jedn¹ cyfrê w sektorze.

                                }
                            }
                        } else {
                            isBad = true;
                        }
                        if (isBad) {
                            Console.WriteLine("\nThis value is uncorrect. Write correct value.\n\n");
                        } else {
                            isChangeLoop = false;
                            SortShips(newValue);
                            options[option] = newValue;
                            Update();


                            // Metoda "GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_SHIPS)" z przekazanie odpowiedniej listy "usingKeys_".


                        }
                    }
                }
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
            public static string SortShips(string newValue) {
                return newValue;
            }
        }
    }
}