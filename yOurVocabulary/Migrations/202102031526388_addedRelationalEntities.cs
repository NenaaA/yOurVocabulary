namespace yOurVocabulary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRelationalEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Words", "Story_Id", "dbo.Stories");
            DropForeignKey("dbo.Stories", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.Stories", "Profile_Id1", "dbo.Profiles");
            DropForeignKey("dbo.Words", "Profile_Id", "dbo.Profiles");
            DropIndex("dbo.Stories", new[] { "Profile_Id" });
            DropIndex("dbo.Stories", new[] { "Profile_Id1" });
            DropIndex("dbo.Words", new[] { "Story_Id" });
            DropIndex("dbo.Words", new[] { "Profile_Id" });
            DropPrimaryKey("dbo.Profiles");
            DropColumn("dbo.Profiles", "Id");
            DropPrimaryKey("dbo.Stories");
            DropColumn("dbo.Stories", "Id");
            DropPrimaryKey("dbo.Words");
            DropColumn("dbo.Words", "Id");
            CreateTable(
                "dbo.ProfileStories",
                c => new
                    {
                        ProfileId = c.Int(nullable: false),
                        StoryId = c.Int(nullable: false),
                        Read = c.Boolean(nullable: false),
                        Rating = c.Single(),
                    })
                .PrimaryKey(t => new { t.ProfileId, t.StoryId })
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .ForeignKey("dbo.Stories", t => t.StoryId, cascadeDelete: true)
                .Index(t => t.ProfileId)
                .Index(t => t.StoryId);
            
            CreateTable(
                "dbo.StoryWords",
                c => new
                    {
                        StoryId = c.Int(nullable: false),
                        WordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StoryId, t.WordId })
                .ForeignKey("dbo.Stories", t => t.StoryId, cascadeDelete: true)
                .ForeignKey("dbo.Words", t => t.WordId, cascadeDelete: true)
                .Index(t => t.StoryId)
                .Index(t => t.WordId);
            
            CreateTable(
                "dbo.ProfileWords",
                c => new
                    {
                        ProfileId = c.Int(nullable: false),
                        WordId = c.Int(nullable: false),
                        LastChecked = c.DateTime(nullable: false),
                        CheckedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProfileId, t.WordId })
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .ForeignKey("dbo.Words", t => t.WordId, cascadeDelete: true)
                .Index(t => t.ProfileId)
                .Index(t => t.WordId);
            
            AddColumn("dbo.Profiles", "ProfileId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Profiles", "ProfileUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Stories", "StoryId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Words", "WordId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Words", "Name", c => c.String());
            AddPrimaryKey("dbo.Profiles", "ProfileId");
            AddPrimaryKey("dbo.Stories", "StoryId");
            AddPrimaryKey("dbo.Words", "WordId");
            CreateIndex("dbo.Profiles", "ProfileUser_Id");
            AddForeignKey("dbo.Profiles", "ProfileUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Profiles", "Email");
            DropColumn("dbo.Stories", "Profile_Id");
            DropColumn("dbo.Stories", "Profile_Id1");
            DropColumn("dbo.Words", "WordName");
            DropColumn("dbo.Words", "CheckedCount");
            DropColumn("dbo.Words", "Story_Id");
            DropColumn("dbo.Words", "Profile_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Words", "Profile_Id", c => c.Int());
            AddColumn("dbo.Words", "Story_Id", c => c.Int());
            AddColumn("dbo.Words", "CheckedCount", c => c.Int(nullable: false));
            AddColumn("dbo.Words", "WordName", c => c.String());
            AddColumn("dbo.Words", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Stories", "Profile_Id1", c => c.Int());
            AddColumn("dbo.Stories", "Profile_Id", c => c.Int());
            AddColumn("dbo.Stories", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Profiles", "Email", c => c.String());
            AddColumn("dbo.Profiles", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Profiles", "ProfileUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StoryWords", "WordId", "dbo.Words");
            DropForeignKey("dbo.ProfileWords", "WordId", "dbo.Words");
            DropForeignKey("dbo.ProfileWords", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.StoryWords", "StoryId", "dbo.Stories");
            DropForeignKey("dbo.ProfileStories", "StoryId", "dbo.Stories");
            DropForeignKey("dbo.ProfileStories", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.ProfileWords", new[] { "WordId" });
            DropIndex("dbo.ProfileWords", new[] { "ProfileId" });
            DropIndex("dbo.StoryWords", new[] { "WordId" });
            DropIndex("dbo.StoryWords", new[] { "StoryId" });
            DropIndex("dbo.ProfileStories", new[] { "StoryId" });
            DropIndex("dbo.ProfileStories", new[] { "ProfileId" });
            DropIndex("dbo.Profiles", new[] { "ProfileUser_Id" });
            DropPrimaryKey("dbo.Words");
            DropPrimaryKey("dbo.Stories");
            DropPrimaryKey("dbo.Profiles");
            DropColumn("dbo.Words", "Name");
            DropColumn("dbo.Words", "WordId");
            DropColumn("dbo.Stories", "StoryId");
            DropColumn("dbo.Profiles", "ProfileUser_Id");
            DropColumn("dbo.Profiles", "ProfileId");
            DropTable("dbo.ProfileWords");
            DropTable("dbo.StoryWords");
            DropTable("dbo.ProfileStories");
            AddPrimaryKey("dbo.Words", "Id");
            AddPrimaryKey("dbo.Stories", "Id");
            AddPrimaryKey("dbo.Profiles", "Id");
            CreateIndex("dbo.Words", "Profile_Id");
            CreateIndex("dbo.Words", "Story_Id");
            CreateIndex("dbo.Stories", "Profile_Id1");
            CreateIndex("dbo.Stories", "Profile_Id");
            AddForeignKey("dbo.Words", "Profile_Id", "dbo.Profiles", "Id");
            AddForeignKey("dbo.Stories", "Profile_Id1", "dbo.Profiles", "Id");
            AddForeignKey("dbo.Stories", "Profile_Id", "dbo.Profiles", "Id");
            AddForeignKey("dbo.Words", "Story_Id", "dbo.Stories", "Id");
        }
    }
}
