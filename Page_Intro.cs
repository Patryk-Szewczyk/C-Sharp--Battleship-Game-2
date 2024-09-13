using Library_GlobalMethods;
using System;
using System.Collections.Generic;

namespace Page_Intro {
    public class Intro {
        public static void RenderPage() {
            Intro.Info();


            // Test losowania statków:
            /*List<int> plansza = new List<int>();
            List<int> statki = new List<int> { 2, 2, 2, 3, 3, 4, 5 };   // Limit 10 statków o długości 10
            List<List<int>> listaStatkow = new List<List<int>>();
            for (int i = 0; i < 100; i++) {
                plansza.Add(i);
            }
            Console.Clear();
            Console.WriteLine("Loading...");
            listaStatkow = GlobalMethod.RandomShips(plansza, GlobalMethod.PrepareShips(statki));
            Console.Clear();
            Console.WriteLine("\n\n");
            for (int i = 0; i < listaStatkow.Count; i++) {
                for (int j = 0; j < listaStatkow[i].Count; j++) {
                    Console.Write(listaStatkow[i][j] + " ");
                }
                Console.WriteLine();
            }*/


            Intro.LoopCorrectKey();
        }
        private static void Info() {
            Console.WriteLine("\nBattleship 2 AI [Version 1.00]" +
                "\nCopyright (c) Patryk Szewczyk 20841 | 2 INF, AHNS. All rights reserved." +
                "\n\nBattleship Game is simple game which depend of sunking ships between players." +
                "\nTo start game you must do a few activites to mainly set ships." +
                "\n\nI very please YOU about enable full screen." +
                "\nIn else case you see errors in figure of NOT clearing console code (in console top)." +
                "\nAlso I please you about turn on sounds to minimum 20%." +
                "\n\nWARNING! The closing credits were created as an \".html\" file that plays in the" +
                "\ndefault browser. In order for the end credits to play correctly, go to the main" +
                "\ndirectory and set the \"credits.html\" file properties to the option to ALWAYS open" +
                "\nit in the browser. To do this, select the \"select another application\" option and" +
                "\nselect a browser. Additionally maximize your browser window." +
                "\n\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" +
                "\n\nTo continue, click ENTER key:\n");
        }
        private static void LoopCorrectKey() {
            System.ConsoleKeyInfo corrKey;
            bool isEnter = false;
            while (!isEnter) {
                corrKey = Console.ReadKey(true);  // "ture", bo nie chcę widzieć znaku
                if (corrKey.Key == System.ConsoleKey.Enter) {
                    isEnter = true;
                }
            }
        }
    }
}

