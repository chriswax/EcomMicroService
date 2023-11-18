namespace JwtAuthServer.Models
{
    public class AuthResponse
    {
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public int ExpireIn { get; set; }

    }
}
