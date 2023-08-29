using System;

namespace Final.Models
{
    public class Paladin : Adventurer
    {
        public Paladin(string name) : base(name)
        {
            Type = "Paladin";
        }

        public override string Greeting()
        {
            return $"Greetings! I am the honorable Paladin {Name}.";
        }

        // Other properties and methods specific to Paladin
    }
}
