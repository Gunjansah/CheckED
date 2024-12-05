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
    }
}
