namespace yOurVocabulary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesToCreatorApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreatorApplications", "UserId", c => c.String());
            DropColumn("dbo.CreatorApplications", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CreatorApplications", "Email", c => c.String());
            DropColumn("dbo.CreatorApplications", "UserId");
        }
    }
}
