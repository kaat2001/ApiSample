﻿namespace ApiSample.Queries;

public class EmployeeGetQuery
{
    int Page { get; set; } = 1;
    int Limit { get; set; } = 10;
}
