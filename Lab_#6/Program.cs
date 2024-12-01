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
            FlightQueryHandler flightQueryHandler = new FlightQueryHandler();
            flightQueryHandler.Procesing();
        }
    }
}
