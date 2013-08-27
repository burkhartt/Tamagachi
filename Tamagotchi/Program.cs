using System;
using System.Threading;

namespace Tamagotchi {
    internal class Program {
        private static void Main() {                     
            var imageRepository = new ImageRepository();
            var images = imageRepository.GetImages();
            var animal = new Animal(images);
            var menu = new Menu();
            menu.Draw(ConsoleWriter.WriteMenu);

            do {
                if (Console.KeyAvailable) {
                    var action = menu.GetAction(Console.ReadKey(true));
                    animal.PerformAction(action);
                }

                animal.DegradeHealth();
                animal.Draw(new ImageDrawer(ConsoleWriter.WriteAnimal));                
                Thread.Sleep(20);
            } while (!animal.IsDead);

            Console.ReadKey();
        }
    }
}