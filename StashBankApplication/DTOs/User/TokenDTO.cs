namespace StashBankApplication.DTOs.User
{
    public class TokenDTO
    {
        public TokenDTO() { }

        public TokenDTO(
            bool authenticated, 
            string created, 
            string expiration, 
            string acessToken)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AcessToken = acessToken;
        }

        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AcessToken { get; set; }
    }
}
