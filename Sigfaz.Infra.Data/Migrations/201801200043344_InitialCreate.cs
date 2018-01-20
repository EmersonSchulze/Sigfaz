namespace Sigfaz.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        CidadeId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 255, unicode: false),
                        EstadoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CidadeId)
                .ForeignKey("dbo.Estado", t => t.EstadoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 255, unicode: false),
                        Sigla = c.String(nullable: false, maxLength: 255, unicode: false),
                        CodigoIbge = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EstadoId);

            CreateTable(
                    "dbo.AspNetUsers",
                    c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cidade", "EstadoId", "dbo.Estado");
            DropIndex("dbo.Cidade", new[] { "EstadoId" });
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
        }
    }
}
