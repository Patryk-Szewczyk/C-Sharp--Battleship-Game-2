using System;
using System.Collections.Generic;
using Controler;
using Page_Intro;
using Page_Menu;
//using Page_PVP;
//using Page_PVC;
using Page_Instructions;
//using Page_Ranking;
//using Page_Options;
//using Page_Credits;

namespace Controler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Battleship Game 2";
            Console.CursorVisible = false;
            //Console.ForegroundColor = ConsoleColor.Cyan;

            IntroPage.Intro();
            MenuPage.Menu();
        }
    }
}
