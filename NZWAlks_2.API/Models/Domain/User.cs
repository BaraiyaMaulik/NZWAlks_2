namespace NZWAlks_2.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public List<string> Roles { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
