using System;

class Client
{
    public event EventHandler Event1;
    public event EventHandler Event4;

    public string Name { get; }
    public string Address1 { get; }
    public string Address2 { get; }

    public Client(string name, string address1, string address2)
    {
        Name = name;
        Address1 = address1;
        Address2 = address2;
    }

    public void CallTaxi()
    {
        Event1?.Invoke(this, EventArgs.Empty);
    }

    public void ArrivedAtDestination()
    {
        Event4?.Invoke(this, EventArgs.Empty);
    }
}

class Taxi
{
    public event EventHandler Event2;
    public event EventHandler Event3;

    public void TaxiArrived(string address1)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Таксі прибуло за адресою {address1}");
        Console.ResetColor();
        Event2?.Invoke(this, EventArgs.Empty);
    }

    public void StartTrip()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Їдемо");
        Console.ResetColor();
        Event3?.Invoke(this, EventArgs.Empty);
    }

    public void ArrivedAtDestination()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Приїхали");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Таксі вільно");
        Console.ResetColor();
    }

    public void ArrivedAtDestination(string address2)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Приїхали");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Пасажир(ка) прибув(ла) за адресою: {address2}");
        Console.ResetColor();
    }

    public void TaxiFree()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Таксі вільно");
        Console.ResetColor();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Введіть ім'я пасажира(ки): ");
        string name = Console.ReadLine();
        Console.WriteLine("Введіть адресу 1: ");
        string address1 = Console.ReadLine();
        Console.WriteLine("Введіть адресу 2: ");
        string address2 = Console.ReadLine();

        Client client = new Client(name, address1, address2);
        Taxi taxi = new Taxi();

        client.Event1 += (sender, eventArgs) => {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{client.Name} викликав(ла) таксі на адресу {client.Address1}");
            Console.ResetColor();
            taxi.TaxiArrived(client.Address1);
        };

        taxi.Event2 += (sender, eventArgs) => {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{client.Name} сів(ла) у таксі");
            Console.ResetColor();
            taxi.StartTrip();
        };

        taxi.Event3 += (sender, eventArgs) => {
            Console.ForegroundColor = ConsoleColor.Yellow;
            client.ArrivedAtDestination();
        };

        client.Event4 += (sender, eventArgs) => {
            taxi.ArrivedAtDestination(client.Address2);
            taxi.TaxiFree(); 
        };

        client.CallTaxi();
        Console.ReadLine();
    }
}
