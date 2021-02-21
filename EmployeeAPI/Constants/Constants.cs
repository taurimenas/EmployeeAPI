using System;

namespace EmployeeAPI
{
    public static class Constants
    {
        public static int MinLegalWorkingAge { get => 18; }
        public static int MaxLegalWorkingAge { get => 70; }
        public static DateTime MinEmploymentDate { get => new DateTime(2000, 1, 1); }
    }
}
