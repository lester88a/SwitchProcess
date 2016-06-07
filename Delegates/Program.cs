using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    //create a object person
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        //delegate
        public delegate bool TypeFilterDelegate(Person p);

        static void Main(string[] args)
        {
            if (true)
            {
                Console.WriteLine("This is true");
            }
            if (false)
            {
                Console.WriteLine("This is not true");
            }
            //create 3 persons object
            Person p1 = new Person();
            p1.Name = "Jhon";
            p1.Age = 65;

            Person p2 = new Person();
            p2.Name = "Jim";
            p2.Age = 35;

            Person p3 = new Person();
            p3.Name = "Tom";
            p3.Age = 8;

            //Create a list of Person objects and fill it
            List<Person> people = new List<Person>();
            people.Add(p1);
            people.Add(p2);
            people.Add(p3);
            

            foreach (Person p in people)
            {
                Console.WriteLine("{0}({1} yrs old): is {2}.", p.Name, p.Age, Types(p));
            }

            DisplayPeople(people, IsChild);
            DisplayPeople(people, IsAdult);
            DisplayPeople(people, IsSenior);
        }

        static void DisplayPeople(List<Person> people, TypeFilterDelegate filter)
        {
            foreach (Person p in people)
            {
                if (filter(p))
                {
                    Console.WriteLine("{0}, {1} years old", p.Name, p.Age);
                }
            }
        }
        //==========FILTERS===================
        static bool IsChild(Person p)
        {
            return p.Age <= 18;
        }

        static bool IsAdult(Person p)
        {
            return p.Age >= 18;
        }

        static bool IsSenior(Person p)
        {
            return p.Age >= 65;
        }

        static string Types(Person p)
        {
            if (p.Age >= 30 && p.Age<=50)
            {
                return "Father";
            }
            else if (p.Age > 50)
            {
                return "GrandFather";
            }
            else
            {
                return "Son";
            }
        }
        
    }
}
