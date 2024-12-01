using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace Lab__6
{
    internal class FlightQueryHandler
    {
        /*Виконання запитів до інформаційної системи польотів.
         * Створення звітів та відповідей на запити в консольному додатку.*/

        public void Procesing()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            Console.WriteLine("Що ви бажаєте зробити ?");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1)Повернути всі рейси, які здійснюються певною авіакомпанією." +
                " \n2)Повернути всі рейси, які на даний момент затримуються" +
                "\n3)Повернути всі рейси, які вилітають в певний день" +
                "\n4)Повернути всі рейси, які вилітають та прибувають у вказаний проміжок часу (Наприклад: з 2023-05-1T00:00:01 до 2023-05-31T23:59:59) вказаний пункт призначення." +
                "\n5)Повернути всі рейси, які прибули за останню годину або за вказаний\r\nпроміжок часу");
            Console.ResetColor();
            int Num = int.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Зчитуємо дані про польоти з бази даних");

            FlightInformationSystem inform = new FlightInformationSystem();
            inform.SaveData();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Дані зчитано");
            Console.ResetColor();

            switch (Num)
            {
                case 1:
                    Console.WriteLine("Впишіть назву авіо компанії");
                    string company = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ось рейси цієї компанії:");
                    foreach (Flight fli in inform.Flights)
                    {
                        if(company == fli.Airline)
                        {
                            Console.WriteLine($"Рейс № {fli.FlightNumber}");
                        }
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;

                default: Console.WriteLine("Введено не правильний знак"); break;
            }
        }
            
    }
}
