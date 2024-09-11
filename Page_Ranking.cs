using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Library_GlobalMethods;
using Page_Menu;

namespace Page_Ranking {
    public class Ranking {
        public static bool isPage = true;
        public static bool isCorrSign = false;
        public static string[] buttons = { "PVC Mode" };
        public static int currentButton = 0;   // Zawsze pierwszy, bo chc� mie� kursor na g�rze!
        public static string playersLimit_OPTION = "no-limit";   // "no-limit" / "limit"
        public static List<bool> isFile = new List<bool>();  // plik = index
        public static List<string> error = new List<string>();  // b��d odczutu bie��cego pliku = index
        public static List<List<List<string>>> modePlayersInfo = new List<List<List<string>>>();
        public void RenderPage() {
            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);
            Upload.UploadRanking("players_PVC.txt");
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.RenderButtons(buttons, currentButton);
                GlobalMethod.RenderDottedLine(64);
                Upload.ShowRanking(currentButton);
                key = LoopCorrectKey(key);
                currentButton = GlobalMethod.MoveButtons(buttons, currentButton, key);
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
            GlobalMethod.RenderDottedLine(64);
            Console.WriteLine("RANKING: | Moving: arrows/[W][S] | Back to menu: [Backspace]\n");
        }
        public static ConsoleKeyInfo LoopCorrectKey(ConsoleKeyInfo key) {
            while (isCorrSign == false) {   // P�tla ta uniemo�liwia prze�adowanie strony kiedy kliknie si� niew�a�ciwy klawisz.
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.S || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.Backspace) {
                    isCorrSign = true;
                }
            }
            isCorrSign = false;
            if (key.Key == ConsoleKey.Backspace) MenuReturn();
            return key;
        }
        public static void MenuReturn() {
            isPage = false;
            MenuPage.isPage = true;
            MenuPage.Menu();
        }
    }
    public class Upload : Ranking {   // Dlaczego zdecydowa�em si� na dziedziczenie? Poniewa� dwie medoty z tej klasy s� wywo�ywane w klasie "Ranking" w r�nych miejscach. Aby nie zdezorientowa� programist� umie�ci�em t� klas� za klas� "Ranking" bezpo�rednio w tej samej przestrzeni nazw.
        public static void UploadRanking(string filePath) {   // Panel kontrolny
            (bool, string, string) fileInfo = GlobalMethod.UploadFile(filePath);
            isFile.Add(fileInfo.Item1);
            error.Add(fileInfo.Item3);
            if (isFile[isFile.Count - 1] == true) {   // Dodaje si� w linii z "isFile.Add(fileInfo.Item1)", a tutaj bierzemy d�ugo�� listy dynamicznej "isFile" - 1, czyli ostatni indeks, tzn. aktualny plik. Ha! Jestem geniuszem!
                UploadData(fileInfo.Item2);
            }
        }
        public static void UploadData(string filePath) {
            string fileContent = File.ReadAllText(filePath);
            string[] info = null;
            List<List<string>> playersInfo = new List<List<string>>();
            List<string> players = new List<string>(fileContent.Split('*'));
            for (int i = 0; i < players.Count; i++) {   // Ka�dy gracz ma 5 informacji oddzielonych znakiem "#":
                //playersInfo.Add(new List<string>(players[i].Split('#')));  // Spr�buj!
                info = players[i].Split('#'); // Ta
                playersInfo.Add(new List<string>()); // i Ta = 1 linijka, jak z "List<string> players"
                for (int j = 0; j < info.Length; j++) {
                    playersInfo[i].Add(info[j]);
                }
            }
            modePlayersInfo.Add(playersInfo);
        }
        public static void ShowRanking(int mode) {   // Panel kontrolny
            if (isFile[currentButton] == true) {   // Tutaj bierzemy "currentButton", poniewa� nie dodajemy na bierz�co kolejnych zmienneych dla listy dynamicznej "isFile", sk�d (tam) mogli�my od razu walidowa� obs�ug� metody "UploadData".
                SortData(mode);
                RenderData(mode);
            } else {
                Console.WriteLine(error[currentButton]);
            }
        }
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
    }
}