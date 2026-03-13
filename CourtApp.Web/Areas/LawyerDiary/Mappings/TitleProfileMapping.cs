using AutoMapper;
using CourtApp.Application.DTOs.CaseTitle;
using CourtApp.Application.DTOs.CaseTitleRes;
using CourtApp.Application.DTOs.FSTitle;
using CourtApp.Application.Features.CaseTitle;
using CourtApp.Application.Features.FSTitle;
using CourtApp.Domain.Entities.CaseDetails;
using CourtApp.Web.Areas.LawyerDiary.Models.Title;
namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class TitleProfileMapping : Profile
    {
        public TitleProfileMapping()
        {
            CreateMap<FSTitleMViewModel, FSTitleCreateCommand>();
            CreateMap<FSTitleMViewModel, FSTitleUpdateCommand>();
            CreateMap<FSTitleByIdResponse, FSTitleMViewModel>();
            CreateMap<FSTitleResponse, FSTitleLViewModel>();

            CreateMap<TitleViewModel, CreateCaseTitleCommand>();
            CreateMap<ApplicantDetailViewModel, CaseApplicantDetail>();
            CreateMap<TitleViewModel, UpdateCaseTitleCommand>();
            CreateMap<CaseTitleResponse, TitleGetViewModel>();
            CreateMap<ApplicantDetailDto, ApplicantDetailViewModel>();
            CreateMap<CaseTitleByIdResponse, TitleViewModel>();
            CreateMap<CaseApplicantDetailEntity, ApplicantDetailDto>();


        }
    }
}
