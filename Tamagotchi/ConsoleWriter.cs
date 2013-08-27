using System;
using System.Drawing;

namespace Tamagotchi {
    public static class ConsoleWriter {
        public static void WriteAnimal(Color pixel, int x, int y) {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ClosestConsoleColor(pixel.R, pixel.G, pixel.B);
			Console.Write("█");
        }

        public static void WriteMenu(int option, string title) {
            Console.SetCursorPosition(55, 53 + option);
            Console.WriteLine("{0}. {1}", option, title);
        }

        public static void WriteMood(string mood) {
            Console.SetCursorPosition(1, 52);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                                                                  ");
            Console.SetCursorPosition(1, 52);
            Console.WriteLine(mood);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteHealth(int health) {
            Console.SetCursorPosition(1, 50);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                                                                                  ");
            Console.SetCursorPosition(1, 50);
            Console.WriteLine("Health: {0}%", health);
            Console.ForegroundColor = ConsoleColor.White;
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