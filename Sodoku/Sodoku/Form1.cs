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
        // Deklarien und Initalisiern
        int[] auswahl6x6 = { 7, 8, 9, 16, 17, 18, 25, 26, 27, 34, 35, 36, 43, 44, 45, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81 };
        Timer timer = new Timer();
        public static DateTime Now { get; } // Uhrzeit holen


        public Form1()
        {
            InitializeComponent();
            timer.Interval = 1000; // Setzt das Intervall auf 1 Sekunde (1000 Millisekunden)
            timer.Tick += new EventHandler(timer1_Tick); // Fügt das Event hinzu, das bei jedem Tick aufgerufen wird
            timer.Start(); // Startet den Time
        }
        
        void reset()
        {
            for (int i = 1; i <= 81; i++)
            {
                Control[] textBoxes = this.Controls.Find("textBox" + i, true);
                if (textBoxes.Length > 0) // überprüfen ob es das steuerelement gibt 
                {
                    TextBox textBox = textBoxes[0] as TextBox;
                    if (textBox != null)
                    {
                        textBox.Text = "";
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            reset();
            comboBox2.SelectedIndex = 0;
        }
        private void abfragebereiche()
        {
            bool isValid = true;
            HashSet<int>[] rows = new HashSet<int>[9];
            HashSet<int>[] columns = new HashSet<int>[9];
            HashSet<int>[] boxes = new HashSet<int>[9];

            for (int i = 0; i < 9; i++)
            {
                rows[i] = new HashSet<int>();
                columns[i] = new HashSet<int>();
                boxes[i] = new HashSet<int>();
            }

            for (int i = 1; i <= 81; i++)
            {
                Control[] textBoxes = this.Controls.Find("textBox" + i, true);
                if (textBoxes.Length > 0)
                {
                    TextBox textBox = textBoxes[0] as TextBox;
                    int textBoxText;
                    bool isNumeric = int.TryParse(textBox.Text, out textBoxText);
                    if (!isNumeric || textBoxText < 1 || textBoxText > 9)
                    {
                        isValid = false;
                        break;
                    }

                    int row = (i - 1) / 9;
                    int column = (i - 1) % 9;
                    int box = (row / 3) * 3 + column / 3;

                    if (!rows[row].Add(textBoxText) || !columns[column].Add(textBoxText) || !boxes[box].Add(textBoxText))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (isValid)
            {
                MessageBox.Show("Gelöst!");
            }
            else
            {
                MessageBox.Show("Schau nochmal drüber,da ist wohl etwas falsch !");
            }

        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            int i = 0;
            Random rnd = new Random();
            if(comboBox2.SelectedIndex == 0) // Auswahl Combobox2 6x6
            {
                while (i != 6)
                {
                    
                    int rndbutton = auswahl6x6[rnd.Next(auswahl6x6.Length)];
                    int rndnum = rnd.Next(1, 9);
                    Control[] textBoxes = this.Controls.Find("textBox" + rndbutton, true);
                    if (textBoxes.Length > 0) // überprüfen ob es das steuerelement gibt 
                    {
                        TextBox textBox = textBoxes[0] as TextBox;
                        if (textBox != null)
                        {
                            textBox.Text = Convert.ToString(rndnum);
                        }
                    }
                    i++;
                }
            }
            else// Auswahl Combobox2 9x9
            {
                while (i != 9)
                {

                    int rndbutton = rnd.Next(1, 81);
                    int rndnum = rnd.Next(1, 9);
                    Control[] textBoxes = this.Controls.Find("textBox" + rndbutton, true);
                    if (textBoxes.Length > 0) // überprüfen ob es das steuerelement gibt 
                    {
                        TextBox textBox = textBoxes[0] as TextBox;
                        if (textBox != null)
                        {
                            textBox.Text = Convert.ToString(rndnum);
                        }
                    }
                    i++;
                }
            }
           
            

        }

       
    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                foreach (int number in auswahl6x6)
                {
                    Control[] textBoxes = this.Controls.Find("textBox" + number, true);
                    if (textBoxes.Length > 0) // überprüfen ob es das steuerelement gibt 
                    {
                        TextBox textBox = textBoxes[0] as TextBox;
                        if (textBox != null)
                        {
                            textBox.Visible = false;
                        }
                    }

                }
            }
            else 
            {
                foreach (int number in auswahl6x6)
                {
                    Control[] textBoxes = this.Controls.Find("textBox" + number, true);
                    if (textBoxes.Length > 0) // überprüfen ob es das steuerelement gibt 
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
            abfragebereiche();
            
            


        }
        
        private void btnres_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString("HH:mm:ss"); // Aktualisiert den Text der TextBox bei jedem Tick
        }
    }
}
