using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections;

namespace jsonPokedex
{
    public partial class Form1 : Form
    {
        int current = 0;
        private ArrayList pokies = new ArrayList();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Console.WriteLine(File.ReadAllText("Pokemon.json"));
            StreamReader inFile = File.OpenText("Pokemon.json");
            while (inFile.Peek() != -1)
            {
                string p = inFile.ReadLine();
                pokemon pokie = JsonSerializer.Deserialize<pokemon>(p);
                pokies.Add(pokie);
                show();

                //nameTextBox.Text = pokie.name;
                //categoryTextBox.Text = pokie.category;
            }
                inFile.Close();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            var p = new pokemon
            {
                name = nameTextBox.Text,
                category = categoryTextBox.Text,
                picture = pictureBox1.ImageLocation
            };

            string fileName = "Pokemon.json";
            string jsonString = JsonSerializer.Serialize(p);

            pokies.Add(p);

            StreamWriter outFile = File.CreateText(fileName);
            foreach (var item in pokies)
            {
                outFile.WriteLine(JsonSerializer.Serialize(item));
            }

            outFile.Close();

            //File.WriteAllText(fileName, jsonString);
            //Console.WriteLine(File.ReadAllText(fileName));
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            show();
            //string jsonString = File.ReadAllText("Pokemon.json");
            //pokemon pokie = JsonSerializer.Deserialize<pokemon>(jsonString);
            //
            //nameTextBox.Text = pokie.name;
            //categoryTextBox.Text = pokie.category;
        }
        public void show()
        {
            pokemon p = (pokemon)pokies[current];
            nameTextBox.Text = p.name;
            categoryTextBox.Text = p.category;
            if (File.Exists(p.picture)){ pictureBox1.Load(p.picture); }

        }
        private void firstButton_Click(object sender, EventArgs e)
        {
            current = 0;
            show();
        }

        private void lastButton_Click(object sender, EventArgs e)
        {
            current = pokies.Count;
            show();
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            if (current != pokies.Count)
            {
                current--;
                show();
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (current != pokies.Count)
            {
                current++;
                show();
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.Load(openFileDialog1.FileName);
        }
    }
}
