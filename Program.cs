using System;
using Page_Intro;
using Page_Menu;
using Page_Options;

namespace Game {
    internal class Program {
        private static void Main(string[] args) {
            bool isCorrect = false;
            Console.Title = "Battleship Game 2: AI";
            Console.CursorVisible = false;
            Intro.RenderPage();
            Options.Upload.SearchFile(Options.optionsPath);   // Nie trzeba tworzyć instancji klasy "Options", gdyż używam zmiennych globalnych statycznych tej klasy. Utworzona instancja tej klasy będzie zawierała zaktualisowane dane.
            isCorrect = CheckOptionsValid(isCorrect);
            if (isCorrect) Menu.RenderPage();
        }
        private static bool CheckOptionsValid(bool isCorrect) {
            if (Options.isFile) {
                if (Options.isCorrectContent) {
                    isCorrect = true;
                } else {
                    isError(Options.errorCorrectContent);
                }
            } else {
                isError(Options.errorFile);
            }
            return isCorrect;
        }
        private static void isError(string error) {
            Console.Clear();
            Console.WriteLine(error);
            Console.WriteLine("\nFor this reason, the option data necessary for the application to run cannot be downloaded." +
                "\n\n\nClick [ENTER] to exit the program.");
            Console.ReadLine();
        }
    }
}
