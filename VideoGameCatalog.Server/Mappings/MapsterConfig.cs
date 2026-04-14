using Mapster;
using VideoGameCatalog.Server.Models;
using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Server.Mappings;

public static class MapsterConfig
{
    public static void Register()
    {
        TypeAdapterConfig<CollectionItem, VideoGameModel>
            .NewConfig()
            .Map(dest => dest.Platform, src => src.Platform.Name)
            .Map(dest => dest.Publisher, src => src.Publisher != null ? src.Publisher.Name : null)
            .Map(dest => dest.Developer, src => src.Developer != null ? src.Developer.Name : null)
            .Map(dest => dest.Genre, src => src.Genre != null ? src.Genre.Name : null)
            .Map(dest => dest.CoverImageUrl, src => src.CoverImageUrl);

    }
}