using System;
using Isu.Tools;
using Microsoft.VisualBasic.CompilerServices;

namespace Isu.Models
{
    public class CourseNumber
    {
        public CourseNumber(string name)
        {
            if (name[2] < '1' || name[2] > '4') throw new IsuException("Invalid group name");
            if (name[0] != 'M') throw new IsuException("Invalid group name");
            if (name[1] != '3') throw new IsuException("Invalid group name");
            Coursenum = name[2];
        }

        protected CourseNumber()
        {
        }

        public int Coursenum { get; protected set; }
    }
}