using AutoMapper;
using NetTopologySuite.Geometries;
using Project.Application.DTOs.City;
using Project.Application.DTOs.FAQ;
using Project.Application.DTOs.Page;
using Project.Application.DTOs.Province;
using Project.Application.DTOs.User;
using Project.Domain.Entities;

namespace Project.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile(GeometryFactory geometryFactory)
        {
            #region city
            CreateMap<City, CityDTO>()
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province.Name))
                .ReverseMap();
            CreateMap<CreateCity, City>().ReverseMap();
            CreateMap<EditCity, City>().ReverseMap();
            #endregion

            #region province
            CreateMap<Province, ProvinceDTO>().ReverseMap();
            CreateMap<CreateProvince, Province>().ReverseMap();
            CreateMap<EditProvince, Province>().ReverseMap();
            #endregion

            #region user
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, EditUserProfile>().ReverseMap();
            #endregion

            #region page
            CreateMap<Page, PageDTO>().ReverseMap();
            CreateMap<CreatePage, Page>();
            CreateMap<EditPage, Page>();
            #endregion

            #region faq
            CreateMap<FAQ, FAQDTO>().ReverseMap();

            CreateMap<UpsertFAQ, FAQ>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            #endregion
        }

    }
}
