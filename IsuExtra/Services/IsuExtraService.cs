using System;
using System.Collections.Generic;
using IsuExtra.Models;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService
    {
        public List<Group> Groups { get; private set; } = new List<Group>();
        public List<Course> Courses { get; private set; } = new List<Course>();
        public List<Teacher> Teachers { get; private set; } = new List<Teacher>();
        public List<Audience> Audiences { get; private set; } = new List<Audience>();
        public Group AddGroup(string name)
        {
            CourseNumber courseNumber = new CourseNumber(name);
            Group group = new Group(name, courseNumber);
            Groups.Add(group);

            return group;
        }

        public Audience AddAudience(int name)
        {
            var audience = new Audience(name);
            return audience;
        }

        public Teacher AddTeacher(string name)
        {
            var teacher = new Teacher(name);
            return teacher;
        }

        public Student AddStudent(Group group, string name)
        {
            if (!group.GroupIsFull())
            {
                var student = new Student(name, group);
                group.AddStudent(student);

                return student;
            }

            throw new IsuExtraException("Group is full");
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (!newGroup.GroupIsFull())
            {
                var oldgroup = student.Group as Group;
                oldgroup.DeleteStudent(student);
                newGroup.AddStudent(student);
                student.ChangeGroup(newGroup);
            }
            else
            {
                throw new IsuExtraException("Group is full");
            }
        }

        public Student FindStudent(string name)
        {
            foreach (Group group in Groups)
            {
                Student student = group.FindStudentByName(name);
                if (student != null)
                {
                    return student;
                }
            }

            return null;
        }

        public Student GetStudent(int id)
        {
            foreach (Group group in Groups)
            {
                Student student = group.FindStudentById(id);
                if (student != null)
                {
                    return student;
                }
            }

            throw new IsuExtraException("Student by this id:{id.Message} wasn't found");
        }

        public Course AddCourse(string name, MegaFaculty megafaculty)
        {
            var course = new Course(name, megafaculty);
            Courses.Add(course);
            return course;
        }

        public Flow AddFlowToCourse(Course course, string name, int maxstudents)
        {
            var flow = new Flow(name, course, maxstudents);
            course.AddFlow(flow);
            return flow;
        }

        public Lesson AddLessonToGroup(Teacher teacher, Audience audience, string starttime, string endtime, DayOfWeek dayOfWeek, Group group)
        {
            var lesson = new Lesson(teacher, audience, starttime, endtime, dayOfWeek);
            group.AddLesson(lesson);
            lesson.AddGroup(group);
            return lesson;
        }

        public Lesson AddLessonToFlow(Teacher teacher, Audience audience, string starttime, string endtime, DayOfWeek dayOfWeek, Flow flow)
        {
            var lesson = new Lesson(teacher, audience, starttime, endtime, dayOfWeek);
            flow.AddLesson(lesson);
            lesson.AddFlow(flow);
            return lesson;
        }

        public void AddStudentToCourse(Student student, Course course)
        {
            course.AddStudent(student);
        }

        public void DeleteStudentFromCourse(Student student, Course course)
        {
            course.DeleteStudents(student);
        }

        public List<Student> NonEnrolledStudentsByGroup(Group group)
        {
            return group.NonEnrolledStudents();
        }

        public List<Flow> FlowByTheCourse(Course course)
        {
            return course.Flows;
        }

        public List<Student> StudentByTheFlow(Flow flow)
        {
            return flow.Students;
        }
    }
}