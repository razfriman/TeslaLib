using System.Threading.Tasks;

namespace TeslaLib.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            args = new string[]
            {
                "81527cff06843c8634fdc09e8ac0abefb46ac849f38fe1e431c2ef2106796384",
                "c7257eb71a564034f9419ee651c7d0e5f7aa6bfbd18bafb5c5c033b093bb2fa3",
                "***REMOVED***",
                "***REMOVED***"
            };

            if (args.Length < 4)
            {
                System.Console.WriteLine("Not enough args. <clientId> <clientSecret> <email> <password>");
                return;
            }

            var clientId = args[0];
            var clientSecret = args[1];
            var email = args[2];
            var password = args[3];

            var client = new TeslaClient(email, clientId, clientSecret);
            await client.Login(password).ConfigureAwait(false);

            var vehiclesResponse = await client.LoadVehicles().ConfigureAwait(false);

            if (vehiclesResponse.IsSuccess)
            {
                foreach (var car in vehiclesResponse.Data)
                {
                    System.Console.WriteLine(car.Id + " " + car.Vin);
                }
            }
            else
            {
                System.Console.WriteLine($"Error loading vehicles: {vehiclesResponse.StatusCode}-{vehiclesResponse.ErrorMessage}");
            }

            System.Console.ReadKey();
        }
    }
}
