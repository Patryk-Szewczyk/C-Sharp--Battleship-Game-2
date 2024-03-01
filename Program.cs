using System;
using System.Collections.Generic;
using System.Media;
using Game;
using Page_Intro;
using Page_Menu;

namespace Game
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
