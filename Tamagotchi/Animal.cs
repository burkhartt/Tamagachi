using System;
using System.Collections.Generic;
using System.Drawing;

namespace Tamagotchi {
    internal class Animal {
        private readonly Dictionary<Action, Action<Animal>> actions = new Dictionary<Action, Action<Animal>> {
            {Action.Feed, animal => animal.Feed()},
            {Action.Hug, animal => animal.Hug()},
            {Action.Kick, animal => animal.Kick()},
            {Action.Kill, animal => animal.Kill()},
            {Action.Kiss, animal => animal.Kiss()},
            {Action.Pet, animal => animal.Pet()},
            {Action.Scold, animal => animal.Scold()},
            {Action.Water, animal => animal.Water()},
            {Action.Yell, animal => animal.Yell()}
        };

        private readonly Dictionary<Mood, string> dialogs = new Dictionary<Mood, string> {
            {Mood.Dead, "XXXXXXXXXXXXXXXX"},
            {Mood.Depressed, "I hate the world..."},
            {Mood.Excited, "YAY!!! I feel loved!!!"},
            {Mood.Full, "Whew, I am full :-)"},
            {Mood.Happy, "I'm really content right now"},
            {Mood.Hurt, "I am feeling hurt :'-("}
        };

        private readonly IDictionary<Mood, Bitmap> images;

        public Animal(IDictionary<Mood, Bitmap> images) {
            this.images = images;
            Health = 100;
            Mood = Mood.Happy;
        }

        private Mood Mood { get; set; }
        private int Health { get; set; }

        public bool IsDead {
            get { return Health <= 0; }
        }

        private void SetStatus(Mood mood) {
            Mood = mood;
        }

        private void AddHealth(int healthIncrease) {
            Health += healthIncrease;
            Health = Math.Min(100, Health);
            Health = Math.Max(1, Health);
        }

        private void Yell() {
            AddHealth(-15);
            Health = Health - 15;
            SetStatus(Mood.Depressed);
        }

        private void Water() {
            AddHealth(30);
            SetStatus(Mood.Full);
        }

        private void Scold() {
            AddHealth(-5);
            SetStatus(Mood.Depressed);
        }

        private void Pet() {
            AddHealth(5);
            SetStatus(Mood.Happy);
        }

        private void Kiss() {
            AddHealth(100);
            SetStatus(Mood.Excited);
        }

        private void Kill() {
            AddHealth(-100);
            SetStatus(Mood.Dead);
        }

        private void Kick() {
            AddHealth(-10);
            SetStatus(Mood.Hurt);
        }

        private void Hug() {
            AddHealth(10);
            SetStatus(Mood.Happy);
        }

        private void Feed() {
            AddHealth(25);
            SetStatus(Mood.Full);
        }

        public void Draw(ImageDrawer imageDrawer) {
            imageDrawer.Draw(images[Mood]);
        }

        public void DegradeHealth() {
            Health--;
        }

        public void PerformAction(Action action) {
            actions[action](this);
        }

        public void WriteMood(WriteMood writer) {
            writer(dialogs[Mood]);
        }

        public void WriteHealth(WriteHealth writer) {
            writer(Health);
        }
    }
}