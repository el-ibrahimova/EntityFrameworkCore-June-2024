using System.Globalization;
using EventMiWorkshopMVC.Services.Data.Interfaces;
using EventMiWorkshopMVC.Web.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;

namespace EventMiWorkshopMVC.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        // injection of eventService
        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddEventFormModel model, DateTime startDate, DateTime endDate)
        {
            if (!ModelState.IsValid)
            {
                return View(model); //Reload the same page with Model errors
            }

            await this.eventService.AddEvent(model, startDate, endDate);


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {

                EditEventFormModel eventModel = await this.eventService.GetEventById(id.Value);

                return View(eventModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, EditEventFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isStartDateValid = DateTime.TryParse(model.StartDate, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime startDate);

            bool isEndDateValid = DateTime.TryParse(model.EndDate, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime endDate);

            if (!isStartDateValid)
            {
                ModelState.AddModelError(nameof(model.StartDate), "Invalid Start Date Format!");
                return View(model);
            }

            if (!isEndDateValid)
            {
                ModelState.AddModelError(nameof(model.EndDate), "Invalid End Date Format!");
                return View(model);
            }

            try
            {
                await this.eventService.EditEventById(id.Value, model, startDate, endDate);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {

                EditEventFormModel eventModel = await this.eventService.GetEventById(id.Value);

                return View(eventModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task <IActionResult> Delete(int? id, EditEventFormModel model)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                await this.eventService.DeleteEventById(id.Value);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
