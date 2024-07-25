using System;
using System.Configuration;
using System.IO;
using System.Linq.Expressions;
using Page_Menu;
using System.Collections.Generic;

namespace Page_PVC
{

    // KIEDY SKOÑCZYSZ GRÊ - SPRÓBUJ ZAAPLICOWAÆ "static" DO INTERFEJSÓW STRON!
    /*interface IPagePVC   // Mog³em opuœciæ interfejs, aby mieæ metody statyczne, ale u¿ywam go poniewa¿ chcê mieæ widoczne na górze nazwy wszystkich metody danej klasy:
    {
        void PVC();   // Wyœwietlenie strony PVC.
        void SelectPlayer();   // Etap 1: Wybór gracza i tworzenie gracza.
        void SetShips_PLAYER();   // Etap 2: Ustawienie statków gracza.
        void SetShips_COMPUTER();   // Etap 3: Ustawienie statków dla komputera.
        void BattleControl();   // Etap 4: Bitwa (panel kontrolny).
        void Battle_PLAYER();   // Przekierowanie na gracza.
        void Battle_COMPUTER();   // Przekierowanie na komputer.

        void SubmitRanking();   // Etap 5: Wpisz wynik do rankingu i poka¿ ranking.
    }*/
    public class PagePVC/* : IPagePVC*/
    {
        public static bool isPVCShipPositingLoop = true;
        public static string userName = "";
        public static PagePVC pagePVC = new PagePVC();
        public static List<List<string>> /*string[,]*/ playersDetails_PARTS = new List<List<string>>();
        public void PVC()
        {
            bool isCorrectSign = false;   // Zmienna walidacji poprawnego klawisza.
            System.ConsoleKeyInfo mainKey = new ConsoleKeyInfo('v', ConsoleKey.V, false, false, false);
            bool isSelectPlayer = false;
            bool isOption_CREATE = false, isOption_DELETE = false;
            int player_IDX = 0;
            pagePVC.DownloadPlayers();

            // Reset zmiennych poza-pêtlowych strony: [gracz] | CHcê aby po wejœciu na stronê wyboru u¿ytkowników, kursor zawsze wskazywa³ pierwszego.
            player_IDX = 0;
            PagePVC.userName = playersDetails_PARTS[0][0];

            while (PagePVC.isPVCShipPositingLoop == true)
            {
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                // Wyœwietlenie informacji strony (stanu wyboru u¿ytkownika):
                if (isSelectPlayer == false)
                {
                    pagePVC.SelectUserPart(PagePVC.playersDetails_PARTS, player_IDX);
                    if (isOption_CREATE == true)
                    {
                        isOption_CREATE = false;
                        // Dodawanie u¿ytkownika:
                        pagePVC.addUser(PagePVC.playersDetails_PARTS);
                        // Ponowne wyœwietlenie stanu wyboru u¿ytkownika:
                        pagePVC.SelectUserPart(PagePVC.playersDetails_PARTS, player_IDX);
                    }
                    else if (isOption_DELETE == true)
                    {
                        isOption_DELETE = false;
                        // Uwuwanie u¿ytkownika:
                        pagePVC.deleteUser(PagePVC.playersDetails_PARTS);
                        // Ponowne wyœwietlenie stanu wyboru u¿ytkownika:
                        pagePVC.SelectUserPart(PagePVC.playersDetails_PARTS, player_IDX);
                    }
                }
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                // Operacja klawiszowe:
                else if (isSelectPlayer == true)
                {
                    pagePVC.SetShips_PLAYER(mainKey, PagePVC.userName);   // Ta medota musi byæ przrd metod¹ "Console.ReadKey()", poniewa¿ jej wynik musi byæ
                    // wyswietlony po metodzie "Console.Clear()", której stan (stan wyœwietlacza konsoli) jest zatrzymywany przez "Console.ReadKey()".
                }
                // Pêtla ta uniemo¿liwia prze³adowanie strony kiedy kliknie siê niew³aœciwy klawisz.
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
                    PagePVC.isPVCShipPositingLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    isSelectPlayer = false;
                    MenuPage.Menu();
                }
                else if (mainKey.Key == System.ConsoleKey.Enter && (isOption_CREATE == false || isOption_DELETE == false))
                {
                    isSelectPlayer = true;
                }
                if (isSelectPlayer == false)   // Poruszanie siê po przyciskach (obliczenia):
                {
                    if (mainKey.Key == System.ConsoleKey.UpArrow || mainKey.Key == System.ConsoleKey.W)
                    {
                        //playerButtNum_POINTER = (playerButtNum_POINTER < PagePVC.playersDetails_PARTS.Count) ? playerButtNum_POINTER += 1 : playerButtNum_POINTER;
                        if (player_IDX > 0)
                        {
                            player_IDX--;
                            PagePVC.userName = PagePVC.playersDetails_PARTS[player_IDX][0];
                        }
                    }
                    else if (mainKey.Key == System.ConsoleKey.DownArrow || mainKey.Key == System.ConsoleKey.S)
                    {
                        //playerButtNum_POINTER = (playerButtNum_POINTER > 1) ? playerButtNum_POINTER -= 1 : playerButtNum_POINTER;
                        if (player_IDX < PagePVC.playersDetails_PARTS.Count - 1)
                        {
                            player_IDX++;
                            PagePVC.userName = PagePVC.playersDetails_PARTS[player_IDX][0];
                        }
                    }
                }
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            }
        }
        public void SelectUserPart(List<List<string>> playersDetails_PARTS, int player_IDX)   // Wyœwietlenie wszytkich informacji na stronie "PagePVC":  ShowPageData(string[,] array, int index_number) 
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
        }
        public void DownloadPlayers()   // Pobranie danych z pliku tekstowego u¿ytkowaników: (nie zrobi³em globalnej tablicy jako listy i z tego powodu zrobi³em osobn¹ metode do wyœwietlania zaktualizowanej tablicy graczy)
        {
            // Odczytaj ca³y tekst z pliku
            string fileContent = File.ReadAllText("players.txt");
            string[] players = fileContent.Split('*');
            string[,] playersDetails_PARTS_AR = new string[players.Length, 5];
            List<List<string>> playersDetails_PARTS_LT_Level_1 = new List<List<string>>();
            string[] playerDetails_BLOCK = null;
            for (int i = 0; i < players.Length; i++)
            {
                List<string> playersDetails_PARTS_LT_Level_2 = new List<string>();
                // Ka¿dy gracz ma 5 informacji oddzielonych znakiem "#":
                playerDetails_BLOCK = players[i].Split('#');
                for (int j = 0; j < playerDetails_BLOCK.Length; j++)
                {
                    playersDetails_PARTS_AR[i, j] = playerDetails_BLOCK[j];
                    playersDetails_PARTS_LT_Level_2.Add(playerDetails_BLOCK[j]);
                }
                playersDetails_PARTS_LT_Level_1.Add(playersDetails_PARTS_LT_Level_2);
            }
            PagePVC.playersDetails_PARTS = playersDetails_PARTS_LT_Level_1;   // Wczeœniej: playersDetails_PARTS_AR
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
            string fileContent = File.ReadAllText("players.txt");
            string userName = "";

            Console.WriteLine("\n- - - - - - - - - - - - - -\n");
            Console.Write("Create user: ");
            userName = Console.ReadLine();

            fileContent += "*" + userName + "#0#0#0#0%";
            File.WriteAllText("players.txt", fileContent);
            pagePVC.DownloadPlayers();   // Ponowne wczytanie zaktualizowanej bazy danych u¿ytkowników.
            Console.WriteLine("\n\nAdd new player."); ;
            Console.WriteLine("\nClick ENTER to reload page.");
            Console.ReadLine();
        }
        public void deleteUser(List<List<string>> playersDetails_PARTS)
        {
            string deleteUser_VALUE = "";
            string correctUser = "empty";
            int del_IDX = 0;
            string fileContent = "";

            Console.WriteLine("\n- - - - - - - - - - - - - -\n");
            Console.Write("Delete user: ");
            deleteUser_VALUE = Console.ReadLine();

            // Walidacja poprawnoœci u¿ytkowanika do usuniêcia:
            for (int i = 0; i < playersDetails_PARTS.Count; i++)
            {
                if (playersDetails_PARTS[i][0] == deleteUser_VALUE)
                {
                    correctUser = playersDetails_PARTS[i][0];
                    del_IDX = i;
                    break;
                }
            }
            if (correctUser != "empty")
            {
                // Usuniêcie okreœlonej listy zagnie¿d¿onej z listy nadrzêdnej danych u¿ytkowników:
                playersDetails_PARTS.RemoveAt(del_IDX);

                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                // Test: OK
                /*Console.WriteLine("\n\nTest:\n");
                for (int i = 0; i < playersDetails_PARTS.Count; i++)
                {
                    Console.WriteLine();
                    for (int j = 0; j < playersDetails_PARTS[i].Count; j++)
                    {
                        Console.Write(playersDetails_PARTS[i][j] + "   ");
                    }
                }
                Console.WriteLine("\n");*/
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

                // Tworzenie wartoœci zmiennej, przechowuj¹cej dane do zaktualizowania bazy danych u¿ytkowników "players.txt":
                for (int i = 0; i < playersDetails_PARTS.Count; i++)
                {
                    if (i > 0)   // UWAGA!!! Znak "*" wystêpuje tylko przy rozdzieleniu u¿ytkowników, nie ma go na pocz¹tku i koñcu!
                    {
                        fileContent += "*";
                    }
                    for (int j = 0; j < playersDetails_PARTS[i].Count; j++)
                    {
                        if (j < playersDetails_PARTS[i].Count - 1)   // Mniejszy ni¿ ostatni indeks.
                        {
                            fileContent += playersDetails_PARTS[i][j] + "#";
                        }
                        else if (j == playersDetails_PARTS[i].Count - 1)   // Ostatni indeks, aby nie by³o znaku "#" przed znakiem "*".
                        {
                            fileContent += playersDetails_PARTS[i][j];
                        }
                    }
                };

                // Zaktualizowanie danych bazy danych u¿ytkowników "players.txt":
                File.WriteAllText("players.txt", fileContent);
                pagePVC.DownloadPlayers();   // Ponowne wczytanie zaktualizowanej bazy danych u¿ytkowników.

                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                // Test: OK
                /*Console.WriteLine("\n\nDane do zapisu do pliku \"players.txt\":\n");
                Console.WriteLine(fileContent);*/
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

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

            // Walidacja poruszania siê po planszy:
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