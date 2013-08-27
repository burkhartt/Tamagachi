using System;
using System.Drawing;

namespace Tamagotchi {
    public static class ConsoleWriter {
        public static void WriteAnimal(Color pixel, int x, int y) {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ClosestConsoleColor(pixel.R, pixel.G, pixel.B);
            Console.Write("#");
        }

        public static void WriteMenu(int option, string title) {
            Console.SetCursorPosition(55, 3 + option);
            Console.WriteLine("{0}. {1}", option, title);
        }

        private static ConsoleColor ClosestConsoleColor(byte r, byte g, byte b) {
            ConsoleColor ret = 0;
            double rr = r, gg = g, bb = b, delta = Double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof (ConsoleColor))) {
                var n = Enum.GetName(typeof (ConsoleColor), cc);
                var c = Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
                var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta) {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }
    }
}