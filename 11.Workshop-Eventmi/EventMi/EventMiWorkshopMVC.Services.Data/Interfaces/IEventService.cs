﻿using EventMiWorkshopMVC.Web.ViewModels.Event;

namespace EventMiWorkshopMVC.Services.Data.Interfaces
{
    public interface IEventService
    {
        Task AddEvent(AddEventFormModel eventFormModel, DateTime startDate, DateTime endDate);

        Task<EditEventFormModel> GetEventById(int id);

        Task EditEventById(int id, EditEventFormModel eventFormModel, DateTime startDate, DateTime endDate);

        Task DeleteEventById(int id);
    }
}
