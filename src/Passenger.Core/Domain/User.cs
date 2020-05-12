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
    public string Role { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    protected User()
    {
    }

    protected User(Guid userId,string email, string username, string password, string salt, string role)
    {
      Id = userId;
      SetEmail(email);
      SetUsername(username);
      SetPassword(password, salt);
      SetRole(role);
      CreatedAt = DateTime.UtcNow;
    }

    public static User Create(Guid userId, string email, string username, string password, string salt, string role)
      => new User(userId, email, username, password, salt, role);

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

      Username = username.ToLowerInvariant();
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

      Email = email.ToLowerInvariant();
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

      if(Password == password)
      {
        return;
      }

      Salt = salt;
      Password = password;
      UpdatedAt = DateTime.UtcNow;
    }

    public void SetRole(string role)
    {
      if(string.IsNullOrEmpty(role))
      {
        throw new DomainException(ErrorCodes.InvalidRole, "Role can't be null or empty");
      }

      if(role == Role)
      {
        return;
      }

      Role = role;
      UpdatedAt = DateTime.UtcNow;
    }
  }
}