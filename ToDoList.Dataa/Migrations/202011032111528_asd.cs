namespace ToDoList.Dataa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "CreatedBy");
            DropColumn("dbo.AspNetUsers", "CreatedDate");
            DropColumn("dbo.AspNetUsers", "ModifiedBy");
            DropColumn("dbo.AspNetUsers", "ModifiedDate");
            DropColumn("dbo.AspNetUsers", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "ModifiedBy", c => c.String());
            AddColumn("dbo.AspNetUsers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreatedBy", c => c.String());
        }
    }
}
