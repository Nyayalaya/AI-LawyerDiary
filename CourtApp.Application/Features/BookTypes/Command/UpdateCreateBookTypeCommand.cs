using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Features.BookTypes.Command
{
    public class UpdateCreateBookTypeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string BookType { get; set; }
    }
    public class UpdateBookTypeCommandHandler : IRequestHandler<UpdateCreateBookTypeCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookTypeRepository _bookTypeRepository;

        public UpdateBookTypeCommandHandler(IBookTypeRepository _bookTypeRepository, IUnitOfWork unitOfWork)
        {
            this._bookTypeRepository = _bookTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateCreateBookTypeCommand command, CancellationToken cancellationToken)
        {
            var bookType = await _bookTypeRepository.GetByIdAsync(command.Id);

            if (bookType == null)
            {
                return Result<Guid>.Fail($"Book Type Not Found.");
            }
            else
            {
                bookType.Name_En = command.BookType ?? bookType.Name_En;
                await _bookTypeRepository.UpdateAsync(bookType);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(bookType.Id);
            }
        }
    }
}