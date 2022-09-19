using System;
using System.Collections.Generic;
using IsuExtra.Models;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTests
    {
        IsuExtraService isu = new IsuExtraService();
            
        [Test]
        public void NewCourseOGNPAddStudentOnCourseDeleteStudentsFromCourse()
        {
            Course course1 = isu.AddCourse("OOP",MegaFaculty.TranslationalInformationTechnologies);
            Flow flow1 = isu.AddFlowToCourse(course1,"oop1",2);
            Group group1 = isu.AddGroup("J3261");
            Teacher teacher1 = isu.AddTeacher("Mayatin");
            Audience audience1 = isu.AddAudience(331);
            isu.AddLessonToFlow(teacher1, audience1, "11:40", "13:10", DayOfWeek.Thursday, flow1);
            isu.AddLessonToGroup(teacher1, audience1, "11:40", "13:10", DayOfWeek.Friday, group1);
            Student student1 = isu.AddStudent(group1, "Ivan");
            isu.AddStudentToCourse(student1, course1);
            if(student1.Flows.Count == 1) Assert.Pass();
            isu.DeleteStudentFromCourse(student1, course1);
            if(student1.Flows.Count == 0) Assert.Pass();
            isu.AddLessonToGroup(teacher1, audience1, "11:40", "13:10", DayOfWeek.Thursday, group1);
            Assert.Catch<IsuExtraException>(()=>
            {
                isu.AddStudentToCourse(student1, course1);
            });
        }

        [Test]
        public void GetListOfStudentsListOfFlowsListOfNonEnrolledStudents()
        {
            Course course1 = isu.AddCourse("OOP",MegaFaculty.TranslationalInformationTechnologies);
            Flow flow1 = isu.AddFlowToCourse(course1,"oop1",2);
            Flow flow2 = isu.AddFlowToCourse(course1,"oop2",2);
            Group group1 = isu.AddGroup("J3261");
            Teacher teacher1 = isu.AddTeacher("Mayatin");
            Audience audience1 = isu.AddAudience(331);
            isu.AddLessonToFlow(teacher1, audience1, "11:40", "13:10", DayOfWeek.Thursday, flow1);
            isu.AddLessonToGroup(teacher1, audience1, "11:40", "13:10", DayOfWeek.Friday, group1);
            Student student1 = isu.AddStudent(group1, "Ivan");
            Student student2 = isu.AddStudent(group1, "Dmitriy");
            isu.AddStudentToCourse(student1, course1);
            isu.AddStudentToCourse(student2, course1);
            List<Flow> flows = isu.FlowByTheCourse(course1);
            List<Student> students = isu.StudentByTheFlow(flow1);
            Student student3 = isu.AddStudent(group1, "Katya");
            Student student4 = isu.AddStudent(group1, "Igor");
            List<Student> nonenrolledstudents = isu.NonEnrolledStudentsByGroup(group1);
            
            Assert.AreEqual(flows, course1.Flows);
            Assert.AreEqual(students, flow1.Students);
            if(nonenrolledstudents.Count == 2) Assert.Pass();

        }
    }
}