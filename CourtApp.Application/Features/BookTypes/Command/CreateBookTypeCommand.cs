using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;
using System;

namespace CourtApp.Application.Features.BookTypes.Command
{
    public partial class CreateBookTypeCommand : IRequest<Result<Guid>>
    {
        public string Name_En { get; set; }
    }

    public class CreateBooTypeCommandHandler : IRequestHandler<CreateBookTypeCommand, Result<Guid>>
    {
        private readonly IBookTypeRepository _bookTypeRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateBooTypeCommandHandler(IBookTypeRepository _bookTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._bookTypeRepository = _bookTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateBookTypeCommand request, CancellationToken cancellationToken)
        {
            var bookType = _mapper.Map<BookTypeEntity>(request);
            await _bookTypeRepository.InsertAsync(bookType);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(bookType.Id);
        }
    }
}