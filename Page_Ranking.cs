using System;
using System.Collections.Generic;
using System.IO;
using Library_GlobalMethods;
using Page_Menu;

namespace Page_Ranking {
    public class Ranking {
        public static bool isPage = true;
        public static bool isCorrSign = false;
        public static string[] buttons = { "PVC Mode" };
        public static int currentButton = 0;   // Zawsze pierwszy, bo chcê mieæ kursor na górze!
        public static string playersLimit_OPTION = "no-limit";   // "no-limit" / "limit"
        public void RenderPage() {
            ConsoleKeyInfo key = new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);
            while (isPage == true) {
                Console.Clear();
                RenderTitle();
                GlobalMethod.RenderButtons(buttons, currentButton);
                GlobalMethod.RenderDottedLine(64);
                RenderContent(currentButton);
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
        public static void RenderContent(int currentButton) {
            switch (currentButton) {
                case 0: PVC.ShowRanking(); break;
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
        public class PVC {
            public static void ShowRanking() {
                (bool, string) isFile = UploadFile("players_PVC.txt");
                if (isFile.Item1 == true) {
                    List<List<string>> playersInfo = UploadData(isFile.Item2);
                    Sort(playersInfo);
                    Render(playersInfo);
                }
            }
            public static (bool, string) UploadFile(string filePath) {
                (bool, string) fileInfo = (false, filePath);   // Krotkê nienazwan¹ mo¿na modyfikowaæ, a nazwan¹ nie.
                try {
                    fileInfo.Item1 = true;
                    string fileContent = File.ReadAllText(filePath);
                    fileInfo.Item2 = filePath;
                }
                catch (IOException error) {
                    fileInfo.Item1 = false;
                    Console.WriteLine("An error has been detected while reading the file:\n");
                    Console.WriteLine(error.Message);
                }
                return fileInfo;
            }
            public static List<List<string>> UploadData(string filePath) {
                string fileContent = File.ReadAllText(filePath);
                string[] playerDetails = null;
                List<string> players = new List<string>(fileContent.Split('*'));
                List<List<string>> playersInfo = new List<List<string>>();
                for (int i = 0; i < players.Count; i++) {   // Ka¿dy gracz ma 5 informacji oddzielonych znakiem "#":
                    playerDetails = players[i].Split('#');
                    playersInfo.Add(new List<string>());
                    for (int j = 0; j < playerDetails.Length; j++) {
                        playersInfo[i].Add(playerDetails[j]);
                    }
                }
                return playersInfo;
            }
            public static void Sort(List<List<string>> playersInfo) {
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
            public static void Render(List<List<string>> playersInfo) {
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
                for (int i = 0; i < playersInfo.Count; i++) {                  // Najpierw posortuje ich, bo ja ci z najd³u¿sz¹ nazw¹ zostali dodani na pocz¹tku, to bêd¹ uwzglêdnieni, nawet pomimo ich ni¿szego wyniku ni¿ TOP 10.
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
                    playersLimit = (playersInfo.Count >= playersLimit) ? playersLimit : playersInfo.Count;   // Ograniczony limit wyœwietlania graczy w rankingu.
                } else if (playersLimit_OPTION == "no-limit") {
                    playersLimit = playersInfo.Count;   // Wyœwietlanie graczy bez limitu.
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