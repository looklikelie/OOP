using System;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Lesson
    {
        public Lesson(Teacher teacher, Audience audience, string starttime, string endtime, DayOfWeek dayOfWeek)
        {
            Teacher = teacher;
            Audience = audience;
            DayOfWeek = dayOfWeek;
            if ((starttime[0] < '0' || starttime[0] > '2')
            || (starttime[1] < '0' || starttime[1] > '9')
            || (starttime[2] != ':')
            || (starttime[3] < '0' || starttime[3] > '5')
            || (starttime[4] < '0' || starttime[4] > '9'))
            {
                throw new IsuExtraException("Wrong starttime format");
            }

            if ((endtime[0] < '0' || endtime[0] > '2')
                || (endtime[1] < '0' || endtime[1] > '9')
                || (endtime[2] != ':')
                || (endtime[3] < '0' || endtime[3] > '5')
                || (endtime[4] < '0' || endtime[4] > '9'))
            {
                throw new IsuExtraException("Wrong endtime format");
            }

            Starttime = Convert.ToDateTime(starttime);
            Endtime = Convert.ToDateTime(endtime);
            if (Starttime > Endtime)
            {
                throw new IsuExtraException("starttime is later then endtime");
            }
        }

        public Teacher Teacher { get; private set; }
        public Audience Audience { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }
        public DateTime Starttime { get; }
        public DateTime Endtime { get; }
        public Group Group { get; private set; }
        public Flow Flow { get; private set; }

        public void AddGroup(Group group)
        {
            Group = group;
        }

        public void AddFlow(Flow flow)
        {
            Flow = flow;
        }
    }
}