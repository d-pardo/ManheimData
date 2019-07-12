using AutoMapper;
using ManheimData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManheimData
{
    public static class Mapper
    {
        public static IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg
                .CreateMap<SalesResponse, EventModel>()
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
                cfg
                .CreateMap<ListingsResponse, VehicleModel>()
                    .ForMember(dest => dest.Vin, opts => opts.MapFrom(src => src.VehicleInformation.vin))
                    .ForMember(dest => dest.Year, opts => opts.MapFrom(src => src.VehicleInformation.year))
                    .ForMember(dest => dest.Color, opts => opts.MapFrom(src => src.VehicleInformation.exteriorColor))
                    .ForMember(dest => dest.BodyStyle, opts => opts.MapFrom(src => src.VehicleInformation.bodyStyle))
                    .ForMember(dest => dest.EngineType, opts => opts.MapFrom(src => src.VehicleInformation.engine))
                    .ForMember(dest => dest.Miles, opts => opts.MapFrom(src => src.VehicleInformation.mileage))
                    .ForMember(dest => dest.Model, opts => opts.MapFrom(src => src.VehicleInformation.model))
                    .ForMember(dest => dest.Make, opts => opts.MapFrom(src => src.VehicleInformation.make))
                    .ForMember(dest => dest.LaneNumber, opts => opts.MapFrom(src => src.SaleInformation.laneNumber))
                    .ForMember(dest => dest.RunNumber, opts => opts.MapFrom(src => src.SaleInformation.runNumber))
                    .ForMember(dest => dest.ExternalId, opts => opts.MapFrom(src => src.SaleInformation.uniqueId))
                    .ForMember(dest => dest.BasePrice, opts => opts.MapFrom(src => src.VehicleInformation.adjMmr))
                    .ForMember(dest => dest.UpdateTime,
                        opts => opts.MapFrom(src => DateTime.ParseExact(src.VehicleInformation.updateTimestamp, "yyyyMMddHHmmss", null))
                     )
                    .ForMember(dest => dest.ImageUri, opts => opts.MapFrom(src => src.VehicleInformation.images.Select(image => image.largeUrl)));

            });

            return configuration.CreateMapper();
        }

    }
}
