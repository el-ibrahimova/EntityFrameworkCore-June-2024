using System.Globalization;
using EventMiWorkshopMVC.Data;
using EventMiWorkshopMVC.Data.Models;
using EventMiWorkshopMVC.Services.Data.Interfaces;
using EventMiWorkshopMVC.Web.ViewModels.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventMiWorkshopMVC.Services.Data
{
    public class EventService : IEventService
    {
        private readonly EventMiDbContext dbContext;

        public EventService(EventMiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddEvent(AddEventFormModel eventFormModel, DateTime startDate, DateTime endDate)
        {
            Event newEvent = new Event()
            {
                Name = eventFormModel.Name,
                StartDate = startDate,
                EndDate = endDate,
                Place = eventFormModel.Place
            };

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EditEventFormModel> GetEventById(int id)
        {
            Event? eventDb = this.dbContext
                .Events
                .FirstOrDefault(e => e.Id == id);

            if (eventDb == null)
            {
                throw new ArgumentException();
            }

            if (!eventDb.IsActive!.Value)
            {
                throw new InvalidOperationException();
            }

            EditEventFormModel eventFound = new EditEventFormModel()
                {
                    Name = eventDb.Name,
                    StartDate = eventDb.StartDate.ToString("G"),
                    EndDate = eventDb.EndDate.ToString("G"),
                    Place = eventDb.Place
                };

            return eventFound;
        }

        public async Task EditEventById(int id, EditEventFormModel eventFormModel, DateTime startDate, DateTime endDate)
        {
            Event eventToEdit = await dbContext
                .Events
            .FirstAsync(e => e.Id == id);

            if (!eventToEdit.IsActive!.Value)
            {
                throw new InvalidOperationException();
            }

            eventToEdit.Name = eventFormModel.Name;
            eventToEdit.StartDate= startDate;
            eventToEdit.EndDate = endDate;
            eventToEdit.Place = eventFormModel.Place;

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteEventById(int id)
        {
            Event? eventToDelete = await this.dbContext
                .Events
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventToDelete == null)
            {
                throw new ArgumentNullException();
            }

            if (!eventToDelete.IsActive!.Value)
            {
                throw new InvalidOperationException();
            }

            this.dbContext.Events.Remove(eventToDelete);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
