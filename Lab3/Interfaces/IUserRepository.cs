using System;
namespace Lab3.Interfaces
{
	public interface IUserRepository
    {
		bool ValidateCredentials(string username, string password);
	}
}
