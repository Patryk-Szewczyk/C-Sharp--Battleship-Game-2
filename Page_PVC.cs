using System;
using System.IO;
using Page_Menu;
using System.Collections.Generic;

namespace Page_PVC {
    public class PVC {
        public static bool isPVC = true;
        public static string userName = "";
        public static PVC pagePVC = new PVC();
        public static List<List<string>> playersDetails_PARTS = new List<List<string>>();
        public static string isDeletedDirection = "ABOVE";   // Przy usuwaniu użytkowanika powyżej kursowa i równo z ni trzeba przy wyświetlaniu kursora przesunąć go w dół.
        public static int player_IDX = 0;
        public void RenderPage() {   // Wyświetlenie strony PVC i zarazem panel kontrolny tej strony.
            bool isCorrSign = false;   // Zmienna walidacji poprawnego klawisza.
            System.ConsoleKeyInfo mainKey = new ConsoleKeyInfo('v', ConsoleKey.V, false, false, false);
            bool isSelectPlayer = false;
            //bool isPVCGame = false;
            bool isOption_CREATE = false, isOption_DELETE = false;
            pagePVC.DownloadPlayers();

            // Reset zmiennych poza-pętlowych strony: [gracz] | CHcę aby po wejściu na stronę wyboru użytkowników, kursor zawsze wskazywał pierwszego.
            player_IDX = 0;
            PVC.userName = playersDetails_PARTS[0][0];

            while (PVC.isPVC == true) {
                if (isSelectPlayer == false) {
                    pagePVC.SelectUserPart(PVC.playersDetails_PARTS, PVC.player_IDX);
                    if (isOption_CREATE == true) {
                        isOption_CREATE = false;
                        pagePVC.addUser(PVC.playersDetails_PARTS);
                        pagePVC.SelectUserPart(PVC.playersDetails_PARTS, PVC.player_IDX);
                    } else if (isOption_DELETE == true) {
                        isOption_DELETE = false;
                        pagePVC.deleteUser(PVC.playersDetails_PARTS);
                        pagePVC.SelectUserPart(PVC.playersDetails_PARTS, PVC.player_IDX);
                    }
                }
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                // Operacja klawiszowe:
                else if (isSelectPlayer == true) {
                    pagePVC.SetShips_PLAYER(mainKey, PVC.userName);   // Ta medota musi być przrd metodą "Console.ReadKey()", ponieważ jej wynik musi być
                    // wyswietlony po metodzie "Console.Clear()", której stan (stan wyświetlacza konsoli) jest zatrzymywany przez "Console.ReadKey()".
                }
                // Pętla ta uniemożliwia przeładowanie strony kiedy kliknie się niewłaściwy klawisz.
                while (isCorrSign == false) {
                    mainKey = Console.ReadKey(true);
                    if (mainKey.Key == System.ConsoleKey.W || mainKey.Key == System.ConsoleKey.S || mainKey.Key == System.ConsoleKey.D || mainKey.Key == System.ConsoleKey.A || mainKey.Key == System.ConsoleKey.UpArrow || mainKey.Key == System.ConsoleKey.DownArrow || mainKey.Key == System.ConsoleKey.LeftArrow || mainKey.Key == System.ConsoleKey.RightArrow || mainKey.Key == System.ConsoleKey.C || mainKey.Key == System.ConsoleKey.P || mainKey.Key == System.ConsoleKey.Enter || mainKey.Key == System.ConsoleKey.Backspace) {
                        isCorrSign = true;
                    }
                }
                isCorrSign = false;
                // Akcje na klawisze:
                isOption_CREATE = (mainKey.Key == System.ConsoleKey.C) ? true : false;
                isOption_DELETE = (mainKey.Key == System.ConsoleKey.P) ? true : false;
                if (mainKey.Key == System.ConsoleKey.Backspace) {
                    PVC.isPVC = false;
                    MenuPage.isMenu = true;
                    isSelectPlayer = false;
                    MenuPage.Menu();
                }
                else if (mainKey.Key == System.ConsoleKey.Enter && (isOption_CREATE == false || isOption_DELETE == false)) {
                    isSelectPlayer = true;
                }
                if (isSelectPlayer == false) {   // Poruszanie się po przyciskach (obliczenia):
                    if (mainKey.Key == System.ConsoleKey.UpArrow || mainKey.Key == System.ConsoleKey.W) {
                        //playerButtNum_POINTER = (playerButtNum_POINTER < PagePVC.playersDetails_PARTS.Count) ? playerButtNum_POINTER += 1 : playerButtNum_POINTER;
                        if (PVC.player_IDX > 0) {
                            PVC.player_IDX--;
                            PVC.userName = PVC.playersDetails_PARTS[PVC.player_IDX][0];
                        }
                    }
                    else if (mainKey.Key == System.ConsoleKey.DownArrow || mainKey.Key == System.ConsoleKey.S) {
                        //playerButtNum_POINTER = (playerButtNum_POINTER > 1) ? playerButtNum_POINTER -= 1 : playerButtNum_POINTER;
                        if (PVC.player_IDX < PVC.playersDetails_PARTS.Count - 1) {
                            PVC.player_IDX++;
                            PVC.userName = PVC.playersDetails_PARTS[PVC.player_IDX][0];
                        }
                    }
                }
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            }
        }
        public void SelectUserPart(List<List<string>> playersDetails_PARTS, int player_IDX) {   // Wyświetlenie wszytkich informacji na stronie "PagePVC":  ShowPageData(string[,] array, int index_number) 
            Console.Clear();
            Console.WriteLine("BBBBBBB   BB    BB   BBBBBBB");
            Console.WriteLine("BB    BB  BB    BB  BB      ");
            Console.WriteLine("BB    BB  BB    BB  BB      ");
            Console.WriteLine("BBBBBBB   BB    BB  BB      ");
            Console.WriteLine("BB         BB  BB   BB      ");
            Console.WriteLine("BB          BBBB    BB      ");
            Console.WriteLine("BB           BB      BBBBBBB");
            Console.WriteLine("\n- - - - - - - - - - - - - -\n");
            Console.WriteLine("PVC MODE: | Moving: arrows/[W][S] | Click = ENTER | Create player: [C] | Delete player: [P] | Back to menu: [Backspace]\n");

            if (PVC.isDeletedDirection == "BELOW") {   // Kiedy usunięto użytkownika poniżej kursora, zmniejsza się wartość lokalizacji kursora względem wyświetlonych użytkowaników.
                player_IDX = player_IDX - 1;
                PVC.player_IDX = PVC.player_IDX - 1;
                //Console.WriteLine("BELOW");
            } else if (PVC.isDeletedDirection == "CENTER") {
                PVC.userName = PVC.playersDetails_PARTS[PVC.player_IDX][0];
                //Console.WriteLine("CENTER");
            } //else if (PagePVC.isDeletedDirection == "ABOVE") {
              //Console.WriteLine("ABOVE");
              //}

            Console.WriteLine("Select player: [" + PVC.userName + "]");
            if (playersDetails_PARTS != null) {
                for (int i = 0, j = 0; i < playersDetails_PARTS.Count; i++, j++) {
                    if (j == player_IDX) {
                        Console.WriteLine("> " + playersDetails_PARTS[player_IDX][0]);
                    } else {
                        Console.WriteLine("  " + playersDetails_PARTS[i][0]);
                    }
                }
            }
            PVC.isDeletedDirection = "ABOVE";
        }
        public void DownloadPlayers() {   // Pobranie danych z pliku tekstowego użytkowaników: (nie zrobiłem globalnej tablicy jako listy i z tego powodu zrobiłem osobną metode do wyświetlania zaktualizowanej tablicy graczy)
            string fileContent = File.ReadAllText("players_PVC.txt");   // Odczytaj cały tekst z pliku
            string[] players = fileContent.Split('*');
            string[,] playersDetails_PARTS_AR = new string[players.Length, 5];
            List<List<string>> playersDetails_PARTS_LT_Level_1 = new List<List<string>>();
            string[] playerDetails_BLOCK = null;
            for (int i = 0; i < players.Length; i++) {
                List<string> playersDetails_PARTS_LT_Level_2 = new List<string>();
                playerDetails_BLOCK = players[i].Split('#');   // Każdy gracz ma 5 informacji oddzielonych znakiem "#":
                for (int j = 0; j < playerDetails_BLOCK.Length; j++) {
                    playersDetails_PARTS_AR[i, j] = playerDetails_BLOCK[j];
                    playersDetails_PARTS_LT_Level_2.Add(playerDetails_BLOCK[j]);
                }
                playersDetails_PARTS_LT_Level_1.Add(playersDetails_PARTS_LT_Level_2);
            }
            PVC.playersDetails_PARTS = playersDetails_PARTS_LT_Level_1;   // Wcześniej: playersDetails_PARTS_AR
            // Zapisz dane do pliku:
            /*try {
                // Zapisz tekst do pliku
                File.WriteAllText(filePath, content);
                Console.WriteLine("Data has been written to the file.");
            }
            catch (IOException ex) {
                Console.WriteLine("An error occurred while writing to the file:");
                Console.WriteLine(ex.Message);
            }*/
        }
        public void addUser(List<List<string>> playersDetails_PARTS) {
            string fileContent = File.ReadAllText("players_PVC.txt");
            string userName = "";
            string createState = "";   // "empty" / "correct" / "uncorrect" / "the-same"
            bool isAtLeastValidSign = false;
            char[] valid_CHAR_AR = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', 
                                  'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', 
                                  'ą', 'ć', 'ę', 'ł', 'ń', 'ó', 'ś', 'ż', 'ź',
                                  'Ą', 'Ć', 'Ę', 'Ł', 'Ń', 'Ó', 'Ś', 'Ż', 'Ź',
                                  '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
                                  ' ' };   // UWAGA! Znak ' ' (nic) jest MEGA ważny, gdyż później po walidaci odpowiednich znaków usuwamy nadmiar spacji i brak tego znaku
                                           // w tej tablicy spowodowałby, iż nazwa gracza ze spacją nie przechodziła by walidacji na poprawne znaki.
            Console.WriteLine("\n- - - - - - - - - - - - - -\n");
            Console.WriteLine("Create user. Available signs are letters and numbers.\n");
            Console.Write("User name: ");
            userName = Console.ReadLine();
            createState = "correct";
            // Sprawdzenie czy znajduje się przynajmniej jeden dopuszczalny znak: | Chcę uzyskać przekształcenie: "   " = "" (czyli: nic)
            for (int i = 0; i < userName.Length; i++) {
                if (userName[i] != ' ') {
                    isAtLeastValidSign = true;
                    break;
                }
            }
            if (isAtLeastValidSign == false) {
                userName = "";
                createState = "empty";
            }
            // Sprawdzenie poprawności znaków:
            if (createState == "correct") {
                int isUncorrectSign_COUNTER = 0;
                for (int i = 0; i < userName.Length; i++) {
                    isUncorrectSign_COUNTER = 0;
                    for (int j = 0; j < valid_CHAR_AR.Length; j++) {
                        if (userName[i] != valid_CHAR_AR[j]) {
                            isUncorrectSign_COUNTER++;
                        }
                    }
                    if (isUncorrectSign_COUNTER == valid_CHAR_AR.Length) {
                        createState = "uncorrect";
                    }
                }
            }
            // Usuwanie nadmiaru spacji:
            string userName_NEW = "";
            if (createState == "correct") {
                int startIDX = 0;
                int endIDX = userName.Length;
                // Ustalanie "początku" właściwego stringa do pozbywania się zbędnych spacji w jego środku:
                for (int i = 0; i < userName.Length; i++) {
                    if (userName[i] != ' ') {
                        break;
                    }
                    startIDX++;
                }
                // Ustalanie "końca" właściwego stringa do pozbywania się zbędnych spacji w jego środku:
                for (int i = userName.Length - 1; i >= 0; i--) {
                    if (userName[i] != ' ') {
                        break;
                    }
                    endIDX--;
                }
                // Tworzenie właściwej nazwy użytkownika bez zbędnych spacji:
                int spaceOverflow_COUNTER = 0;
                for (int i = startIDX; i < endIDX; i++) {
                    if (userName[i] == ' ') {
                        spaceOverflow_COUNTER++;
                        if (spaceOverflow_COUNTER == 1) {
                            userName_NEW += userName[i];
                        }
                    }
                    else if (userName[i] != ' ') {
                        spaceOverflow_COUNTER = 0;
                        userName_NEW += userName[i];
                    }
                }
            }
            userName = userName_NEW;
            // Sprawdzenie czy tworzony użytkownik nie znajduje się już w bazie danych użytkowników:
            if (createState == "correct") {
                for (int i = 0; i < playersDetails_PARTS.Count; i++) {
                    if (userName == playersDetails_PARTS[i][0]) {
                        createState = "the-same";
                    }
                }
            }
            // Wybór właściwego komunikatu:
            switch (createState) {
                case "correct":
                    fileContent += "*" + userName + "#0#0#0#0%";
                    File.WriteAllText("players_PVC.txt", fileContent);
                    pagePVC.DownloadPlayers();   // Ponowne wczytanie zaktualizowanej bazy danych użytkowników.
                    Console.WriteLine("\n\nAdd new user: [" + userName + "]."); ;
                    Console.WriteLine("\nClick ENTER to reload page.");
                    Console.ReadLine();
                    break;
                case "empty":
                    Console.WriteLine("\n\nUser has not been create, because you didn't write user name.");
                    Console.WriteLine("\nClick ENTER to reload page.");
                    Console.ReadLine();
                    break;
                case "uncorrect":
                    Console.WriteLine("\n\nUser has not been create, because you write uncorrect sign/s. Write user name with correct signs.");
                    Console.WriteLine("\nClick ENTER to reload page.");
                    Console.ReadLine();
                    break;
                case "the-same":
                    Console.WriteLine("\n\nUser has not been create, because this user name is already hired.");
                    Console.WriteLine("\nClick ENTER to reload page.");
                    Console.ReadLine();
                    break;
            }
        }
        public void deleteUser(List<List<string>> playersDetails_PARTS) {
            string deleteUser_VALUE = "";
            string correctUser = "";
            int del_IDX = 0;
            string fileContent = "";

            Console.WriteLine("\n- - - - - - - - - - - - - -\n");
            Console.Write("Delete user: ");
            deleteUser_VALUE = Console.ReadLine();

            // Walidacja poprawności użytkowanika do usunięcia:
            for (int i = 0; i < playersDetails_PARTS.Count; i++) {
                if (playersDetails_PARTS[i][0] == deleteUser_VALUE) {
                    correctUser = playersDetails_PARTS[i][0];
                    del_IDX = i;
                    break;
                }
            }

            if (correctUser != "") {
                // Określenie czy usuwany użytkownik znajduje się przed, albo równo z kursorem:
                if (PVC.player_IDX > del_IDX) {
                    PVC.isDeletedDirection = "BELOW";
                } else if (PVC.player_IDX == del_IDX) {
                    PVC.isDeletedDirection = "CENTER";
                }

                // Usunięcie określonej listy zagnieżdżonej z listy nadrzędnej danych użytkowników:
                playersDetails_PARTS.RemoveAt(del_IDX);

                // Tworzenie wartości zmiennej, przechowującej dane do zaktualizowania bazy danych użytkowników "players.txt":
                for (int i = 0; i < playersDetails_PARTS.Count; i++) {
                    if (i > 0) {   // UWAGA!!! Znak "*" występuje tylko przy rozdzieleniu użytkowników, nie ma go na początku i końcu!
                        fileContent += "*";
                    }
                    for (int j = 0; j < playersDetails_PARTS[i].Count; j++) {
                        if (j < playersDetails_PARTS[i].Count - 1) {   // Mniejszy niż ostatni indeks.
                            fileContent += playersDetails_PARTS[i][j] + "#";
                        } else if (j == playersDetails_PARTS[i].Count - 1) {   // Ostatni indeks, aby nie było znaku "#" przed znakiem "*".
                            fileContent += playersDetails_PARTS[i][j];
                        }
                    }
                };

                // Zaktualizowanie danych bazy danych użytkowników "players.txt":
                File.WriteAllText("players_PVC.txt", fileContent);
                pagePVC.DownloadPlayers();   // Ponowne wczytanie zaktualizowanej bazy danych użytkowników.

                Console.WriteLine("\n\nUser [" + correctUser + "] has been deleted.");
                Console.WriteLine("\nClick ENTER to reload page.");
                Console.ReadLine();
            } else {
                Console.WriteLine("\n\nChoosed user doesn't exist.");
                Console.WriteLine("\nClick ENTER to reload page.");
                Console.ReadLine();
            }
        }
        public void SetShips_PLAYER(System.ConsoleKeyInfo key, string userName) {
            Console.Clear();
            // Walidacja poruszania się po planszy:
            Console.WriteLine("Player: [" + userName + "]");
            int setVal = SetShipsPVC.CursorNavigate(key);
            Console.WriteLine("Ship initial coordinate: " + setVal);
        }
        public class SetShipsPVC {
            public static int cursor = 0;
            public static int CursorNavigate(System.ConsoleKeyInfo key) {
                System.ConsoleKey[] direction_1 = new ConsoleKey[4] { System.ConsoleKey.UpArrow, System.ConsoleKey.DownArrow, System.ConsoleKey.RightArrow, System.ConsoleKey.LeftArrow };
                System.ConsoleKey[] direction_2 = new ConsoleKey[4] { System.ConsoleKey.W, System.ConsoleKey.S, System.ConsoleKey.D, System.ConsoleKey.A };
                int[] add = new int[4] { -10, 10, 1, -1 };
                bool[] stop = new bool[4] { false, false, false, false };
                int[,] valNum = new int[4, 10] {
                    /*Up:*/    { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                    /*Down:*/  { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 },
                    /*Right:*/ { 9, 19, 29, 39, 49, 59, 69, 79, 89, 99 },
                    /*Left:*/  { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90 }
                };
                for (int i = 0; i < valNum.GetLength(0); i++) {
                    stop[i] = false;
                    for (int j = 0; j < valNum.GetLength(1); j++) {
                        if (cursor == valNum[i,j]) stop[i] = true;
                    }
                    if (!stop[i]) {
                        if (key.Key == direction_1[i] || key.Key == direction_2[i]) {
                            cursor += add[i];
                        }
                    }
                }
                return cursor;
            }
        }
        public void SetShips_COMPUTER() {

        }
        public void BattleControl() {

        }
        public void Battle_PLAYER() {

        }
        public void Battle_COMPUTER() {

        }
        public void SubmitRanking() {

        }
    }
}