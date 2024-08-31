using System;
using System.IO;
using Page_Menu;

namespace Page_Ranking {
    public class PageRanking {
        public static bool isRankingLoop = true;
        public static bool isCorrectSign = false;
        public static string[] rankingButtons = { "PVC Mode" };
        public static int rankingButtNum = rankingButtons.Length;
        public static string playersLimit_OPTION = "no-limit";   // "no-limit" / "limit"
        public void Ranking() {
            System.ConsoleKey key = System.ConsoleKey.Backspace;   // Dowolny niew�a�ciwy klawisz.
            System.ConsoleKeyInfo corr_key;
            while (isRankingLoop == true) {
                Console.Clear();
                Console.WriteLine("BBBBBBB     BBBB    BBBB  BB  BB    BB  BB  BBBB  BB   BBBBBB ");
                Console.WriteLine("BB    BB   BB  BB   BB BB BB  BB   BB   BB  BB BB BB  BB    BB");
                Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB  BB    BB  BB BB BB  BB      ");
                Console.WriteLine("BBBBBBB   BBBBBBBB  BB BB BB  BBBBB     BB  BB BB BB  BB  BBB ");
                Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB  BB    BB  BB BB BB  BB  B BB");
                Console.WriteLine("BB    BB  BB    BB  BB BB BB  BB   BB   BB  BB BB BB  BB    BB");
                Console.WriteLine("BB    BB  BB    BB  BB  BBBB  BB    BB  BB  BB  BBBB   BBBBBB ");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("RANKING: | Moving: arrows/[W][S] | Back to menu: [Backspace]\n");
                for (int i = 0, j = rankingButtons.Length; i < rankingButtons.Length; i++, j--) {
                    if (j == rankingButtNum) {
                        Console.WriteLine("> " + rankingButtons[i]);
                    } else {
                        Console.WriteLine("  " + rankingButtons[i]);
                    }
                }
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                switch (rankingButtNum) {
                    /*case 2:
                        PageRanking pvp = new PageRanking();
                        pvp.Scores_PVP();
                        break;*/
                    case 1:
                        PageRanking pvc = new PageRanking();
                        pvc.Scores_PVC();
                        break;
                }
                while (isCorrectSign == false) {   // P�tla ta uniemo�liwia prze�adowanie strony kiedy kliknie si� niew�a�ciwy klawisz.
                    corr_key = Console.ReadKey(true);
                    if (corr_key.Key == System.ConsoleKey.W || corr_key.Key == System.ConsoleKey.S || corr_key.Key == System.ConsoleKey.UpArrow || corr_key.Key == System.ConsoleKey.DownArrow || corr_key.Key == System.ConsoleKey.Backspace) {
                        isCorrectSign = true;
                        key = corr_key.Key;
                    }
                }
                isCorrectSign = false;
                // Poruszanie si� po przyciskach (obliczenia):
                if (key == System.ConsoleKey.UpArrow || key == System.ConsoleKey.W) {
                    rankingButtNum = (rankingButtNum < rankingButtons.Length) ? rankingButtNum += 1 : rankingButtNum;
                } else if (key == System.ConsoleKey.DownArrow || key == System.ConsoleKey.S) {
                    rankingButtNum = (rankingButtNum > 1) ? rankingButtNum -= 1 : rankingButtNum;
                } else if (key == System.ConsoleKey.Backspace) {
                    isRankingLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
        public void Scores_PVC() {
            string filePath = "players_PVC.txt";
            try
            {
                // Odczytaj ca�y tekst z pliku
                string fileContent = File.ReadAllText(filePath);
                string[] players = fileContent.Split('*');
                string[,] playersNested = new string[players.Length, 5];
                string[] playerDetails = null;
                for (int i = 0; i < players.Length; i++) {
                    // Ka�dy gracz ma 5 informacji oddzielonych znakiem "#":
                    playerDetails = players[i].Split('#');
                    for (int j = 0; j < playerDetails.Length; j++) {
                        playersNested[i, j] = playerDetails[j];
                    }
                }
                // Sortowanie graczy wzgl�dem ilo�ci zdobytych punkt�w:
                bool isEnd = false;
                while (isEnd == false) {
                    isEnd = true;
                    for (int i = 0; i < players.Length - 1; i++) {
                        // Por�wnujemy po ilo�ci zdobytych punkt�w (kolumna 1), zmieniamy warunek na < 0
                        if (int.Parse(playersNested[i, 1]) < int.Parse(playersNested[i + 1, 1])) {
                            // Zamiana miejscami ca�ego wiersza
                            for (int j = 0; j < playersNested.GetLength(1); j++) {
                                string cell = playersNested[i, j];
                                playersNested[i, j] = playersNested[i + 1, j];
                                playersNested[i + 1, j] = cell;
                            }
                            isEnd = false;
                        }
                    }
                };
                // Odczytaj ca�y tekst z pliku | OK
                /*for (int i = 0; i < players.Length; i++)
                {
                    Console.Write(playersNested[i, 0]);
                    Console.WriteLine();
                }*/
                // Wy�wietlanie zawarto�ci playersName dla sprawdzenia
                
                // WA�NE: Zr�b w opcjach tak, aby mo�na by�o sprawdzi� wyniki wszystkich graczy lub 10 najlepszych /////////////////////////////////////////
                // i dostosujpod tym wzgl�dem sprawdzenie d�ugo�ci nazwy najd�usz�ego gracza pod tym wzgl�dem! /////////////////////////////////////////////
                
                string space_TH = "";
                string minus_TH = "";
                string space_TD = "";
                string place = "";
                int longestSpace = -1;
                int playerLength = 0;
                int longestFirstCol = 0;
                int firstColAdd = 0;
                int playersLimit = 10;   // Limit wy�wietlanych graczy.
                for (int i = 0; i < players.Length; i++) {                  // Najpierw posortuje ich, bo ja ci z najd�u�sz� nazw� zostali dodani na pocz�tku,
                                                                            // to b�d� uwzgl�dnieni, nawet pomimo ich ni�szego wyniku ni� TOP 10.
                    playerLength = playersNested[i, 0].Length;
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
                Console.WriteLine("| PLACE | PLAYER"+ space_TH + " | SCORE | SUNKEN | LOSS | ACCURATE |");
                Console.WriteLine("|" + minus_TH + "---------------------------------------------------|");
                if (PageRanking.playersLimit_OPTION == "limit") {
                    playersLimit = (players.Length >= playersLimit) ? playersLimit : players.Length;   // Ograniczony limit wy�wietlania graczy w rankingu.
                } else if (PageRanking.playersLimit_OPTION == "no-limit") {
                    playersLimit = players.Length;   // Wy�wietlanie graczy bez limitu.
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
                            firstColAdd = longestFirstCol - playersNested[i, j].Length;
                            for (int k = 0; k < firstColAdd; k++) {
                                space_TD += " ";
                            }
                            Console.Write(playersNested[i, j] + space_TD + " | ");
                        } else if (j == 1) {
                            space_TD = "";
                            firstColAdd = 5 - playersNested[i, j].Length;   // 5 - Score
                            for (int k = 0; k < firstColAdd; k++) {
                                space_TD += " ";
                            }
                            Console.Write(space_TD + playersNested[i, j] + " | ");
                        } else if (j == 2) {
                            Console.Write("     " + playersNested[i, j] + " | ");
                        } else if (j == 3) {
                            Console.Write("   " + playersNested[i, j] + " | ");
                        } else if (j == 4) {
                            space_TD = "";
                            firstColAdd = 8 - playersNested[i, j].Length;   // 8 - ACCURATE
                            for (int k = 0; k < firstColAdd; k++) {
                                space_TD += " ";
                            }
                            Console.Write(space_TD + playersNested[i, j] + " | ");
                        }
                    }
                    Console.WriteLine("\n|" + minus_TH + "---------------------------------------------------|");
                }
            } catch (IOException ex)
            {
                Console.WriteLine("An error has been detected while reading the file:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}