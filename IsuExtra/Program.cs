using System;
using IsuExtra.Models;
using IsuExtra.Services;

namespace IsuExtra
{
    internal class Program
    {
        private static void Main()
        {
            var isu = new IsuExtraService();
            Group group1 = isu.AddGroup("M3212");
            Student student1 = isu.AddStudent(group1, "Dimas");
            Student student2 = isu.AddStudent(group1, "Igor");
            Student student0 = isu.AddStudent(group1, "Ivan");
            Group group2 = isu.AddGroup("G3312");
            Console.WriteLine(student0.Group.Name);
            isu.ChangeStudentGroup(student0, group2);
            Console.WriteLine(student0.Group.Name);
            Student findstudent = isu.FindStudent("Ivan");
            Console.WriteLine(findstudent.Name);
            findstudent = isu.GetStudent(3);
            Console.WriteLine(findstudent.Name);
            MegaFaculty f0 = 0;
            Console.WriteLine(f0);
            Course course = isu.AddCourse("laser tecnology", MegaFaculty.PhysicsAndEngineering);
            Flow flow = isu.AddFlowToCourse(course, "petushari", 2);
        }
    }
}
