﻿namespace Common.Dto.Employee;

public class EmployeeInfoDto
{
        public required Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime BirthDate { get; set; }

    }
