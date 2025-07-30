namespace _02_consumer_producer;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Choose mode: 1 = Producer, 2 = Consumer");
        var choice = Console.ReadLine();

        if (choice == "1")
        {
            await Producer.Run();
        }
        else if (choice == "2")
        {
            await Consumer.Run();
        }
    }
}