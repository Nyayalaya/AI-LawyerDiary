using AutoMapper;
using CourtApp.Application.Features.CaseDetails.Dtos;
using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Linq;

namespace CourtApp.Application.Features.CaseDetails.Extensions
{
    public static class CaseMappingExtensions
    {
        public static IMappingExpression<CaseEntity, TDestination>
            MapCaseBasicInfo<TDestination>(
                this IMappingExpression<CaseEntity, TDestination> map)
            where TDestination : CaseBasicInfoDto
        {
            return map

                .ForMember(d => d.Id,
                    o => o.MapFrom(s => s.Id))

                .ForMember(d => d.InstitutionDate,
                    o => o.MapFrom(s => s.InstitutionDate.ToString("dd/MM/yyyy")))

                .ForMember(d => d.CaseTitle,
                    o => o.MapFrom(s =>
                        s.CaseFirstTitle + " VS " + s.CaseSecondTitle))

                .ForMember(d => d.CaseNumberYear,
                    o => o.MapFrom(s =>
                        !string.IsNullOrWhiteSpace(s.CaseNo)
                            ? s.CaseNo + "/" + s.CaseYear
                            : s.CaseYear.ToString()))

                .ForMember(d => d.CaseNumber,
                    o => o.MapFrom(s => s.CaseNo))

                .ForMember(d => d.CaseYear,
                    o => o.MapFrom(s => s.CaseYear))

                .ForMember(d => d.Court,
                    o => o.MapFrom(s => s.Court != null ? s.Court.Name : ""))

                .ForMember(d => d.CaseType,
                    o => o.MapFrom(s => s.CaseCategory.Name))

                .ForMember(d => d.Stage,
                    o => o.MapFrom(s =>
                        s.CaseStage != null ? s.CaseStage.Name : ""))

                .ForMember(d => d.Status,
                    o => o.MapFrom(s =>
                        s.IsDisposed ? "Disposed" : "Pending"))

                .ForMember(d => d.NextDate,
                    o => o.MapFrom(s =>
                        s.NextDate ??
                        s.CaseProceedingEntities
                            .OrderByDescending(p => p.NextDate)
                            .Select(p => p.NextDate)
                            .FirstOrDefault()))

                .ForMember(d => d.ParentCaseId,
                    o => o.MapFrom(s => s.ParentCaseId))

                .ForMember(d => d.HasChildCases,
                    o => o.MapFrom(s =>
                        s.ChildCases.Any(c => !c.IsDeleted)))

                .ForMember(d => d.CourtDistrict,
                    o => o.MapFrom(s =>
                        s.CourtDistrict != null ? s.CourtDistrict.Name : ""))

                .ForMember(d => d.CourtComplex,
                    o => o.MapFrom(s =>
                        s.CourtComplex != null ? s.CourtComplex.Name : ""))

                .ForMember(d => d.IsImportant,
                    o => o.MapFrom(s => s.IsImportant))

                .ForMember(d => d.IsUrgent,
                    o => o.MapFrom(s => s.IsUrgent))

                .ForMember(d => d.IsDisposed,
                    o => o.MapFrom(s => s.IsDisposed));
        }
    }
}