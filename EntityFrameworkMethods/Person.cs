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

        public Person(int ıd, string name, string lastName)
        {
            Id = ıd;
            Name = name;
            LastName = lastName;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

    }
}
