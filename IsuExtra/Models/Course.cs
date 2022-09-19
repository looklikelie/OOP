using System.Collections.Generic;
using System.Dynamic;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Course
    {
        public Course(string name, MegaFaculty megafaculty)
        {
            Name = name;
            Megafaculty = megafaculty;
        }

        public string Name { get; private set; }
        public MegaFaculty Megafaculty { get; private set; }
        public List<Flow> Flows { get; private set; } = new List<Flow>();

        public void AddFlow(Flow flow)
        {
            Flows.Add(flow);
        }

        public void AddStudent(Student student)
        {
            if (student.Flows.Count == 2)
            {
                throw new IsuExtraException("Student already have 2 OGNP courses");
            }

            if ((student.Group as Group).MegaFaculty == Megafaculty)
            {
                throw new IsuExtraException("OGNP course have the same mega-faculty");
            }

            foreach (Flow flow in Flows)
            {
                if (!flow.FlowIsFull())
                {
                    foreach (Lesson lesson in flow.Shedule)
                    {
                        foreach (Lesson lessonthatstudentalreadyhave in (student.Group as Group)?.Shedule)
                        {
                            if (lesson.DayOfWeek == lessonthatstudentalreadyhave.DayOfWeek
                                && ((lessonthatstudentalreadyhave.Starttime <= lesson.Starttime
                                     && lessonthatstudentalreadyhave.Endtime >= lesson.Starttime)
                                    || (lessonthatstudentalreadyhave.Starttime <= lesson.Endtime
                                        && lessonthatstudentalreadyhave.Endtime >= lesson.Endtime)))
                            {
                                throw new IsuExtraException("The student's schedule is superimposed on the flow schedule");
                            }
                        }
                    }

                    student.Flows.Add(flow);
                    flow.AddStudent(student);
                    return;
                }
            }

            throw new IsuExtraException("All flows in this course are filled");
        }

        public void DeleteStudents(Student studentthatwewantremove)
        {
            foreach (Flow flow in Flows)
            {
                foreach (Student student in flow.Students)
                {
                    if (student == studentthatwewantremove)
                    {
                        studentthatwewantremove.Flows.Remove(flow);
                        flow.RemoveStudent(studentthatwewantremove);
                        return;
                    }
                }
            }

            throw new IsuExtraException("Student was not enrolled in this course");
        }
    }
}