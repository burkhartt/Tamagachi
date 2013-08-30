using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Tamagotchi {
    internal class ImageRepository {
        private readonly Dictionary<Mood, Image> images;

        public ImageRepository(string baseImagePath) {
            images = new Dictionary<Mood, Image> {
                {Mood.Happy, GetImage(baseImagePath, "happypanda.gif")},
                {Mood.Excited, GetImage(baseImagePath, "excitedpanda.jpg")},
                {Mood.Full, GetImage(baseImagePath, "fullpanda.jpg")},
                {Mood.Hurt, GetImage(baseImagePath, "sadpanda.jpg")},
                {Mood.Depressed, GetImage(baseImagePath, "depressedpanda.png")},
                {Mood.Dead, GetImage(baseImagePath, "deadpanda.jpg")}
            };
        }

        private static Image GetImage(string baseImagePath, string filename) {
            return Image.FromFile(Path.Combine(baseImagePath, filename));
        }

        public Image GetImage(Mood mood) {
            return images[mood];
        }
    }
}