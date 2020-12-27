namespace Hotel.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Data.Models.Enums;

    public class ConferenceHallSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ConferenceHalls.Any())
            {
                return;
            }

            await dbContext.ConferenceHalls.AddAsync(new ConferenceHall
            {
                Id = "conferenceid",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                EventType = (ConfHallEventType)Enum.Parse(typeof(ConfHallEventType), "Conference"),
                Description = "Conference",
                CurrentCapacity = 80,
                MaxCapacity = 100,
                Price = 50,
            });

            await dbContext.ConferenceHalls.AddAsync(new ConferenceHall
            {
                Id = "teambuildingid",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                EventType = (ConfHallEventType)Enum.Parse(typeof(ConfHallEventType), "TeamBuilding"),
                Description = "TeamBuilding",
                CurrentCapacity = 70,
                MaxCapacity = 100,
                Price = 60,
            });

            await dbContext.ConferenceHalls.AddAsync(new ConferenceHall
            {
                Id = "sportsid",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                EventType = (ConfHallEventType)Enum.Parse(typeof(ConfHallEventType), "Sports"),
                Description = "Sports",
                CurrentCapacity = 99,
                MaxCapacity = 100,
                Price = 80,
            });

            await dbContext.ConferenceHalls.AddAsync(new ConferenceHall
            {
                Id = "businessmeetingid",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                EventType = (ConfHallEventType)Enum.Parse(typeof(ConfHallEventType), "BusinessMeeting"),
                Description = "BusinessMeeting",
                CurrentCapacity = 35,
                MaxCapacity = 100,
                Price = 100,
            });
        }
    }
}
