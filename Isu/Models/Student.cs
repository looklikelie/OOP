namespace Isu.Models
{
    public class Student
    {
        private string _name;
        private Group _group;
        private int _id;

        public Student(string name, Group group)
        {
            _name = name;
            _group = group;
            _id = NewId;
            NewId++;
        }

        public static int NewId { get; private set; } = 1;

        public int Id
        {
            get => _id;
        }

        public string Name
        {
            get => _name;
        }

        public Group Group
        {
            get => _group;
        }

        public void ChangeGroup(Group newgroup)
        {
            _group = newgroup;
        }
    }
}