using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C__Calculator
{
    public partial class Form1 : Form
    {
        StringBuilder current = new StringBuilder();
        StringBuilder result = new StringBuilder();
        char operation = 'z';
        bool lastButtonClickedIsOpButton = false;
        public Form1()
        {
            InitializeComponent();
            button1.Click += numberButtonClicked;
            button2.Click += numberButtonClicked;
            button3.Click += numberButtonClicked;
            button4.Click += numberButtonClicked;
            button5.Click += numberButtonClicked;
            button6.Click += numberButtonClicked;
            button7.Click += numberButtonClicked;
            button8.Click += numberButtonClicked;
            button9.Click += numberButtonClicked;
            button0.Click += numberButtonClicked;
            buttonPlus.Click += operationButtonClicked;
            buttonMinus.Click += operationButtonClicked;
            buttonMultiply.Click += operationButtonClicked;
            buttonDivide.Click += operationButtonClicked;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            performOperation();
        }

        private void numberButtonClicked(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if(clickedButton != null)
            {
                if (operation == 'z')
                {
                    result.Clear();
                    prev_TB.Text = "";
                }
                lastButtonClickedIsOpButton = false;
                //MessageBox.Show(lastButtonClickedIsOpButton.ToString());
                current.Append(clickedButton.Text);
                result_TB.Text = current.ToString();
                if (result.Length > 0) prev_TB.Text = result.ToString();
            }
        }
        private void operationButtonClicked(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                operation = clickedButton.Text[0];
                if (result.Length > 0)
                {
                    // change operation
                    if (!Char.IsLetter(result[result.Length - 1]) && lastButtonClickedIsOpButton)
                    {
                        try
                        {
                            result.Remove(result.Length - 2, 2);
                            result.Append(current.ToString() + " " + operation);
                            prev_TB.Text = result.ToString();
                        } catch(Exception ex)
                        {
                            result_TB.Text = "ERROR";
                        }
                        
                    }
                    else
                    {
                        //performOperation();
                        result.Append(current.ToString() + " " + operation);
                        prev_TB.Text = result.ToString();
                    }
                }
                else
                {
                    result.Append(current.ToString() + " " + operation);
                    prev_TB.Text = result.ToString();
                }
                current.Clear();
                result_TB.Text = "0";
                lastButtonClickedIsOpButton = true;
            }
        }

        private void performOperation()
        {
            
            try
            {
                string calculation = result.ToString() + current.ToString();
                var result_value = new System.Data.DataTable().Compute(calculation, null);
                int calculation_result = Convert.ToInt32(result_value);
                result.Clear();
                result.Append(calculation_result);
                current.Clear();
                operation = 'z';
                result_TB.Text = result.ToString();
                prev_TB.Text = "0";
                Console.WriteLine(calculation_result);
            }
            catch (Exception exc)
            {
                //MessageBox.Show("Invalid input, please enter a valid number.");
                result_TB.Text = "Syntax Error";
                result.Clear();
                current.Clear();
            }
        }

        private void prev_TB_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if(current.Length > 0)
            {
                current.Remove(current.Length - 1, 1);
                result_TB.Text = current.ToString();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            result.Clear();
            current.Clear();
            operation = 'z';
            prev_TB.Text = "";
            result_TB.Text = "0";
        }
    }
}
