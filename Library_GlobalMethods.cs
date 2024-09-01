using System;
using System.Collections.Generic;
using System.Configuration;

namespace Library_GlobalMethods
{
    public class GlobalMethod
    {
        public static void Color(string text, ConsoleColor color)   // Kolorowy tekst
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public static int SearchRem(List<int> array, int target) {   // Szukanie wartości i jej indeksu (lokalizacji) w tablicy:
            int resultVal_Idx = -1;   // W kontekście losowania statków dla komputera, oznacza to kolizję pola począttkowego nowego statku z już istniejącym.
            for (int i = 0; i < array.Count; i++) {
                if (target == array[i]) {
                    resultVal_Idx = i;
                    break;
                }
            }
            return resultVal_Idx;
        }
        /*public static (int, int) SearchRemTar(List<int> array, int target) {   // Szukanie wartości i jej indeksu (lokalizacji) w tablicy:
            int resultVal = -1;
            int resultVal_Idx = -1;   // W kontekście losowania statków dla komputera, oznacza to kolizję pola począttkowego nowego statku z już istniejącym.
            for (int i = 0; i < array.Count; i++) {
                if (target == array[i]) {
                    resultVal = array[i];
                    resultVal_Idx = i;
                    break;
                }
            }
            return (resultVal, resultVal_Idx);
        }*/
        public static List<List<int>> RandShips(List<int> array, int[] fleet) {
            // Deklaracja dopowiedniej grupy statków: Dzięki liście dynamicznej mogę tworzyć dowolnych rozmiarów grupę statków, kontrolowaną z poziomu przekazywania uprzednio ustalonych parametrów do metody.
            List<List<int>> shipsList = new List<List<int>>();
            for (int i = 0; i < fleet.Length; i++) {
                List<int> ship = new List<int>();
                shipsList.Add(ship);
                for (int j = 0; j < fleet[i]; j++) {
                    shipsList[i].Add(i + 1);
                }
            }

            // Zapełnianie statków zawlidowanymi współrzędnymi:
            bool isCor = false;
            List<int> board = array;
            string dirVal = "";
            string[] dirAr = new string[2] { "toRight", "toBottom" };
            Random rand = new Random();
            //Console.WriteLine("Random test: " + rand.Next(0, board.Count));   // (0, 100)
            int initField = 0;
            int srchSpc = 0;
            int shipDist = 0;
            int limit = 0;
            bool isShip = false;
            while (isCor == false) {
                //Console.Clear();   // SKASUJ PÓŹNIEJ TO!!!!!!!!!!!!!!!!!!
                //Console.WriteLine("- - - - - - - - - - - - - - - - - -");
                //Console.WriteLine("NOWY WHILE");
                board = new List<int>(array);   // Dlaczego tak, a nie board = array ? Gdyż lista jest przekazywana nie kopią a referencją, w związku z czym odwołuję się do pierwotnie zadeklarowanej listy i zmniejszam ją w nieskończoność, zamiast tworzyć nową kopię.
                //Console.WriteLine("Board length: " + board.Count);
                for (int i = 0; i < shipsList.Count; i++) {
                    // Początkowe pole:
                    dirVal = dirAr[rand.Next(0, dirAr.Length)];
                    //dirNumGap = (dirVal == "toBottom") ? 10 : dirNumGap;
                    initField = rand.Next(0, board.Count);
                    srchSpc = GlobalMethod.SearchRem(board, initField);
                    if (srchSpc == -1) break;   // Kolizję pola początkowego nowego statku z już istniejącym.
                    board.Remove(srchSpc);
                    //Console.WriteLine("- - - - - - - - - - - - - - - - - -");
                    //Console.WriteLine("Board length: " + board.Count);
                    //Console.WriteLine("initField: " +  initField);
                    //Console.WriteLine("Długość: " + shipsList[i].Count + " | Kierunek: " + dirVal + " | Znaleziony: " + binSorSpc_tuple.Item1 + " | Index listy do usunięcia: " + binSorSpc_tuple.Item2);
                    // Walidacja wychodzenia poza planszę:
                    if (dirVal == "toRight") {
                        // Inicjalizacja statku:
                        shipDist = initField + (shipsList[i].Count - 1);
                        limit = (Convert.ToString(initField).Length == 1) ? 9 : 9 + (10 * int.Parse(Convert.ToChar(Convert.ToString(initField)[0]).ToString()));
                        if (shipDist > limit) break;   // Jeżeli statek wychodzi poza planszę, losuj statki od nowa.
                        // Tworzenie statku:
                        if (shipsList[i].Count > 1) {   // Tworzenie pól długości dla statków powyżej 1 pola długości (2, 3, 4 ...).
                            for (int j = 1; j < shipsList[i].Count; j++) {
                                isShip = false;
                                srchSpc = GlobalMethod.SearchRem(board, initField + j);
                                if (srchSpc != -1) {   // Jeżeli nie ma kolizji statku
                                    board.Remove(srchSpc);
                                    shipsList[i][j] = initField + j;
                                    isShip = true;
                                }
                            }
                            if (isShip == false) break;
                        }
                        shipsList[i][0] = initField;   // W sumie, zrób tak, żeby aktualizacja wszystkiego była na końcu. Inicjacyjna wartość współrzędnej nie ucieknie Ci :) Jest to na końcu aby nie wciskać wartości gdy reszta pól długości statku będzie miała nieprawidłowe współrzędne. Operacja ta zaoszczędza mocy obliczeniowej.
                    } else {
                        shipDist = (initField * 10) + ((shipsList[i].Count * 10) - 10);
                        limit = (Convert.ToString(initField).Length == 1) ? 90 : 90 + (1 * int.Parse(Convert.ToChar(Convert.ToString(initField)[1]).ToString()));
                        if (shipDist > limit) break;
                        if (shipsList[i].Count > 1) {
                            for (int j = 10; j < shipsList[i].Count * 10; j=j+10) {
                                isShip = false;
                                srchSpc = GlobalMethod.SearchRem(board, initField + j);
                                if (srchSpc != -1) {   // Jeżeli nie ma kolizji statku
                                    board.Remove(srchSpc);
                                    shipsList[i][j/10] = initField + j;
                                    isShip = true;
                                }
                            }
                            if (isShip == false) break;
                        }
                        shipsList[i][0] = initField;
                    }
                    if (i == shipsList.Count - 1) isCor = true;
                }

                // Pozostało: Ogarnięcie tworzenia statów od punktu początkowego, zapisywania ich współrzędnych statków

                //Console.WriteLine("Kierunek: " + dirVal);
                //Console.WriteLine("Odstęp: " + dirNumGap);
                //Console.ReadLine();
            }
            
            return shipsList;
        }
    }
}
