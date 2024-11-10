using AutoMapper;
using Common.Dto.Employees;
using Common.Interfaces.Dto;
using Common.Interfaces.Dto.Banks;
using Common.Interfaces.Dto.Invoices;
using DataModel.Entities;

namespace Common.Implementations;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Employee, EmployeeShortInfoDto>();
        CreateMap<Employee, EmployeeInfoDto>().ReverseMap();

        CreateMap<Customer, CustomerDto>();
        CreateMap<BankInfo, BankInfoDto>();
        //TODO!
        CreateMap<BankCorrespondent, BankCorrespondentDto>(); 
        CreateMap<Invoice, InvoiceDto>().ReverseMap();
        CreateMap<InvoicePosition, InvoicePositionDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ReverseMap();

    }
}
