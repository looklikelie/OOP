using System.Buffers.Text;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class CourseNumber : Isu.Models.CourseNumber
    {
        public CourseNumber(string name)
        {
            Coursenum = name[2];
        }
    }
}