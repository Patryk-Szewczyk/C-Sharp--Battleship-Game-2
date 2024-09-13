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
        public static string[] buttons = { "PVC Mode", "PVP Mode"};
        public static int currentButton = 0;   // Zawsze pierwszy, bo chcê mieæ kursor na górze!
        public static List<ConsoleKey> usingKeys = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace };
        public static string playersLimit_OPTION = "no-limit";   // "no-limit" / "limit"
        public static List<bool> isFile = new List<bool>();  // plik = index
        public static List<bool> isCorrectContent = new List<bool>();  // plik = index
        public static List<string> errorFile = new List<string>();  // b³¹d odczutu bie¿¹cego pliku = index
        public static List<string> errorFileContent = new List<string>();  // b³¹d odczutu bie¿¹cego pliku = index
        public static List<List<List<string>>> modePlayersInfo = new List<List<List<string>>>();
        public void RenderPage() {
            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);
            Upload.UploadRanking("players_PVC.txt", 5);   // Je¿eli chcesz podpi¹æ kolejny ranking jedyne co trzeba zrobiæ, to dodaæ nazwê przycisku i skopiowaæ t¹ metodê z podaniem nazwy pliku z rozszerzeniem.
            Upload.UploadRanking("players_PVP.txt", 5);
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.Page.RenderButtons(buttons, currentButton);
                GlobalMethod.Page.RenderDottedLine(64);
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
            GlobalMethod.Page.RenderDottedLine(64);
            Console.WriteLine("RANKING: | Moving: arrows/[W][S] | Back to menu: [Backspace]\n");
        }
        public static void ShowRanking(int mode) {   // Panel kontrolny
            if (isFile[currentButton] == true) {   // Tutaj bierzemy "currentButton", poniewa¿ nie dodajemy na bierz¹co kolejnych zmienneych dla listy dynamicznej "isFile", sk¹d (tam) mogliœmy od razu walidowaæ obs³ugê metody "UploadData".
                if (isCorrectContent[currentButton] == true) {
                    Data.SortData(mode);
                    Data.RenderData(mode);
                } else {
                    Console.WriteLine(errorFileContent[currentButton]);
                }
            } else {
                Console.WriteLine(errorFile[currentButton]);
            }
        }
    }
    public class Upload : Ranking {   // Dziedziczenie, bo te metody korzystaj¹ ze zmiennej/nych z klasy "Ranking". (Chocia¿ mo¿na umieœciæ te klasy wewn¹trz klasy "Ranking" i efekt taki sam. Chcia³em aby przez dziedziczenie nakierowaæ, ¿e te klasy potrzebuj¹ zmiennych z klasy "Ranking") Dlaczego dziedziczenie i klasa nie znajduje siê na zwen¹trz? Nie wewn¹trz klasy "Ranking", dla lepszej czytelnoœci i ta klasa dziedziczy klasê "Ranking", poniewa¿ u¿yta jej zmiennych statycznik i tym samym nie chcê niepotrzebnie tworzyæ instancji klasy "Ranking".
        public static void UploadRanking(string filePath, int detailsAmount) {   // Panel kontrolny
            (bool, string, string) fileInfo = GlobalMethod.UploadFile(filePath);
            isFile.Add(fileInfo.Item1);
            errorFile.Add(fileInfo.Item3);
            if (isFile[isFile.Count - 1] == true) {   // Dodaje siê w linii z "isFile.Add(fileInfo.Item1)", a tutaj bierzemy d³ugoœæ listy dynamicznej "isFile" - 1, czyli ostatni indeks, tzn. aktualny plik. Ha! Jestem geniuszem!
                ValidateData(fileInfo.Item2, detailsAmount);
            }
        }
        public static void ValidateData(string filePath, int detailsAmount) {
            isCorrectContent.Add(true);
            errorFileContent.Add("");
            modePlayersInfo.Add(new List<List<string>>());
            string errorMessage = "The data format is \"" + filePath + "\" not correct. It should be:\nuser#data#data#data#data*user#data#data#data#data#data";
            string content = File.ReadAllText(filePath);
            string fileContent = GlobalMethod.TrimAllContent(content);
            List<List<string>> playersInfo = new List<List<string>>();
            if (fileContent == "") {
                isCorrectContent[errorFileContent.Count - 1] = false;
                errorFileContent[errorFileContent.Count - 1] = errorMessage;
            } else if (fileContent != "") {
                try {   // Rozk³ad danych
                    List<string> players = new List<string>(fileContent.Split('*'));
                    for (int i = 0; i < players.Count; i++) {
                        playersInfo.Add(new List<string>(players[i].Split('#')));
                    }
                }
                catch {   // Nie podaje parametru b³êdu, poniewa¿ chcê jedynie poinformaowaæ o nieprawid³owym formacie danych.
                    isCorrectContent[errorFileContent.Count - 1] = false;
                    errorFileContent[errorFileContent.Count - 1] = errorMessage;
                }
                finally {   // Walidacja danych
                    for (int i = 0; i < playersInfo.Count; i++) {   // Sprawdzenie czy któraœ z informacji ka¿dego gracza jest pusta.
                        for (int j = 0; j < playersInfo[i].Count; j++) {
                            if (playersInfo[i][j] == "") {
                                isCorrectContent[errorFileContent.Count - 1] = false;
                                errorFileContent[errorFileContent.Count - 1] = errorMessage;
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < playersInfo.Count; i++) {   // Sprawdzenie czy ka¿dy gracz ma tak¹ sam¹ liczbê danych przedzielonych znakiem "#"
                        if (playersInfo[i].Count != detailsAmount) {
                            isCorrectContent[errorFileContent.Count - 1] = false;
                            errorFileContent[errorFileContent.Count - 1] = errorMessage;
                            break;
                        }
                    } //if (playersInfo[playersInfo.Count - 1].Count == detailsAmount) modePlayersInfo[errorFileContent.Count - 1] = playersInfo;
                    if (isCorrectContent[playersInfo.Count - 1] == true) modePlayersInfo[errorFileContent.Count - 1] = playersInfo;
                }
            }
        }
    }
    public class Data : Ranking {
        public static void SortData(int mode) {
            bool isEnd = false;
            string cell = "";
            while (isEnd == false) {   // Sortowanie graczy wzglêdem iloœci zdobytych punktów.
                isEnd = true;
                for (int i = 0; i < modePlayersInfo[mode].Count - 1; i++) {
                    // Porównujemy po iloœci zdobytych punktów (kolumna 1), zmieniamy warunek na < 0
                    if (int.Parse(modePlayersInfo[mode][i][1]) < int.Parse(modePlayersInfo[mode][i + 1][1])) {
                        // Zamiana miejscami ca³ego wiersza
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
            // WA¯NE: Zrób w opcjach tak, aby mo¿na by³o sprawdziæ wyniki wszystkich graczy lub 10 najlepszych
            // i dostosujpod tym wzglêdem sprawdzenie d³ugoœci nazwy najd³usz¿ego gracza pod tym wzglêdem!



            // Najpierw zrób dzia³aj¹ce opcje!






            string space_TH = "";
            string minus_TH = "";
            string space_TD = "";
            string place = "";
            int longestSpace = -1;
            int playerLength = 0;
            int longestFirstCol = 0;
            int firstColAdd = 0;
            int playersLimit = 10;   // Limit wyœwietlanych graczy.
            for (int i = 0; i < modePlayersInfo[mode].Count; i++) {                  // Najpierw posortuje ich, bo ja ci z najd³u¿sz¹ nazw¹ zostali dodani na pocz¹tku, to bêd¹ uwzglêdnieni, nawet pomimo ich ni¿szego wyniku ni¿ TOP 10.
                playerLength = modePlayersInfo[mode][i][0].Length;
                if (playerLength > longestSpace) {
                    longestSpace = playerLength;
                }
            }
            longestSpace -= 6;   // Player (odj¹æ d³ugoœæ)
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
                playersLimit = (modePlayersInfo[mode].Count >= playersLimit) ? playersLimit : modePlayersInfo[mode].Count;   // Ograniczony limit wyœwietlania graczy w rankingu.
            } else if (playersLimit_OPTION == "no-limit") {
                playersLimit = modePlayersInfo[mode].Count;   // Wyœwietlanie graczy bez limitu.
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