using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
       


        public Form1()
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 1000; // Setzt das Intervall auf 1 Sekunde (1000 Millisekunden)
            timer.Tick += new EventHandler(timer1_Tick); // Fügt das Event hinzu, das bei jedem Tick aufgerufen wird
            timer.Start(); // Startet den Time
             // Erstelle einen neuen Timer
           
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
                        textBox.Text = ""; // clearen von allen 81 textboxen 
                        // or textBox.Clear();
                    }
                }
            }
        }


        private void abfragebereiche()
        {
            // Deklaration und Initialisierung
            bool isValid = true;
            HashSet<int>[] rows = new HashSet<int>[9];
            HashSet<int>[] columns = new HashSet<int>[9];
            HashSet<int>[] boxes = new HashSet<int>[9];

            // Erstellung der HashSets für jede Zeile, Spalte und Box
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
                    // Textbox in eine Zahl konvertieren
                    int textBoxText;
                    bool isNumeric = int.TryParse(textBox.Text, out textBoxText);

                    //Konvertierung erfolgreich und Zahl im gültigen Bereich 
                    if (!isNumeric || textBoxText < 1 || textBoxText > 9)
                    {
                        isValid = false;
                        break;
                    }

                    // Füge die Zahl in das grid ein
                    int row = (i - 1) / 9;
                    int column = (i - 1) % 9;
                    int box = (row / 3) * 3 + column / 3;

                    // Überprüfe, ob die Zahl bereits in der entsprechenden Zeile, Spalte oder Box vorhanden ist
                    if (!rows[row].Add(textBoxText) || !columns[column].Add(textBoxText) || !boxes[box].Add(textBoxText))
                    {
                        //wenn die Zahl bereits vorhanden ist
                        isValid = false;
                        break;
                    }
                }
            }

            // Nachricht anzeigen wenn es Sudoku gelöst oder nicht gelöst wurde!
            if (isValid)
            {
                MessageBox.Show("Gelöst!");
            }
            else
            {
                MessageBox.Show("Schau nochmal drüber,da ist wohl etwas falsch !");
            }

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reset(); // Textboxen.text leeren
            comboBox2.SelectedIndex = 0; // Standard 6x6 auswählen
        }
      
        private void btnStart_Click(object sender, EventArgs e)
        {
            timer2.Start();
            // Deklaration und Initialisierung
            int i = 0;
            Random rnd = new Random(); // random definieren

            if (comboBox2.SelectedIndex == 0) // Auswahl Combobox2 6x6
            {
                while (i != 6) // 6 mal ausführen
                {

                    int rndbutton = auswahl6x6[rnd.Next(auswahl6x6.Length)]; // random zahl nur in dem Bereich 
                    int rndnum = rnd.Next(1, 9);
                    Control[] textBoxes = this.Controls.Find("textBox" + rndbutton, true); // steuerelement suchen
                    if (textBoxes.Length > 0) // überprüfen ob es das steuerelement gibt 
                    {
                        TextBox textBox = textBoxes[0] as TextBox;
                        if (textBox != null)
                        {
                            textBox.Text = Convert.ToString(rndnum); // der Text des Steuerelements wird mit einer random zahl beschrieben
                        }
                    }
                    i++;
                }
            }
            else// Auswahl Combobox2 9x9
            {
                while (i != 9) // 9 mal ausführen
                {

                    int rndbutton = rnd.Next(1, 81); // alle textboxen werden ausgewählt 
                    int rndnum = rnd.Next(1, 9);
                    Control[] textBoxes = this.Controls.Find("textBox" + rndbutton, true);
                    if (textBoxes.Length > 0) // überprüfen ob es das steuerelement gibt 
                    {
                        TextBox textBox = textBoxes[0] as TextBox;
                        if (textBox != null)
                        {
                            textBox.Text = Convert.ToString(rndnum);// der Text des Steuerelements wird mit einer random zahl beschrieben
                        }
                    }
                    i++;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0) // jede Zahl die außerhalb des 6x6 feldes liegt soll nicht Visible sein.
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
            else  // jede Zahl die außerhalb des 6x6 feldes liegt soll Visible sein.
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

        private void timer2_Tick(object sender, EventArgs e)
        {
           
        }
    }
}
