using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI
{
    public static class Constants
    {
        public static int MinLegalWorkingAge { get => 18; }
        public static int MaxLegalWorkingAge { get => 70; }
        public static DateTime MinEmploymentDate { get => new DateTime(2000, 1, 1); }
    }
}
