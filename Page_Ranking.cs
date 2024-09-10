using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using Library_GlobalMethods;
using Page_Menu;

namespace Page_Ranking {
    public class Ranking {
        public static bool isPage = true;
        public static bool isCorrSign = false;
        public static string[] buttons = { "PVC Mode" };
        public static int currentButton = buttons.Length;
        public static string playersLimit_OPTION = "no-limit";   // "no-limit" / "limit"
        public static List<string> players = new List<string>();
        public static List<List<string>> playersInfo = new List<List<string>>();
        public void RenderPage() {
            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.RenderButtons(buttons, currentButton);
                GlobalMethod.RenderDottedLine(64);
                RenderInfo(currentButton);
                key = LoopCorrectKey(key);
                MoveButtons(key);
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
        public static void RenderInfo(int currentButton) {
            switch (currentButton) {
                case 1: PVC.ShowRanking(); break;
            }
        }
        public static void MenuReturn() {
            isPage = false;
            MenuPage.isPage = true;
            MenuPage.Menu();
        }
        public static ConsoleKeyInfo LoopCorrectKey(ConsoleKeyInfo key) {
            while (isCorrSign == false) {   // Pêtla ta uniemo¿liwia prze³adowanie strony kiedy kliknie siê niew³aœciwy klawisz.
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.S || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.Backspace) {
                    isCorrSign = true;
                }
            }
            isCorrSign = false;
            if (key.Key == ConsoleKey.Backspace) MenuReturn();
            return key;
        }
        public static void MoveButtons(ConsoleKeyInfo key) {
            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W) {   // Poruszanie siê po przyciskach (obliczenia):
                currentButton = (currentButton < buttons.Length) ? currentButton += 1 : currentButton;
            } else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S) {
                currentButton = (currentButton > 1) ? currentButton -= 1 : currentButton;
            }
        }

        public class PVC {
            public static void ShowRanking() {
                bool isFile = UploadFile("players_PVC.txt");
                if (isFile == true) {
                    Sort();
                    RenderRanking();
                    // WA¯NE: Zrób w opcjach tak, aby mo¿na by³o sprawdziæ wyniki wszystkich graczy lub 10 najlepszych
                    // i dostosujpod tym wzglêdem sprawdzenie d³ugoœci nazwy najd³usz¿ego gracza pod tym wzglêdem!
                }
            }
            public static bool UploadFile(string filePath) {
                players.Clear();
                playersInfo.Clear();
                bool isFile = false;
                try {
                    isFile = true;
                    string fileContent = File.ReadAllText(filePath);
                    string[] playerDetails = null;
                    players = new List<string>(fileContent.Split('*'));
                    for (int i = 0; i < players.Count; i++) {   // Ka¿dy gracz ma 5 informacji oddzielonych znakiem "#":
                        playerDetails = players[i].Split('#');
                        playersInfo.Add(new List<string>());
                        for (int j = 0; j < playerDetails.Length; j++) {
                            playersInfo[i].Add(playerDetails[j]);
                        }
                    }
                }
                catch (IOException ex) {
                    isFile = false;
                    Console.WriteLine("An error has been detected while reading the file:\n");
                    Console.WriteLine(ex.Message);
                }
                return isFile;
            }
            public static void Sort() {
                bool isEnd = false;
                while (isEnd == false) {   // Sortowanie graczy wzglêdem iloœci zdobytych punktów.
                    isEnd = true;
                    for (int i = 0; i < playersInfo.Count - 1; i++) {
                        // Porównujemy po iloœci zdobytych punktów (kolumna 1), zmieniamy warunek na < 0
                        if (int.Parse(playersInfo[i][1]) < int.Parse(playersInfo[i + 1][1])) {
                            // Zamiana miejscami ca³ego wiersza
                            for (int j = 0; j < playersInfo[i].Count; j++) {
                                string cell = playersInfo[i][j];
                                playersInfo[i][j] = playersInfo[i + 1][j];
                                playersInfo[i + 1][j] = cell;
                            }
                            isEnd = false;
                        }
                    }
                };
            }
            public static void RenderRanking() {
                string space_TH = "";
                string minus_TH = "";
                string space_TD = "";
                string place = "";
                int longestSpace = -1;
                int playerLength = 0;
                int longestFirstCol = 0;
                int firstColAdd = 0;
                int playersLimit = 10;   // Limit wyœwietlanych graczy.
                for (int i = 0; i < players.Count; i++) {                  // Najpierw posortuje ich, bo ja ci z najd³u¿sz¹ nazw¹ zostali dodani na pocz¹tku, to bêd¹ uwzglêdnieni, nawet pomimo ich ni¿szego wyniku ni¿ TOP 10.
                    playerLength = playersInfo[i][0].Length;
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
                    playersLimit = (players.Count >= playersLimit) ? playersLimit : players.Count;   // Ograniczony limit wyœwietlania graczy w rankingu.
                } else if (playersLimit_OPTION == "no-limit") {
                    playersLimit = players.Count;   // Wyœwietlanie graczy bez limitu.
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
                            firstColAdd = longestFirstCol - playersInfo[i][j].Length;
                            for (int k = 0; k < firstColAdd; k++) {
                                space_TD += " ";
                            }
                            Console.Write(playersInfo[i][j] + space_TD + " | ");
                        } else if (j == 1) {
                            space_TD = "";
                            firstColAdd = 5 - playersInfo[i][j].Length;   // 5 - Score
                            for (int k = 0; k < firstColAdd; k++) {
                                space_TD += " ";
                            }
                            Console.Write(space_TD + playersInfo[i][j] + " | ");
                        } else if (j == 2) {
                            Console.Write("     " + playersInfo[i][j] + " | ");
                        } else if (j == 3) {
                            Console.Write("   " + playersInfo[i][j] + " | ");
                        } else if (j == 4) {
                            space_TD = "";
                            firstColAdd = 8 - playersInfo[i][j].Length;   // 8 - ACCURATE
                            for (int k = 0; k < firstColAdd; k++) {
                                space_TD += " ";
                            }
                            Console.Write(space_TD + playersInfo[i][j] + " | ");
                        }
                    }
                    Console.WriteLine("\n|" + minus_TH + "---------------------------------------------------|");
                }
            }
        }
    }
}