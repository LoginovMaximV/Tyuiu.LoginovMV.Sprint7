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
            int area = Convert.ToInt32(textBoxSquare_LMV.Text);
            int GDP = Convert.ToInt32(textBoxEconomic_LMV.Text);
            int population = Convert.ToInt32(textBoxPopulation_LMV.Text);
            string nationality = textBoxNations_LMV.Text;
            string currency = textBoxCurrency_LMV.Text;
            string data = $"{countryName};{capital};{area};{GDP};{population};{nationality};{currency}";
            string fileName = $"{countryName}.csv";
            string filePath = Path.Combine("Countries", fileName);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(data);
            }
            if (File.Exists(filePath))
            {
                MessageBox.Show($"Файл с именем {countryName} был создан") ;
            }
            else
            {
                MessageBox.Show("Файл не найден","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteCountry_LMV_Click(object sender, EventArgs e)
        {
            openFileDialogTask_LMV.ShowDialog();
            openFilePath = openFileDialogTask_LMV.FileName;
            if (File.Exists(openFilePath))
            {
                File.Delete(openFilePath);
                MessageBox.Show("Файл удален");
            }
            else
            {
                MessageBox.Show("Файл не найден","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
