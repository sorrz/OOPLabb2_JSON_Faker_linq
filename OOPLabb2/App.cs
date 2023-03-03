using Bogus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace OOPLabb2
{
    internal class App
    {
        public List<Person> listOfPersons = new List<Person>();
        public List<Person> secondaryList = new List<Person>();
        private string filePath = "C:\\Users\\danst\\source\\repos\\OOPLabb2\\OOPLabb2\\customers.json";

        internal void Run()
        {

            FrontLoad();

            while (true)
            {
                // Lägga in 100 till  i listan
                Console.WriteLine("1. Skapa");
                // Skriv in text
                // Lista alla som innehåller texten i namn eller city
                Console.WriteLine("2. Sök");
                Console.WriteLine("3. Count");

                //  UserInput for Menu  Choice
                int userInput = 0;
                userInput = Convert.ToInt32(Console.ReadLine());
                
                    if (userInput == 1)
                        CreateFake();
                    if (userInput == 2)
                        CustomerSearch();
                    if (userInput == 3)
                        CountCustomers();
                
            }

        }

        private void CustomerSearch()
        {
            Console.Write("What do you wanna sort on, enter a Name or City: ");
            var input = Console.ReadLine();
            var sorted = listOfPersons.Where(e => e.FirstName == input || e.City == input)
                .Select(e => e.FirstName + e.City + e.Country).ToList();
            foreach (var item in sorted)
            {
                Console.WriteLine(item);
            }
            return;
        }

        private void CountCustomers()
        {
            var x = listOfPersons.Count();
            Console.WriteLine(x);
            return;
        }

        private void FrontLoad()
        {
            var text = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(text))
                listOfPersons = JsonConvert.DeserializeObject<List<Person>>(text);
            else
                listOfPersons = new List<Person>();
        }

        private void ConvertAndSave()
        {
            var text = JsonConvert.SerializeObject(listOfPersons);
            // Eventuellt en tömning här.
            File.WriteAllText(filePath, text, Encoding.UTF8);
            return;
        }

        private void CreateFake()
        {
            File.WriteAllText(filePath, String.Empty);
            var faker2 = new Faker<Person>()
                   .RuleFor(person => person.FirstName, faker2 => faker2.Person.FirstName)
                   .RuleFor(person => person.LastName, faker2 => faker2.Person.LastName)
                   .RuleFor(person => person.PhoneNumber, faker2 => faker2.Person.Phone)
                   .RuleFor(person => person.EmailAdress, faker2 => faker2.Person.Email)
                   .RuleFor(person => person.StreetAdress, faker2 => faker2.Address.StreetName())
                   .RuleFor(person => person.ZipCode, faker2 => faker2.Address.ZipCode())
                   .RuleFor(person => person.City, faker2 => faker2.Address.City())
                   .RuleFor(person => person.Country, faker2 => faker2.Address.Country())
                   .RuleFor(person => person.CreditCardNumber, faker2 => faker2.Finance.CreditCardNumber());


            secondaryList = faker2.Generate(100);
            listOfPersons.AddRange(secondaryList);

            ConvertAndSave();
            

        }
    }
}
