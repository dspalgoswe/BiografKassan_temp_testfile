using System;
using System.Collections.Generic;

namespace BiografKassan
{
    // Klass som representerar kund
    public class Customer
    {
        public int Age { get; set; }

        // Konstruktor utifrån ålder
        public Customer(int age)
        {
            Age = age;
        }
    }

    // Klass f. biljettpriset
    public class TicketPricing
    {
        // Metod för att beräkna biljettpriser utifrån en lista av kunder
        public decimal CalculateTotalPrice(List<Customer> customers)
        {
            decimal total = 0;

            foreach (var customer in customers)
            {
                total += GetTicketPrice(customer.Age);
            }

            return total;
        }

        // Metod som ger biljettpris utifrån ålder
        private decimal GetTicketPrice(int age)
        {
            if (age < 6)
                return 0; // De minsta går gratis
            else if (age > 99)
                return 0; // Hundraåringar går gratis
            else if (age < 18)
                return 80; // Ungdomspris
            else if (age >= 65)
                return 90; // Pensionärspris
            else
                return 120; // Standardpris
        }
    }

    // Klass för att hantera meny och användargränssnitt
    public class Menu
    {
        public void Show()
        {
            Console.WriteLine("Välkommen! Du har två alternativ");
            Console.WriteLine("1. Beräkna biljettpriser");
            Console.WriteLine("0. Avsluta");
        }

        public int GetChoice()
        {
            Console.Write("Välj ett alternativ: ");
            string input = Console.ReadLine();
            return int.TryParse(input, out int choice) ? choice : -1; // Returnera -1 vid felaktig inmatning
        }
    }

    // Huvudprogram, dvs "Main"
    public class Program
    {
        public static void Main(string[] args)
        {
            Menu menu = new Menu();
            TicketPricing ticketPricing = new TicketPricing();
            List<Customer> customers = new List<Customer>();

            while (true)
            {
                menu.Show();
                int choice = menu.GetChoice();

                // Menyalternativ med switch-sats
                switch (choice)
                {
                    case 1:
                        Console.Write("Ange antal personer: ");
                        int numberOfCustomers = int.Parse(Console.ReadLine());

                        for (int i = 0; i < numberOfCustomers; i++)
                        {
                            Console.Write($"Ange ålder för person {i + 1}: ");
                            int age = int.Parse(Console.ReadLine());
                            customers.Add(new Customer(age)); // Lägg till kund i listan
                        }

                        decimal totalPrice = ticketPricing.CalculateTotalPrice(customers);
                        Console.WriteLine($"Sammanlagt biljettpris för {numberOfCustomers} personer: {totalPrice} kr");
                        customers.Clear(); // Rensa listan för nästa användning
                        break;

                    case 0:
                        Console.WriteLine("Tack för ditt besök!");
                        return; // Avsluta programmet

                    default:
                        Console.WriteLine("Felaktig input. Ange '1' för att beräkna biljettpriser eller '0' för att avsluta. Försök igen!");
                        break;
                }
            }
        }
    }
}