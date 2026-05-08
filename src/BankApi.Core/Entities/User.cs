using BankApi.Core.Enums;

namespace BankApi.Core.Entities;

public class User
{
    public User(string fullName, string email, string document, DateOnly birthDate, string phone, string password)
    {
        FullName = fullName;
        Situation = EUserSituation.Active;
        Email = email;
        Document = document;
        BirthDate = birthDate;
        Phone = phone;
        Password = password;
    }
    
    private User() { } 

    public Guid Id { get; private set; }

    public string FullName { get; private set; }

    public EUserSituation Situation { get; private set; }

    public string Email { get; private set; }

    public string Document { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public string Phone { get; private set; }

    public string Password { get; private set; }
}