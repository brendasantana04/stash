namespace StashBankApplication.DTOs.User
{
    public class AccountCredentialsDTO
    {
        public AccountCredentialsDTO() { }
        public AccountCredentialsDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
