using Bogus.DataSets;
using System;
using System.Net;

namespace Benchmarkator.Generator.Entities;

public class UserDetails
{
    public int Id { get; init; }
    public bool IsActive { get; init; }
    public PersonalDetails Personal { get; init; } = null!;
    public AddressDetails Address { get; init; } = null!;
    public EmploymentDetails Employment { get; init; } = null!;
    public BankAccountDetails BankAccount { get; init; } = null!;
    public SecurityInfo Security { get; init; } = null!;
    public DateTime RegistrationDate { get; init; }

    public class PersonalDetails
    {
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public DateTime BirthDay { get; init; }
        public string Avatar { get; init; } = null!;
        public string About { get; init; } = null!;
    }

    public class AddressDetails
    {
        public string EmailAddress { get; init; } = null!;
        public string StreetAddress { get; init; } = null!;
        public string City { get; init; } = null!;
        public string PostalCode { get; init; } = null!;
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string Country { get; init; } = null!;
        public string PhoneNumber { get; init; } = null!;
    }

    public class EmploymentDetails
    {
        public string CompanyName { get; init; } = null!;
    }

    public class BankAccountDetails
    {
        public string AccountName { get; init; } = null!;
        public string AccountNr { get; init; } = null!;
        public string AccountCurrency { get; init; } = null!;
    }

    public class SecurityInfo
    {
        public string VisitorIp { get; init; } = null!;
        public string VisitorUserAgent { get; init; } = null!;
    }
}
