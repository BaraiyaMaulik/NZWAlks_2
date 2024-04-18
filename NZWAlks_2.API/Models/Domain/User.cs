using System.ComponentModel.DataAnnotations.Schema;

namespace NZWAlks_2.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }

        //This property is used for static roles
        //public List<string> Roles { get; set; }

        //Navigation Property [Many to many relation between user and role]
        public List<User_Role> UserRoles { get; set; }
    }
}
