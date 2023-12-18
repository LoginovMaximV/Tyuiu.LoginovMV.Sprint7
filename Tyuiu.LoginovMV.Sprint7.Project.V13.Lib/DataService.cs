using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tyuiu.LoginovMV.Sprint7.Project.V13.Lib
{
    public class DataService
    {
        public string[,] GetMatrix(string path)
        {
            string fileData = File.ReadAllText(path);
            // разделение на строки
            fileData = fileData.Replace('\n', '\r');
            string[] lines = fileData.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // количество строк и столбцов
            int rows = lines.Length;
            int columns = lines[0].Split(';').Length;

            // массив
            string [,] mas = new string [columns, rows];
            for (int i = 0; i < rows; i++)
            {
                string[] values = lines[i].Split(';');
                for (int j = 0; j < columns; j++)
                {
                    mas[j, i] = (values[j]);
                }
            }
            
            return mas;
        }
    }
    

}
