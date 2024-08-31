using System;
using Page_Intro;
using Page_Menu;

namespace Game {
    internal class Program {
        private static void Main(string[] args) {
            Console.Title = "Battleship Game 2";
            Console.CursorVisible = false;
            IntroPage.Intro();
            MenuPage.Menu();
        }
    }
}