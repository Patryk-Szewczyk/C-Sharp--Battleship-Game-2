using System;
using System.Configuration;
using System.IO;
using System.Linq.Expressions;
using Page_Menu;
using System.Collections.Generic;

namespace Page_PVC
{

    // KIEDY SKO�CZYSZ GR� - SPR�BUJ ZAAPLICOWA� "static" DO INTERFEJS�W STRON!
    /*interface IPagePVC   // Mog�em opu�ci� interfejs, aby mie� metody statyczne, ale u�ywam go poniewa� chc� mie� widoczne na g�rze nazwy wszystkich metody danej klasy:
    {
        void PVC();   // Wy�wietlenie strony PVC.
        void SelectPlayer();   // Etap 1: Wyb�r gracza i tworzenie gracza.
        void SetShips_PLAYER();   // Etap 2: Ustawienie statk�w gracza.
        void SetShips_COMPUTER();   // Etap 3: Ustawienie statk�w dla komputera.
        void BattleControl();   // Etap 4: Bitwa (panel kontrolny).
        void Battle_PLAYER();   // Przekierowanie na gracza.
        void Battle_COMPUTER();   // Przekierowanie na komputer.

        void SubmitRanking();   // Etap 5: Wpisz wynik do rankingu i poka� ranking.
    }*/
    public class PagePVC/* : IPagePVC*/
    {
        public static bool isPVCLoop = true;
        public static string userName = "";
        public static PagePVC pagePVC = new PagePVC();
        public static List<List<string>> /*string[,]*/ playersDetails_PARTS = new List<List<string>>();
        public static string isDeletedDirection = "ABOVE";   // Przy usuwaniu u�ytkowanika powy�ej kursowa i r�wno z ni trzeba przy wy�wietlaniu kursora przesun�� go w d�.
        public static int player_IDX = 0;
        public void PVC()
        {
            bool isCorrectSign = false;   // Zmienna walidacji poprawnego klawisza.
            System.ConsoleKeyInfo mainKey = new ConsoleKeyInfo('v', ConsoleKey.V, false, false, false);
            bool isSelectPlayer = false;
            bool isPVCGame = false;
            bool isOption_CREATE = false, isOption_DELETE = false;
            //int player_IDX = 0;
            pagePVC.DownloadPlayers();

            // Reset zmiennych poza-p�tlowych strony: [gracz] | CHc� aby po wej�ciu na stron� wyboru u�ytkownik�w, kursor zawsze wskazywa� pierwszego.
            player_IDX = 0;
            PagePVC.userName = playersDetails_PARTS[0][0];

            while (PagePVC.isPVCLoop == true)
            {
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                // Wy�wietlenie informacji strony (stanu wyboru u�ytkownika):
                if (isSelectPlayer == false)
                {
                    pagePVC.SelectUserPart(PagePVC.playersDetails_PARTS, PagePVC.player_IDX);
                    if (isOption_CREATE == true)
                    {
                        isOption_CREATE = false;
                        // Dodawanie u�ytkownika:
                        pagePVC.addUser(PagePVC.playersDetails_PARTS);
                        // Ponowne wy�wietlenie stanu wyboru u�ytkownika:
                        pagePVC.SelectUserPart(PagePVC.playersDetails_PARTS, PagePVC.player_IDX);
                    }
                    else if (isOption_DELETE == true)
                    {
                        isOption_DELETE = false;
                        // Uwuwanie u�ytkownika:
                        pagePVC.deleteUser(PagePVC.playersDetails_PARTS);
                        // Ponowne wy�wietlenie stanu wyboru u�ytkownika:
                        pagePVC.SelectUserPart(PagePVC.playersDetails_PARTS, PagePVC.player_IDX);
                    }
                }
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                // Operacja klawiszowe:
                else if (isSelectPlayer == true)
                {
                    pagePVC.SetShips_PLAYER(mainKey, PagePVC.userName);   // Ta medota musi by� przrd metod� "Console.ReadKey()", poniewa� jej wynik musi by�
                    // wyswietlony po metodzie "Console.Clear()", kt�rej stan (stan wy�wietlacza konsoli) jest zatrzymywany przez "Console.ReadKey()".
                }
                // P�tla ta uniemo�liwia prze�adowanie strony kiedy kliknie si� niew�a�ciwy klawisz.
                while (isCorrectSign == false)
                {
                    mainKey = Console.ReadKey(true);
                    if (mainKey.Key == System.ConsoleKey.W || mainKey.Key == System.ConsoleKey.S || mainKey.Key == System.ConsoleKey.D || mainKey.Key == System.ConsoleKey.A || mainKey.Key == System.ConsoleKey.UpArrow || mainKey.Key == System.ConsoleKey.DownArrow || mainKey.Key == System.ConsoleKey.LeftArrow || mainKey.Key == System.ConsoleKey.RightArrow || mainKey.Key == System.ConsoleKey.C || mainKey.Key == System.ConsoleKey.P || mainKey.Key == System.ConsoleKey.Enter || mainKey.Key == System.ConsoleKey.Backspace)
                    {
                        isCorrectSign = true;
                    }
                }
                isCorrectSign = false;
                // Akcje na klawisze:
                isOption_CREATE = (mainKey.Key == System.ConsoleKey.C) ? true : false;
                isOption_DELETE = (mainKey.Key == System.ConsoleKey.P) ? true : false;
                if (mainKey.Key == System.ConsoleKey.Backspace)
                {
                    PagePVC.isPVCLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    isSelectPlayer = false;
                    MenuPage.Menu();
                }
                else if (mainKey.Key == System.ConsoleKey.Enter && (isOption_CREATE == false || isOption_DELETE == false))
                {
                    isSelectPlayer = true;
                }
                if (isSelectPlayer == false)   // Poruszanie si� po przyciskach (obliczenia):
                {
                    if (mainKey.Key == System.ConsoleKey.UpArrow || mainKey.Key == System.ConsoleKey.W)
                    {
                        //playerButtNum_POINTER = (playerButtNum_POINTER < PagePVC.playersDetails_PARTS.Count) ? playerButtNum_POINTER += 1 : playerButtNum_POINTER;
                        if (PagePVC.player_IDX > 0)
                        {
                            PagePVC.player_IDX--;
                            PagePVC.userName = PagePVC.playersDetails_PARTS[PagePVC.player_IDX][0];
                        }
                    }
                    else if (mainKey.Key == System.ConsoleKey.DownArrow || mainKey.Key == System.ConsoleKey.S)
                    {
                        //playerButtNum_POINTER = (playerButtNum_POINTER > 1) ? playerButtNum_POINTER -= 1 : playerButtNum_POINTER;
                        if (PagePVC.player_IDX < PagePVC.playersDetails_PARTS.Count - 1)
                        {
                            PagePVC.player_IDX++;
                            PagePVC.userName = PagePVC.playersDetails_PARTS[PagePVC.player_IDX][0];
                        }
                    }
                }
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            }
        }
        public void SelectUserPart(List<List<string>> playersDetails_PARTS, int player_IDX)   // Wy�wietlenie wszytkich informacji na stronie "PagePVC":  ShowPageData(string[,] array, int index_number) 
        {
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

            if (PagePVC.isDeletedDirection == "BELOW")   // Kiedy usuni�to u�ytkownika poni�ej kursora, zmniejsza si� warto�� lokalizacji kursora wzgl�dem wy�wietlonych u�ytkowanik�w.
            {
                player_IDX = player_IDX - 1;
                PagePVC.player_IDX = PagePVC.player_IDX - 1;
            }
            else if (PagePVC.isDeletedDirection == "CENTER")
            {
                //int nextIDX = PagePVC.player_IDX + 1;
                PagePVC.userName = PagePVC.playersDetails_PARTS[PagePVC.player_IDX][0];
            }

            Console.WriteLine("Select player: [" + PagePVC.userName + "]");
            if (playersDetails_PARTS != null)
            {
                for (int i = 0, j = 0; i < playersDetails_PARTS.Count; i++, j++)
                {
                    if (j == player_IDX)
                    {
                        Console.WriteLine("> " + playersDetails_PARTS[player_IDX][0]);
                    }
                    else
                    {
                        Console.WriteLine("  " + playersDetails_PARTS[i][0]);
                    }
                }
            }
            PagePVC.isDeletedDirection = "ABOVE";
        }
        public void DownloadPlayers()   // Pobranie danych z pliku tekstowego u�ytkowanik�w: (nie zrobi�em globalnej tablicy jako listy i z tego powodu zrobi�em osobn� metode do wy�wietlania zaktualizowanej tablicy graczy)
        {
            // Odczytaj ca�y tekst z pliku
            string fileContent = File.ReadAllText("players_PVC.txt");
            string[] players = fileContent.Split('*');
            string[,] playersDetails_PARTS_AR = new string[players.Length, 5];
            List<List<string>> playersDetails_PARTS_LT_Level_1 = new List<List<string>>();
            string[] playerDetails_BLOCK = null;
            for (int i = 0; i < players.Length; i++)
            {
                List<string> playersDetails_PARTS_LT_Level_2 = new List<string>();
                // Ka�dy gracz ma 5 informacji oddzielonych znakiem "#":
                playerDetails_BLOCK = players[i].Split('#');
                for (int j = 0; j < playerDetails_BLOCK.Length; j++)
                {
                    playersDetails_PARTS_AR[i, j] = playerDetails_BLOCK[j];
                    playersDetails_PARTS_LT_Level_2.Add(playerDetails_BLOCK[j]);
                }
                playersDetails_PARTS_LT_Level_1.Add(playersDetails_PARTS_LT_Level_2);
            }
            PagePVC.playersDetails_PARTS = playersDetails_PARTS_LT_Level_1;   // Wcze�niej: playersDetails_PARTS_AR
            // Zapisz dane do pliku:
            /*try
            {
                // Zapisz tekst do pliku
                File.WriteAllText(filePath, content);
                Console.WriteLine("Data has been written to the file.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred while writing to the file:");
                Console.WriteLine(ex.Message);
            }*/
        }
        public void addUser(List<List<string>> playersDetails_PARTS)
        {
            string fileContent = File.ReadAllText("players_PVC.txt");
            string userName = "";
            string createState = "";   // "empty" / "correct" / "uncorrect" / "the-same"
            bool isAtLeastValidSign = false;
            char[] valid_CHAR_AR = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', 
                                  'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', 
                                  '�', '�', '�', '�', '�', '�', '�', '�', '�',
                                  '�', '�', '�', '�', '�', '�', '�', '�', '�',
                                  '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
                                  ' ' };   // UWAGA! Znak ' ' (nic) jest MEGA wa�ny, gdy� p�niej po walidaci odpowiednich znak�w usuwamy nadmiar spacji i brak tego znaku
                                           // w tej tablicy spowodowa�by, i� nazwa gracza ze spacj� nie przechodzi�a by walidacji na poprawne znaki.

