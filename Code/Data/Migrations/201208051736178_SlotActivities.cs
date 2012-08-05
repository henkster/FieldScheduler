namespace Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SlotActivities : DbMigration
    {
        public override void Up()
        {
            AddColumn("Slots", "AllowedActivityAsInt", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Slots", "AllowedActivityAsInt");
        }
    }
}
