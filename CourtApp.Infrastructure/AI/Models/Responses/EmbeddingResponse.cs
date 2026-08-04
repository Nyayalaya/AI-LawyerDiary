using System.Collections.Generic;

namespace CourtApp.Infrastructure.AI.Models.Responses;


public sealed record EmbeddingResponse
{

    public IList<float> Embedding
    {
        get; init;
    } = new List<float>();


    public string Model
    {
        get; init;
    } = string.Empty;


    public string Provider
    {
        get;
        init;
    } = string.Empty;

}