            Console.WriteLine("\n- - - - - - - - - - - - - -\n");
            Console.WriteLine("Create user. Available signs are letters and numbers.\n");
            Console.Write("User name: ");
            userName = Console.ReadLine();
            createState = "correct";

            // Sprawdzenie czy znajduje si� przynajmniej jeden dopuszczalny znak: | Chc� uzyska� przekszta�cenie: "   " = "" (czyli: nic)
            for (int i = 0; i < userName.Length; i++)
            {
                if (userName[i] != ' ')
                {
                    isAtLeastValidSign = true;
                    break;
                }
            }
            if (isAtLeastValidSign == false)
            {
                userName = "";
                createState = "empty";
            }

            if (createState == "correct")   // Sprawdzenie poprawno�ci znak�w:
            {
                int isUncorrectSign_COUNTER = 0;

                for (int i = 0; i < userName.Length; i++)
                {
                    isUncorrectSign_COUNTER = 0;
                    for (int j = 0; j < valid_CHAR_AR.Length; j++)
                    {
                        if (userName[i] != valid_CHAR_AR[j])
                        {
                            isUncorrectSign_COUNTER++;
                        }
                    }
                    if (isUncorrectSign_COUNTER == valid_CHAR_AR.Length)
                    {
                        createState = "uncorrect";
                    }
                }
            }

