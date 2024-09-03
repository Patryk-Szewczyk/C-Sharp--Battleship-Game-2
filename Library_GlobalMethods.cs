using System;
using System.Collections.Generic;
using System.Configuration;

namespace Library_GlobalMethods {
    public class GlobalMethod {
        public static void Color(string text, ConsoleColor color) {   // Kolorowy tekst
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public static int SearchRemA(List<int> array, int target) {   // Szukanie wartości i jej indeksu (lokalizacji) w tablicy:
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
        public static List<List<int>> RandShips(List<int> array, List<int> fleet) {
            // Deklaracja dopowiedniej grupy statków: Dzięki liście dynamicznej mogę tworzyć dowolnych rozmiarów grupę statków, kontrolowaną z poziomu przekazywania uprzednio ustalonych parametrów do metody.
            List<List<int>> shipsList = new List<List<int>>();
            for (int i = 0; i < fleet.Count; i++) {
                List<int> ship = new List<int>();
                shipsList.Add(ship);
                for (int j = 0; j < fleet[i]; j++) {
                    shipsList[i].Add(i + 1);
                }
            }

            bool isCor = false;
            List<int> board = array;
            string dirVal = "";
            string[] dirAr = new string[2] { "toRight", "toBottom" };
            Random rand = new Random();
            int initField = 0;
            int srchSpc = 0;
            int dirDist = 1, numSlice = 0, numVal = 10;
            int shipDist = 0, limit = 0;
            double minBott = shipsList.Count / 2, bottCount = 0;
            minBott = (shipsList.Count % 2 == 1) ? minBott = Math.Floor(minBott) + rand.Next(0, 2) : minBott = shipsList.Count / 2;   // Jeżeli mam liczbę nieparzystą, to czy będzie więcej czy mniej statków "toBottom" o 1 zależy od losowości.
            bool isShip = false;
            while (!isCor) {
                board = new List<int>(array);   // Dlaczego tak, a nie board = array ? Gdyż lista jest przekazywana nie kopią a referencją, w związku z czym odwołuję się do pierwotnie zadeklarowanej listy i zmniejszam ją w nieskończoność, zamiast tworzyć nową kopię.
                bottCount = 0;
                for (int i = 0; i < shipsList.Count; i++) {
                    initField = rand.Next(0, board.Count);
                    srchSpc = GlobalMethod.SearchRemA(board, initField);
                    if (srchSpc == -1) break;   // Kolizję pola początkowego nowego statku z już istniejącym.
                    board.RemoveAt(srchSpc);
                    dirVal = dirAr[rand.Next(0, dirAr.Length)];
                    dirDist = (dirVal == "toRight") ? 1 : 10;
                    numSlice = (dirVal == "toRight") ? 0 : 1;
                    numVal = (dirVal == "toRight") ? 10 : 1;
                    if (dirVal == "toBottom") bottCount++;
                    shipDist = (initField * dirDist) + ((shipsList[i].Count * dirDist) - dirDist);   // Obliczanie długości statku na planszy.
                    limit = (Convert.ToString(initField).Length == 1) ? (9 * dirDist) : (9 * dirDist) + (numVal * int.Parse(Convert.ToChar(Convert.ToString(initField)[numSlice]).ToString()));   // Jeżeli statek jest większy niż 1 pole długości - współrzędna inicjacyjna jest "ciachana" w celu dodania odpowiedniej jej częsci do bazowej liczby limitu, aby ostatecznie wyznaczyć odpowiedni limit dla statku znajdującym się w odpowiednim polu. Np. Dla statku o długości 3, w kierunku "toRight" bazawa wartość limitu wynosi 9, jeżeli współrzędna początkowa wynosi 54, to wycinana jest 5, mnożona przez 10 i dodawana do 9, w związku z czym mamy 59. Analogicznie jest w przypadku toBottom, tylko wartości są tak dostosowane aby dotyczyły NIE częsści dziesiętnej, a części jedności. Wówczas będziemy mięli 94.
                    if (shipDist > limit) break;   // Jeżeli statek wychodzi poza planszę, losuj statki od nowa. Jeżeli np. mamy statek długości 3, "toRight", a współrzędną początkową 54, to limit wynosi 59, a "shipDist" wynosi [współrzędna początkowo] + [długość statku] - 1, czyli 56. Wówczas mamy 56 <= 59, co jest prawdą, więc tworzenie staatku przechodzi ten etap.
                    if (shipsList[i].Count > 1) {   // Tworzenie pól długości dla statków powyżej 1 pola długości (2, 3, 4 ...).
                        for (int j = 1 * dirDist; j < shipsList[i].Count * dirDist; j=j+dirDist) {
                            isShip = false;
                            srchSpc = GlobalMethod.SearchRemA(board, initField + j);
                            if (srchSpc != -1) {   // Jeżeli nie ma kolizji statku
                                board.RemoveAt(srchSpc);
                                shipsList[i][j/dirDist] = initField + j;
                                isShip = true;
                            }
                            if (!isShip) break;   // Dlaczego dwa "break" i warunek? Normalnie wystarczyłby ten, ale ponieważ "break" ogranicza się do najbliższego "for", więc zrobiłem specjalny warunek, za pomocą którego będziemy kontrolować "break" pętli for wewnętrznej i "właściwej" nadrzędnej.
                        }
                        if (!isShip) break;
                    }
                    shipsList[i][0] = initField;   // Jest to na końcu aby nie wciskać wartości gdy reszta pól długości statku będzie miała nieprawidłowe współrzędne. Operacja ta zaoszczędza mocy obliczeniowej.
                    if (i == shipsList.Count - 1) {   // Jeżeli 7 statków uzyskało poprawne wspoółrzędne, przejdź dalej;
                        if (bottCount == minBott) isCor = true;   // Jeżeli liczba statków "toBottom" jest właściwa, zakończ algorytm.
                    }
                }
            }
            return shipsList;
        }
    }
}
