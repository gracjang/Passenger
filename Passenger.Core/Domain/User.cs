using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Passenger.Core.Exceptions;

namespace Passenger.Core.Domain
{
    public class User
    {
        private static readonly Regex _emailRegex = new Regex(
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            RegexOptions.CultureInvariant);

        public Guid Id { get; protected set; }

        public string Email { get; protected set; }

        public string Username { get; protected set; }

        public string Password { get; protected set; }

        public string Salt { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public DateTime UpdatedAt { get; protected set; }

        protected User()
        {  
        }

        public User(string email, string username, string password, string salt)
        {   
            Id = Guid.NewGuid();
            Email = email;
            Username = username.ToLowerInvariant();
            Password = password;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetUsername(string username)
        {
            if(string.IsNullOrEmpty(username))
            {
                throw new DomainException(ErrorCodes.InvalidUsername, "Username can't be null");
            }
            if(Username == username)
            {
                return;
            }

            Username = username;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Email can't be null");
            }
            if(!_emailRegex.IsMatch(email))
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Email have bad format");
            }
            if(Email == email)
            {
                return;
            }

            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, string salt)
        {
            if(string.IsNullOrEmpty(password))
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password can't be empty.");
            }
            if(string.IsNullOrEmpty(salt))
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Salt can't be empty.");
            }
            if(password.Length < 6)
            {
                throw new DomainException(
                    ErrorCodes.InvalidPassword, "Password must contain at least 6 characters.");
            }
            if(password.Length > 30)
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password can't contain more than 30 characters.");
            }
            if(Password == password)
            {
                return;
            }

            Salt = salt;
            Password = password;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}