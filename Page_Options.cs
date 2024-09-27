using System;
using System.IO;
using System.Collections.Generic;
using Page_Menu;
using Page_PVC;
using Page_Instructions;
using Page_Ranking;
using Page_Options;
using Page_Credits;

namespace Library_GlobalMethods {
    public class GlobalMethod {   // Metody o zasięgu globalnym, które mają niezmiennną formę i mogą przydać się wszędzie.
        public class SoundControl {
            public static void PlaySound(string filepath) {
                try {
                    Menu.currentSound.SoundLocation = filepath;
                    if (Options.options[Options.optMusic] == "ON") Menu.currentSound.PlayLooping();
                }
                catch (Exception error) {
                    Console.WriteLine("Music file is not found. Check your filepath.\n\n" + error);
                }
            }
            public static void StopSound() {
                Menu.currentSound.Stop();
            }
            public static void ResumeSound() {
                Menu.currentSound.PlayLooping();
            }
        }
        public static (bool, string, string) UploadFile(string filePath) {
            (bool, string, string) fileInfo = (true, filePath, "");   // Krotkę nienazwaną można modyfikować, a nazwaną nie.
            try {
                //fileInfo.Item1 = true;
                string fileContent = File.ReadAllText(filePath);
                fileInfo.Item2 = filePath;
            }
            catch (IOException error) {
                fileInfo.Item1 = false;
                fileInfo.Item3 = "File cannot be found.\n\n" + error.Message;
            }
            return fileInfo;
        }
        public static void Color(string text, ConsoleColor color) {   // Kolorowy tekst
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public static int SearchRemoveAt(List<int> array, int target) {   // Szukanie wartości i jej indeksu (lokalizacji) w tablicy:
            int result = -1;   // W kontekście losowania statków dla komputera, oznacza to kolizję pola począttkowego nowego statku z już istniejącym.
            for (int i = 0; i < array.Count; i++) {
                if (target == array[i]) {
                    result = i;
                    break;
                }
            }
            return result;
        }
        public static string TrimAllContent(string content) {
            string text = "";
            for (int i = 0; i < content.Length; i++) {
                if (content[i] != ' ') {
                    text += content[i];
                }
            }
            return text;
        }
        public static string StringPlayersInfo(List<List<string>> playersInfo) {   // Zamienia stringową listę dwówymiarową na stringa według formatu dla players.
            string text = "";
            List<List<string>> content = playersInfo;
            for (int i = 0; i < content.Count; i++) {
                for (int j = 0; j < content[i].Count; j++) {
                    text += content[i][j];
                    if (j < content[i].Count - 1) text += "#";
                }
                if (i < content.Count - 1) text += "*";
            }
            return text;
        }
        public class Page {
            public static void RenderDottedLine(int length) {
                string text = "";
                for (int i = 0; i < length; i = i + 2) {
                    text += (i == length - 2) ? "-" : "- ";
                }
                Console.WriteLine("\n" + text + "\n");
            }
            public static void RenderButtons(string[] buttons, int currentButton) {
                for (int i = 0, button = 0; i < buttons.Length; i++, button++) {
                    if (button == currentButton) {
                        Console.WriteLine("-> " + buttons[i]);
                    } else {
                        Console.WriteLine("   " + buttons[i]);
                    }
                }
            }
            public static ConsoleKeyInfo SelectUsingKeys(int currentButton, int page_ID, ConsoleKeyInfo key, string[] buttons, List<ConsoleKey> usingKeys_STANDARD, List<ConsoleKey> usingKeys_TOP, List<ConsoleKey> usingKeys_DOWN, List<ConsoleKey> usingKeys_ONE) {
                if (buttons.Length == 1) key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_ONE);
                else {
                    if (currentButton == 0) key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_TOP);// Dlaczego nie użyłem "switch"? Ponieważ w switch można używać tylko stałych wartości i z tego powodu nie mogę zrobić stałej obliczonej na podstawie: "const int down = buttons.Length - 1;"
                    else if (currentButton == buttons.Length - 1) key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_DOWN);
                    else key = GlobalMethod.Page.LoopCorrectKey(page_ID, key, usingKeys_STANDARD);
                }
                return key;
            }
            public static ConsoleKeyInfo LoopCorrectKey(int page_ID, ConsoleKeyInfo key, List<ConsoleKey> usingKeys) {
                bool isCorrSign = false;
                while (isCorrSign == false) {
                    key = Console.ReadKey(true);
                    for (int i = 0; i < usingKeys.Count; i++) {
                        if (key.Key == usingKeys[i]) {
                            isCorrSign = true;
                        }
                    }
                }
                if (key.Key == ConsoleKey.Backspace) MenuReturn(page_ID);
                return key;
            }
            public static (bool, ConsoleKeyInfo) LoopCorrectKey_GameMode(bool isEnterPart, ConsoleKeyInfo key, List<ConsoleKey> usingKeys) {   //GameMode_WithoutFirstRead
                bool isCorrSign = false;
                while (isCorrSign == false) {
                    if (isEnterPart == false) {
                        key = Console.ReadKey(true);
                    }
                    for (int i = 0; i < usingKeys.Count; i++) {
                        if (key.Key == usingKeys[i]) {
                            isCorrSign = true;
                        }
                    }
                }
                if (isEnterPart) isEnterPart = false;
                //if (key.Key == ConsoleKey.Backspace) MenuReturn(0);   // TYLKO DLA TESTÓW!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return (isEnterPart, key);
            }
            public static void MenuReturn(int ID_page) {
                switch (ID_page) {
                    case 0: PVC.isPage = false; break;
                    case 1: Instructions.isPage = false; break;
                    case 2: Ranking.isPage = false; break;
                    case 3: Options.isPage = false; break;
                    case 4: Credits.isPage = false; break;
                }
                Menu.isPage = true;
                Menu.RenderPage();
            }
            public static int MoveButtons(string[] buttons, int currentButton, ConsoleKeyInfo key) {
                if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W) {   // Pierwszy element ma index 0, a ostatni 5, więc jeżeli idziemy do góry, czyli do pierwszego, musimy odejmować.
                    currentButton = (currentButton > 0) ? currentButton - 1 : currentButton;
                } else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S) {
                    currentButton = (currentButton < buttons.Length - 1) ? currentButton + 1 : currentButton;   // Pierwszy element ma index 0, a ostatni 5, więc jeżeli idziemy do dołu, czyli do szóstego, musimy dodawać.
                }
                return currentButton;
            }
        }
        public class Board {
            public static void Top() {
                Color(" _______________________________________________       _______________________________________________ ", ConsoleColor.Green);
                Console.WriteLine();
            }
            public static void Bottom() {
                Color("|_______________________________________________|     |_______________________________________________|", ConsoleColor.Green);
            }
            public static void SpaceVertical() {
                Color("|                                               |     |                                               |", ConsoleColor.Green);
                Console.WriteLine();
            }
            public static void SpaceHorizontal() {
                Console.Write("     ");
            }
            public static void Left() {
                Color("|    ", ConsoleColor.Green);
            }
            public static void Right() {
                Color("    |", ConsoleColor.Green);
            }
            public static void Sign(string sign, bool isCursor) {
                string text = (isCursor) ? sign : " " + sign + "  ";
                Console.WriteLine(text);
            }
            public static void Cursor(string sign, bool isCursor) {
                Console.WriteLine("{");
                Sign(sign, isCursor);
                Console.WriteLine("}");
                isCursor = false;
            }
            public static void Render(List<int> board, string mode, string[,] data) {   // 3 parametr jest "jako wzór, zastanów się jak to ogarnąć..."
                // Algorytm wyświetlający planszę dla instrukcji, ustawiania statków gracza i bitwy.
            }
        }

        // Przenieś to do PVC:
        public static List<List<int>> PrepareShips(List<int> shipsInfo) {
            List<List<int>> shipsList = new List<List<int>>();
            for (int i = 0; i < shipsInfo.Count; i++) {
                List<int> ship = new List<int>();
                shipsList.Add(ship);
                for (int j = 0; j < shipsInfo[i]; j++) {
                    shipsList[i].Add(i + 1);
                }
            }
            return shipsList;
        }
        public static List<List<int>> RandomShips(List<int> array, List<List<int>> shipsList) {   // Zrób klasę RandomShips - w której zadeklarujesz zmienne globalne aby później ich już nie deklarować


            // Powróć do prostrzej wersji tego algorytmu: odzielny "toRight" i "toBottom"


            bool isCor = false;
            List<int> board = array;
            string dirVal = "";
            string[] dirAr = new string[2] { "toRight", "toBottom" };
            Random rand = new Random();
            int init = 0;
            int rem = 0;
            int dirDist = 1, numSlice = 0, numVal = 10;
            int shipDist = 0, limit = 0;
            double minBott = shipsList.Count / 2, bottCount = 0;
            minBott = (shipsList.Count % 2 == 1) ? minBott = Math.Floor(minBott) + rand.Next(0, 2) : minBott = shipsList.Count / 2;   // Jeżeli mam liczbę nieparzystą, to czy będzie więcej czy mniej statków "toBottom" o 1 zależy od losowości.
            bool isShip = false;
            while (!isCor) {
                board = new List<int>(array);   // Dlaczego tak, a nie board = array ? Gdyż lista jest przekazywana nie kopią a referencją, w związku z czym odwołuję się do pierwotnie zadeklarowanej listy i zmniejszam ją w nieskończoność, zamiast tworzyć nową kopię.
                bottCount = 0;
                for (int i = 0; i < shipsList.Count; i++) {
                    init = rand.Next(0, board.Count);
                    rem = GlobalMethod.SearchRemoveAt(board, init);
                    if (rem == -1) break;   // Kolizję pola początkowego nowego statku z już istniejącym.
                    board.RemoveAt(rem);
                    dirVal = dirAr[rand.Next(0, dirAr.Length)];
                    dirDist = (dirVal == "toRight") ? 1 : 10;
                    numSlice = (dirVal == "toRight") ? 0 : 1;
                    numVal = (dirVal == "toRight") ? 10 : 1;
                    if (dirVal == "toBottom") bottCount++;
                    shipDist = (init * dirDist) + ((shipsList[i].Count * dirDist) - dirDist);   // Obliczanie długości statku na planszy.
                    limit = (Convert.ToString(init).Length == 1) ? (9 * dirDist) : (9 * dirDist) + (numVal * (int)char.GetNumericValue(Convert.ToChar(Convert.ToString(init)[numSlice])));   // Jeżeli statek jest większy niż 1 pole długości - współrzędna inicjacyjna jest "ciachana" w celu dodania odpowiedniej jej częsci do bazowej liczby limitu, aby ostatecznie wyznaczyć odpowiedni limit dla statku znajdującym się w odpowiednim polu. Np. Dla statku o długości 3, w kierunku "toRight" bazawa wartość limitu wynosi 9, jeżeli współrzędna początkowa wynosi 54, to wycinana jest 5, mnożona przez 10 i dodawana do 9, w związku z czym mamy 59. Analogicznie jest w przypadku toBottom, tylko wartości są tak dostosowane aby dotyczyły NIE częsści dziesiętnej, a części jedności. Wówczas będziemy mięli 94.
                    if (shipDist > limit) break;   // Jeżeli statek wychodzi poza planszę, losuj statki od nowa. Jeżeli np. mamy statek długości 3, "toRight", a współrzędną początkową 54, to limit wynosi 59, a "shipDist" wynosi [współrzędna początkowo] + [długość statku] - 1, czyli 56. Wówczas mamy 56 <= 59, co jest prawdą, więc tworzenie staatku przechodzi ten etap.
                    if (shipsList[i].Count > 1) {   // Tworzenie pól długości dla statków powyżej 1 pola długości (2, 3, 4 ...).
                        for (int j = 1 * dirDist; j < shipsList[i].Count * dirDist; j=j+dirDist) {
                            isShip = false;
                            rem = GlobalMethod.SearchRemoveAt(board, init + j);
                            if (rem != -1) {   // Jeżeli nie ma kolizji statku
                                board.RemoveAt(rem);
                                shipsList[i][j/dirDist] = init + j;
                                isShip = true;
                            }
                            if (!isShip) break;   // Dlaczego dwa "break" i warunek? Normalnie wystarczyłby ten, ale ponieważ "break" ogranicza się do najbliższego "for", więc zrobiłem specjalny warunek, za pomocą którego będziemy kontrolować "break" pętli for wewnętrznej i "właściwej" nadrzędnej.
                        }
                        if (!isShip) break;
                    }
                    shipsList[i][0] = init;   // Jest to na końcu aby nie wciskać wartości gdy reszta pól długości statku będzie miała nieprawidłowe współrzędne. Operacja ta zaoszczędza mocy obliczeniowej.
                    if (i == shipsList.Count - 1) if (bottCount == minBott) isCor = true;   // Jeżeli 7 statków uzyskało poprawne wspoółrzędne, przejdź dalej -> Jeżeli liczba statków "toBottom" jest właściwa, zakończ algorytm.
                }
            }
            return shipsList;
        } 
    
    
    
    }
}
