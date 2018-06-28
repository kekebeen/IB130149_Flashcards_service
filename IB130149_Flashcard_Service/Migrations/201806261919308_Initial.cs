namespace IB130149_Flashcard_Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dekovi",
                c => new
                    {
                        DeckId = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        KategorijaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DeckId)
                .ForeignKey("dbo.Kategorije", t => t.KategorijaId, cascadeDelete: true)
                .Index(t => t.KategorijaId);
            
            CreateTable(
                "dbo.Kategorije",
                c => new
                    {
                        KategorijaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.KategorijaId);
            
            CreateTable(
                "dbo.Pitanja",
                c => new
                    {
                        PitanjeId = c.Int(nullable: false, identity: true),
                        Pitanje = c.String(),
                        Odgovor = c.String(),
                        DeckId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PitanjeId)
                .ForeignKey("dbo.Dekovi", t => t.DeckId, cascadeDelete: true)
                .Index(t => t.DeckId);
            
            CreateTable(
                "dbo.Korisnici",
                c => new
                    {
                        KorisnikId = c.Int(nullable: false, identity: true),
                        Ime = c.String(nullable: false, maxLength: 50),
                        Prezime = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Lozinka = c.String(nullable: false, maxLength: 50),
                        KorisnickoIme = c.String(nullable: false, maxLength: 50),
                        ProfilId = c.Int(),
                    })
                .PrimaryKey(t => t.KorisnikId)
                .ForeignKey("dbo.Profili", t => t.ProfilId)
                .Index(t => t.ProfilId);
            
            CreateTable(
                "dbo.Profili",
                c => new
                    {
                        ProfilId = c.Int(nullable: false, identity: true),
                        Ulica = c.String(),
                        Grad = c.String(),
                        Opstina = c.String(),
                        BrojTelefona = c.String(),
                    })
                .PrimaryKey(t => t.ProfilId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Korisnici", "ProfilId", "dbo.Profili");
            DropForeignKey("dbo.Pitanja", "DeckId", "dbo.Dekovi");
            DropForeignKey("dbo.Dekovi", "KategorijaId", "dbo.Kategorije");
            DropIndex("dbo.Korisnici", new[] { "ProfilId" });
            DropIndex("dbo.Pitanja", new[] { "DeckId" });
            DropIndex("dbo.Dekovi", new[] { "KategorijaId" });
            DropTable("dbo.Profili");
            DropTable("dbo.Korisnici");
            DropTable("dbo.Pitanja");
            DropTable("dbo.Kategorije");
            DropTable("dbo.Dekovi");
        }
    }
}
