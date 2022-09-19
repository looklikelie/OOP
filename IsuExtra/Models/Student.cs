using System.Collections.Generic;
using Isu.Models;

namespace IsuExtra.Models
{
    public class Student : Isu.Models.Student
    {
        public Student(string name, Group group)
            : base(name, group)
        {
        }

        public List<Flow> Flows { get; private set; } = new List<Flow>();
    }
}