            string userName_NEW = "";
            if (createState == "correct")   // Usuwanie nadmiaru spacji:
            {
                int startIDX = 0;
                int endIDX = userName.Length;

                // Ustalanie "pocz�tku" w�a�ciwego stringa do pozbywania si� zb�dnych spacji w jego �rodku:
                for (int i = 0; i < userName.Length; i++)
                {
                    if (userName[i] != ' ')
                    {
                        break;
                    }
                    startIDX++;
                }
                // Ustalanie "ko�ca" w�a�ciwego stringa do pozbywania si� zb�dnych spacji w jego �rodku:
                for (int i = userName.Length - 1; i >= 0; i--)
                {
                    if (userName[i] != ' ')
                    {
                        break;
                    }
                    endIDX--;
                }
                // Tworzenie w�a�ciwej nazwy u�ytkownika bez zb�dnych spacji:
                int spaceOverflow_COUNTER = 0;
                for (int i = startIDX; i < endIDX; i++)
                {
                    if (userName[i] == ' ')
                    {
                        spaceOverflow_COUNTER++;
                        if (spaceOverflow_COUNTER == 1)
                        {
                            userName_NEW += userName[i];
                        }
                    }
                    else if (userName[i] != ' ')
                    {
                        spaceOverflow_COUNTER = 0;
                        userName_NEW += userName[i];
                    }
                }
            }
            userName = userName_NEW;

