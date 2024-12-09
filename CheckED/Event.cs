using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckED
{
    public class Event
    {
        [PrimaryKey, AutoIncrement]
        public int EventId { get; set; } 

        public int CreatorId { get; set; } 

        public string EventName { get; set; } 

        public string EventDate { get; set; }

        public string EventDescription { get; set; }

        public string ImageUrl { get; set; }

        public int NumGoing { get; set; }

        public string RegistrationFormLink { get; set; } 

    }
}
