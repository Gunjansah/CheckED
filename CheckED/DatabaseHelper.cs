using System;
using SQLite;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

namespace CheckED
{
    public class DatabaseHelper
    {
        readonly SQLiteAsyncConnection _database;

        public DatabaseHelper(string dbPath)
        {
            try
            {
                _database = new SQLiteAsyncConnection(dbPath);

                // Create tables if they do not exist
                _database.CreateTableAsync<User>().Wait();
                _database.CreateTableAsync<Event>().Wait();
                _database.CreateTableAsync<ContactMessage>().Wait(); // Added ContactMessage table

                Console.WriteLine("Database initialized and tables created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization failed: {ex.Message}");
            }
        }

        // User Methods

        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<User> GetUserAsync(string email)
        {
            return _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            return _database.Table<User>().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> UpdateUserAsync(User user)
        {
            return _database.UpdateAsync(user);
        }

        // Event Methods

        public Task<int> SaveEventAsync(Event evnt)
        {
            return _database.InsertAsync(evnt);
        }

        public Task<List<Event>> GetEventsForUserAsync(int userId)
        {
            return _database.Table<Event>().Where(e => e.CreatorId == userId).ToListAsync();
        }

        public async Task<List<Event>> GetRegisteredEventsUserAsync(int userId)
        {
            var user = await _database.Table<User>().Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (user != null && !string.IsNullOrEmpty(user.RegisteredEvents))
            {
                var registeredEventIds = JsonSerializer.Deserialize<List<int>>(user.RegisteredEvents);

                var allEvents = await _database.Table<Event>().ToListAsync();

                var registeredEvents = allEvents.Where(e => registeredEventIds.Contains(e.EventId)).ToList();

                return registeredEvents;
            }

            return new List<Event>();
        }

        public async Task UpdateUserRegisteredEventsAsync(int userId, int eventId, bool isRegistering)
        {
            var user = await _database.Table<User>().Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                var registeredEventIds = string.IsNullOrEmpty(user.RegisteredEvents)
                    ? new List<int>()
                    : JsonSerializer.Deserialize<List<int>>(user.RegisteredEvents);

                if (isRegistering)
                {
                    if (!registeredEventIds.Contains(eventId))
                        registeredEventIds.Add(eventId);
                }
                else
                {
                    registeredEventIds.Remove(eventId);
                }

                user.RegisteredEvents = JsonSerializer.Serialize(registeredEventIds);

                await _database.UpdateAsync(user);
            }
        }

        public Task<int> UpdateEventAsync(Event evnt)
        {
            return _database.UpdateAsync(evnt);
        }

        public Task<List<Event>> GetAllEventsAsync()
        {
            return _database.Table<Event>().ToListAsync();
        }

        // ContactMessage Methods

        public Task<int> SaveContactMessageAsync(ContactMessage message)
        {
            return _database.InsertAsync(message);
        }

        public Task<List<ContactMessage>> GetAllContactMessagesAsync()
        {
            return _database.Table<ContactMessage>().OrderByDescending(m => m.Timestamp).ToListAsync();
        }
    }
}
