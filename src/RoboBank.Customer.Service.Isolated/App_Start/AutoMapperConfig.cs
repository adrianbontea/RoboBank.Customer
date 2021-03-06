﻿using System;
using AutoMapper;
using RoboBank.Customer.Application.DTOs;
using RoboBank.Customer.Domain;
using RoboBank.Customer.Service.Isolated.Custom;
using RoboBank.Customer.Service.Isolated.Models;

namespace RoboBank.Customer.Service.Isolated
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DynamicInfo, Dynamic>();
                cfg.CreateMap<ProfileInfo, Domain.Profile>();
                cfg.CreateMap<CustomerInfo, Domain.Customer>()
                    .Ignore(dest => dest.CanHaveWebsite)
                    .ForMember(dest => dest.Person, opt => opt.MapFrom(src => (PersonType)Enum.Parse(typeof(PersonType), src.Person)));

                cfg.CreateMap<Dynamic, DynamicInfo>();
                cfg.CreateMap<Domain.Profile, ProfileInfo>();
                cfg.CreateMap<Domain.Customer, CustomerInfo>()
                    .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person.ToString()));

                cfg.CreateMap<DynamicModel, DynamicInfo>()
                    .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties.ToGenericDictionary()));
                cfg.CreateMap<ProfileModel, ProfileInfo>()
                    .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties.ToGenericDictionary()));
                cfg.CreateMap<CustomerModel, CustomerInfo>()
                    .Ignore(dest => dest.CanHaveWebsite);

                cfg.CreateMap<DynamicInfo, DynamicModel>()
                    .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties.ToJsonNetDictionary()));
                cfg.CreateMap<ProfileInfo, ProfileModel>()
                    .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties.ToJsonNetDictionary()));
                cfg.CreateMap<CustomerInfo, CustomerModel>();
            });
        }
    }
}