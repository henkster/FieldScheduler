namespace Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ExternalTeamValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Teams", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("Teams", "Name", c => c.String());
        }
    }
}
