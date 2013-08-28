using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tamagotchi {
    internal class ImageRepository {
        private static readonly string Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Images";

        private readonly Dictionary<Mood, Bitmap> images = new Dictionary<Mood, Bitmap> {
                {Mood.Happy, ResizeImage(Image.FromFile(Path + @"\happypanda.gif"), 80, 50)},
                {Mood.Excited, ResizeImage(Image.FromFile(Path + @"\excitedpanda.jpg"), 80, 50)},
                {Mood.Full, ResizeImage(Image.FromFile(Path + @"\fullpanda.jpg"), 80, 50)},
                {Mood.Hurt, ResizeImage(Image.FromFile(Path + @"\sadpanda.jpg"), 80, 50)},
                {Mood.Depressed, ResizeImage(Image.FromFile(Path + @"\depressedpanda.png"), 80, 50)},
                {Mood.Dead, ResizeImage(Image.FromFile(Path + @"\deadpanda.jpg"), 80, 50)}
            }; 

        public IDictionary<Mood, Bitmap> GetImages() {
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

        public Bitmap GetImage(Mood mood) {
            return GetImages()[mood];
        }
    }
}