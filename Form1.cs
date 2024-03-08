using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Sodoku
{
    public partial class Form1 : Form
    {
        int[] textBoxNumbers21 = new int[82];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 1; i <= 81; i++)
            {
                // Zufällig auswählen, ob diese TextBox beschrieben werden soll
                if (rnd.Next(10) > 6) // Ändern Sie die "6" auf eine andere Zahl, um die Wahrscheinlichkeit zu ändern
                {
                    Control[] textBoxes = this.Controls.Find("textBox" + i, true);
                    if (textBoxes.Length > 0)
                    {
                        TextBox textBox = textBoxes[0] as TextBox;
                        if (textBox != null)
                        {
                            // Setzen Sie den Text der TextBox auf eine zufällige Zahl zwischen 1 und 9
                            textBox.Text = rnd.Next(1, 10).ToString();
                        }
                    }
                }
            }
        }

        bool IsValidSudokuRow(int[] row)
        {
            bool[] seen = new bool[10];
            foreach (int number in row)
            {
                if (number < 1 || number > 9 || seen[number])
                {
                    return false;
                }
                seen[number] = true;
            }
            return true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //array mit werten textboxen
            int[] textBoxNumbers = { 49, 44, 43, 45, 46, 47, 48, 57, 58, 42, 64, 72, 41, 63, 71, 40, 62, 70, 39, 61, 69, 38, 60, 68, 59, 37, 67, 43, 66, 56, 55, 54, 53, 52, 51, 50, 57, 65, 81, 80, 79, 78, 77, 76, 75, 74, 73 };

            if (comboBox2.SelectedIndex == 0) // inhalt 0
            {
                // textboxen auf true and false je nach selectedindex 0 oder 1
                foreach (int number in textBoxNumbers)
                {
                    Control[] textBoxes = this.Controls.Find("textBox" + number, true); 
                    if (textBoxes.Length > 0)
                    {
                        TextBox textBox = textBoxes[0] as TextBox;
                        if (textBox != null)
                        {
                            textBox.Visible = false;
                        }
                    }
                }

            }
            else if (comboBox2.SelectedIndex == 1) // inhalt 1 == 9x9
            {
                foreach (int number in textBoxNumbers)
                {
                    Control[] textBoxes = this.Controls.Find("textBox" + number, true);
                    if (textBoxes.Length > 0)
                    {
                        TextBox textBox = textBoxes[0] as TextBox;
                        if (textBox != null)
                        {
                            textBox.Visible = true;
                        }
                    }
                }
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
        }

    }
}
