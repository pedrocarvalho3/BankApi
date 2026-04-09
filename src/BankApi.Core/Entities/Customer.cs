using BankApi.Core.Enums;

namespace BankApi.Core.Entities;

public class Customer
{
    public Customer(string fullName, string email, string document, DateOnly birthDate, string phone, string password)
    {
        FullName = fullName;
        Situation = ECustomerSituation.Active;
        Email = email;
        Document = document;
        BirthDate = birthDate;
        Phone = phone;
        Password = password;
    }
    
    private Customer() { } 

    public Guid Id { get; private set; }

    public string FullName { get; private set; }

    public ECustomerSituation Situation { get; private set; }

    public string Email { get; private set; }

    public string Document { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public string Phone { get; private set; }

    public string Password { get; private set; }
}