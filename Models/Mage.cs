using System;

namespace Final.Models
{
    public class Mage : Adventurer
    {
        public Mage(string name) : base(name)
        {
            Type = "Mage";
        }

        public override string Greeting()
        {
            return $"Greetings! I am the powerful Mage {Name}.";
        }

        // Other properties and methods specific to Mage
    }
}
