using System;
using System.Configuration;
using System.IO;
using System.Linq.Expressions;
using Page_Menu;
using System.Collections.Generic;

namespace Page_PVC
{

    // KIEDY SKO—CZYSZ GR  - SPR”BUJ ZAAPLICOWA∆ "static" DO INTERFEJS”W STRON!
    /*interface IPagePVC   // Mog≥em opuúciÊ interfejs, aby mieÊ metody statyczne, ale uøywam go poniewaø chcÍ mieÊ widoczne na gÛrze nazwy wszystkich metody danej klasy:
    {
        void PVC();   // Wyúwietlenie strony PVC.
        void SelectPlayer();   // Etap 1: WybÛr gracza i tworzenie gracza.
        void SetShips_PLAYER();   // Etap 2: Ustawienie statkÛw gracza.
        void SetShips_COMPUTER();   // Etap 3: Ustawienie statkÛw dla komputera.
        void BattleControl();   // Etap 4: Bitwa (panel kontrolny).
        void Battle_PLAYER();   // Przekierowanie na gracza.
        void Battle_COMPUTER();   // Przekierowanie na komputer.

        void SubmitRanking();   // Etap 5: Wpisz wynik do rankingu i pokaø ranking.
    }*/
    public class PagePVC/* : IPagePVC*/
    {
        //----------------------------------------------------------------------------------------
        public static bool isPVCShipPositingLoop = true;
        public static bool isCorrectSign = false;
        public static System.ConsoleKeyInfo mainKey;
        //public static int playersLimit = 20;
        //public static int playerButtNum = 0;   // Zawsze ostatni, bo chcÍ mieÊ kursor na gÛrze!
        public static bool isSelectPlayer = false;
        public static string userName = "";
        public static int player_IDX = 0;
        public static bool isOption_CREATE = false, isOption_DELETE = false;
        public static string addUser_VALUE = "", deleteUser_VALUE = "";
        //----------------------------------------------------------------------------------------
        // TE MUSZ• BY∆ GLOBALNE!!! - - - - -------------------------------------------------- ResztÍ przenieú jako lokalne do w≥aúciwych metod!
        public static PagePVC pagePVC = new PagePVC();
        public static List<List<string>> /*string[,]*/ playersDetails_PARTS = new List<List<string>>();
        //----------------------------------------------------------------------------------------
        public void PVC()
        {

            pagePVC.DownloadPlayers();
            int playerButtNum_POINTER = PagePVC.playersDetails_PARTS.Count;   // UWAGA!!! Ta deklaracja ma znajdowaÊ siÍ tutaj, poniewaø w zmienna "playersDetails_PARTS"   // Zawsze ostatni <wartoúÊ>, bo chcÍ mieÊ kursor na gÛrze!
            // jest zadeklarowana jako pusta lista zagnieødøona, a jej wartoúÊ jest okreúlana w metodzie "DownloadPlayers".
            // Reset zmiennych poza-pÍtlowych strony: [gracz]
            PagePVC.player_IDX = 0;
            PagePVC.userName = playersDetails_PARTS[0][0];

            System.ConsoleKeyInfo key;
            while (isPVCShipPositingLoop == true)
            {
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                // Wyúwietlenie informacji strony (stanu wyboru uøytkownika):
                if (PagePVC.isSelectPlayer == false)
                {
                    pagePVC.SelectUserPart(PagePVC.playersDetails_PARTS, playerButtNum_POINTER);
                    if (PagePVC.isOption_CREATE == true)
                    {
                        PagePVC.isOption_CREATE = false;
                        Console.WriteLine("\n- - - - - - - - - - - - - -\n");
                        Console.Write("UtwÛrz uøytkownika: ");
                        PagePVC.addUser_VALUE = Console.ReadLine();
                        // Dodawanie uøytkownika:
                        pagePVC.addUser(PagePVC.addUser_VALUE, PagePVC.playersDetails_PARTS);
                        // Ponowne wyúwietlenie stanu wyboru uøytkownika:
                        pagePVC.SelectUserPart(PagePVC.playersDetails_PARTS, playerButtNum_POINTER);
                    }
                    else if (PagePVC.isOption_DELETE == true)
                    {
                        PagePVC.isOption_DELETE = false;
                        Console.WriteLine("\n- - - - - - - - - - - - - -\n");
                        Console.Write("Skasuj uøytkownika: ");
                        PagePVC.deleteUser_VALUE = Console.ReadLine();
                        // Uwuwanie uøytkownika:
                        pagePVC.deleteUser(PagePVC.deleteUser_VALUE, PagePVC.playersDetails_PARTS);
                        // Ponowne wyúwietlenie stanu wyboru uøytkownika:
                        pagePVC.SelectUserPart(PagePVC.playersDetails_PARTS, playerButtNum_POINTER);
                    }
                }
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                // Operacja klawiszowe:
                else if (PagePVC.isSelectPlayer == true)
                {
                    pagePVC.SetShips_PLAYER(PagePVC.mainKey);   // Ta medota musi byÊ przrd metodπ "Console.ReadKey()", poniewaø jej wynik musi byÊ
                    // wyswietlony po metodzie "Console.Clear()", ktÛrej stan (stan wyúwietlacza konsoli) jest zatrzymywany przez "Console.ReadKey()".
                }
                // PÍtla ta uniemoøliwia prze≥adowanie strony kiedy kliknie siÍ niew≥aúciwy klawisz.
                while (isCorrectSign == false)
                {
                    PagePVC.mainKey = Console.ReadKey(true);
                    if (PagePVC.mainKey.Key == System.ConsoleKey.W || PagePVC.mainKey.Key == System.ConsoleKey.S || PagePVC.mainKey.Key == System.ConsoleKey.D || PagePVC.mainKey.Key == System.ConsoleKey.A || PagePVC.mainKey.Key == System.ConsoleKey.UpArrow || PagePVC.mainKey.Key == System.ConsoleKey.DownArrow || PagePVC.mainKey.Key == System.ConsoleKey.LeftArrow || PagePVC.mainKey.Key == System.ConsoleKey.RightArrow || PagePVC.mainKey.Key == System.ConsoleKey.C || PagePVC.mainKey.Key == System.ConsoleKey.P || PagePVC.mainKey.Key == System.ConsoleKey.Enter || PagePVC.mainKey.Key == System.ConsoleKey.Backspace)
                    {
                        isCorrectSign = true;
                    }
                }
                isCorrectSign = false;
                // Akcje na klawisze:
                PagePVC.isOption_CREATE = (PagePVC.mainKey.Key == System.ConsoleKey.C) ? true : false;
                PagePVC.isOption_DELETE = (PagePVC.mainKey.Key == System.ConsoleKey.P) ? true : false;
                if (PagePVC.mainKey.Key == System.ConsoleKey.Backspace)
                {
                    isPVCShipPositingLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    PagePVC.isSelectPlayer = false;
                    MenuPage.Menu();
                }
                else if (PagePVC.mainKey.Key == System.ConsoleKey.Enter && (PagePVC.isOption_CREATE == false || PagePVC.isOption_DELETE == false))
                {
                    PagePVC.isSelectPlayer = true;
                }
                if (PagePVC.isSelectPlayer == false)   // Poruszanie siÍ po przyciskach (obliczenia):
                {
                    if (PagePVC.mainKey.Key == System.ConsoleKey.UpArrow || PagePVC.mainKey.Key == System.ConsoleKey.W)
                    {
                        playerButtNum_POINTER = (playerButtNum_POINTER < PagePVC.playersDetails_PARTS.Count) ? playerButtNum_POINTER += 1 : playerButtNum_POINTER;
                        if (PagePVC.player_IDX > 0)
                        {
                            PagePVC.player_IDX--;
                            PagePVC.userName = PagePVC.playersDetails_PARTS[PagePVC.player_IDX][0];
                        }
                    }
                    else if (PagePVC.mainKey.Key == System.ConsoleKey.DownArrow || PagePVC.mainKey.Key == System.ConsoleKey.S)
                    {
                        playerButtNum_POINTER = (playerButtNum_POINTER > 1) ? playerButtNum_POINTER -= 1 : playerButtNum_POINTER;
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
        public void SelectUserPart(List<List<string>> playersDetails_PARTS, int playerButtNum_POINTER)   // Wyúwietlenie wszytkich informacji na stronie "PagePVC":  ShowPageData(string[,] array, int index_number) 
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
                for (int i = 0, j = playersDetails_PARTS.Count; i < playersDetails_PARTS.Count; i++, j--)
                {
                    if (j == playerButtNum_POINTER)
                    {
                        Console.WriteLine("> " + playersDetails_PARTS[i][0]);
                    }
                    else
                    {
                        Console.WriteLine("  " + playersDetails_PARTS[i][0]);
                    }
                }
            }
        }
        public void DownloadPlayers()   // Pobranie danych z pliku tekstowego uøytkowanikÛw: (nie zrobi≥em globalnej tablicy jako listy i z tego powodu zrobi≥em osobnπ metode do wyúwietlania zaktualizowanej tablicy graczy)
        {
            // Odczytaj ca≥y tekst z pliku
            string fileContent = File.ReadAllText("players.txt");
            string[] players = fileContent.Split('*');
            string[,] playersDetails_PARTS_AR = new string[players.Length, 5];
            List<List<string>> playersDetails_PARTS_LT_Level_1 = new List<List<string>>();
            string[] playerDetails_BLOCK = null;
            for (int i = 0; i < players.Length; i++)
            {
                List<string> playersDetails_PARTS_LT_Level_2 = new List<string>();
                // Kaødy gracz ma 5 informacji oddzielonych znakiem "#":
                playerDetails_BLOCK = players[i].Split('#');
                for (int j = 0; j < playerDetails_BLOCK.Length; j++)
                {
                    playersDetails_PARTS_AR[i, j] = playerDetails_BLOCK[j];
                    playersDetails_PARTS_LT_Level_2.Add(playerDetails_BLOCK[j]);
                }
                playersDetails_PARTS_LT_Level_1.Add(playersDetails_PARTS_LT_Level_2);
            }
            PagePVC.playersDetails_PARTS = playersDetails_PARTS_LT_Level_1;   // Wczeúniej: playersDetails_PARTS_AR
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
        public void addUser(string userName, List<List<string>> playersDetails_PARTS)
        {
            string fileContent = File.ReadAllText("players.txt");
            fileContent += "*" + userName + "#0#0#0#0%";
            File.WriteAllText("players.txt", fileContent);
            pagePVC.DownloadPlayers();   // Ponowne wczytanie zaktualizowanej bazy danych uøytkownikÛw.
            PagePVC.player_IDX++;   // UWAGA! ROZWI•Ø TEN PROBLEM NA WSKAèNIKU!!!
            PagePVC.userName = PagePVC.playersDetails_PARTS[PagePVC.player_IDX][0];
            Console.WriteLine("\nDodano uøytkownika: " + userName);
            Console.ReadLine();
        }
        public void deleteUser(string userName, List<List<string>> playersDetails_PARTS)
        {
            Console.WriteLine("Uøytkownik do usuniÍcia: " + userName);
            Console.ReadLine();
        }
        public void SetShips_PLAYER(System.ConsoleKeyInfo key)
        {
            Console.Clear();

            // Walidacja poruszania siÍ po planszy:
            Console.WriteLine("Ustaw swoje statki: [" + PagePVC.userName + "]");
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