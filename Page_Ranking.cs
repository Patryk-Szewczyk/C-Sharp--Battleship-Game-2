using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using Library_GlobalMethods;
using Page_Menu;

namespace Page_Ranking {
    public class Ranking {
        public static int page_ID = 2;
        public static bool isPage = false;
        public static int pageLineLength = 64;
        public static string[] buttons = { "PVC Mode", "PVP Mode"};
        public static int currentButton = 0;   // Zawsze pierwszy, bo chcę mieć kursor na górze!
        public static List<ConsoleKey> usingKeys = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace };
        public static string playersLimit_OPTION = "no-limit";   // "no-limit" / "limit"
        public const int detailsAmount = 5;
        public static List<bool> isFile = new List<bool>();  // plik = index
        public static List<bool> isCorrectContent = new List<bool>();  // plik = index
        public static List<string> errorFile = new List<string>();  // błąd odczutu bieżącego pliku = index
        public static List<string> errorCorrectContent = new List<string>();  // błąd odczutu bieżącego pliku = index
        public static List<List<List<string>>> modePlayersInfo = new List<List<List<string>>>();
        public void RenderPage() {
            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);
            //Upload.SearchFile("players_PVC.txt");   // Przeniesiono do Intro.   // Jeżeli chcesz podpiąć kolejny ranking jedyne co trzeba zrobić, to dodać nazwę przycisku i skopiować tą metodę z podaniem nazwy pliku z rozszerzeniem.
            //Upload.SearchFile("players_PVP.txt");
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.Page.RenderButtons(buttons, currentButton);
                GlobalMethod.Page.RenderDottedLine(pageLineLength);
                ShowRanking(currentButton);
                key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys);
                currentButton = GlobalMethod.Page.MoveButtons(buttons, currentButton, key);
            }
        }
        public static void RenderTitle() {
            Console.WriteLine("BBBBBBB     BBBB    BBBB  BB  BB    BB  BB  BBBB  BB   BBBBBB ");
            Console.WriteLine("BB    BB   BB  BB   BB BB BB  BB   BB   BB  BB BB BB  BB    BB");
            Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB  BB    BB  BB BB BB  BB      ");
            Console.WriteLine("BBBBBBB   BBBBBBBB  BB BB BB  BBBBB     BB  BB BB BB  BB  BBB ");
            Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB  BB    BB  BB BB BB  BB  B BB");
            Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB   BB   BB  BB BB BB  BB    BB");
            Console.WriteLine("BB    BB  BB    BB  BB  BBBB  BB    BB  BB  BB  BBBB   BBBBBB ");
            GlobalMethod.Page.RenderDottedLine(pageLineLength);
            Console.WriteLine("RANKING: | Moving: arrows/[W][S] | Back to menu: [Backspace]\n");
        }
        public static void ShowRanking(int mode) {   // Panel kontrolny
            if (isFile[currentButton] == true) {   // Tutaj bierzemy "currentButton", ponieważ nie dodajemy na bierząco kolejnych zmienneych dla listy dynamicznej "isFile", skąd (tam) mogliśmy od razu walidować obsługę metody "UploadData".
                if (isCorrectContent[currentButton] == true) {
                    Data.SortData(mode);
                    Data.RenderData(mode);
                } else {
                    Console.WriteLine(errorCorrectContent[currentButton]);
                }
            } else {
                Console.WriteLine(errorFile[currentButton]);
            }
        }
        public class Upload {   // Dlaczego nie przeniosłem tych klas bezpośrednio do namespace, a zamiast tego umieściłem je wewnątrz klasy "Ranking". (W klasie "Options" jest tak samo.) Ponieważ ta klasa i klasa "Options" posiada klasę "Upload". W klasie "Program" metoda "Main" wywołuje metodę "UploadOptions" klasy "Upload". W przypadku przeniesienia tej klasy bezpośrednio do namespace i użycia dziedziczenia byłyby problemy z odpowiednim wskazaniem właściwej metody oraz straciłoby to na czytelności i użyteczności kodu w kontekście tego typu problemu. Z powodu braku estetyki w przypadku tej sytuacji, aby naprawić estetykę tego typu styl (klasy potomne) został zaimplementowany na stałe w tym projekcie.
            public static void SearchFile(string filePath) {   // Panel kontrolny
                (bool, string, string) fileInfo = GlobalMethod.UploadFile(filePath);
                isFile.Add(fileInfo.Item1);
                errorFile.Add(fileInfo.Item3);
                if (isFile[isFile.Count - 1] == true) {   // Dodaje się w linii z "isFile.Add(fileInfo.Item1)", a tutaj bierzemy długość listy dynamicznej "isFile" - 1, czyli ostatni indeks, tzn. aktualny plik. Ha! Jestem geniuszem!
                    UploadData(fileInfo.Item2);
                }
            }
            public static void UploadData(string filePath) {
                isCorrectContent.Add(true);
                errorCorrectContent.Add("");
                modePlayersInfo.Add(new List<List<string>>());
                string errorMessage = "The data format is \"" + filePath + "\" not correct. It should be:\nuser#data#data#data#data*user#data#data#data#data#data";
                string content = File.ReadAllText(filePath);
                string fileContent = GlobalMethod.TrimAllContent(content);
                List<List<string>> playersInfo = new List<List<string>>();
                if (fileContent == "") {
                    isCorrectContent[errorCorrectContent.Count - 1] = false;
                    errorCorrectContent[errorCorrectContent.Count - 1] = "This data file is empty. Create new user and play game.";
                } else if (fileContent != "") {
                    try {   // Rozkład danych
                        List<string> players = new List<string>(fileContent.Split('*'));
                        for (int i = 0; i < players.Count; i++) {
                            playersInfo.Add(new List<string>(players[i].Split('#')));
                        }
                        for (int i = 0; i < playersInfo.Count; i++) {   // Sprawdzenie czy któraś z informacji każdego gracza jest pusta.
                            for (int j = 0; j < playersInfo[i].Count; j++) {
                                if (playersInfo[i][j] == "") {
                                    isCorrectContent[errorCorrectContent.Count - 1] = false;
                                    errorCorrectContent[errorCorrectContent.Count - 1] = errorMessage;
                                    break;
                                }
                            }
                        }
                        for (int i = 0; i < playersInfo.Count; i++) {   // Sprawdzenie czy każdy gracz ma taką samą liczbę danych przedzielonych znakiem "#"
                            if (playersInfo[i].Count != detailsAmount) {
                                isCorrectContent[errorCorrectContent.Count - 1] = false;
                                errorCorrectContent[errorCorrectContent.Count - 1] = errorMessage;
                                break;
                            }
                        }
                        if (isCorrectContent[isCorrectContent.Count - 1] == true) modePlayersInfo[errorCorrectContent.Count - 1] = playersInfo;
                    }
                    catch {   // Nie podaje parametru błędu, ponieważ chcę jedynie poinformaować o nieprawidłowym formacie danych.
                        isCorrectContent[errorCorrectContent.Count - 1] = false;
                        errorCorrectContent[errorCorrectContent.Count - 1] = errorMessage;
                    }
                }
            }
        }
        public class Data {
            public static void SortData(int mode) {
                bool isEnd = false;
                string cell = "";
                while (isEnd == false) {   // Sortowanie graczy względem ilości zdobytych punktów.
                    isEnd = true;
                    for (int i = 0; i < modePlayersInfo[mode].Count - 1; i++) {
                        // Porównujemy po ilości zdobytych punktów (kolumna 1), zmieniamy warunek na < 0
                        if (int.Parse(modePlayersInfo[mode][i][1]) < int.Parse(modePlayersInfo[mode][i + 1][1])) {
                            // Zamiana miejscami całego wiersza
                            for (int j = 0; j < modePlayersInfo[mode][i].Count; j++) {
                                cell = modePlayersInfo[mode][i][j];
                                modePlayersInfo[mode][i][j] = modePlayersInfo[mode][i + 1][j];
                                modePlayersInfo[mode][i + 1][j] = cell;
                            }
                            isEnd = false;
                        }
                    }
                };
            }
            public static void RenderData(int mode) {
                // WAŻNE: Zrób w opcjach tak, aby można było sprawdzić wyniki wszystkich graczy lub 10 najlepszych
                // i dostosujpod tym względem sprawdzenie długości nazwy najdłuszżego gracza pod tym względem!



                // Najpierw zrób działające opcje!






                string space_TH = "";
                string minus_TH = "";
                string space_TD = "";
                string place = "";
                int longestSpace = -1;
                int playerLength = 0;
                int longestFirstCol = 0;
                int firstColAdd = 0;
                int playersLimit = 10;   // Limit wyświetlanych graczy.
                for (int i = 0; i < modePlayersInfo[mode].Count; i++) {                  // Najpierw posortuje ich, bo ja ci z najdłuższą nazwą zostali dodani na początku, to będą uwzględnieni, nawet pomimo ich niższego wyniku niż TOP 10.
                    playerLength = modePlayersInfo[mode][i][0].Length;
                    if (playerLength > longestSpace) {
                        longestSpace = playerLength;
                    }
                }
                longestSpace -= 6;   // Player (odjąć długość)
                longestSpace = (longestSpace <= 0) ? 0 : longestSpace;
                longestFirstCol = longestSpace + 6;
                for (int i = 0; i < longestSpace; i++) {
                    space_TH += " ";
                    minus_TH += "-";
                }



                Console.WriteLine("|" + minus_TH + "---------------------------------------------------|");
                Console.WriteLine("| PLACE | PLAYER" + space_TH + " | SCORE | SUNKEN | LOSS | ACCURATE |");
                Console.WriteLine("|" + minus_TH + "---------------------------------------------------|");
                if (playersLimit_OPTION == "limit") {
                    playersLimit = (modePlayersInfo[mode].Count >= playersLimit) ? playersLimit : modePlayersInfo[mode].Count;   // Ograniczony limit wyświetlania graczy w rankingu.
                } else if (playersLimit_OPTION == "no-limit") {
                    playersLimit = modePlayersInfo[mode].Count;   // Wyświetlanie graczy bez limitu.
                }
                for (int i = 0; i < playersLimit; i++) {
                    Console.Write("| ");
                    for (int j = 0; j < 5; j++) {
                        if (j == 0) {
                            place = (i + 1).ToString() + ".";
                            space_TD = "";
                            firstColAdd = 5 - place.Length;   // 5 - PLACE
                            for (int k = 0; k < firstColAdd; k++) {
                                space_TD += " ";
                            }
                            Console.Write(space_TD + place + " | ");
                            space_TD = "";
                            firstColAdd = longestFirstCol - modePlayersInfo[mode][i][j].Length;
                            for (int k = 0; k < firstColAdd; k++) {
                                space_TD += " ";
                            }
                            Console.Write(modePlayersInfo[mode][i][j] + space_TD + " | ");
                        } else if (j == 1) {
                            space_TD = "";
                            firstColAdd = 5 - modePlayersInfo[mode][i][j].Length;   // 5 - Score
                            for (int k = 0; k < firstColAdd; k++) {
                                space_TD += " ";
                            }
                            Console.Write(space_TD + modePlayersInfo[mode][i][j] + " | ");
                        } else if (j == 2) {
                            Console.Write("     " + modePlayersInfo[mode][i][j] + " | ");
                        } else if (j == 3) {
                            Console.Write("   " + modePlayersInfo[mode][i][j] + " | ");
                        } else if (j == 4) {
                            space_TD = "";
                            firstColAdd = 8 - modePlayersInfo[mode][i][j].Length;   // 8 - ACCURATE
                            for (int k = 0; k < firstColAdd; k++) {
                                space_TD += " ";
                            }
                            Console.Write(space_TD + modePlayersInfo[mode][i][j] + " | ");
                        }
                    }
                    Console.WriteLine("\n|" + minus_TH + "---------------------------------------------------|");




                }
            }
        }
    }
}
