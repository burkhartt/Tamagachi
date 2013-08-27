using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

		private readonly Dictionary<Predicate<int>, Action<Animal>> moodRules = new Dictionary<Predicate<int>, Action<Animal>> {
			{ (x => x == 0), animal => animal.SetMood(Mood.Dead) },
			{ (x => x > 0 && x < 40), animal => animal.SetMood(Mood.Depressed) },
			{ (x => x >= 40 && x < 60), animal => animal.SetMood(Mood.Hurt)},
			{ (x => x >= 60 && x < 80), animal => animal.SetMood(Mood.Happy)},
			{ (x => x >= 80), animal => animal.SetMood(Mood.Excited)}
		};

        private readonly IDictionary<Mood, Bitmap> images;
	    private int moodStabilityCounter;

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

        private void SetMood(Mood mood) {
			if (mood != Mood) {
				OnMoodChange(mood, images[mood]);
				OnDialogChange(dialogs[mood]);
			}
            Mood = mood;
        }

        private void AddHealth(int healthIncrease) {
            Health += healthIncrease;
            Health = Math.Min(100, Health);
            Health = Math.Max(0, Health);
	        OnHealthChange(Health);			
			if (moodStabilityCounter > 0) {
				return;
			}

			foreach (var rule in moodRules.Where(rule => rule.Key.Invoke(Health))) {
				rule.Value(this);
			}
        }

        private void Yell() {
	        ActionPerformed();
            AddHealth(-15);
            SetMood(Mood.Depressed);
        }

	    private void ActionPerformed() {
		    moodStabilityCounter = 5;
	    }

	    private void Water() {
			ActionPerformed();
            AddHealth(30);
            SetMood(Mood.Full);
        }

        private void Scold() {
			ActionPerformed();
            AddHealth(-5);
            SetMood(Mood.Depressed);
        }

        private void Pet() {
			ActionPerformed();
            AddHealth(5);
            SetMood(Mood.Happy);
        }

        private void Kiss() {
			ActionPerformed();
            AddHealth(100);
            SetMood(Mood.Excited);
        }

        private void Kill() {
            AddHealth(-100);
        }

        private void Kick() {
			ActionPerformed();
            AddHealth(-10);
            SetMood(Mood.Hurt);
        }

        private void Hug() {
			ActionPerformed();
            AddHealth(10);
            SetMood(Mood.Happy);
        }

        private void Feed() {
			ActionPerformed();
            AddHealth(25);
            SetMood(Mood.Full);
        }

        public void DegradeHealth() {
            AddHealth(-1);
	        moodStabilityCounter--;
        }

        public void PerformAction(Action action) {
            actions[action](this);
        }

	    public event OnMoodChangeHandler OnMoodChange;
	    public event OnHealthChangeHandler OnHealthChange;
	    public event OnDialogChangeHandler OnDialogChange;
    }
}