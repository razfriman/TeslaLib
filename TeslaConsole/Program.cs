using System;
using TeslaLib;

namespace TeslaConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string clientId = "";
            string clientSecret = "";

            string email = "";
            string password = "";            

            TeslaClient client = new TeslaClient(email, clientId, clientSecret);

            client.Login(password);

            var vehicles = client.LoadVehicles();

            foreach (TeslaVehicle car in vehicles)
            {
                Console.WriteLine(car.Id + " " + car.VIN);
            }

            Console.ReadKey();
        }
    }
}
