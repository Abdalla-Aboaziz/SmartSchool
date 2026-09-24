namespace SmartSchool.Application.Features.Authentication.User.Responses
{
    public class GetUserByIdUserResponse
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
