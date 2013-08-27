using System;
using System.Drawing;

namespace Tamagotchi {
    internal class ImageDrawer {
        private readonly DrawAnimal drawer;

        public ImageDrawer(DrawAnimal drawer) {
            this.drawer = drawer;
        }

        public void Draw(Bitmap image) {
            for (var x = 0; x < image.Width; x++) {
                for (var y = 0; y < image.Height; y++) {
                    var pixel = image.GetPixel(x, y);
                    drawer(pixel, x, y);
                }
            }
        }
    }
}