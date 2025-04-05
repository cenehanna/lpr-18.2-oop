using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lpr_18._2_oop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "Приклад: 3.5, -2.0, 7.1, -4.3, 1.2, 0.5, -6.7";
            textBox7.Text = "Приклад: {1, 2, 3, 4},{ 5, 6, 7, 8},{ 9, 10, 11, 12}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] input = textBox1.Text.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            double[] array;

            try
            {
                array = input.Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
            }
            catch
            {
                MessageBox.Show("Некоректний ввід! Введіть дійсні числа через пробіл або кому. Для дробів використовуйте крапку.");
                return;
            }

            double product = 1;
            bool hasPositive = false;
            foreach (double num in array)
            {
                if (num > 0)
                {
                    product *= num;
                    hasPositive = true;
                }
            }
            if (!hasPositive) product = 0;
            textBox2.Text = product.ToString();

            double minValue = array.Min();
            int minIndex = Array.IndexOf(array, minValue);
            double sumBeforeMin = array.Take(minIndex).Sum();
            textBox3.Text = sumBeforeMin.ToString();

            double[] evenIndexed = new double[(array.Length + 1) / 2];
            double[] oddIndexed = new double[array.Length / 2];
            int evenIdx = 0, oddIdx = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (i % 2 == 0)
                    evenIndexed[evenIdx++] = array[i];
                else
                    oddIndexed[oddIdx++] = array[i];
            }

            Array.Sort(evenIndexed);
            Array.Sort(oddIndexed);

            double[] combinedArray = new double[array.Length];
            int combinedIndex = 0;
            for (int i = 0; i < evenIndexed.Length; i++)
            {
                combinedArray[combinedIndex] = evenIndexed[i];
                combinedIndex += 2;
            }
            combinedIndex = 1;
            for (int i = 0; i < oddIndexed.Length; i++)
            {
                combinedArray[combinedIndex] = oddIndexed[i];
                combinedIndex += 2;
            }

            textBox4.AppendText(string.Join(", ", combinedArray));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string input = textBox7.Text.Trim();

            try
            {
                input = input.Trim('{', '}');

                string[] rowStrings = input.Split(new[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);

                int rowCount = rowStrings.Length;
                int colCount = -1;

                List<List<int>> matrixList = new List<List<int>>();

                foreach (string rowStr in rowStrings)
                {
                    string[] values = rowStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    List<int> row = values.Select(v => int.Parse(v.Trim())).ToList();

                    if (colCount == -1)
                        colCount = row.Count;
                    else if (row.Count != colCount)
                        throw new Exception("Кількість стовпців у рядках не збігається.");

                    matrixList.Add(row);
                }

                int[,] matrix = new int[rowCount, colCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        matrix[i, j] = matrixList[i][j];
                    }
                }

                StringBuilder matrixDisplay = new StringBuilder();
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        matrixDisplay.Append(matrix[i, j] + " ");
                    }
                    matrixDisplay.AppendLine();
                }

                textBox8.Text = matrixDisplay.ToString();
                textBox5.Text = matrix[0, 0].ToString(); 
                textBox6.Text = matrix[rowCount - 1, colCount - 1].ToString(); 
            }
            catch
            {
                MessageBox.Show("Некоректний ввід! Перевірте формат введення масиву.");
            }
        }



    }
}
