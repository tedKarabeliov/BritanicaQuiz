namespace BritanicaQuiz.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuizEnrolments", "TotalPointsTeacher", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuizEnrolments", "TotalPointsTeacher");
        }
    }
}
