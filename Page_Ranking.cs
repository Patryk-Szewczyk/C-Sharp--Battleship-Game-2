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
        public static int currentButton = 0;   // Zawsze pierwszy, bo chc� mie� kursor na g�rze!
        public static List<ConsoleKey> usingKeys = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace };
        public static string playersLimit_OPTION = "no-limit";   // "no-limit" / "limit"
        public const int detailsAmount = 5;
        public static List<bool> isFile = new List<bool>();  // plik = index
        public static List<bool> isCorrectContent = new List<bool>();  // plik = index
        public static List<string> errorFile = new List<string>();  // b��d odczutu bie��cego pliku = index
        public static List<string> errorCorrectContent = new List<string>();  // b��d odczutu bie��cego pliku = index
        public static List<List<List<string>>> modePlayersInfo = new List<List<List<string>>>();
        public void RenderPage() {
            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);
            //Upload.SearchFile("players_PVC.txt");   // Przeniesiono do Intro.   // Je�eli chcesz podpi�� kolejny ranking jedyne co trzeba zrobi�, to doda� nazw� przycisku i skopiowa� t� metod� z podaniem nazwy pliku z rozszerzeniem.
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
            if (isFile[currentButton] == true) {   // Tutaj bierzemy "currentButton", poniewa� nie dodajemy na bierz�co kolejnych zmienneych dla listy dynamicznej "isFile", sk�d (tam) mogli�my od razu walidowa� obs�ug� metody "UploadData".
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
        public class Upload {   // Dlaczego nie przenios�em tych klas bezpo�rednio do namespace, a zamiast tego umie�ci�em je wewn�trz klasy "Ranking". (W klasie "Options" jest tak samo.) Poniewa� ta klasa i klasa "Options" posiada klas� "Upload". W klasie "Program" metoda "Main" wywo�uje metod� "UploadOptions" klasy "Upload". W przypadku przeniesienia tej klasy bezpo�rednio do namespace i u�ycia dziedziczenia by�yby problemy z odpowiednim wskazaniem w�a�ciwej metody oraz straci�oby to na czytelno�ci i u�yteczno�ci kodu w kontek�cie tego typu problemu. Z powodu braku estetyki w przypadku tej sytuacji, aby naprawi� estetyk� tego typu styl (klasy potomne) zosta� zaimplementowany na sta�e w tym projekcie.
            public static void SearchFile(string filePath) {   // Panel kontrolny
                (bool, string, string) fileInfo = GlobalMethod.UploadFile(filePath);
                isFile.Add(fileInfo.Item1);
                errorFile.Add(fileInfo.Item3);
                if (isFile[isFile.Count - 1] == true) {   // Dodaje si� w linii z "isFile.Add(fileInfo.Item1)", a tutaj bierzemy d�ugo�� listy dynamicznej "isFile" - 1, czyli ostatni indeks, tzn. aktualny plik. Ha! Jestem geniuszem!
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
                    try {   // Rozk�ad danych
                        List<string> players = new List<string>(fileContent.Split('*'));
                        for (int i = 0; i < players.Count; i++) {
                            playersInfo.Add(new List<string>(players[i].Split('#')));
                        }
                        for (int i = 0; i < playersInfo.Count; i++) {   // Sprawdzenie czy kt�ra� z informacji ka�dego gracza jest pusta.
                            for (int j = 0; j < playersInfo[i].Count; j++) {
                                if (playersInfo[i][j] == "") {
                                    isCorrectContent[errorCorrectContent.Count - 1] = false;
                                    errorCorrectContent[errorCorrectContent.Count - 1] = errorMessage;
                                    break;
                                }
                            }
                        }
                        for (int i = 0; i < playersInfo.Count; i++) {   // Sprawdzenie czy ka�dy gracz ma tak� sam� liczb� danych przedzielonych znakiem "#"
                            if (playersInfo[i].Count != detailsAmount) {
                                isCorrectContent[errorCorrectContent.Count - 1] = false;
                                errorCorrectContent[errorCorrectContent.Count - 1] = errorMessage;
                                break;
                            }
                        }
                        if (isCorrectContent[isCorrectContent.Count - 1] == true) modePlayersInfo[errorCorrectContent.Count - 1] = playersInfo;
                    }
                    catch {   // Nie podaje parametru b��du, poniewa� chc� jedynie poinformaowa� o nieprawid�owym formacie danych.
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
                while (isEnd == false) {   // Sortowanie graczy wzgl�dem ilo�ci zdobytych punkt�w.
                    isEnd = true;
                    for (int i = 0; i < modePlayersInfo[mode].Count - 1; i++) {
                        // Por�wnujemy po ilo�ci zdobytych punkt�w (kolumna 1), zmieniamy warunek na < 0
                        if (int.Parse(modePlayersInfo[mode][i][1]) < int.Parse(modePlayersInfo[mode][i + 1][1])) {
                            // Zamiana miejscami ca�ego wiersza
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
                // WA�NE: Zr�b w opcjach tak, aby mo�na by�o sprawdzi� wyniki wszystkich graczy lub 10 najlepszych
                // i dostosujpod tym wzgl�dem sprawdzenie d�ugo�ci nazwy najd�usz�ego gracza pod tym wzgl�dem!



                // Najpierw zr�b dzia�aj�ce opcje!






                string space_TH = "";
                string minus_TH = "";
                string space_TD = "";
                string place = "";
                int longestSpace = -1;
                int playerLength = 0;
                int longestFirstCol = 0;
                int firstColAdd = 0;
                int playersLimit = 10;   // Limit wy�wietlanych graczy.
                for (int i = 0; i < modePlayersInfo[mode].Count; i++) {                  // Najpierw posortuje ich, bo ja ci z najd�u�sz� nazw� zostali dodani na pocz�tku, to b�d� uwzgl�dnieni, nawet pomimo ich ni�szego wyniku ni� TOP 10.
                    playerLength = modePlayersInfo[mode][i][0].Length;
                    if (playerLength > longestSpace) {
                        longestSpace = playerLength;
                    }
                }
                longestSpace -= 6;   // Player (odj�� d�ugo��)
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
                    playersLimit = (modePlayersInfo[mode].Count >= playersLimit) ? playersLimit : modePlayersInfo[mode].Count;   // Ograniczony limit wy�wietlania graczy w rankingu.
                } else if (playersLimit_OPTION == "no-limit") {
                    playersLimit = modePlayersInfo[mode].Count;   // Wy�wietlanie graczy bez limitu.
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
            public static List<List<string>> UpdateData(string filePath) {   // Metoda jest wywo�ywana po kiedy "pierwszy warunek walidacji danych" jest spe�niony (w "Options").
                string fileContent = File.ReadAllText(filePath);
                List<List<string>> playersInfo = new List<List<string>>();
                List<string> players = new List<string>(fileContent.Split('*'));
                for (int i = 0; i < players.Count; i++) {
                    playersInfo.Add(new List<string>(players[i].Split('#')));
                }
                return playersInfo;
            }
        }
    }
}
