using System;
using System.Collections.Generic;
using System.Linq;

namespace Tamagotchi {
    internal class Menu {
        public Dictionary<Action, string> Options = new Dictionary<Action, string> {
            {Action.Kill, "Kill it"},
            {Action.Feed, "Feed"},
            {Action.Hug, "Give a hug"},
            {Action.Kick, "Kick it"},
            {Action.Kiss, "Give it a big kiss"},
            {Action.Pet, "Pet the animal"},
            {Action.Scold, "Scold"},
            {Action.Water, "Give water"},
            {Action.Yell, "Yell at it"},
        };

        public void Draw(DrawMenu drawer) {
            foreach (var option in Options.OrderBy(x => (int)x.Key)) {
                drawer((int)option.Key, option.Value);
            }
        }

        public Action GetAction(ConsoleKeyInfo key) {
            var numberPressed = key.KeyChar - 48;
            if (numberPressed <= 0 || numberPressed >= 10) {
                return Action.Kill;
            }
            return (Action) numberPressed;
        }
    }
}