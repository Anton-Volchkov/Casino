using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            balance = 500;
            SetBalance();
            
        }
        
        int balance;
        Colors userColors;

        private void Button5_Click(object sender, EventArgs e)
        {
           
            Application.Exit();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            if ( string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Вы не  указали сумму ставки!");
                return;
            }
            if (Int32.TryParse(textBox1.Text, out int result))
            {
                if (result < 1 )
                {
                    MessageBox.Show("Некорректная ставка!");
                    return;
                }
                if (balance - result >= 0)
                {

                    if (radioButton1.Checked)
                    {
                        userColors = Colors.Black;
                    }
                    else if (radioButton2.Checked)
                    {
                        userColors = Colors.Red;
                    }
                    else if (radioButton3.Checked)
                    {
                        userColors = Colors.Green;
                    }
                    else
                    {
                        MessageBox.Show("Выберите цвет!");
                        return;
                    }

                    SetBoolean(false);

                    balance -= result;
                    SetBalance();
                    timer1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("У вас недостаточно средств!");
                }

            }
        }

        private void SetBoolean(bool setbool)
        {
            textBox1.Enabled = setbool;
            button1.Enabled = setbool;
            groupBox1.Enabled = setbool;
            textBox2.Enabled = setbool;
        }

        public void SetBalance()
        {
            label1.Text = $"Баланс: {balance}$";
        }

        int StopRoll = 0;
        public void Roll(Colors userColor)
        {
            pictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.Refresh();

            Colors cl = Colors.Green;
            Random rnd = new Random();
            int roll = rnd.Next(0, 36);
            label2.Text = roll.ToString();

            if (Black.Contains(roll))
            {
                panel2.BackColor = Color.Black;
                cl = Colors.Black;
            }
            else if (Rad.Contains(roll))
            {
                panel2.BackColor = Color.Red;
                cl = Colors.Red;
            }
            else if (roll == 0)
            {
                panel2.BackColor = Color.Green;
                cl = Colors.Green;
            }
            StopRoll++;

            if (StopRoll == 50)
            {
                StopRoll = 0;
                timer1.Enabled = false;
                int coef = 0;
                if (string.IsNullOrWhiteSpace(textBox2.Text) && cl == userColor)
                {
                    coef = 2;
                }
                else if (cl == userColor && roll == Convert.ToInt32(textBox2.Text) )
                {
                    coef = cl == Colors.Green ? 12 : 4;
                }
                else
                {
                    MessageBox.Show("Вы проиграли!");
                    SetBoolean(true);
                    return;
                }

                balance += Convert.ToInt32(textBox1.Text) * coef;
                MessageBox.Show("Поздравляем с победой!");
                SetBalance();
                SetBoolean(true);
            }
            
            
        }

        public int[] Black { get; set; } = {10,29,8,31,6,33,4,35,2,28,26,11,20,17,22,15,24,13 };
        public int[] Rad { get; set; } = { 27,35,12,19,18,21,16,23,14,6,30,7,32,5,34,3,36,1 };
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Roll(userColors);
        }
    }
}
