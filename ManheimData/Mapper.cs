using AutoMapper;
using ManheimData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManheimData
{
    public static class Mapper
    {
        public static IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SalesResponse, EventModel>()
                 .ForMember(dest => dest.Id, opts => opts.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.ExternalIds, opts => opts.MapFrom(src => src.uniqueIds))
                .ForMember(dest => dest.SaleDate, opts => opts.MapFrom(src => src.saleDate))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.auctionName.ToUpper()))
                .ForMember(dest => dest.SaleYear, opts => opts.MapFrom(src => src.saleYear))
                .ForMember(dest => dest.TimeStamp,
                    opt => opt.MapFrom(src => DateUtils.StringToUnixTime(src.saleDateTime)
                    ))
                .ForMember(dest => dest.ZipCode, opts => opts.MapFrom(src => "000"))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => "auction"))
                .ForMember(dest => dest.HasVehicles, opts => opts.MapFrom(src => 0)); // When vehicles are inserted, HasVehicles could change to true

            });

            return configuration.CreateMapper();
        }

    }
}
