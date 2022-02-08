using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Button[] btn = new Button[9];

        public int[] horizontal = new int[3] { 0, 0, 0};
        public int[] vertical = new int[3] { 0, 0, 0 };
        public int[] diagonal = new int[2] { 0, 0 };

        public bool round = false;
        public bool AI_turn = false;
        public int btn_counter = 0;
        public Form1()
        {
            InitializeComponent();
            btn[0] = button1;
            btn[1] = button2;
            btn[2] = button3;
            btn[3] = button4;
            btn[4] = button5;
            btn[5] = button6;
            btn[6] = button7;
            btn[7] = button8;
            btn[8] = button9;
        }
        void AI_move(string line, int coordinates)
        {
            if (line == "brak")
            {
                Random rnd = new Random();
                int box = rnd.Next(8);
                int direction = rnd.Next(1, 3);
                while (btn[box].Enabled != true)
                {
                    if (direction == 1)
                    {
                        if (box == 9)
                            box = -1;
                        ++box;
                    }
                    else
                    {
                        if (box == 0)
                            box = 9;

                        --box;
                    }
                }
                action(box);
            }
            else
            {
                switch (line)
                {
                    case "horizontal":
                        coordinates *= 3;
                        while (btn[coordinates-1].Enabled == false)
                            --coordinates;
                        action(coordinates-1);
                        break;
                    case "vertical":
                        while (btn[coordinates-1].Enabled == false)
                            coordinates +=3;
                        action(coordinates-1);
                        break;
                    case "diagonal":
                        int tmp = 5;
                        while (btn[tmp - 1].Enabled == false)
                        {
                            tmp += coordinates;
                            coordinates = coordinates * -2;
                        }
                        action(tmp-1);
                        break;
                }
            }
        }
        void reset()
        {

            Array.Clear(horizontal, 0, 3);
            Array.Clear(vertical, 0, 3);
            Array.Clear(diagonal, 0, 2);
            int number = 9;     //ilość pól
            for(int i = 0; i < number; ++i)
            {
                btn[i].Enabled = true;
                btn[i].Text = " ";
            }

            round = false;
            btn_counter = 0;

        /*Form1 NewForm = new Form1();
        NewForm.Show();
        this.Dispose(false);*/
    }
        void end(int result)
        {
            string message;
            string tittle = "Koniec";
            if (result == 1)
                message = "Wygrywa X";
            else if (result == 2)
                message = "Wygrywa O";
            else
                message = "Remis";
            MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
            DialogResult D_result = MessageBox.Show(message, tittle, buttons);
            if (D_result == DialogResult.Retry)
                reset();
            else
                this.Close();
        }
        void check(int sum)
        {
            if (sum == 3)
                end(1);
            else if (sum == -3)
                end(2);
            else if (btn_counter == 9)
                end(0);
        }
        private void button_action(Button button, int horizontal, int vertical, int diagonal)
        {
            button.Enabled = false;
            ++btn_counter;
            if(round == false)
            {
                button.Text = "X";
                if (horizontal > 0)
                    check(++this.horizontal[horizontal - 1]);
                if (vertical > 0) 
                    check(++this.vertical[vertical-1]);
                if (diagonal > 0 && diagonal < 3) 
                    check(++this.diagonal[diagonal-1]);
                else if (diagonal == 3)
                {
                   check(++this.diagonal[0]);
                   check(++this.diagonal[1]);
                }
            }
            else
            {
                button.Text = "O";
                if (horizontal > 0)
                    check(--this.horizontal[horizontal - 1]);
                if (vertical > 0)
                    check(--this.vertical[vertical - 1]);
                if (diagonal > 0 && diagonal < 3)
                    check(--this.diagonal[diagonal - 1]);
                else if (diagonal == 3)
                {
                    check(--this.diagonal[0]);
                    check(--this.diagonal[1]);
                }
            }
            round = !round;
            AI_turn = !AI_turn;
            if (singleplayer.Checked == true && AI_turn == true)
            {
                string line = "brak";
                int coordinates = 0;
                if (this.horizontal[horizontal - 1] == 2 || this.horizontal[horizontal - 1] == -2)
                {
                    line = "horizontal";
                    coordinates = horizontal;
                }
                if (this.vertical[vertical - 1] == 2 || this.vertical[vertical - 1] == 2)
                {
                    line = "vertical";
                    coordinates = vertical;
                }
                if (this.diagonal[0] == 2 || this.diagonal[0] == -2)
                {
                    line = "diagonal";
                    coordinates = 4;
                }
                if (this.diagonal[1] == 2 || this.diagonal[1] == -2)
                {
                    line = "diagonal";
                    coordinates = 1;
                }
                AI_move(line, coordinates);
            }
        }
        private void action(int a)
        {
            switch(a)
            {
                case 0:
                    button_action(btn[a], 1, 1, 1);
                    break;
                case 1:
                    button_action(btn[a], 1, 2, 0);
                    break;
                case 2:
                    button_action(btn[a], 1, 3, 2);
                    break;
                case 3:
                    button_action(btn[a], 2, 1, 0);
                    break;
                case 4:
                    button_action(btn[a], 2, 2, 3);
                    break;
                case 5:
                    button_action(btn[a], 2, 3, 0);
                    break;
                case 6:
                    button_action(btn[a], 3, 1, 2);
                    break;
                case 7:
                    button_action(btn[a], 3, 2, 0);
                    break;
                case 8:
                    button_action(btn[a], 3, 3, 1);
                    break;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            action(0);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            action(1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            action(2);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            action(3);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            action(4);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            action(5);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            action(6);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            action(7);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            action(8);
        }
    }
}
