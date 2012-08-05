namespace Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class PhoneReset : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Users", "PhoneNumber", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("Users", "PhoneNumber", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
