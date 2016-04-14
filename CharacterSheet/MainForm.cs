using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CharacterSheet
{
    public partial class MainForm : Form
    {
        public static List<Character> characterDB = new List<Character>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            CreateForm cf = new CreateForm();
            cf.Show();
        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            ViewForm vf = new ViewForm();
            vf.Show();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            XmlTextWriter writer = new XmlTextWriter("characters.xml", null);

            //Write the "Class" element
            writer.WriteStartElement("Characters");
            for (int i = 0; i < characterDB.Count(); i++)
            {

                writer.WriteStartElement("Character");

                //Write sub-elements
                writer.WriteElementString("name", characterDB[i].name);
                writer.WriteElementString("class", characterDB[i].charClass);
                writer.WriteElementString("dexterity", characterDB[i].dexterity);
                writer.WriteElementString("strength", characterDB[i].strength);
                writer.WriteElementString("health", characterDB[i].health);
                writer.WriteElementString("perk", characterDB[i].perk);

                writer.WriteEndElement();
            }

            // end the "Class" element
            writer.WriteEndElement();

            //Write the XML to file and close the writer
            writer.Close();
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string newName, newHeroClass, newDexterity, newStrength, newHealth, newPerk;
            newName = newHeroClass = newDexterity = newStrength = newHealth = newPerk = "";

            // Open the file to be read
            XmlTextReader reader = new XmlTextReader("characters.xml");

            int i = 1;

            // Continue to read each element and text until the file is done
            while (reader.Read())
            {
                // If the currently read item is text then print it to screen,
                // otherwise the loop repeats getting the next piece of information
                if (reader.NodeType == XmlNodeType.Text)
                {
                    if (i == 1) { newName = reader.Value; i++; }
                    else if (i == 2) { newHeroClass = reader.Value; i++; }
                    else if (i == 3) { newDexterity = reader.Value; i++; }
                    else if (i == 4) { newStrength = reader.Value; i++; }
                    else if (i == 5) { newHealth = reader.Value; i++; }
                    else if (i == 6)
                    {
                        newPerk = reader.Value;
                        i = 1;
                        Character c = new Character(newName, newHeroClass, newDexterity, newStrength, newHealth, newPerk);
                        characterDB.Add(c);
                    }

                }
            }
            // When done reading the file close it
            reader.Close();
        }
    }
}
