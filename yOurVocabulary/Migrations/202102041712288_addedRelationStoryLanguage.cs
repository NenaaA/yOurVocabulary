namespace yOurVocabulary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRelationStoryLanguage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stories", "Language_Code", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Stories", "Language_Code");
            AddForeignKey("dbo.Stories", "Language_Code", "dbo.Languages", "Code", cascadeDelete: true);
            DropColumn("dbo.Stories", "Language");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stories", "Language", c => c.String(nullable: false));
            DropForeignKey("dbo.Stories", "Language_Code", "dbo.Languages");
            DropIndex("dbo.Stories", new[] { "Language_Code" });
            DropColumn("dbo.Stories", "Language_Code");
        }
    }
}
