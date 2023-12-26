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
using System.Configuration;
using System.Data.SqlClient;

namespace Tyuiu.LoginovMV.Sprint7.Project.V13
{
    public partial class FormMain : Form
    {
        private SqlConnection sqlConnection = null;
        public FormMain()
        {
            InitializeComponent();
            openFileDialogTask_LMV.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Все файлы(*.*)|*.*";
            saveFileDialogTask_LMV.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Все файлы(*.*)|*.*";
        }
        public string openFilePath;
        DataService ds = new DataService();
        public string FolderContr = @"C:\Users\Валерий\source\repos\Tyuiu.LoginovMV.Sprint7\Tyuiu.LoginovMV.Sprint7.Project.V13\bin\Debug\Countries";


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
            textBoxWriteCountry_LMV.Text = Path.GetFileNameWithoutExtension(openFileDialogTask_LMV.FileName);
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
            dataGridViewInfo_LMV.Rows[2].HeaderCell.Value = "Площадь в км^2:";
            dataGridViewInfo_LMV.Rows[3].HeaderCell.Value = "ВВП в млрд.$:";
            dataGridViewInfo_LMV.Rows[4].HeaderCell.Value = "Валюта:";
            dataGridViewInfo_LMV.Rows[5].HeaderCell.Value = "Население :";
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

        private void buttonFlag_LMV_Click(object sender, EventArgs e)
        {
            string flag = textBoxWriteCountry_LMV.Text;
            string flagfilename = $"{flag}.jpg";
            string flagfilePath = Path.Combine("Flags", flagfilename);
            if (File.Exists(flagfilePath))
            {
                string flagCountry = flagfilePath;
                pictureBoxFlag_LMV.ImageLocation = flagCountry;
            }
            else
            {
                MessageBox.Show($"Флаг {flag} не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string gerb = textBoxWriteCountry_LMV.Text;
            string gerbfilename = $"{gerb}.jpg";
            string gerbfilePath = Path.Combine("Gerb", gerbfilename);
            
            if (File.Exists(gerbfilePath))
            {
                string gerbCountry = gerbfilePath;
                pictureBoxGerb_LMV.ImageLocation = gerbCountry;
            }
            else
            {
                MessageBox.Show($"Герб  не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


        private void buttonLoadTab_LMV_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Countries", sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridViewCountries_LMV.DataSource = dataSet.Tables[0];
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.countriesTableAdapter.Fill(this.databaseCDataSet.Countries);
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CountryList"].ConnectionString);
            sqlConnection.Open();
            comboBoxColumn_LMV.SelectedIndex = 0;
        }

        private void textBoxSearchColumn_LMV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(textBoxSearchColumn_LMV.Text))
                {
                    countriesBindingSource.Filter = string.Empty;
                }
                else
                {
                    countriesBindingSource.Filter = string.Format("{0}='{1}'", comboBoxColumn_LMV.Text, textBoxSearchColumn_LMV.Text);
                }
            }
        }

        private void buttonRedaction_LMV_Click(object sender, EventArgs e)
        {
            openFileDialogTask_LMV.ShowDialog();
            openFilePath = openFileDialogTask_LMV.FileName;
            string[,] matrix = ds.GetMatrix(openFilePath);
            textBoxCountryName_LMV.Text = matrix[0, 0];
            textBoxCapital_LMV.Text = matrix[1, 0];
            textBoxSquare_LMV.Text = matrix[2, 0];
            textBoxEconomic_LMV.Text = matrix[3, 0];
            textBoxCurrency_LMV.Text = matrix[4, 0];
            textBoxPopulation_LMV.Text = matrix[5, 0];
            textBoxNations_LMV.Text = matrix[6, 0];
            textBoxLang_LMV.Text = matrix[7, 0];
            textBoxContinent_LMV.Text = matrix[8, 0];
            textBoxReligion_LMV.Text = matrix[9, 0];
        }

        private void buttonGDP_LMV_Click(object sender, EventArgs e)
        {
            string countryname = textBoxWriteCountry_LMV.Text;
            string filename = $"{countryname}.csv";
            string filePath = Path.Combine("Countries", filename);
            if (File.Exists(filePath))
            {
                string[,] matrix = ds.GetMatrix(filePath);
                double GDP = Convert.ToDouble(matrix[3, 0])*Math.Pow(10,9);
                double Population = Convert.ToDouble(matrix[5, 0]);
                double res = Math.Round(ds.GDPperCapita(GDP, Population), 1);
                textBoxGDP_LMV.Text = Convert.ToString(res);
            }
        }

        private void buttonDensity_LMV_Click(object sender, EventArgs e)
        {
            string countryname = textBoxWriteCountry_LMV.Text;
            string filename = $"{countryname}.csv";
            string filePath = Path.Combine("Countries", filename);
            if (File.Exists(filePath))
            {
                string[,] matrix = ds.GetMatrix(filePath);
                double Area = Convert.ToDouble(matrix[2, 0]);
                double Population = Convert.ToDouble(matrix[5, 0]);
                double res = Math.Round(ds.GDPperCapita(Population,Area), 1);
                textBoxDensity_LMV.Text = Convert.ToString(res);
            }
        }

        private void comboBoxSelectColumn_LMV_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateChart(comboBoxSelectColumn_LMV.SelectedItem.ToString());
        }
        private void UpdateChart(string columnName)
        {
            string query = $"SELECT CountryName, {columnName} FROM Countries";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection);

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            chartFunction_LMV.Series["Series1"].Points.Clear();
            chartFunction_LMV.ChartAreas[0].AxisX.Interval = 1;
            foreach (DataRow row in dataTable.Rows)
            {
                string countryName = row["CountryName"].ToString();
                double columnValue = Convert.ToDouble(row[columnName]);

                chartFunction_LMV.Series["Series1"].Points.AddXY(countryName, columnValue);
            }
        }

        private void buttonAddCountryInBase_LMV_Click(object sender, EventArgs e)
        {
            openFileDialogTask_LMV.ShowDialog();
            string openFilePath = openFileDialogTask_LMV.FileName;
            var encoding = Encoding.GetEncoding("UTF-8"); 
            var lines = File.ReadAllLines(openFilePath, encoding);

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CountryList"].ConnectionString))
            {
                connection.Open();

                foreach (var line in lines)
                {
                    var values = line.Split(';');

                    if (values.Length == 10) 
                    {
                        string countryName = values[0];
                        string capital = values[1];
                        int area = int.Parse(values[2]);
                        int ggp = int.Parse(values[3]);
                        string currency = values[4];
                        int population = int.Parse(values[5]);
                        string nationality = values[6];
                        string language = values[7];
                        string continent = values[8];
                        string religion = values[9];

                        string query = $"INSERT INTO Countries (CountryName, Capital, Area, GGP, Population, Currency, Nation, Language, Continent, Religion) VALUES " +
                            $"(N'{countryName}', N'{capital}', '{area}', '{ggp}', N'{population}', N'{currency}', N'{nationality}', N'{language}', N'{continent}', N'{religion}')";

                        SqlCommand command = new SqlCommand(query, connection);

                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonDeleteCountryBase_LMV_Click(object sender, EventArgs e)
        {
            openFileDialogTask_LMV.ShowDialog();
            string openFilePath = Path.GetFileNameWithoutExtension(openFileDialogTask_LMV.FileName);
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CountryList"].ConnectionString))
            {
                connection.Open();
                string query = "DELETE FROM Countries WHERE CountryName = @FileName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FileName", openFilePath);
                command.ExecuteNonQuery();
            }
        }
    }
}
