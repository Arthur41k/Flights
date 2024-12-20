﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lab__6
{
    internal class FlightQueryHandler
    {
     

        public void Procesing()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            Console.WriteLine("Що ви бажаєте зробити ?");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1)Повернути всі рейси, які здійснюються певною авіакомпанією." +
                " \n2)Повернути всі рейси, які на даний момент затримуються" +
                "\n3)Повернути всі рейси, які вилітають в певний день" +
                "\n4)Повернути всі рейси, які вилітають та прибувають у вказаний проміжок часу (Наприклад: з 2023-05-1T00:00:01 до 2023-05-31T23:59:59) вказаний пункт призначення." +
                "\n5)Повернути всі рейси, які прибули за останню годину або за вказаний проміжок часу");
            Console.ResetColor();
            Console.Write("###:");
            int Num;
            Num = int.Parse(Console.ReadLine()); 
           
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Зчитуємо дані про польоти з бази даних");
            Thread.Sleep(1000);

            Flight flg = new Flight();
            flg.LoadFlights();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Дані зчитано");
            Console.ResetColor();

            switch (Num)
            {
                case 1:
                    FlightInformationSystem FIS = new FlightInformationSystem();

                    Console.WriteLine("Впишіть назву авіо компанії");
                    Console.Write("###:");
                    string company = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ось рейси цієї компанії:");
                    Thread.Sleep(1500);
                    foreach (JObject flight in flg.flightsArray)
                    {
                        if(flight["Airline"]?.ToString() == company)
                        {
                            Console.WriteLine($"Рейс № {flight["FlightNumber"]}");
                            FIS.ProcessingJson(flight);
                        }

                    }
                    Console.ResetColor();
                    FIS.SaveJson();
                    break;
                case 2:
                    FlightInformationSystem fis = new FlightInformationSystem();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ось всі рейси що затримуються:");
                    Thread.Sleep(1500);
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (JObject flight in flg.flightsArray)
                    {
                        if (flight["Status"]?.ToString() == "Delayed")
                        {
                            
                            Console.WriteLine($"Рейс № {flight["FlightNumber"]}");
                            fis.ProcessingJson(flight);

                        }
                    }
                    Console.ResetColor();
                    fis.SaveJson();
                    break;
                case 3:
                    FlightInformationSystem FlIS = new FlightInformationSystem();

                    Console.WriteLine("В який день ?  (Формат: yyyy-MM-dd)");
                    Console.Write("###:");
                    string time = Console.ReadLine();

                    if (!DateTime.TryParse(time, out DateTime targetDate))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Некоректний формат дати. Спробуйте ще раз.");
                        Console.ResetColor();
                        return;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    bool flightsFound = false;
                    foreach (JObject flight in flg.flightsArray)
                    {
                        if (DateTime.TryParse(flight["DepartureTime"]?.ToString(), out DateTime departureTime) &&
                            departureTime.Date == targetDate.Date)
                        {
                            Console.WriteLine($"Рейс № {flight["FlightNumber"]} - Час відправлення: {departureTime:HH:mm}");
                            FlIS.ProcessingJson(flight);
                            flightsFound = true;
                        }
                    }

                    if (!flightsFound)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("На вибрану дату рейсів немає.");
                    }

                    
                    Console.ResetColor();
                    FlIS.SaveJson();
                    break;
                case 4:
                    FlightInformationSystem FISy = new FlightInformationSystem();


                    Console.WriteLine("Введіть початкову дату та час (Формат: yyyy-MM-ddTHH:mm:ss):");
                    Console.Write("###: ");
                    string startTimeInput = Console.ReadLine();

                    Console.WriteLine("Введіть кінцеву дату та час (Формат: yyyy-MM-ddTHH:mm:ss):");
                    Console.Write("###: ");
                    string endTimeInput = Console.ReadLine();

                    Console.WriteLine("Введіть пункт призначення:");
                    Console.Write("###: ");
                    string destination = Console.ReadLine();

                    
                    if (!DateTime.TryParse(startTimeInput, out DateTime startTime) || !DateTime.TryParse(endTimeInput, out DateTime endTime))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Некоректний формат дати. Спробуйте ще раз.");
                        Console.ResetColor();
                        return;
                    }

                    
                    var filteredFlights = flg.flightsArray.Where(flight => DateTime.TryParse(flight["DepartureTime"]?.ToString(), out DateTime departureTime) 
                    && DateTime.TryParse(flight["ArrivalTime"]?.ToString(), out DateTime arrivalTime) 
                    && departureTime >= startTime && arrivalTime <= endTime && flight["Destination"]?.ToString().Equals(destination, StringComparison.OrdinalIgnoreCase) 
                    == true).ToList();

                    
                    if (filteredFlights.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Рейси, які відповідають вашим критеріям:");
                        foreach (JObject flight in filteredFlights)
                        {
                            Console.WriteLine($"Рейс № {flight["FlightNumber"]} - Час відправлення: {flight["DepartureTime"]} - Час прибуття: {flight["ArrivalTime"]} - Пункт призначення: {flight["Destination"]}");
                            FISy.ProcessingJson(flight);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("На вибраний проміжок часу та пункт призначення рейсів немає.");
                    }

                    Console.ResetColor();
                    FISy.SaveJson();
                    break;
                case 5:
                    FlightInformationSystem FInS = new FlightInformationSystem();

                    Console.WriteLine("Введіть початкову дату та час (Формат: yyyy-MM-ddTHH:mm:ss) або натисніть Enter, щоб пропустити:");
                    Console.Write("###: ");
                    string stTimeInput = Console.ReadLine();

                    Console.WriteLine("Введіть кінцеву дату та час (Формат: yyyy-MM-ddTHH:mm:ss) або натисніть Enter, щоб пропустити:");
                    Console.Write("###: ");
                    string enTimeInput = Console.ReadLine();

                    
                    DateTime now = DateTime.Now;

                    
                    DateTime? stTime = null;
                    DateTime? enTime = null;

                    if (!string.IsNullOrEmpty(stTimeInput))
                    {
                        if (!DateTime.TryParse(stTimeInput, out DateTime start))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Некоректний формат початкової дати. Спробуйте ще раз.");
                            Console.ResetColor();
                            break;
                        }
                        stTime = start;
                    }

                    if (!string.IsNullOrEmpty(enTimeInput))
                    {
                        if (!DateTime.TryParse(enTimeInput, out DateTime end))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Некоректний формат кінцевої дати. Спробуйте ще раз.");
                            Console.ResetColor();
                            break;
                        }
                        enTime = end;
                    }


                    
                    var filterFlights = flg.flightsArray
                        .Where(flight => DateTime.TryParse(flight["DepartureTime"]?.ToString(), out DateTime departureTime) &&
                                         DateTime.TryParse(flight["ArrivalTime"]?.ToString(), out DateTime arrivalTime) &&
                                         (arrivalTime >= now.AddHours(-1) ||
                                          (stTime.HasValue && enTime.HasValue && departureTime >= stTime && arrivalTime <= enTime)))
                        .ToList();

                   
                    if (filterFlights.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Рейси, які відповідають вашим критеріям:");
                        foreach (JObject flight in filterFlights)
                        {
                            Console.WriteLine($"Рейс № {flight["FlightNumber"]} - Час відправлення: {flight["DepartureTime"]} - Час прибуття: {flight["ArrivalTime"]}- Пункт призначення: {flight["Destination"]}");
                            FInS.ProcessingJson(flight);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("На вибраний проміжок часу рейсів немає.");
                    }

                    Console.ResetColor();
                    FInS.SaveJson();
                    break;

                default: Console.WriteLine("Введено не правильний знак"); break;
            }
        }
            
    }
}
