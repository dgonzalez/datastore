using System;
using System.Collections;
using System.Collections.Generic;
using DatStore.Model;
using DatStore.Repositories;
using Google.Cloud.Datastore.V1;

namespace DatStore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Key AddUser(User user)
        {
            return _userRepository.AddUser(user);
        }

        public ArrayList FindUsers(int offset, int limit)
        {
            return _userRepository.FindUsers(offset, limit);
        }

        public ArrayList FindUsersBySurname(int offset, int limit, string surname)
        {
            return _userRepository.FindUsersBySurname(offset, limit, surname);
        }

        public ArrayList FindUsersByNameAndSurname(int offset, int limit, string name, string surname)
        {
            return _userRepository.FindUsersByNameAndSurname(offset, limit, name, surname);
        }

        public ArrayList FindUsersByMultipleCriteria(int offset, int limit,string name, string surname)
        {
            return _userRepository.FindByMultipleCriteria(offset, limit, new SearchCriteria("name", name), new SearchCriteria("surname", surname));
        }
    }
}
