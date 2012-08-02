namespace Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class FieldConflicts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "FieldsConflictFields",
                c => new
                    {
                        Field_Id = c.Int(nullable: false),
                        Conflict_Field_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Field_Id, t.Conflict_Field_Id })
                .ForeignKey("Fields", t => t.Field_Id)
                .ForeignKey("Fields", t => t.Conflict_Field_Id)
                .Index(t => t.Field_Id)
                .Index(t => t.Conflict_Field_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("FieldsConflictFields", new[] { "Conflict_Field_Id" });
            DropIndex("FieldsConflictFields", new[] { "Field_Id" });
            DropForeignKey("FieldsConflictFields", "Conflict_Field_Id", "Fields");
            DropForeignKey("FieldsConflictFields", "Field_Id", "Fields");
            DropTable("FieldsConflictFields");
        }
    }
}
