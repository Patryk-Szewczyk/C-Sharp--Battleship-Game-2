using System;

namespace Page_Intro
{
    public class IntroPage
    {
        public static void Intro()
        {
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
            System.ConsoleKeyInfo corr_key;
            bool isEnter = false;
            while (isEnter == false)
            {
                corr_key = Console.ReadKey(true);  // "ture", bo nie chcę widzieć znaku
                if (corr_key.Key == System.ConsoleKey.Enter)
                {
                    isEnter = true;
                }
            }
        }
    }
}

