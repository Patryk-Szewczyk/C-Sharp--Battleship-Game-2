using System;
using System.Collections.Generic;

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
            int resultVal = -1;
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
            List<int> board = new List<int>();
            string dirVal = "";
            string[] dirAr = new string[2] { "toRight", "toBottom" };
            int dirNumGap = 1;
            Random rand = new Random();
            //Console.WriteLine("Random test: " + rand.Next(0, board.Count));   // (0, 100)
            int initField = 0;
            ValueTuple<int, int> binSorSpc_tuple = (0, 0);
            while (isCor == false) {
                board = array;
                for (int i = 0; i < shipsList.Count; i++) {
                    // Początkowe pole:
                    dirVal = dirAr[rand.Next(0, dirAr.Length)];
                    dirNumGap = (dirVal == "toBottom") ? 10 : dirNumGap;
                    initField = rand.Next(0, board.Count);
                    binSorSpc_tuple = GlobalMethod.SearchRem(board, initField);
                    board.Remove(binSorSpc_tuple.Item2);
                    Console.WriteLine("- - - - - - - - - - - - - - - - - -");
                    Console.WriteLine("Znaleziony: " + binSorSpc_tuple.Item1 + " | Index listy do usunięcia: " + binSorSpc_tuple.Item2);
                    // Walidacja wychodzenia poza planszę:

                }

                Console.WriteLine("Kierunek: " + dirVal);
                Console.WriteLine("Odstęp: " + dirNumGap);
                Console.ReadLine();
            }
            //for (int m = 0; m < 100; m++) {
            for (int i = 0; i < fleet.Length; i++) {
                    for (int j = 0; j < fleet[i]; j++) {
                        shipsList[i][j] = j + 1;
                    }
                }
            //}
            
            return shipsList;
        }
    }
}
