﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkMethods.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public String Adres { get; set; }
        public int PersonId { get; set; }//bunu ayırt ettirmek için yazıyoruz
        public Person Person { get; set; }
    }
}