            if (createState == "correct")   // Sprawdzenie czy tworzony u�ytkownik nie znajduje si� ju� w bazie danych u�ytkownik�w:
            {
                for (int i = 0; i < playersDetails_PARTS.Count; i++)
                {
                    if (userName == playersDetails_PARTS[i][0])
                    {
                        createState = "the-same";
                    }
                }
            }

            // Sprawdzenie czy wyst�puj� poprawne znaki w ka�dym indeksie nazwy

            switch (createState)
            {
                case "correct":
                    fileContent += "*" + userName + "#0#0#0#0%";
                    File.WriteAllText("players_PVC.txt", fileContent);
                    pagePVC.DownloadPlayers();   // Ponowne wczytanie zaktualizowanej bazy danych u�ytkownik�w.
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
        public void deleteUser(List<List<string>> playersDetails_PARTS)
        {
            string deleteUser_VALUE = "";
            string correctUser = "";
            int del_IDX = 0;
            string fileContent = "";

            Console.WriteLine("\n- - - - - - - - - - - - - -\n");
            Console.Write("Delete user: ");
            deleteUser_VALUE = Console.ReadLine();

            // Walidacja poprawno�ci u�ytkowanika do usuni�cia:
            for (int i = 0; i < playersDetails_PARTS.Count; i++)
            {
                if (playersDetails_PARTS[i][0] == deleteUser_VALUE)
                {
                    correctUser = playersDetails_PARTS[i][0];
                    del_IDX = i;
                    break;
                }
            }

            if (correctUser != "")
            {
                // Okre�lenie czy usuwany u�ytkownik znajduje si� przed, albo r�wno z kursorem:
                if (PagePVC.player_IDX > del_IDX)
                {
                    PagePVC.isDeletedDirection = "BELOW";
                }
                else if (PagePVC.player_IDX == del_IDX)
                {
                    PagePVC.isDeletedDirection = "CENTER";
                }

                // Usuni�cie okre�lonej listy zagnie�d�onej z listy nadrz�dnej danych u�ytkownik�w:
                playersDetails_PARTS.RemoveAt(del_IDX);

                // Tworzenie warto�ci zmiennej, przechowuj�cej dane do zaktualizowania bazy danych u�ytkownik�w "players.txt":
                for (int i = 0; i < playersDetails_PARTS.Count; i++)
                {
                    if (i > 0)   // UWAGA!!! Znak "*" wyst�puje tylko przy rozdzieleniu u�ytkownik�w, nie ma go na pocz�tku i ko�cu!
                    {
                        fileContent += "*";
                    }
                    for (int j = 0; j < playersDetails_PARTS[i].Count; j++)
                    {
                        if (j < playersDetails_PARTS[i].Count - 1)   // Mniejszy ni� ostatni indeks.
                        {
                            fileContent += playersDetails_PARTS[i][j] + "#";
                        }
                        else if (j == playersDetails_PARTS[i].Count - 1)   // Ostatni indeks, aby nie by�o znaku "#" przed znakiem "*".
                        {
                            fileContent += playersDetails_PARTS[i][j];
                        }
                    }
                };

                // Zaktualizowanie danych bazy danych u�ytkownik�w "players.txt":
                File.WriteAllText("players_PVC.txt", fileContent);
                pagePVC.DownloadPlayers();   // Ponowne wczytanie zaktualizowanej bazy danych u�ytkownik�w.

                Console.WriteLine("\n\nUser [" + correctUser + "] has been deleted.");
                Console.WriteLine("\nClick ENTER to reload page.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\n\nChoosed user doesn't exist.");
                Console.WriteLine("\nClick ENTER to reload page.");
                Console.ReadLine();
            }
        }
        public void SetShips_PLAYER(System.ConsoleKeyInfo key, string userName)
        {
            Console.Clear();

            // Walidacja poruszania si� po planszy:
            Console.WriteLine("Set your ships: [" + userName + "]");
            int setVal = SetShips_PLAYER_CLASS.CursorNavigate(key);
            Console.WriteLine("Set value: " + setVal);
        }
        public class SetShips_PLAYER_CLASS
        {
            public static int cursorVal = 0;
            public static int[] toUpBlock_AR = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            public static int[] toDownBlock_AR = new int[10] { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 };
            public static int[] toRightBlock_AR = new int[10] { 9, 19, 29, 39, 49, 59, 69, 79, 89, 99 };
            public static int[] toLeftBlock_AR = new int[10] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90 };
            public static bool toUpBlock_BOOL = false, toDownBlock_BOOL = false, toRightBlock_BOOL = false, toLeftBlock_BOOL = false;
            public static int CursorNavigate(System.ConsoleKeyInfo key)
            {
                SetShips_PLAYER_CLASS.toUpBlock_BOOL = false;
                SetShips_PLAYER_CLASS.toDownBlock_BOOL = false;
                SetShips_PLAYER_CLASS.toRightBlock_BOOL = false;
                SetShips_PLAYER_CLASS.toLeftBlock_BOOL = false;
                for (int i = 0; i < SetShips_PLAYER_CLASS.toUpBlock_AR.Length; i++)
                {
                    if (SetShips_PLAYER_CLASS.cursorVal == toUpBlock_AR[i])
                    {
                        SetShips_PLAYER_CLASS.toUpBlock_BOOL = true;
                    }
                }
                if (SetShips_PLAYER_CLASS.toUpBlock_BOOL == false)
                {
                    if (key.Key == System.ConsoleKey.UpArrow || key.Key == System.ConsoleKey.W)
                    {
                        cursorVal -= 10;
                    }
                }
                for (int i = 0; i < SetShips_PLAYER_CLASS.toDownBlock_AR.Length; i++)
                {
                    if (SetShips_PLAYER_CLASS.cursorVal == toDownBlock_AR[i])
                    {
                        SetShips_PLAYER_CLASS.toDownBlock_BOOL = true;
                    }
                }
                if (SetShips_PLAYER_CLASS.toDownBlock_BOOL == false)
                {
                    if (key.Key == System.ConsoleKey.DownArrow || key.Key == System.ConsoleKey.S)
                    {
                        cursorVal += 10;
                    }
                }
                for (int i = 0; i < SetShips_PLAYER_CLASS.toRightBlock_AR.Length; i++)
                {
                    if (SetShips_PLAYER_CLASS.cursorVal == toRightBlock_AR[i])
                    {
                        SetShips_PLAYER_CLASS.toRightBlock_BOOL = true;
                    }
                }
                if (SetShips_PLAYER_CLASS.toRightBlock_BOOL == false)
                {
                    if (key.Key == System.ConsoleKey.RightArrow || key.Key == System.ConsoleKey.D)
                    {
                        cursorVal += 1;
                    }
                }
                for (int i = 0; i < SetShips_PLAYER_CLASS.toLeftBlock_AR.Length; i++)
                {
                    if (SetShips_PLAYER_CLASS.cursorVal == toLeftBlock_AR[i])
                    {
                        SetShips_PLAYER_CLASS.toLeftBlock_BOOL = true;
                    }
                }
                if (SetShips_PLAYER_CLASS.toLeftBlock_BOOL == false)
                {
                    if (key.Key == System.ConsoleKey.LeftArrow || key.Key == System.ConsoleKey.A)
                    {
                        cursorVal -= 1;
                    }
                }
                return SetShips_PLAYER_CLASS.cursorVal;
            }
        }
        public void SetShips_COMPUTER()
        {

        }
        public void BattleControl()
        {

        }
        public void Battle_PLAYER()
        {

        }
        public void Battle_COMPUTER()
        {

        }
        public void SubmitRanking()
        {

        }
    }
}