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
        public static string ConvertTo_A0(int coordinate) {
            string corrForm = Convert.ToString(coordinate);
            switch (corrForm[0]) {
                case '0': corrForm = "A" + corrForm[0]; break;
                case '1': corrForm = "A" + corrForm[0]; break;
                case '2': corrForm = "A" + corrForm[0]; break;
                case '3': corrForm = "A" + corrForm[0]; break;
                case '4': corrForm = "A" + corrForm[0]; break;
                case '5': corrForm = "A" + corrForm[0]; break;
                case '6': corrForm = "A" + corrForm[0]; break;
                case '7': corrForm = "A" + corrForm[0]; break;
                case '8': corrForm = "A" + corrForm[0]; break;
                case '9': corrForm = "A" + corrForm[0]; break;
            }
            if (coordinate > 9) {   // "34"   [0] = 3 | [1] = 4
                corrForm = Convert.ToString(coordinate);
                switch (corrForm[0]) {
                    case '1': corrForm = "B" + corrForm[1]; break;
                    case '2': corrForm = "C" + corrForm[1]; break;
                    case '3': corrForm = "D" + corrForm[1]; break;
                    case '4': corrForm = "E" + corrForm[1]; break;
                    case '5': corrForm = "F" + corrForm[1]; break;
                    case '6': corrForm = "G" + corrForm[1]; break;
                    case '7': corrForm = "H" + corrForm[1]; break;
                    case '8': corrForm = "I" + corrForm[1]; break;
                    case '9': corrForm = "J" + corrForm[1]; break;
                }
            }
            return corrForm;
        }
        public static int[] ConvertTo_IntArray(List<int> list) {
            int[] array = new int[list.Count];
            for (int i = 0; i < list.Count; i++) array[i] = list[i];
            return array;
        }
        public static List<int> ConvertTo_IntList(int[] array) {
            List<int> list = new List<int>();
            for (int i = 0; i < array.Length; i++) list.Add(array[i]);
            return list;
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
        }
    }
}
