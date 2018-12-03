using EventApp.Services.GuestService.GuestDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Services.GuestService
{
    public interface IGuestService
    {
        IEnumerable<GuestDTO> GetGuestsForAnEvent(int eventId);
        IEnumerable<GuestDTO> GetAllMajorGuestsForAnEvent(int eventId);
        IEnumerable<GuestDTO> GetGuestsThatWillAttendAnEvent(int eventId);
        IEnumerable<GuestDTO> TopFiveGenerousGuests(int eventId);
        void UpdateGiftAmount(int eventId, int guestId, decimal giftAmount);
    }
}
