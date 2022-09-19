using System.Collections.Generic;
using Isu.Tools;
using Microsoft.VisualBasic.CompilerServices;

namespace Isu.Models
{
    public class Group
    {
        public Group(string name, CourseNumber courseNumber)
        {
            Name = name;
            Coursenumber = courseNumber;
            Numberofstudents = 0;
        }

        protected Group()
        {
        }

        public string Name { get; protected set; }
        public List<Student> Students { get; private set; } = new List<Student>();
        public CourseNumber Coursenumber { get; protected set; }
        public int Numberofstudents { get; private set; }

        public void AddStudent(Student student)
        {
            Students.Add(student);
            Numberofstudents++;
        }

        public Student FindStudentById(int id)
        {
            foreach (Student student in Students)
            {
                if (id == student.Id)
                {
                    return student;
                }
            }

            return null;
        }

        public Student FindStudentByName(string name)
        {
            foreach (Student student in Students)
            {
                if (name == student.Name)
                {
                    return student;
                }
            }

            return null;
        }

        public void DeleteStudent(Student student)
        {
            Students.Remove(student);
            Numberofstudents--;
        }

        public bool GroupIsFull()
        {
            if (Numberofstudents < 25) return false;
            else return true;
        }
    }
}