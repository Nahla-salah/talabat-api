namespace Shared.DTO_s.OrderDto
{
    public record AddressDto
    {

        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string street { get; init; }

    }
}