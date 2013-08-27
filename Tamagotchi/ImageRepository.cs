using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tamagotchi {
    internal class ImageRepository {
        public IDictionary<Status, Bitmap> GetImages() {
            var images = new Dictionary<Status, Bitmap> {
                {Status.Happy, ResizeImage(Image.FromFile(@"C:\Users\Tim Burkhart\Pictures\panda.jpg"), 50, 50)},
                {Status.Sad, ResizeImage(Image.FromFile(@"C:\Users\Tim Burkhart\Pictures\sadpanda.jpg"), 50, 50)},
                {Status.Dead, ResizeImage(Image.FromFile(@"C:\Users\Tim Burkhart\Pictures\deadpanda.jpg"), 50, 50)}
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