using AutoMapper;
using InvoiceTool.Application.Models;
using InvoiceTool.Domain.Entities;

namespace InvoiceTool.Application.Mapper
{
    public class AutoMapperConfiguration
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<Invoice, InvoiceModel>()
                    .ForMember(dest => dest.InvoiceLines, opt => opt.MapFrom(src => src.InvoiceLines));

                c.CreateMap<InvoiceModel, Invoice>()
                    .ForMember(dest => dest.InvoiceLines, opt => opt.MapFrom(src => src.InvoiceLines));

                c.CreateMap<InvoiceLine, InvoiceLineModel>();
                c.CreateMap<InvoiceLineModel, InvoiceLine>();

                c.CreateMap<Customer, CustomerModel>();
                c.CreateMap<CustomerModel, Customer>();
            });

            return new AutoMapper.Mapper(config);
        }
    }
}
