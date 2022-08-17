using AutoMapper;
using NetTopologySuite.Geometries;
using Project.Application.DTOs.BilateralContractTemplate;
using Project.Application.DTOs.ChatWithExpert;
using Project.Application.DTOs.ChatWithExpertMessage;
using Project.Application.DTOs.ChatWithExpertRequest;
using Project.Application.DTOs.City;
using Project.Application.DTOs.Expert;
using Project.Application.DTOs.FAQ;
using Project.Application.DTOs.FinancialContractTemplate;
using Project.Application.DTOs.Page;
using Project.Application.DTOs.Payment;
using Project.Application.DTOs.Post;
using Project.Application.DTOs.PostCategory;
using Project.Application.DTOs.Province;
using Project.Application.DTOs.Transaction;
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

            #region experts
            CreateMap<Expert, ExpertDTO>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Expert, ExpertCompact>();

            CreateMap<Expert, UpsertExpert>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region payments
            CreateMap<AddPayment, Payment>()
                .ForMember(dest => dest.IsCompleted, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionCode, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentType, opt => opt.Ignore())
                .ForMember(dest => dest.TypeActionId, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region transactions
            CreateMap<Transaction, TransactionDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User == null ? "" : src.User.Nickname))
                .ReverseMap();
            #endregion

            #region chat with expert request
            CreateMap<ChatWithExpertRequest, ChatWithExpertRequestDTO>()
                .ForMember(dest => dest.Expert, opt => opt.MapFrom(src => src.Expert == null ? "" : src.Expert.Name))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User == null ? "" : src.User.Nickname))
                .ReverseMap();
            #endregion

            #region chat with expert 
            CreateMap<ChatWithExpert, ChatWithExpertDTO>()
                .ForMember(dest => dest.Expert, opt => opt.MapFrom(src => src.Expert == null ? "" : src.Expert.Name))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User == null ? "" : src.User.Nickname))
                .ForMember(dest => dest.TotalMessages, opt => opt.MapFrom(src => src.Messages == null ? 0 : src.Messages.Count))
                .ReverseMap();
            #endregion

            #region chat with expert messages
            CreateMap<ChatWithExpertMessage, ChatWithExpertMessageDTO>()
                .ReverseMap();
            #endregion

        }
    }
}
