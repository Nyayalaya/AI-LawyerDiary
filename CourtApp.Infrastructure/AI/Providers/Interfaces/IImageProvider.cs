using CourtApp.Infrastructure.AI.Models.Requests;
using CourtApp.Infrastructure.AI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.AI.Providers.Interfaces
{
    public interface IImageProvider : IAIProvider
    {
        Task<ImageResponse> GenerateImageAsync(
            ImageRequest request,
            CancellationToken cancellationToken = default);
    }
}
