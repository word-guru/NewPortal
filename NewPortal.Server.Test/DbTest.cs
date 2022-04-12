using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewPortal.Server.Lib.DataBase;
using NewPortal.Server.Lib.Models;
using Xunit;

namespace NewPortal.Server.Test
{
    public class DbTest
    {
        [Fact]
        public async Task GetAllNews_Test()
        {
            IEnumerable<News> expectedNews = new List<News>
            {
                new()
                {
                    Id = 1,
                    Title = "News 1",
                    Content = "content 1",
                    DateOfCreation = new DateTime(2022, 4, 12, 18, 48, 51),
                    Author = 1
                }
            };

            var db = new DB();
            var actualNews = await db.GetAllNews();

            Assert.Equal(expectedNews, actualNews);
        }

        [Fact]
        public async Task GetAllUsers_Test()
        {
            IEnumerable<User> expectedUser = new List<User>
            {
                new()
                {
                    Id = 1,
                    Login = "admin",
                    Password = "12345",
                    email = "admin@email.ru",
                    FirstName = "Admin",
                    LastName = "Admin",
                    MiddleName = "Admin"
                }
            };

            var db = new DB();
            var actualUsers = await db.GetAllUsers();
            
            Assert.Equal(expectedUser,actualUsers);
        }
    }
}