using System;

namespace RDtask2
{
    public class Student : IComparable<Student>
    {
        public string Name { get; set; }

        public string NameOfTest { get; set; }

        public DateTime DateOfPassing { get; set; }

        public int Mark { get; set; }

        public int CompareTo(Student other)
        {
            return Mark.CompareTo(other.Mark);
        }

        public override string ToString() => $"{Name} - {NameOfTest} - {DateOfPassing} - {Mark}";

    }
}
