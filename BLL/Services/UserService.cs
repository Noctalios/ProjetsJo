﻿using ProjetsJo.BLL.Interfaces;
using ProjetsJo.DAL.Interfaces;
using ProjetsJo.Entites;

namespace ProjetsJo.BLL.Services
{
    public class UserService : IUserService
    {
        private IUserData _userData;

        public UserService(IUserData userData)
        {
            _userData = userData;
        }

        public void CreateUser(string userName, string email, string password)
        {
            _userData.CreateUser(userName, email, password);
        }

        public User GetUser(string email, string password)
        {
            return _userData.GetUser(email, password);
        }
    }
}
