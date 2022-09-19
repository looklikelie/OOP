using Isu.Models;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;
        private IsuService _isu = new IsuService();

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = null;
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            var testgroup = _isu.AddGroup("M3212");
            var teststudent = _isu.AddStudent(testgroup, "Вася Петров");
                
            Assert.AreEqual(teststudent.Group, testgroup);
            Assert.AreEqual(teststudent, _isu.FindStudent(teststudent.Name));
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var testgroup = _isu.AddGroup("M3105");
                for (int i = 0; i < 27; i++)
                {
                    var student = _isu.AddStudent(testgroup, i.ToString());
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var testgroup = _isu.AddGroup("M3902");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            var testgroup1 = _isu.AddGroup("M3122");
            var testgroup2 = _isu.AddGroup("M3120");
            var teststudent = _isu.AddStudent(testgroup1, "Vasiliy Petrov");
            _isu.ChangeStudentGroup(teststudent, testgroup2);
            
            Assert.AreNotSame(testgroup1.Name, teststudent.Group.Name);
        }
    }
}