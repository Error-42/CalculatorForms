using System;
using System.CodeDom;
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
            string input = textBoxInput.Text;

            if (input == "")
            {
                labelResult.Text = "";
                return;
            }

            Result<List<Token>> tokens = Lexer.Lex(input);
            if (tokens.IsErr())
            {
                labelResult.Text = tokens.Err!;
                return;
            }

            Result<Node> node = Parser.Parse(tokens.Val!);
            if (node.IsErr())
            {
                labelResult.Text = node.Err!;
                return;
            }

            Result<double> result = node.Val!.Evaluate();
            if (result.IsErr())
            {
                labelResult.Text = node.Err!;
                return;
            }

            labelResult.Text = result.Val!.ToString();
        }

        private void Clear()
        {
            textBoxInput.Text = "";
        }

        private void AddChar(char c)
        {
            textBoxInput.Text += c;
        }

        private void RemoveChar()
        {
            if (textBoxInput.Text.Length >= 1)
            {
                textBoxInput.Text = textBoxInput.Text.Substring(0, textBoxInput.Text.Length - 1);
            }
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

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            UpdateAnswer();
        }
    }
}
