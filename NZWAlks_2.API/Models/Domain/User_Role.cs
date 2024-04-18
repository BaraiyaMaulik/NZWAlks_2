namespace NZWAlks_2.API.Models.Domain
{
    public class User_Role
    {
        public Guid Id { get; set; }

        //Navigation Property
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public Role Role  { get; set; }

    }
}
