using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Queries.Lawyer
{

    public class HandlerQueryLawyerGetAll : IRequestHandler<QueryGetAllLawyerEntity, Result<List<ResponseGetAllLawyer>>>
    {
        //private readonly ILawyerCacheRepository _Repository;
        //private readonly IMapper _mapper;
        //public HandlerQueryLawyerGetAll(ILawyerCacheRepository _Repository, IMapper _mapper)
        //{
        //    this._mapper = _mapper;
        //    this._Repository = _Repository;
        //}
        //public async Task<Result<List<ResponseGetAllLawyer>>> Handle(QueryGetAllLawyerEntity request, CancellationToken cancellationToken)
        //{
        //    var bookTypeList = await _Repository.GetCachedListAsync();
        //    var mappedBookTye = _mapper.Map<List<ResponseGetAllLawyer>>(bookTypeList);
        //    return Result<List<ResponseGetAllLawyer>>.Success(mappedBookTye);
        //}
        public Task<Result<List<ResponseGetAllLawyer>>> Handle(QueryGetAllLawyerEntity request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
