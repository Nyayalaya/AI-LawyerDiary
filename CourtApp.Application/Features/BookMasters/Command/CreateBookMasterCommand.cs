using CourtApp.Application.Common;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.BookMasters.Command
{
    public class CreateBookMasterCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string PublisherName { get; set; }
        public Guid BookTypeId { get; set; }
        public Guid PublisherId { get; set; }

    }
    public class CreateBookMasterCommandHandler : IRequestHandler<CreateBookMasterCommand, Result<Guid>>
    {
        private readonly IBookMasterRepository _Repository;
        private readonly IBookTypeRepository _bookTypeRepo;
        private readonly IPublicationRepository _publisherRepo;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateBookMasterCommandHandler(IBookMasterRepository _Repository, IUnitOfWork unitOfWork,
            IMapper mapper, IBookTypeRepository bookTypeRepo, IPublicationRepository publisherRepo)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bookTypeRepo = bookTypeRepo;
            _publisherRepo = publisherRepo;
        }

        public async Task<Result<Guid>> Handle(CreateBookMasterCommand request, CancellationToken cancellationToken)
        {
            var bookType = _mapper.Map<LDBookEntity>(request);
            bookType.Publisher = _publisherRepo.GetByIdAsync(request.PublisherId).Result;
            bookType.BookType = _bookTypeRepo.GetByIdAsync(request.BookTypeId).Result;
            await _Repository.InsertAsync(bookType);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(bookType.Id);
        }
    }
}
