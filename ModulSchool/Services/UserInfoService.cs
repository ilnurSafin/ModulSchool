using System;
using System.Threading.Tasks;
using Dapper;
using ModulSchool.Models;
using ModulSchool.Services.Interfaces;
using Npgsql;

namespace ModulSchool.Services
{
    public class UserInfoService : IUserInfoService
    {
        //файл дампа базы лежит с проектом, если его не будет, то пожалуйста просто подключите чью-либо другую базу, либо базу уважаемого и крутого Рината
        private const string ConnectionString =
            "host=localhost; port=5432; database=homework3; username=postgres; password=1";
    
        public async Task<User> GetById(Guid id)//поиск пользователя по его id
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return await connection.QuerySingleAsync<User>("SELECT * FROM public.\"users\" WHERE \"id\" = @id", new {id});
            }
        }
        public async Task<User> AppendUser(User user)//добавление нового пользователя в базу
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                string query = "INSERT INTO users (id, email, nickname, phone) VALUES (@id, @email, @nickname, @phone)";
    
                await connection.QuerySingleAsync<User>(query, user);
            }
            return await Task.FromResult<User>(user);
        }
        
    }
}