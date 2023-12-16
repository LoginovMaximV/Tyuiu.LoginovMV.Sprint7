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
    }
    public class Country
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public int Population { get; set; }
        public string GetInfo()
        {
            return $"Страна: {Name}, Столица: {Capital}, Население: {Population}";
        }
    }
    public class CountryManager
    {
        private List<Country> countries = new List<Country>();

        public void LoadDataFromCSV(string filePath)
        {
            countries.Clear(); // Очищаем список перед загрузкой данных
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var country = new Country
                    {
                        Name = values[0],
                        Capital = values[1],
                        Population = int.Parse(values[2])
                        // Добавьте присвоение других свойств, 
                        // соответствующих структуре CSV файла
                    };
                    countries.Add(country);
                }
            }
        }

        public void SaveDataToCSV(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var country in countries)
                {
                    writer.WriteLine($"{country.Name},{country.Capital},{country.Population}");
                    // Добавьте запись других свойств, 
                    // соответствующих структуре CSV файла
                }
            }
        }

        public void AddCountry(string name, string capital, int population)
        {
            var country = new Country
            {
                Name = name,
                Capital = capital,
                Population = population
            };
            countries.Add(country);
        }

        public void RemoveCountry(string name)
        {
            var country = countries.FirstOrDefault(c => c.Name == name);
            if (country != null)
            {
                countries.Remove(country);
            }
        }

        public string GetCountryInfo(string name)
        {
            var country = countries.FirstOrDefault(c => c.Name == name);
            if (country != null)
            {
                return country.GetInfo();
            }
            else
            {
                return $"Страна с названием {name} не найдена";
            }
        }
    }

}
