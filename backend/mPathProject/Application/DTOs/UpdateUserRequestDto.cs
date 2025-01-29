namespace mPathProject.Application.DTOs
{
    public class UpdateUserRequestDto
    {
        public string Email { get; set; }
        public string? Password { get; set; }
        public string UserRole { get; set; }
    }
}
