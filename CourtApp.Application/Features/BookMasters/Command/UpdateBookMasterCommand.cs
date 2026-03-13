using CourtApp.Application.Common;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.BookMasters.Command
{
    public class UpdateBookMasterCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string PublisherName { get; set; }
        public Guid BookTypeId { get; set; }
        public Guid PublisherId { get; set; }
    }
    public class UpdateBookMasterCommandHandler : IRequestHandler<UpdateBookMasterCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookMasterRepository _Repository;
        private readonly IBookTypeRepository _bookTypeRepo;
        private readonly IPublicationRepository _publisherRepo;

        public UpdateBookMasterCommandHandler(IBookMasterRepository _Repository,
            IUnitOfWork unitOfWork, IBookTypeRepository _bookTypeRepo, IPublicationRepository _publisherRepo)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
            this._bookTypeRepo = _bookTypeRepo;
            this._publisherRepo = _publisherRepo;
        }

        public async Task<Result<Guid>> Handle(UpdateBookMasterCommand command, CancellationToken cancellationToken)
        {
            var book = await _Repository.GetByIdAsync(command.Id);
            if (book == null)
                return Result<Guid>.Fail($"Book not found.");
            else
            {
                book.Publisher = _publisherRepo.GetByIdAsync(command.PublisherId).Result;
                book.BookType = _bookTypeRepo.GetByIdAsync(command.BookTypeId).Result;
                book.Year = command.Year;
                await _Repository.UpdateAsync(book);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(book.Id);
            }
        }
    }
}
