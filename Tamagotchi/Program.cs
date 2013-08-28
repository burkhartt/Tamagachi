using System;
using System.Drawing;
using System.Threading;
using ConsoleExtender;

namespace Tamagotchi {
    internal class Program {
        private static void Main() {          
			var imageDrawer = new ImageDrawer(ConsoleWriter.WriteAnimal);
            var imageRepository = new ImageRepository();

            var animal = new Animal();
			animal.OnMoodChange += mood => imageDrawer.Draw(imageRepository.GetImage(mood));
	        animal.OnHealthChange += ConsoleWriter.WriteHealth;
	        animal.OnDialogChange += ConsoleWriter.WriteMood;
			
	        ConsoleHelper.SetConsoleFont(5);
            var menu = new Menu();
            menu.Draw(ConsoleWriter.WriteMenu);

            do {
                if (Console.KeyAvailable) {
                    var action = menu.GetAction(Console.ReadKey(true));
                    animal.PerformAction(action);
                }
				Console.Beep();

                animal.DegradeHealth();                
                Thread.Sleep(400);
            } while (!animal.IsDead);

			Console.Beep(1000, 5000);
	        Console.ReadKey();
        }
    }
}