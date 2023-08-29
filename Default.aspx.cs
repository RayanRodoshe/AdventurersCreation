using Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final
{
    public partial class Default : System.Web.UI.Page
    {
        List<string> AdventurerTypes = new List<string>()
        {
            "Mage",
            "Paladin",
        };
        List<Adventurer> adventurers = new List<Adventurer>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (string AdventurerType in AdventurerTypes)
                {
                    ddlType.Items.Add(new ListItem(AdventurerType));
                }
            }
        }

        protected void Clear_Form()
        {
            txtName.Text = string.Empty;
            ddlType.SelectedIndex = 0;
        }

        protected void Display_Adventurers()
        {
            tblAdventurers.Rows.Clear(); // Clear existing rows

            foreach (Adventurer adventurer in adventurers)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                HyperLink link = new HyperLink();
                link.NavigateUrl = $"Details.aspx?id={adventurers.IndexOf(adventurer)}";
                link.Text = $"{adventurer.Name} ({adventurer.Type})";
                cell.Controls.Add(link);
                row.Cells.Add(cell);
                tblAdventurers.Rows.Add(row);
            }
        }

        protected void btnCreateAdventurer_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string type = ddlType.SelectedValue;

            if (type == "Paladin")
            {
                adventurers.Add(new Paladin(name));
            }
            else if (type == "Mage")
            {
                adventurers.Add(new Mage(name));
            }
            else
            {
                adventurers.Add(new Adventurer(name));
            }

            Session["adventurers"] = adventurers;
            Clear_Form();
            Display_Adventurers();
        }
    }
}
