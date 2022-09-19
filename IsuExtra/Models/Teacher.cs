using System.Collections.Generic;

namespace IsuExtra.Models
{
    public class Teacher
    {
        public Teacher(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}