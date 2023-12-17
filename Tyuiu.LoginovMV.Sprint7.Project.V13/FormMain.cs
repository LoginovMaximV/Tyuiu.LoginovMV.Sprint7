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
            if (string.IsNullOrWhiteSpace(countryName) || string.IsNullOrWhiteSpace(capital) ||
    string.IsNullOrWhiteSpace(textBoxSquare_LMV.Text) || string.IsNullOrWhiteSpace(textBoxEconomic_LMV.Text) ||
    string.IsNullOrWhiteSpace(textBoxPopulation_LMV.Text) || string.IsNullOrWhiteSpace(nationality) ||
    string.IsNullOrWhiteSpace(currency))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                

                string data = $"{countryName};{capital};{area};{GDP};{population};{nationality};{currency}";
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
    }
}
