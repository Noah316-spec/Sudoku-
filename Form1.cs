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
        
        public Form1()
        {
            InitializeComponent();
        }
        void reset()
        {
            for (int i = 1; i <= 81; i++)
            {
                Control[] buttons = this.Controls.Find("button" + i, true);
                if (buttons.Length > 0)
                {
                    Button button = buttons[0] as Button;
                    if (button != null)
                    {
                        button.Text = "";
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            reset();
            comboBox2.SelectedIndex = 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int i = 0;
            Random rnd = new Random();
            if(comboBox2.SelectedIndex == 1)
            {
                while (i != 10)
                {
                    int rndbutton = rnd.Next(1, 81);
                    int rndnum = rnd.Next(1, 9);
                    Control[] buttons = this.Controls.Find("button" + rndbutton, true);
                    if (buttons.Length > 0)
                    {
                        Button button = buttons[0] as Button;
                        if (button != null)
                        {
                            button.Text = Convert.ToString(rndnum);
                        }
                    }
                    i++;
                }
            }
            else
            {
                while (i != 7)
                {

                    int rndbutton = rnd.Next(1, 81);
                    int rndnum = rnd.Next(1, 9);
                    Control[] buttons = this.Controls.Find("button" + rndbutton, true);
                    if (buttons.Length > 0)
                    {
                        Button button = buttons[0] as Button;
                        if (button != null)
                        {
                            button.Text = Convert.ToString(rndnum);
                        }
                    }
                    i++;
                }
            }
           
            

        }
     
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] wert = { 7, 8, 9, 10, 11, 12, 19, 20, 21, 28, 29, 30, 37, 38, 39, 48, 47, 46, 57, 56, 55, 63, 62, 61, 60, 59, 58, 72, 71, 70, 69, 68, 67, 66, 65, 64, 73, 74, 75, 76, 77, 78, 79, 80, 81 };
            if (comboBox2.SelectedIndex == 0)
            {
                foreach (int number in wert)
                {
                    Control[] buttons = this.Controls.Find("button" + number, true);
                    if (buttons.Length > 0) // überprüfen ob es das steuerelement gibt 
                    {
                        Button button = buttons[0] as Button;
                        if (button != null)
                        {
                            button.Visible = false;
                        }
                    }
                }
            }
            else 
            {
                foreach (int number in wert)
                {
                    Control[] buttons = this.Controls.Find("button" + number, true);
                    if (buttons.Length > 0)
                    {
                        Button button = buttons[0] as Button;
                        if (button != null)
                        {
                            button.Visible = true;
                        }
                    }
                }
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            
            
            


        }
        
        private void btnres_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}
