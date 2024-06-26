using System;

namespace GlobalMethods_Bank
{
    public class GlobalMethod
    {
        public static void Color(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
