using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Library_GlobalMethods;
using Page_Menu;
using Page_Options;

namespace Page_Ranking {
    public class Ranking {
        public static int page_ID = 2;
        public static bool isPage = false;
        public static int pageLineLength = 64;
        public static string[] buttons = { "PVC Mode", "PVP Mode"};
        public static int currentButton = 0;   // Zawsze pierwszy, bo chcę mieć kursor na górze!
        public static List<ConsoleKey> usingKeys = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Backspace };
        public static string playersLimit_OPTION = "no-limit";   // "no-limit" / "limit"
        public const int detailsAmount = 6;
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
                string errorMessage = "The data format is \"" + filePath + "\" not correct. It should be:\n" + GenerateFormat(detailsAmount);
                string content = File.ReadAllText(filePath);
                string fileContent = content;   // WCZEŚNIEJ: GlobalMethod.TrimAllContent(content); Opuściłe, aby "TrimAllContent" nie pożerał spacji w nazwie użytkowników. Potrzeba ogólnie lepszej walidacji (szczególnie na odpowiednie dopuszczalne wartości dla każdego z pól danych użytkowanika), ale lepsza taka niż nic - podstawowa.
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
            public static string GenerateFormat(int optionsNum) {   //user#data#data#data#data#data*user#data#data#data#data#data
                string format = "";
                for (int i = 0; i < 2; i++) {
                    if (i == 0) format += "user#";
                    if (i == 1) format += "*user#";
                    for (int j = 0; j < optionsNum; j++) {
                        format += "data";
                        if (j < optionsNum - 1) format += "#";
                    }
                }
                return format;
            }
        }
        public class Data {
            public static void SortData(int mode) {
                bool isEnd = false;
                string field = "";
                int scoreIdx = 2;
                while (isEnd == false) {   // Sortowanie bąbelkowe graczy względem ilości zdobytych punktów.
                    isEnd = true;
                    for (int i = 0; i < modePlayersInfo[mode].Count - 1; i++) {
                        if (int.Parse(modePlayersInfo[mode][i][scoreIdx]) < int.Parse(modePlayersInfo[mode][i + 1][scoreIdx])) {
                            for (int j = 0; j < modePlayersInfo[mode][i].Count; j++) {
                                field = modePlayersInfo[mode][i][j];
                                modePlayersInfo[mode][i][j] = modePlayersInfo[mode][i + 1][j];
                                modePlayersInfo[mode][i + 1][j] = field;
                            }
                            isEnd = false;
                        }
                    }
                };
            }
            public static void RenderData(int mode) {
                string secondColTit = "PLAYER";
                int players = modePlayersInfo[mode].Count;
                int limit = (Options.options[Options.optTopPlayers] == "ON") ? 10 : players;
                limit = (players < limit) ? players : limit;
                (string, string, int) tuple = MakeSpaces(mode, limit, secondColTit);
                string playerSpace = tuple.Item1;
                string minusSpace = tuple.Item2;
                int longestNameSpace = tuple.Item3;
                Console.WriteLine("|------------------------" + minusSpace + "-----------------------------------------|");
                Console.WriteLine("| PLACE | " + secondColTit + playerSpace + " | BEST SCORE | BATTLE | SUNKEN | LOSS | ACCURATE |");
                Console.WriteLine("|------------------------" + minusSpace + "-----------------------------------------|");
                string data = "";
                int place = 0;
                int score = 0;
                int sunken = 0;
                int loss = 0;
                int accurate = 0;
                string extraSpace = "";
                for (int i = 0; i < limit; i++) {
                    Console.Write("| ");
                    place++;
                    if (place <= 9) Console.Write("   " + place + ". | ");
                    else if (place > 9 && place <= 99) Console.Write("  " + place + ". | ");
                    else if (place > 99 && place <= 999) Console.Write(" " + place + ". | ");
                    else if (place > 999 && place <= 9999) Console.Write(place + ". | ");
                    for (int j = 0; j < Ranking.detailsAmount; j++) {
                        if (j == 1) data = modePlayersInfo[mode][i][j + 1];   // Cała ta sztuczka polega na zamianie miejscamidwóch pól danych z indeksu [1] i [2] (zamiana naprzemian).
                        else if (j == 2) data = modePlayersInfo[mode][i][j - 1];   // Nie chciałem zmieniać układu danych w pliku, bo zaburzyłoby to formę aktualnej metody resetu danych graczy, przez co musiałbym tam robić tego samego typu operację, co tutaj.
                        else data = modePlayersInfo[mode][i][j];
                        switch (j) {
                            case 0:
                                extraSpace = ExtraPlayerSpace(longestNameSpace, data.Length);
                                Console.Write(data + extraSpace + " | ");
                                break;
                            case 1:
                                score = int.Parse(data);
                                if (score <= 9) Console.Write("         " + data + " | ");
                                else if (score > 9 && score <= 99) Console.Write("        " + data + " | ");
                                else if (score > 99 && score <= 999) Console.Write("       " + data + " | ");
                                else if (score > 999 && score <= 9999) Console.Write("      " + data + " | ");
                                else if (score > 9999 && score <= 99999) Console.Write("     " + data + " | ");
                                else if (score > 99999 && score <= 999999) Console.Write("    " + data + " | ");
                                else if (score > 999999 && score <= 9999999) Console.Write("   " + data + " | ");   // Maksymalny wynik przy aktualnym punktowaniu: 8 812 382 punktów.
                                break;
                            case 2:
                                if (data == "win") Console.Write("   " + data + " | ");
                                else if (data == "win") Console.Write("  " + data + " | ");
                                else if (data == "?") Console.Write("     " + data + " | ");
                                break;
                            case 3:
                                sunken = int.Parse(data);
                                if (sunken <= 9) Console.Write("     " + data + " | ");
                                else if (sunken > 9 && sunken <= 99) Console.Write("    " + data + " | ");
                                break;
                            case 4:
                                loss = int.Parse(data);
                                if (loss <= 9) Console.Write("   " + data + " | ");
                                else if (loss > 9 && loss <= 99) Console.Write("  " + data + " | ");
                                break;
                            case 5:
                                accurate = int.Parse(data.Substring(0, data.Length - 1));
                                if (accurate <= 9) Console.Write("      " + data + " |");   // UWAGA!!!!!!!!!! Ostatni elemnt NIE MA " ", po "|"
                                else if (accurate > 9 && accurate <= 99) Console.Write("     " + data + " |");
                                else if (accurate > 99 && accurate <= 999) Console.Write("    " + data + " |");
                                break;
                        }
                    }
                    Console.WriteLine("\n|" + minusSpace + "-----------------------------------------------------------------|");
                }
            }
            public static (string, string, int) MakeSpaces(int mode, int playersLimit, string secondColTit) {
                int playerLength = 0;
                int longestSpace = 0;
                int limit = (modePlayersInfo[mode].Count < playersLimit) ? modePlayersInfo[mode].Count : playersLimit;
                for (int i = 0; i < limit; i++) {
                    playerLength = modePlayersInfo[mode][i][0].Length;
                    if (playerLength > longestSpace) {
                        longestSpace = playerLength;
                    }
                }
                (string, string, int) space = ("", "", 0);
                space.Item3 = longestSpace;
                longestSpace -= secondColTit.Length;   // Odejmujemy długość tytułu drugiej kolumny ("PLAYERS").
                longestSpace = (longestSpace <= 0) ? 0 : longestSpace;
                for (int i = 0; i < longestSpace; i++) {
                    space.Item1 += " ";
                    space.Item2 += "-";
                }
                return (space.Item1, space.Item2, space.Item3);
            }
            public static string ExtraPlayerSpace(int longestNameLength, int currentNameLength) {
                int length = longestNameLength - currentNameLength;
                string extraSpace = "";
                for (int i = 0; i < length; i++) {
                    extraSpace += " ";
                }
                return extraSpace;
            }
        }
    }
}
