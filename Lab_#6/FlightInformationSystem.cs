using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab__6
{
    internal class FlightInformationSystem
    {
        public JArray Jar = new JArray();   
         
      
         public void ProcessingJson(JObject job) 
        {
            Jar.Add(job);
        }

        public void SaveJson()
        {
            Console.WriteLine("Бажаєте зберегти дані в файлі JSON формату \n Так(1) Ні(2)");
            Console.Write("###:");
            int num = int.Parse(Console.ReadLine());

            if (num == 1)
            {
                
                string jsonString = Jar.ToString();

                
                string filePath = "C:\\Users\\fakty\\OneDrive\\Рабочий стол\\JSON.json";
                
                
                if (!File.Exists(filePath))
                {
                    
                    File.WriteAllText(filePath, jsonString);
                    Console.WriteLine($"JSON файл успішно створено та збережено за адресою: {filePath}");
                }
                else
                {
                    Console.WriteLine($"Файл за адресою {filePath} вже існує.");
                }
            }
            else if (num == 2)
            {
                return;
            }
            else 
            {
                Console.WriteLine("Введено не правильну цифру або символ");
            }
        }

    }
}
