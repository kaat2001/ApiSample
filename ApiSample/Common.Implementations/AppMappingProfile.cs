using AutoMapper;
using Common.Dto.Employees;
using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Implementations;

public class AppMappingProfile :Profile
{
    public AppMappingProfile()
    {
        CreateMap<Employee, EmployeeShortInfoDto>();
        CreateMap<Employee, EmployeeInfoDto>().ReverseMap();
    }
}
