namespace mPathProject.Application.DTOs
{
    public class CreateUserRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
}
