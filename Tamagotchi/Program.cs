using System;
using System.Runtime.InteropServices;
using System.Threading;
using ConsoleExtender;

namespace Tamagotchi {
    internal class Program {
        private static void Main() {
            var imageRepository = new ImageRepository();
            var images = imageRepository.GetImages();
            var animal = new Animal(images);
			animal.SetDrawer(new ImageDrawer(ConsoleWriter.WriteAnimal));
			
	        ConsoleHelper.SetConsoleFont(5);
            var menu = new Menu();
            menu.Draw(ConsoleWriter.WriteMenu);

            do {
                if (Console.KeyAvailable) {
                    var action = menu.GetAction(Console.ReadKey(true));
                    animal.PerformAction(action);
                }

                animal.DegradeHealth();                
                animal.WriteMood(ConsoleWriter.WriteMood);
                animal.WriteHealth(ConsoleWriter.WriteHealth);
                Thread.Sleep(400);
            } while (!animal.IsDead);

            Console.ReadKey();
        }
    }
}