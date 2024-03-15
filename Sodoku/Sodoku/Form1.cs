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
            
            TextBox[,] board = new TextBox[9, 9];

            // Füllen Sie das Array mit Ihren TextBoxen
            int counter = 1;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = (TextBox)this.Controls["textBox" + counter];
                    counter++;
                }
            }

            // Jetzt können Sie die Sudoku-Lösungsfunktion aufrufen
            bool solved = SolveSudoku(board);

        }
        public bool SolveSudoku(TextBox[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    // Wir suchen eine leere Zelle
                    if (board[row, col].Text == "")
                    {
                        // Wir versuchen mögliche Zahlen
                        for (int number = 1; number <= 9; number++)
                        {
                            if (CanPlace(board, row, col, number))
                            {
                                // Die Nummer wurde platziert
                                board[row, col].Text = number.ToString();

                                // Wir rufen die Funktion rekursiv auf, um den Rest des Rasters zu füllen
                                if (SolveSudoku(board))
                                {
                                    return true;
                                }

                                // Wenn die Platzierung dieser Zahl zu einer ungültigen Lösung führt, setzen wir die Zelle zurück
                                board[row, col].Text = "";
                            }
                        }

                        // Wenn keine Zahl platziert werden kann, kehren wir zur vorherigen Zelle zurück
                        return false;
                    }
                }
            }

            // Das Rätsel wurde gelöst
            return true;
        }

        public bool CanPlace(TextBox[,] board, int row, int col, int number)
        {
            string numStr = number.ToString();

            // Überprüfen der Zeile
            for (int i = 0; i < 9; i++)
            {
                if (board[row, i].Text == numStr)
                {
                    return false;
                }
            }

            // Überprüfen der Spalte
            for (int i = 0; i < 9; i++)
            {
                if (board[i, col].Text == numStr)
                {
                    return false;
                }
            }

            // Überprüfen der Box
            int startRow = row - row % 3;
            int startCol = col - col % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i + startRow, j + startCol].Text == numStr)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnres_Click(object sender, EventArgs e)
        {
            reset();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString("HH:mm:ss"); // Aktualisiert den Text der TextBox bei jedem Tick
        }

        private int GetColumn(int textBoxNumber)
        {
            if (textBoxNumber % 9 == 0)
            {
                return 8;
            }
            else
            {
                return (textBoxNumber % 9) - 1;
            }
        }
        private int GetRow(int textBoxNumber)
        {
            return (textBoxNumber - 1) / 9;
        }
        private void button1_Click(object sender, EventArgs e) // Prüfen 
        {
            // Deklaration und Initialisierung
            bool isValid = true;
            HashSet<int>[] rows = new HashSet<int>[9];
            HashSet<int>[] columns = new HashSet<int>[9];

            // Erstellung der HashSets für jede Zeile und Spalte
            for (int i = 0; i < 9; i++)
            {
                rows[i] = new HashSet<int>();
                columns[i] = new HashSet<int>();
            }

            // Überprüfung der Zeilen und Spalten
            for (int i = 1; i <= 81; i++)
            {
                
                Control[] textBoxes = this.Controls.Find("textBox" + i, true);
                if (textBoxes.Length > 0)
                {
                    TextBox textBox = textBoxes[0] as TextBox;
                    // Textbox in eine Zahl konvertieren
                    int textBoxText;
                    bool isNumeric = int.TryParse(textBox.Text, out textBoxText);
                    
                    // Konvertierung erfolgreich und Zahl im gültigen Bereich 
                    if (textBoxText < 1 || textBoxText > 9)
                    {
                        isValid = false;
                        break;
                    }

                    // Füge die Zahl in die entsprechende Zeile und Spalte ein
                    int row = GetRow(i);
                    int column = GetColumn(i);

                    // Überprüfe, ob die Zahl bereits in der entsprechenden Zeile oder Spalte vorhanden ist
                    if (!rows[row].Add(textBoxText) || !columns[column].Add(textBoxText))
                    {
                        // Wenn die Zahl bereits vorhanden ist
                        isValid = false;
                        break;
                        
                    }
                }
            }
            
            // Nachricht anzeigen, wenn das Sudoku gelöst wurde oder nicht!
            if (isValid)
            {
                MessageBox.Show("Gelöst!");
            }
            else
            {
                MessageBox.Show("Schau nochmal drüber, da ist wohl etwas falsch !");
            }
            
        }
    }
}
