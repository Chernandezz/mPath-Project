namespace mPathProject.Application.DTOs
{
    public class LoginResponseDto
    {
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
