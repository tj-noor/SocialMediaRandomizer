using System;

namespace CsvRandomizer
{
    public static class Utilities
    {
        /// <summary>
        /// Writes a Line to Console with the Requested Color
        /// </summary>
        /// <param name="line"></param>
        /// <param name="color"></param>
        public static void Log(string line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ResetColor();
        }

        public static ConsoleKeyInfo LogWithKeyInput(string line, ConsoleColor color)
        {
            Log(line, color);
            return Console.ReadKey(true);
        }
    }
}