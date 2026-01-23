using VideoGameCatalog.Shared.Models;

namespace VideoGameCatalog.Server.Data
{
    public class MockVideoGameData
    {
        public static List<VideoGameModel> Games => new()
        {
            new VideoGameModel
                {
                    Id = 1,
                    Platform = "SNES",
                    Category = "Physical",
                    UserRecordType = "Owned",
                    Title = "Chrono Trigger",
                    Country = "JP",
                    ReleaseType = "Original",
                    Publisher = "Square",
                    Developer = "Square",
                    Genre = "RPG",
                    CreatedAt = "1995, 3, 11",
                    Ownership = "Owned",
                    PriceLoose = "49.99",
                    PriceCIB = "129.99",
                    PriceNew = null,
                    YourPrice = "129.99",
                    PricePaid = "120.00",
                    ItemCondition = "Excellent",
                    BoxCondition = "Good",
                    ManualCondition = "Fair",
                    Notes = "Cart in excellent shape; box has corner wear.",
                    Tags =  "rpg, classic, time-travel",
                    Metacritic = "98"
                },

            new VideoGameModel
            {
                Id = 2,
                Platform = "PlayStation 4",
                Category = "Digital",
                UserRecordType = "Wishlist",
                Title = "The Last of Us Part II",
                Country = "US",
                ReleaseType = "Standard",
                Publisher = "Sony",
                Developer = "Naughty Dog",
                Genre = "Action-Adventure",
                CreatedAt = "2020, 6, 19",
                Ownership = "Wishlist",
                PriceLoose = null,
                PriceCIB = null,
                PriceNew = "59.99",
                YourPrice = null,
                PricePaid = null,
                ItemCondition = null,
                BoxCondition = null,
                ManualCondition = null,
                Notes = "Digital standard edition on wishlist.",
                Tags = "story, singleplayer, dramatic",
                Metacritic = "93"
            },

            new VideoGameModel
            {
                Id = 3,
                Platform = "Nintendo Switch",
                Category = "Physical",
                UserRecordType = "For Trade",
                Title = "The Legend of Zelda: Breath of the Wild",
                Country = "US",
                ReleaseType = "Original",
                Publisher = "Nintendo",
                Developer = "Nintendo EPD",
                Genre = "Action-Adventure",
                CreatedAt = "2017, 3, 3",
                Ownership = "For Trade",
                PriceLoose = "34.99",
                PriceCIB = "49.99",
                PriceNew = "59.99",
                YourPrice = "40.00",
                PricePaid = "35.00",
                ItemCondition = "Very Good",
                BoxCondition = "Very Good",
                ManualCondition = "Very Good",
                Notes = "Includes map.",
                Tags = "open-world, adventure, zelda",
                Metacritic = "97"
            },

            new VideoGameModel
            {
                Id = 4,
                Platform = "PC",
                Category = "Digital",
                UserRecordType = "Owned",
                Title = "Hades",
                Country = "Global",
                ReleaseType = "Indie",
                Publisher = "Supergiant Games",
                Developer = "Supergiant Games",
                Genre = "Roguelike",
                CreatedAt = "2020, 9, 17",
                Ownership = "Owned",
                PriceLoose = null,
                PriceCIB = null,
                PriceNew = "24.99",
                YourPrice = "9.99",
                PricePaid = "9.99",
                ItemCondition = null,
                BoxCondition = null,
                ManualCondition = null,
                Notes = "Steam key; highly replayable.",
                Tags = "roguelike, indie, replayable" ,
                Metacritic = "93"
            },

            new VideoGameModel
            {
                Id = 5,
                Platform = "Game Boy",
                Category = "Physical",
                UserRecordType = "Owned",
                Title = "Pokémon Red",
                Country = "US",
                ReleaseType = "First Print",
                Publisher = "Nintendo",
                Developer = "Game Freak",
                Genre = "RPG",
                CreatedAt = "1998, 10, 21",
                Ownership = "Owned",
                PriceLoose = "79.99",
                PriceCIB = "199.99",
                PriceNew = null,
                YourPrice = "150.00",
                PricePaid = "80.00",
                ItemCondition = "Good",
                BoxCondition = "Fair",
                ManualCondition = "Good",
                Notes = "Manual has a small stain; collectible.",
                Tags = "pokemon, collectible, handheld" ,
                Metacritic = null
            }
        };
    }
}
