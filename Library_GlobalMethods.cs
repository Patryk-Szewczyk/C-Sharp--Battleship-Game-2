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
        public static (int, int) SearchRem(List<int> array, int target) {   // Szukanie wartości i jej indeksu (lokalizacji) w tablicy:
            int resultVal = -1;   // W kontekście losowania statków dla komputera, oznacza to kolizję pola począttkowego nowego statku z już istniejącym.
            int resultVal_Idx = -1;
            for (int i = 0; i < array.Count; i++) {
                if (target == array[i]) {
                    resultVal = array[i];
                    resultVal_Idx = i;
                    break;
                }
            }
            return (resultVal, resultVal_Idx);
        }
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
            //int dirNumGap = 1;
            Random rand = new Random();
            //Console.WriteLine("Random test: " + rand.Next(0, board.Count));   // (0, 100)
            int initField = 0;
            ValueTuple<int, int> binSorSpc_tuple = (0, 0);
            int shipDist = 0;
            int limit = 0;
            while (isCor == false) {
                Console.Clear();   // SKASUJ PÓŹNIEJ TO!!!!!!!!!!!!!!!!!!
                Console.WriteLine("- - - - - - - - - - - - - - - - - -");
                Console.WriteLine("NOWY WHILE");
                board = new List<int>(array);   // Dlaczego tak, a nie board = array ? Gdyż lista jest przekazywana nie kopią a referencją, w związku z czym odwołuję się do pierwotnie zadeklarowanej listy i zmniejszam ją w nieskończoność, zamiast tworzyć nową kopię.
                Console.WriteLine("Board length: " + board.Count);
                for (int i = 0; i < shipsList.Count; i++) {
                    // Początkowe pole:
                    dirVal = dirAr[rand.Next(0, dirAr.Length)];
                    //dirNumGap = (dirVal == "toBottom") ? 10 : dirNumGap;
                    initField = rand.Next(0, board.Count);
                    binSorSpc_tuple = GlobalMethod.SearchRem(board, initField);
                    board.Remove(binSorSpc_tuple.Item2);
                    Console.WriteLine("- - - - - - - - - - - - - - - - - -");
                    Console.WriteLine("Board length: " + board.Count);
                    Console.WriteLine("initField: " +  initField);
                    Console.WriteLine("Długość: " + shipsList[i].Count + " | Kierunek: " + dirVal + " | Znaleziony: " + binSorSpc_tuple.Item1 + " | Index listy do usunięcia: " + binSorSpc_tuple.Item2);
                    // Walidacja wychodzenia poza planszę:
                    if (dirVal == "toRight") {
                        // Inicjalizacja statku:
                        shipDist = initField + (shipsList[i].Count - 1);
                        limit = (Convert.ToString(initField).Length == 1) ? 9 : 9 + (10 * int.Parse(Convert.ToChar(Convert.ToString(initField)[0]).ToString()));
                        if (shipDist > limit) break;
                        shipsList[i][0] = initField;
                        // Tworzenie statku:
                    } else {
                        // Inicjalizacja statku:
                        shipDist = (initField * 10) + ((shipsList[i].Count * 10) - 10);
                        limit = (Convert.ToString(initField).Length == 1) ? 90 : 90 + (1 * int.Parse(Convert.ToChar(Convert.ToString(initField)[1]).ToString()));
                        if (shipDist > limit) break;
                        shipsList[i][0] = initField;
                        // Tworzenie statku:
                    }
                    if (binSorSpc_tuple.Item1 == -1 && binSorSpc_tuple.Item2 == -1) break;   // Drugi warunek oznacza kolizję pola początkowego nowego statku z już istniejącym.
                    if (i == shipsList.Count - 1) isCor = true;
                }

                // Pozostało: Ogarnięcie tworzenia statów od punktu początkowego, zapisywania ich współrzędnych statków

                //Console.WriteLine("Kierunek: " + dirVal);
                //Console.WriteLine("Odstęp: " + dirNumGap);
                //Console.ReadLine();
            }
            //for (int m = 0; m < 100; m++) {
              /*for (int i = 0; i < fleet.Length; i++) {
                    for (int j = 0; j < fleet[i]; j++) {
                        shipsList[i][j] = j + 1;
                    }
                }*/
            //}
            
            return shipsList;
        }
    }
}
