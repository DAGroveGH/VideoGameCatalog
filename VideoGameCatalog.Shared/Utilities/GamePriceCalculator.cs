using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Shared.Utilities;

public static class GamePriceCalculator
{
    public static decimal GetGameValue(VideoGameModel game)
    {
        if (game == null) return 0;

        return game.Ownership?.ToLower() switch
        {
            "loose" => game.PriceLoose ?? 0,
            "cib" => game.PriceCIB ?? 0,
            "new" => game.PriceNew ?? 0,
            _ => game.YourPrice ?? game.PricePaid ?? 0
        };
    }
}