namespace Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UserChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Users", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("Users", "EmailAddress", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("Users", "PhoneNumber", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("Users", "Username", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("Users", "Password", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("Users", "Password", c => c.String());
            AlterColumn("Users", "Username", c => c.String());
            AlterColumn("Users", "PhoneNumber", c => c.String());
            AlterColumn("Users", "EmailAddress", c => c.String());
            AlterColumn("Users", "Name", c => c.String());
        }
    }
}
