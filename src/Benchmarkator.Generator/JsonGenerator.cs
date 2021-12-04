using Benchmarkator.Generator.Entities;
using Bogus;
using System.Text.Json;

namespace Benchmarkator.Generator
{
    public partial class JsonGenerator
    {
        public static readonly JsonGenerator Instance = new JsonGenerator(DefaultValues.RandomSeed);

        private static readonly JsonSerializerOptions DefaultSerializerOptions =
            new JsonSerializerOptions 
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
                WriteIndented = true 
            };

        private Faker _faker;

        public JsonGenerator(int seed)
        {
            _faker = new Faker();
            _faker.Random = new Randomizer(seed);
        }

        public string SmallJson()
        {
            var order = CreateOrder(_faker);
            return JsonSerializer.Serialize(order, DefaultSerializerOptions);
        }

        public string MediumJson()
        {
            var customer = CreateUserDetails(_faker);
            return JsonSerializer.Serialize(customer, DefaultSerializerOptions);
        }

        public string LargeJson()
        {
            var orderItem = new Faker<OrderDetails.OrderItem>()
                .RuleFor(i => i.Id, f => f.Random.Guid())
                .RuleFor(i => i.ItemName, f => f.Commerce.ProductName())
                .RuleFor(i => i.Description, f => f.Commerce.ProductDescription());

            var orderDetails = new OrderDetails
            {
                Order = CreateOrder(_faker),
                Customer = CreateUserDetails(_faker),
                Description = _faker.Lorem.Paragraphs(4),
                Note = _faker.Lorem.Paragraphs(2),
                Items = orderItem.GenerateBetween(16, 32).ToArray(),
            };

            return JsonSerializer.Serialize(orderDetails, DefaultSerializerOptions);
        }

        private static Order CreateOrder(Faker faker) => 
            new Order
            {
                OrderId = faker.Random.Number(1, 100),
                CustomerName = faker.Name.FullName(),
                Item = faker.Commerce.ProductName(),
                Quantity = faker.Random.Number(1, 10)
            };

        private static UserDetails CreateUserDetails(Faker faker) =>
            new UserDetails
            {
                Id = faker.Random.Number(1, 9999),
                Personal = new UserDetails.PersonalDetails
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    Avatar = faker.Internet.Avatar(),
                    BirthDay = faker.Date.Past(21),
                    About = faker.Lorem.Paragraph(5),
                },
                Address = new UserDetails.AddressDetails
                {
                    EmailAddress = faker.Internet.Email(),
                    StreetAddress = faker.Address.StreetAddress(),
                    City = faker.Address.City(),
                    PostalCode = faker.Address.ZipCode(),
                    Latitude = faker.Address.Latitude(),
                    Longitude = faker.Address.Longitude(),
                    Country = faker.Address.Country(),
                    PhoneNumber = faker.Phone.PhoneNumber(),
                },
                BankAccount = new UserDetails.BankAccountDetails
                {
                    AccountName = faker.Finance.AccountName(),
                    AccountNr = faker.Finance.Account(),
                    AccountCurrency = faker.Finance.Currency().Code,
                },
                Employment = new UserDetails.EmploymentDetails
                {
                    CompanyName = faker.Company.CompanyName(),
                },
                Security = new UserDetails.SecurityInfo
                {
                    VisitorIp = faker.Internet.Ip(),
                    VisitorUserAgent = faker.Internet.UserAgent(),
                },
                IsActive = faker.Random.Bool(),
                RegistrationDate = faker.Date.Past(),
            };
    }
}
