using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventApp.Data.Entities;
using EventApp.Data.Infrastructure;
using EventApp.Services.GuestService.GuestDtos;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace EventApp.Services.GuestService
{
    public class GuestService : IGuestService
    {
        private readonly IUnitOfWork unitOfWork;
        private IRepository<Guest> guestRepo;
        private IRepository<EventGuest> eventGuestRepo;

        public GuestService(IRepository<Guest> guestRepo, IRepository<EventGuest> eventGuestRepo, IUnitOfWork unitOfWork)
        {
            this.guestRepo = guestRepo;
            this.eventGuestRepo = eventGuestRepo;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GuestDTO> GetAllMajorGuestsForAnEvent(int eventId)
        {
            var guests = guestRepo.Query().Where(x => x.Age > 18).ToList();
            return guests.Select(g => new GuestDTO().InjectFrom(g) as GuestDTO);
        }

        public IEnumerable<GuestDTO> GetGuestsForAnEvent(int eventId)
        {
            var guests = eventGuestRepo.Query().Include(x => x.Event).Include(x => x.Guest).Where(x => x.Event.Id == eventId).Select(x => x.Guest).ToList();
            return guests.Select(g => new GuestDTO().InjectFrom(g) as GuestDTO);
        }

        public IEnumerable<GuestDTO> GetGuestsThatWillAttendAnEvent(int eventId)
        {
            var guests = eventGuestRepo.Query().Where(x => x.HasAttended).ToList();
            return guests.Select(g => new GuestDTO().InjectFrom(g) as GuestDTO);
        }

        public IEnumerable<GuestDTO> TopFiveGenerousGuests(int eventId)
        {
            var guests = eventGuestRepo.Query().Where(x => x.HasAttended).OrderByDescending(x => x.GiftAmount).Take(5).ToList();
            return guests.Select(g => new GuestDTO().InjectFrom(g) as GuestDTO);
        }

        public void UpdateGiftAmount(int eventId, int guestId, decimal giftAmount)
        {
            var eventGuest = eventGuestRepo.Query().FirstOrDefault(x => x.GuestId == guestId && x.EventId == eventId);
            if (eventGuest != null)
            {
                eventGuest.GiftAmount = giftAmount;
                eventGuestRepo.Update(eventGuest);
                unitOfWork.Commit();
            }

        }
    }
}
