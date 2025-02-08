namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCalledAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketInRooms", "CalledAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketInRooms", "CalledAt");
        }
    }
}
