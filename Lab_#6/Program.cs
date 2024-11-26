using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Lab__6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string Path = "C:\\Users\\fakty\\OneDrive\\Рабочий стол\\Lab_#6\\flights_data.json";
            string JsData = File.ReadAllText(Path);

            JObject jsonObject = JObject.Parse(JsData);

            // Отримуємо масив "flights"
            JArray flightsArray = (JArray)jsonObject["flights"];

            // Перебираємо кожен об'єкт у масиві
            foreach (JObject flight in flightsArray)
            {
                string flightNumber = flight["FlightNumber"].ToString();
                Console.WriteLine(flightNumber);
            }
        }
    }
}
