using SQLite;
using System;

namespace CheckED
{
    public class ContactMessage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }          // User's name
        public string Email { get; set; }         // User's email
        public string Message { get; set; }       // Message content
        public DateTime Timestamp { get; set; }    // Time of submission
    }
}
