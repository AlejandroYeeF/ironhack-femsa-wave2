using System;
using Lab3.Interfaces;

namespace Lab3.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly Dictionary<string, string> users = new Dictionary<string, string>()
		{
			{ "validUser", "validPass" }
		};

        public bool ValidateCredentials(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(username))
			{
				return false;
			}

			return users.ContainsKey(username) && users[username] == password;
        }
    }
}

