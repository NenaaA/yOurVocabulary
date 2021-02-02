namespace yOurVocabulary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creatorApplicationAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreatorApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CreatorApplications");
        }
    }
}
