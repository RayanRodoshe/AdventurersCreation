using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class Adventurer
    {
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Vitality { get; set; }
        public double Mana { get; set; }

        protected internal double StrengthMultiplier
        {
            get { return 1; }
        }

        protected internal double DexterityMultiplier
        {
            get { return 1; }
        }

        protected internal double VitalityMultiplier
        {
            get { return 1; }
        }

        protected internal double ManaMultiplier
        {
            get { return 1; }
        }

        public List<Item> EquippedItems { get; set; } = new List<Item>();

        public Adventurer(string name)
        {
            Random random = new Random();
            Name = name;
            Type = "Adventurer";

            Strength = random.Next(2, 10) * StrengthMultiplier;
            Dexterity = random.Next(2, 10) * DexterityMultiplier;
            Vitality = random.Next(2, 10) * VitalityMultiplier;
            Mana = random.Next(2, 10) * ManaMultiplier;
        }

        public bool Item_Equipped(Item item)
        {
            return EquippedItems.Find((equippedItem) => equippedItem.Name == item.Name) != null;
        }

        public void Equip_Item(Item item)
        {
            if (Strength >= item.StrengthRequirement && Dexterity >= item.DexterityRequirement && Mana >= item.ManaRequirement)
            {
                if (!Item_Equipped(item))
                {
                    EquippedItems.Add(item);
                }
            }
            else
            {
                throw new Exception($"{item.Name} could not be equipped.");
            }
        }

        public void Unequip_Item(Item item)
        {
            if (Item_Equipped(item))
            {
                EquippedItems.RemoveAt(EquippedItems.FindIndex((equippedItem) => equippedItem.Name == item.Name));
            }
        }

        public virtual string Greeting()
        {
            return "Adventure awaits...";
        }
    }
}
