using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace demoSqlite
{
    public class LocalDBServices
    {
        private const string DBName = "demoDB.db3";
        private readonly SQLiteAsyncConnection connection;

        public LocalDBServices() {

            connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DBName));
            connection.CreateTableAsync<Employee>();
        }
        public async Task Create(Employee employee)
        {
            await connection.InsertAsync(employee);
        }

        public async Task Delete(Employee employee)
        {
            await connection.DeleteAsync(employee);
        }

        public async Task Update(Employee employee)
        {
            await connection.UpdateAsync(employee);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await connection.Table<Employee>().ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await connection.Table<Employee>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

    }
}
