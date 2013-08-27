using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tamagotchi {
    internal class ImageRepository {
        public IDictionary<Mood, Bitmap> GetImages() {
            var images = new Dictionary<Mood, Bitmap> {
                {Mood.Happy, ResizeImage(Image.FromFile(@"C:\Users\Tim.Burkhart\Pictures\panda.gif"), 80, 50)},
                {Mood.Excited, ResizeImage(Image.FromFile(@"C:\Users\Tim.Burkhart\Pictures\excitedpanda.jpg"), 80, 50)},
                {Mood.Full, ResizeImage(Image.FromFile(@"C:\Users\Tim.Burkhart\Pictures\fullpanda.jpg"), 80, 50)},
                {Mood.Hurt, ResizeImage(Image.FromFile(@"C:\Users\Tim.Burkhart\Pictures\sadpanda.jpg"), 80, 50)},
                {Mood.Depressed, ResizeImage(Image.FromFile(@"C:\Users\Tim.Burkhart\Pictures\depressedpanda.jpg"), 80, 50)},
                {Mood.Dead, ResizeImage(Image.FromFile(@"C:\Users\Tim.Burkhart\Pictures\deadpanda.png"), 80, 50)}
            };

            return images;
        }

        private static Bitmap ResizeImage(Image image, int width, int height) {
            var result = new Bitmap(width, height);
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(result)) {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }

            return result;
        }
    }
}