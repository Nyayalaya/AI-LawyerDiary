using CourtApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Queries.Lawyer
{
    public class QueryGetAllLawyerEntity : IRequest<Result<List<ResponseGetAllLawyer>>>
    {
    }
}
