namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAccounts", "UserId", "dbo.Users");
            DropIndex("dbo.UserAccounts", new[] { "UserId" });
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "Password", c => c.String());
            DropTable("dbo.UserAccounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Email = c.String(),
                        isPimaryAccount = c.Boolean(nullable: false),
                        UserAccountType = c.Int(nullable: false),
                        EncryptedPassword = c.String(),
                        AccessToken = c.String(),
                        RefreshToken = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Email");
            CreateIndex("dbo.UserAccounts", "UserId");
            AddForeignKey("dbo.UserAccounts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
