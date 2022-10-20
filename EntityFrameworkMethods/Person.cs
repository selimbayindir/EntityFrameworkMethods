using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkMethods
{
    public class Person
    {
        public Person()
        {

        }
        public Person( string name, string lastName, string? city)
        {
            Name = name;
            LastName = lastName;
            City = city;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }
    }
}
