using System;
using System.IO;
using System.Linq.Expressions;
using Page_Menu;

namespace Page_PVC
{

    // KIEDY SKOŃCZYSZ GRĘ - SPRÓBUJ ZAAPLICOWAĆ "static" DO INTERFEJSÓW STRON!
    /*interface IPagePVC   // Mogłem opuścić interfejs, aby mieć metody statyczne, ale używam go ponieważ chcę mieć widoczne na górze nazwy wszystkich metody danej klasy:
    {
        void PVC();   // Wyświetlenie strony PVC.
        void SelectPlayer();   // Etap 1: Wybór gracza i tworzenie gracza.
        void SetShips_PLAYER();   // Etap 2: Ustawienie statków gracza.
        void SetShips_COMPUTER();   // Etap 3: Ustawienie statków dla komputera.
        void BattleControl();   // Etap 4: Bitwa (panel kontrolny).
        void Battle_PLAYER();   // Przekierowanie na gracza.
        void Battle_COMPUTER();   // Przekierowanie na komputer.

        void SubmitRanking();   // Etap 5: Wpisz wynik do rankingu i pokaż ranking.
    }*/
    public class PagePVC/* : IPagePVC*/
    {
        public static bool isPVCShipPositingLoop = true;
        public static System.ConsoleKeyInfo mainKey;
        public void PVC()
        {
            System.ConsoleKeyInfo key;
            while (isPVCShipPositingLoop == true)
            {
                //MenuPage.currentSoundtrack.Stop();   // Test poprwności zamykania i ponownego odtwierania ścieżki dźwiękowej | OK
                //MenuPage.menuSoundtrack_PLAY = false;

                Console.Clear();
                Console.WriteLine("BBBBBBB   BB    BB   BBBBBBB");
                Console.WriteLine("BB    BB  BB    BB  BB      ");
                Console.WriteLine("BB    BB  BB    BB  BB      ");
                Console.WriteLine("BBBBBBB   BB    BB  BB      ");
                Console.WriteLine("BB         BB  BB   BB      ");
                Console.WriteLine("BB          BBBB    BB      ");
                Console.WriteLine("BB           BB      BBBBBBB");
                Console.WriteLine("\n- - - - - - - - - - - - - -\n");
                Console.WriteLine("Back to menu: [Q]\n");

                SelectPlayer();

                //PagePVC.SetShips_PLAYER(PagePVC.mainKey);   // Ta medota musi być przrd metodą "Console.ReadKey()", ponieważ jej wynik musi być
                // wyswietlony po metodzie "Console.Clear()", której stan (stan wyświetlacza konsoli) jest zatrzymywany przez "Console.ReadKey()".

                PagePVC.mainKey = Console.ReadKey(true);
                if (mainKey.Key == System.ConsoleKey.Q)
                {
                    isPVCShipPositingLoop = false;
                    MenuPage.isMenuButtonLoop = true;
                    MenuPage.Menu();
                }
            }
        }
        public static void SelectPlayer()
        {
            Console.WriteLine("Select player:");




            string filePath = "players.txt";
            string content = "Gracz 1";

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

            try
            {
                // Odczytaj cały tekst z pliku
                string fileContent = File.ReadAllText(filePath);
                string[] players = fileContent.Split('*');
                string[,] playersName = new string[players.Length, 5];
                string[] playerDetails = null;
                for (int i = 0; i < players.Length; i++)
                {
                    // Każdy gracz ma 5 informacji oddzielonych znakiem "#":
                    playerDetails = players[i].Split('#');
                    for (int j = 0; j < playerDetails.Length; j++)
                    {
                        playersName[i, j] = playerDetails[j];
                    }

                    // Wypełnienie pozostałych elementów pustymi wartościami, jeśli playerDetails ma mniej niż 5 elementów
                    for (int j = playerDetails.Length; j < 5; j++)
                    {
                        playersName[i, j] = "";
                    }
                }

                // Wyświetlanie zawartości playersName dla sprawdzenia
                /*for (int i = 0; i < players.Length; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Console.Write(playersName[i, j] + " | ");
                    }
                    Console.WriteLine();
                }*/
                for (int i = 0; i < players.Length; i++)
                {
                    Console.Write(playersName[i, 0]);
                    Console.WriteLine();
                }


                //Console.WriteLine("File content:");
                //Console.WriteLine(fileContent);
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred while reading the file:");
                Console.WriteLine(ex.Message);
            }

            // Gracz 1#3480#5#4#22%
            // 3480 - punkty
            // 5 - zatopione statki(wroga)
            // 4 - stracone statki(swoje)
            // 22% - celność






        }
        public static void SetShips_PLAYER(System.ConsoleKeyInfo key)
        {
            // Walidacja poruszania się po planszy:
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
