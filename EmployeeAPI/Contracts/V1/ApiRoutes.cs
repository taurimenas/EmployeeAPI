namespace EmployeeAPI.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Employees
        {
            public const string GetAll = Base + "/employees";
            public const string GetAllByBossId = Base + "/employees/bossId={bossId}";
            public const string GetCountAndAverageSalary = Base + "/employees/role={role}";
            public const string GetAllByNameAndBirthInterval = Base + "/employees/firstName={firstName}/intervalStart={intervalStart}/intervalEnd={intervalEnd}"; //TODO: Change
            public const string Update = Base + "/employees/{employeeId}";
            public const string UpdateSalary = Base + "/employees/employeeId={employeeId}/salary={salary}";
            public const string Delete = Base + "/employees/{employeeId}";
            public const string Get = Base + "/employees/{employeeId}";
            public const string Create = Base + "/employees";
        }
    }

}
