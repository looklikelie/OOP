using System;
using System.Collections.Generic;
using System.Dynamic;
using Isu.Models;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Flow
    {
        public Flow(string name, Course course, int maxstudents)
        {
            Name = name;
            Maxstudents = maxstudents;
            Course = course;
        }

        public string Name { get; private set; }
        public List<Student> Students { get; private set; } = new List<Student>();
        public Course Course { get; private set; }
        public int Maxstudents { get; private set; }
        public List<Lesson> Shedule { get; } = new List<Lesson>();

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public bool FlowIsFull()
        {
            if (Students.Count == Maxstudents)
            {
                return true;
            }

            return false;
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
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
    }
}