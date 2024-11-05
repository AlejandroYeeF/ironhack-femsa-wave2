using System;
using Lab3.Interfaces;

namespace Lab3.Managers
{
    public class UserAuthenticationManager
    {
        private readonly IUserRepository _userRepository;

        public UserAuthenticationManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Authenticate(string? username, string? password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            return _userRepository.ValidateCredentials(username, password);
        }
    }
}

