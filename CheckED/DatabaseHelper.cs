using System;
using SQLite;
using System.Threading.Tasks;

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

                _database.CreateTableAsync<User>().Wait();
                _database.CreateTableAsync<Event>().Wait();




                Console.WriteLine("Database initialized and table created.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Database initialization failed: {ex.Message}");
            }
            
        }

        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<User> GetUserAsync(string email)
        {
            return _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public Task<int> SaveEventAsync(Event evnt)
        {
            return _database.InsertAsync(evnt);
        }

        public Task<List<Event>> GetEventsForUserAsync(int userId)
        {
            return _database.Table<Event>().Where(e => e.UserId == userId).ToListAsync();
        }

        public Task<int> UpdateEventAsync(Event evnt)
        {
            return _database.UpdateAsync(evnt);
        }

        public Task<List<Event>> GetAllEventsAsync()
        {
            return _database.Table<Event>().ToListAsync();
        }


    }
}
