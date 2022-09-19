using System.Collections.Generic;
using Isu.Models;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Group : Isu.Models.Group
    {
        public Group(string name, CourseNumber courseNumber)
        : base(name, courseNumber)
        {
            if (!TestGroupName(name))
            {
                throw new IsuExtraException("Wrong group name");
            }

            Name = name;
            Coursenumber = courseNumber;
            switch (name[0])
            {
                case 'M':
                    MegaFaculty = MegaFaculty.TranslationalInformationTechnologies;
                    break;
                case 'B':
                    MegaFaculty = MegaFaculty.BiotechnologyAndCryogenicSystems;
                    break;
                case 'P':
                    MegaFaculty = MegaFaculty.PhysicsAndEngineering;
                    break;
                default:
                    MegaFaculty = MegaFaculty.EntrepreneurshipAndInnovations;
                    break;
            }
        }

        public MegaFaculty MegaFaculty { get; private set; }
        public List<Lesson> Shedule { get; } = new List<Lesson>();

        public void AddStudent(Student student)
        {
            base.AddStudent(student);
        }

        public new Student FindStudentById(int id)
        {
            return base.FindStudentById(id) as Student;
        }

        public new Student FindStudentByName(string name)
        {
            return base.FindStudentByName(name) as Student;
        }

        public void DeleteStudent(Student student)
        {
            base.DeleteStudent(student);
        }

        public void AddLesson(Lesson lesson)
        {
            foreach (Lesson existinglesson in Shedule)
            {
                if (lesson.DayOfWeek == existinglesson.DayOfWeek
                    && ((existinglesson.Starttime <= lesson.Starttime
                         && existinglesson.Endtime >= lesson.Starttime)
                        || (existinglesson.Starttime <= lesson.Endtime
                            && existinglesson.Endtime >= lesson.Endtime)))
                {
                    throw new IsuExtraException("Group already have lesson in that time");
                }

                Shedule.Add(lesson);
            }
        }

        public List<Student> NonEnrolledStudents()
        {
            List<Student> nonenrolledstudents = new List<Student>();
            foreach (Student student in Students)
            {
                if (student.Flows.Count == 0)
                {
                    nonenrolledstudents.Add(student);
                }
            }

            return nonenrolledstudents;
        }

        private bool TestGroupName(string name)
        {
            if (name.Length != 5
                || (name[0] < 'A' || name[0] > 'Z')
                || (name[1] != '3' && name[1] != '4')
                || (name[2] <= '0' || name[2] > '4')
                || (name[3] < '0' || name[3] > '9')
                || (name[4] < '0' || name[4] > '9'))
            {
                return false;
            }

            return true;
        }
    }
}