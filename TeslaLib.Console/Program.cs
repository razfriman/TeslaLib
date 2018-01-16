namespace TeslaLib.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                System.Console.WriteLine("Not enough args. <clientId> <clientSecret> <email> <password>");
            }
            var clientId = args[0];
            var clientSecret = args[1];
            var email = args[2];
            var password = args[3];

            var client = new TeslaClient(email, clientId, clientSecret);
            client.Login(password);

            var vehicles = client.LoadVehicles();

            foreach (TeslaVehicle car in vehicles)
            {
                System.Console.WriteLine(car.Id + " " + car.VIN);
            }

            System.Console.ReadKey();
        }
    }
}
