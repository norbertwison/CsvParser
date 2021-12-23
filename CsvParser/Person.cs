using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvParser
{
    
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public enum Genders
        {
            Female,
            Male,
        }
        public Genders Gender { get; set; }
        public string School { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
