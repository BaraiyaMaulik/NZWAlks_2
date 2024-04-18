namespace NZWAlks_2.API.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //Navigation Property [Many to many relation between user and role]
        public List<User_Role> UserRoles { get; set; }
    }
}
