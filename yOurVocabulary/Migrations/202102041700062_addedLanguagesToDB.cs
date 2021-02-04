namespace yOurVocabulary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLanguagesToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Languages");
        }
    }
}
