using Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final
{
    public partial class Details : System.Web.UI.Page
    {
        List<Adventurer> adventurers = new List<Adventurer>();
        List<Item> items = Helper.GetAvailableItems();

        int index = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            Get_Index();
            Get_Adventurers();
            Reset_ErrorMessages();

            if (!IsPostBack) { Populate_Items(); }

            Display_Adventurer_Details();
        }

        protected void Get_Index()
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            index = int.Parse(Request.QueryString["id"]);
        }

        protected void Get_Adventurers()
        {
            if (Session["adventurers"] != null)
            {
                adventurers = (List<Adventurer>)Session["adventurers"];
            }
        }

        protected void Reset_ErrorMessages()
        {
            lblErrorMessages.Visible = false;
            lblErrorMessages.Text = "";
        }

        protected void Populate_Items()
        {
            int itemIndex = 0;
            foreach (Item item in items)
            {
                ListItem listItem = new ListItem($"{item.Name} ({item.StrengthRequirement}/{item.DexterityRequirement}/{item.ManaRequirement})", itemIndex.ToString());

                if (adventurers.Count > 0)
                {
                    if (adventurers[index].Item_Equipped(item))
                    {
                        listItem.Selected = true;
                    }
                }

                cblItems.Items.Add(listItem);

                itemIndex++;
            }
        }

        protected void Display_Adventurer_Details()
        {
            if (index >= 0 && index < adventurers.Count)
            {
                Adventurer selectedAdventurer = adventurers[index];
                txtName.InnerText = selectedAdventurer.Name;
                txtType.InnerText = selectedAdventurer.Type;
                txtPhrase.InnerText = selectedAdventurer.Greeting();

                if (selectedAdventurer is Mage mage)
                {
                    lblStrength.Text = mage.StrengthMultiplier.ToString();
                    lblDexterity.Text = mage.DexterityMultiplier.ToString();
                    lblVitality.Text = "N/A"; // Mage doesn't have Vitality
                    lblMana.Text = mage.ManaMultiplier.ToString();
                }
                else if (selectedAdventurer is Paladin paladin)
                {
                    lblStrength.Text = paladin.StrengthMultiplier.ToString();
                    lblDexterity.Text = paladin.DexterityMultiplier.ToString();
                    lblVitality.Text = paladin.VitalityMultiplier.ToString();
                    lblMana.Text = "N/A"; // Paladin doesn't have Mana
                }
            }
        }

        protected void btnEquipItems_Click(object sender, EventArgs e)
        {
            Adventurer selectedAdventurer = adventurers[index];

            foreach (ListItem listItem in cblItems.Items)
            {
                int itemIndex = int.Parse(listItem.Value);
                Item item = items[itemIndex];

                if (listItem.Selected)
                {
                    try
                    {
                        selectedAdventurer.Equip_Item(item);
                    }
                    catch (Exception ex)
                    {
                        lblErrorMessages.Visible = true;
                        lblErrorMessages.Text = ex.Message;
                        listItem.Selected = false;
                    }
                }
                else
                {
                    selectedAdventurer.Unequip_Item(item);
                }
            }
        }
    }
}
