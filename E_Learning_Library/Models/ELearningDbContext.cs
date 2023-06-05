using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_Library.Models;

public partial class ELearningDbContext : DbContext
{
    public ELearningDbContext()
    {
    }

    public ELearningDbContext(DbContextOptions<ELearningDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAnswer> UserAnswers { get; set; }

    public virtual DbSet<UserProgress> UserProgresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=E_LearningDB;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__tmp_ms_x__369373106794E5C8");

            entity.ToTable("Answer");

            entity.Property(e => e.AnswerId).HasColumnName("Answer_id");
            entity.Property(e => e.AnswerText)
                .HasMaxLength(200)
                .HasColumnName("Answer_text");
            entity.Property(e => e.IsCorrect)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Is_correct");
            entity.Property(e => e.QuestionId).HasColumnName("Question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Answer__Question__3B75D760");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__37E005DB9F4A68D2");

            entity.ToTable("Course");

            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("Course_name");
            entity.Property(e => e.Description).HasMaxLength(1000);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__tmp_ms_x__B0B3E0BE85D07A04");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("Question_id");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(500)
                .HasColumnName("Question_text");
            entity.Property(e => e.QuizId).HasColumnName("Quiz_id");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("FK__Question__Quiz_i__3C69FB99");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.QuizId).HasName("PK__Quiz__109441C213B15600");

            entity.ToTable("Quiz");

            entity.Property(e => e.QuizId).HasColumnName("Quiz_id");
            entity.Property(e => e.CourseId).HasColumnName("Course_id");
            entity.Property(e => e.QuizName)
                .HasMaxLength(50)
                .HasColumnName("Quiz_name");

            entity.HasOne(d => d.Course).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Quiz__Course_id__2F10007B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07A8036053");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4EEEE91B9").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534CABFC42B").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Firstname).HasMaxLength(45);
            entity.Property(e => e.IsGraduated)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Is_Graduated");
            entity.Property(e => e.Lastname).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.RoleName)
                .HasMaxLength(15)
                .HasColumnName("Role_name");
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasMany(d => d.Courses).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK__UserCours__Cours__2C3393D0"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__UserCours__User___2B3F6F97"),
                    j =>
                    {
                        j.HasKey("UserId", "CourseId").HasName("PK_Users_Course_id");
                        j.ToTable("UserCourse");
                        j.IndexerProperty<int>("UserId").HasColumnName("User_id");
                        j.IndexerProperty<int>("CourseId").HasColumnName("Course_id");
                    });
        });

        modelBuilder.Entity<UserAnswer>(entity =>
        {
            entity.HasKey(e => e.UserAnswerId).HasName("PK__User_ans__99A7CDDDEC7343CC");

            entity.ToTable("User_answer");

            entity.Property(e => e.UserAnswerId).HasColumnName("User_answer_id");
            entity.Property(e => e.AnswerId).HasColumnName("Answer_id");
            entity.Property(e => e.QuestionId).HasColumnName("Question_id");
            entity.Property(e => e.QuizId).HasColumnName("Quiz_id");

            entity.HasOne(d => d.Answer).WithMany(p => p.UserAnswers)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK__User_answ__Answe__7C4F7684");

            entity.HasOne(d => d.Question).WithMany(p => p.UserAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__User_answ__Quest__7D439ABD");

            entity.HasOne(d => d.Quiz).WithMany(p => p.UserAnswers)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("FK__User_answ__Quiz___7E37BEF6");
        });

        modelBuilder.Entity<UserProgress>(entity =>
        {
            entity.HasKey(e => e.UserProgressId).HasName("PK__UserProg__C200B7500D47A0F9");

            entity.ToTable("UserProgress");

            entity.Property(e => e.UserProgressId).HasColumnName("UserProgress_id");
            entity.Property(e => e.DateCompleted).HasColumnType("datetime");
            entity.Property(e => e.QuizId).HasColumnName("Quiz_id");
            entity.Property(e => e.Score).HasMaxLength(15);
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Quiz).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("FK__UserProgr__Quiz___5CD6CB2B");

            entity.HasOne(d => d.User).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserProgr__User___5BE2A6F2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
