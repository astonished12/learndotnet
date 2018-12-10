using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Web.Models
{
    public class EventGuestsModel
    {
        public EventModel EventModel { get; set; }
        public IEnumerable<GuestModel> Guests { get; set; }
    }
}
