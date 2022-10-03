using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Update function already exists.
        void UpdateAnswer()
        {
            throw new NotImplementedException();
        }

        private void Clear()
        {
            textBoxInput.Text = "";
            UpdateAnswer();
        }

        private void AddChar(char c)
        {
            textBoxInput.Text += c;
            UpdateAnswer();
        }

        private void RemoveChar()
        {
            textBoxInput.Text = textBoxInput.Text.Substring(0, textBoxInput.Text.Length - 1);
            UpdateAnswer();
        }


        #region ClickingOnButtons
        private void buttonC_Click(object sender, EventArgs e) => Clear();
        private void buttonBackspace_Click(object sender, EventArgs e) => RemoveChar();

        private void buttonOpenPar_Click(object sender, EventArgs e) => AddChar('(');
        private void buttonClosePar_Click(object sender, EventArgs e) => AddChar(')');
        private void buttonPlus_Click(object sender, EventArgs e) => AddChar('+');
        private void buttonMinus_Click(object sender, EventArgs e) => AddChar('-');
        private void buttonStar_Click(object sender, EventArgs e) => AddChar('*');
        private void buttonSlash_Click(object sender, EventArgs e) => AddChar('/');
        private void buttonComma_Click(object sender, EventArgs e) => AddChar(',');
        private void button0_Click(object sender, EventArgs e) => AddChar('0');
        private void button1_Click(object sender, EventArgs e) => AddChar('1');
        private void button2_Click(object sender, EventArgs e) => AddChar('2');
        private void button3_Click(object sender, EventArgs e) => AddChar('3');
        private void button4_Click(object sender, EventArgs e) => AddChar('4');
        private void button5_Click(object sender, EventArgs e) => AddChar('5');
        private void button6_Click(object sender, EventArgs e) => AddChar('6');
        private void button7_Click(object sender, EventArgs e) => AddChar('7');
        private void button8_Click(object sender, EventArgs e) => AddChar('8');
        private void button9_Click(object sender, EventArgs e) => AddChar('9');
        #endregion
    }
}
