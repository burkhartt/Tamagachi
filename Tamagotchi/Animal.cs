using System;
using System.Collections.Generic;
using System.Drawing;

namespace Tamagotchi {
    internal class Animal {
        private readonly IDictionary<Status, Bitmap> images;

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

        private void Yell() {
            Health = Health - 15;
        }

        private void Water() {
            Health = Health + 30;
        }

        private void Scold() {
            Health = Health - 5;
        }

        private void Pet() {
            Health = Health + 5;
        }

        private void Kiss() {
            Health = 100;
        }

        private void Kill() {
            Health = 0;
        }

        private void Kick() {
            Health = Health - 10;
        }

        private void Hug() {
            Health = Health + 10;
        }

        private void Feed() {
            Health = Health + 25;
        }

        public Animal(IDictionary<Status, Bitmap> images) {
            this.images = images;
            Health = 100;
        }

        private int Health { get; set; }

        public bool IsDead { get { return Health <= 0; } }

        public void Draw(ImageDrawer imageDrawer) {
            if (IsDead) {
                imageDrawer.Draw(images[Status.Dead]);
                return;
            }
            if (Health < 80) {
                imageDrawer.Draw(images[Status.Sad]);
                return;
            }
            imageDrawer.Draw(images[Status.Happy]);
        }

        public void DegradeHealth() {
            Health--;
        }

        public void PerformAction(Action action) {
            actions[action](this);
        }
    }
}