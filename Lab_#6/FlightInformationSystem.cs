using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lab__6
{
    internal class FlightInformationSystem
    {
        /*Методи для управління та відображення даних про рейси.
         * Обробка та зберігання даних у форматі JSON.System.Text.Json і Newtonsoft.Json*/

        public List<Flight> Flights = new List<Flight>();
        public void SaveData()
        {
            
            string Path = "C:\\Users\\fakty\\OneDrive\\Рабочий стол\\Lab_#6\\flights_data.json";
            string JsData = File.ReadAllText(Path);

            JObject jsonObject = JObject.Parse(JsData);

            // Отримуємо масив "flights"
            JArray flightsArray = (JArray)jsonObject["flights"];

            int i = 0;
            // Перебираємо кожен об'єкт у масиві
            foreach (JObject flight in flightsArray)
            {

                Flight flg = new Flight(i);
                flg.LoadFlights();
                Flights.Add(flg);
                i++;
            }
        }
    }
}
