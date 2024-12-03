﻿using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Lab__6
{
    internal class Flight()
    {
        
        public enum FlightStatus
        {
            OnTime,
            Delayed,
            Cancelled,
            Boarding,
            InFlight
        }
        /*Атрибути для збереження даних про рейси.
            Методи для роботи з даними рейсу*/
        public string FlightNumber { get; set; }
        public string Airline { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public FlightStatus Status { get; set; }
        public TimeSpan Duration { get; set; }
        public string AircraftType { get; set; }
        public string Terminal { get; set; }


        public JArray flightsArray = new JArray();

        /// <summary>
        /// Обробляє дані з JSON файлу і десеарелізує його в масив
        /// </summary>
        public void LoadFlights()
        {
            string Path = "C:\\Users\\fakty\\OneDrive\\Рабочий стол\\Lab_#6\\flights_data.json";
            string JsData = File.ReadAllText(Path);

            JObject jsonObject = JObject.Parse(JsData);

            // Отримуємо масив "flights"
            this.flightsArray = (JArray)jsonObject["flights"];

        }
       
        
        /// <summary>
        /// Зберігає дані про обраний Flight
        /// </summary>
        public void SaveFlight(int N)
        {
            

            int i = 0;
            // Перебираємо кожен об'єкт у масиві
            foreach (JObject flight in flightsArray)
            {
                if (i == N)
                {
                    this.FlightNumber = flight["FlightNumber"].ToString();
                    this.Airline = flight["Airline"].ToString();
                    this.Destination = flight["Destination"].ToString();

                    string StDepartureTime = flight["DepartureTime"].ToString();
                    this.DepartureTime = DateTime.Parse(StDepartureTime);

                    string StArrivalTime = flight["ArrivalTime"].ToString();
                    this.ArrivalTime = DateTime.Parse(StArrivalTime);

                    string StStatus = flight["Status"].ToString();
                    if (Enum.TryParse(StStatus, out FlightStatus status))
                    {
                        this.Status = status;
                    }
                    else
                    {
                        Console.WriteLine("Невідомий статус рейсу: " + StStatus);
                    }

                    string StDuration = flight["Duration"].ToString();
                    this.Duration = TimeSpan.Parse(StDuration);

                    this.AircraftType = flight["AircraftType"].ToString();
                    this.Terminal = flight["Terminal"].ToString();
                }

                i++;
                if (N < 0 || N >= flightsArray.Count)
                {
                    //Console.WriteLine("Індекс рейсу поза межами допустимого діапазону.");
                    return;
                }
            }
        }
            
    }
}



