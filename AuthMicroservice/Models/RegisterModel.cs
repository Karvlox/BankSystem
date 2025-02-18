namespace AuthMicroservice.Models
{
    public class RegisterModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string RoleType { get; set; }
        public required string AccessLevel { get; set; }
        public required int Ci { get; set; }
    }
}
