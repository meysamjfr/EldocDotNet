using AutoMapper;
using NetTopologySuite.Geometries;
using Project.Application.DTOs.BilateralContractTemplate;
using Project.Application.DTOs.City;
using Project.Application.DTOs.FAQ;
using Project.Application.DTOs.FinancialContractTemplate;
using Project.Application.DTOs.Page;
using Project.Application.DTOs.Post;
using Project.Application.DTOs.PostCategory;
using Project.Application.DTOs.Province;
using Project.Application.DTOs.UnilateralContractTemplate;
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

            #region post
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.PostCategory, opt => opt.MapFrom(src => src.PostCategory == null ? "" : src.PostCategory.Title))
                .ReverseMap();

            CreateMap<UpsertPost, Post>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.PostCategory, opt => opt.MapFrom(src => src.PostCategory == null ? "" : src.PostCategory.Title));
            #endregion

            #region post category
            CreateMap<PostCategory, PostCategoryDTO>()
                .ForMember(dest => dest.TotalPosts, opt => opt.MapFrom(src => src.Posts == null ? 0 : src.Posts.Count))
                .ReverseMap();
            CreateMap<UpsertPostCategory, PostCategory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            #endregion

            #region unilateral contract template
            CreateMap<UnilateralContractTemplate, UnilateralContractTemplateDTO>().ReverseMap();

            CreateMap<UpsertUnilateralContractTemplate, UnilateralContractTemplate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            #endregion

            #region bilateral contract template
            CreateMap<BilateralContractTemplate, BilateralContractTemplateDTO>().ReverseMap();

            CreateMap<UpsertBilateralContractTemplate, BilateralContractTemplate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            #endregion

            #region financial contract template
            CreateMap<FinancialContractTemplate, FinancialContractTemplateDTO>().ReverseMap();

            CreateMap<UpsertFinancialContractTemplate, FinancialContractTemplate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            #endregion




        }
    }
}
