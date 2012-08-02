namespace Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UserRolesAsInt : DbMigration
    {
        public override void Up()
        {
            AddColumn("Users", "RolesAsInt", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Users", "RolesAsInt");
        }
    }
}
