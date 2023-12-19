using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Tyuiu.LoginovMV.Sprint7.Project.V13.Lib;

namespace Tyuiu.LoginovMV.Sprint7.Project.V13
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            openFileDialogTask_LMV.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Все файлы(*.*)|*.*";
            saveFileDialogTask_LMV.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Все файлы(*.*)|*.*";
        }
        public string openFilePath;
        DataService ds = new DataService();

        private void buttonAddCountry_LMV_Click(object sender, EventArgs e)
        {
            
            string countryName = textBoxCountryName_LMV.Text;
            string capital = textBoxCapital_LMV.Text;
            double area;
            if (!double.TryParse(textBoxSquare_LMV.Text, out area))
            {
                MessageBox.Show("Введите число в поле Площадь!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double GDP;
            if (!double.TryParse(textBoxEconomic_LMV.Text, out GDP))
            {
                MessageBox.Show("Введите число в поле ВВП!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double population;
            if (!double.TryParse(textBoxPopulation_LMV.Text, out population))
            {
                MessageBox.Show("Введите число в поле Население!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string nationality = textBoxNations_LMV.Text;
            string currency = textBoxCurrency_LMV.Text;
            string language = textBoxLang_LMV.Text;
            string continent = textBoxContinent_LMV.Text;
            string religion = textBoxReligion_LMV.Text;
            if (string.IsNullOrWhiteSpace(countryName) || string.IsNullOrWhiteSpace(capital) ||
    string.IsNullOrWhiteSpace(textBoxSquare_LMV.Text) || string.IsNullOrWhiteSpace(textBoxEconomic_LMV.Text) ||
    string.IsNullOrWhiteSpace(textBoxPopulation_LMV.Text) || string.IsNullOrWhiteSpace(nationality) ||
    string.IsNullOrWhiteSpace(currency) || string.IsNullOrWhiteSpace(language) || string.IsNullOrWhiteSpace(continent) || string.IsNullOrWhiteSpace(religion))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                string data = $"{countryName};{capital};{area};{GDP};{currency};{population};{nationality};{language};{continent};{religion}";
                string fileName = $"{countryName}.csv";
                string filePath = Path.Combine("Countries", fileName);
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(data);
                }
                textBoxCountryName_LMV.Clear();
                textBoxCapital_LMV.Clear();
                textBoxSquare_LMV.Clear();
                textBoxEconomic_LMV.Clear();
                textBoxPopulation_LMV.Clear();
                textBoxNations_LMV.Clear();
                textBoxCurrency_LMV.Clear();
                textBoxContinent_LMV.Clear();
                textBoxLang_LMV.Clear();
                textBoxReligion_LMV.Clear();
                if (File.Exists(filePath))
                {
                    MessageBox.Show($"Файл с именем {countryName} был создан", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonDeleteCountry_LMV_Click(object sender, EventArgs e)
        {
            openFileDialogTask_LMV.ShowDialog();
            openFilePath = openFileDialogTask_LMV.FileName;
            if (File.Exists(openFilePath))
            {
                File.Delete(openFilePath);
                MessageBox.Show("Файл удален","Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Файл не найден","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxSquare_LMV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && (e.KeyChar != ',') && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void buttonSearch_LMV_Click(object sender, EventArgs e)
        {
            openFileDialogTask_LMV.ShowDialog();
            openFilePath = openFileDialogTask_LMV.FileName;
            string[,] matrix = ds.GetMatrix(openFilePath);
            int rows = matrix.GetLength(0);
            int column = matrix.GetLength(1);
            dataGridViewInfo_LMV.RowCount = rows;
            dataGridViewInfo_LMV.ColumnCount = column;
            for (int i = 0; i < column; i++)
            {
                dataGridViewInfo_LMV.Columns[i].Width = 130;
            }
            dataGridViewInfo_LMV.RowHeadersWidth = 170;
            dataGridViewInfo_LMV.Rows[0].HeaderCell.Value = "Название страны:";
            dataGridViewInfo_LMV.Rows[1].HeaderCell.Value = "Столица:";
            dataGridViewInfo_LMV.Rows[2].HeaderCell.Value = "Площадь в тыс.км^2:";
            dataGridViewInfo_LMV.Rows[3].HeaderCell.Value = "ВВП в млрд.$:";
            dataGridViewInfo_LMV.Rows[4].HeaderCell.Value = "Валюта:";
            dataGridViewInfo_LMV.Rows[5].HeaderCell.Value = "Население в тыс.людей:";
            dataGridViewInfo_LMV.Rows[6].HeaderCell.Value = "Национальность:";
            dataGridViewInfo_LMV.Rows[7].HeaderCell.Value = "Язык:";
            dataGridViewInfo_LMV.Rows[8].HeaderCell.Value = "Континент:";
            dataGridViewInfo_LMV.Rows[9].HeaderCell.Value = "Религия:";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    dataGridViewInfo_LMV.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }


        }

        private void buttonEnterCountry_LMV_Click(object sender, EventArgs e)
        {
            string countryname = textBoxWriteCountry_LMV.Text;
            string filename = $"{countryname}.csv";
            string filePath = Path.Combine("Countries", filename);
            if (File.Exists(filePath))
            {
                string[,] matrix = ds.GetMatrix(filePath);
                int rows = matrix.GetLength(0);
                int column = matrix.GetLength(1);
                dataGridViewInfo_LMV.RowCount = rows;
                dataGridViewInfo_LMV.ColumnCount = column;
                for (int i = 0; i < column; i++)
                {
                    dataGridViewInfo_LMV.Columns[i].Width = 130;
                }
                dataGridViewInfo_LMV.RowHeadersWidth = 170;
                dataGridViewInfo_LMV.Rows[0].HeaderCell.Value = "Название страны:";
                dataGridViewInfo_LMV.Rows[1].HeaderCell.Value = "Столица:";
                dataGridViewInfo_LMV.Rows[2].HeaderCell.Value = "Площадь в км^2:";
                dataGridViewInfo_LMV.Rows[3].HeaderCell.Value = "ВВП в млрд.$:";
                dataGridViewInfo_LMV.Rows[4].HeaderCell.Value = "Валюта:";
                dataGridViewInfo_LMV.Rows[5].HeaderCell.Value = "Население:";
                dataGridViewInfo_LMV.Rows[6].HeaderCell.Value = "Национальность:";
                dataGridViewInfo_LMV.Rows[7].HeaderCell.Value = "Язык:";
                dataGridViewInfo_LMV.Rows[8].HeaderCell.Value = "Континент:";
                dataGridViewInfo_LMV.Rows[9].HeaderCell.Value = "Религия:";
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        dataGridViewInfo_LMV.Rows[i].Cells[j].Value = matrix[i, j];
                    }
                }
            }
            else
            {
                MessageBox.Show($"Файл {countryname} не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
