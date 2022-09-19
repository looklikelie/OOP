using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        public IsuService()
        {
            Groups = new List<Group>();
        }

        private List<Group> Groups { get; set; }

        public Group AddGroup(string name)
        {
            CourseNumber courseNumber = new CourseNumber(name);
            Group group = new Group(name, courseNumber);
            Groups.Add(group);

            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (!group.GroupIsFull())
            {
                Student student = new Student(name, group);
                group.AddStudent(student);

                return student;
            }

            throw new IsuException("Group is full");
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

            throw new IsuException("Student by this id:{e.Message} wasn't found");
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

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group group in Groups)
            {
                if (group.Name == groupName)
                {
                    return group.Students;
                }
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student> students = null;
            foreach (Group group in Groups)
            {
                if (group.Coursenumber == courseNumber)
                {
                    foreach (Student student in group.Students)
                    {
                        students.Add(student);
                    }
                }
            }

            return students;
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group group in Groups)
            {
                if (group.Name == groupName)
                {
                    return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> groups = null;
            foreach (Group group in Groups)
            {
                if (group.Coursenumber == courseNumber)
                {
                    groups.Add(group);
                }
            }

            return groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (!newGroup.GroupIsFull())
            {
                Group oldgroup = student.Group;
                oldgroup.DeleteStudent(student);
                newGroup.AddStudent(student);
                student.ChangeGroup(newGroup);
            }
            else
            {
                throw new IsuException("Group is full");
            }
        }
    }
}