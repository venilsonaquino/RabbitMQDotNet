namespace _05_pub_sub;

public class Program
{
      static void Main(string[] args)
    {
        Console.WriteLine("Choose mode: 1 = Producer, 2 = Consumer");
        var choice = Console.ReadLine();

        if (choice == "1")
        {
          Producer.Run();
        }
        else if (choice == "2")
        {
          Consumer.Run();
        }
    }
}
