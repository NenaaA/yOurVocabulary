namespace yOurVocabulary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastCheckedNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProfileWords", "LastChecked", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProfileWords", "LastChecked", c => c.DateTime(nullable: false));
        }
    }
}
