using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WorkTracker.Model;

public partial class WorktrackerContext : DbContext
{
    public WorktrackerContext()
    {
    }

    public WorktrackerContext(DbContextOptions<WorktrackerContext> options)
        : base(options)
    {
    }
    



    public virtual DbSet<Pauselog> Pauselogs { get; set; }

    public virtual DbSet<Sector> Sectors { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Todolist> Todolists { get; set; }

    public virtual DbSet<Todolistitem> Todolistitems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Worksession> Worksessions { get; set; }

    public virtual DbSet<Worksessionreport> Worksessionreports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
           
            string connectionString = ConfigurationManager.ConnectionStrings["WorkTrackerDatabase"].ConnectionString;

            optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Pauselog>(entity =>
        {
            entity.HasKey(e => new { e.IdWorkSession, e.StartTime })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("pauselog");

            entity.Property(e => e.IdWorkSession).HasColumnName("idWorkSession");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");

            entity.HasOne(d => d.IdWorkSessionNavigation).WithMany(p => p.Pauselogs)
                .HasForeignKey(d => d.IdWorkSession)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PauseLog_WorkSession1");
        });

        modelBuilder.Entity<Sector>(entity =>
        {
            entity.HasKey(e => e.IdSector).HasName("PRIMARY");

            entity.ToTable("sector");

            entity.Property(e => e.IdSector).HasColumnName("idSector");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.HourlyRate).HasPrecision(5, 2);
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.OvertimeHourlyRate).HasPrecision(5, 2);

            entity.HasMany(d => d.ManagerUsernames).WithMany(p => p.IdSectors)
                .UsingEntity<Dictionary<string, object>>(
                    "Sectormanager",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("ManagerUsername")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_SectorManager_User1"),
                    l => l.HasOne<Sector>().WithMany()
                        .HasForeignKey("IdSector")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Sector_has_Manager_Sector1"),
                    j =>
                    {
                        j.HasKey("IdSector", "ManagerUsername")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("sectormanager");
                        j.HasIndex(new[] { "ManagerUsername" }, "fk_SectorManager_User1_idx");
                        j.HasIndex(new[] { "IdSector" }, "fk_Sector_has_Manager_Sector1_idx");
                        j.IndexerProperty<int>("IdSector").HasColumnName("idSector");
                        j.IndexerProperty<string>("ManagerUsername").HasMaxLength(45);
                    });
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("PRIMARY");

            entity.ToTable("task");

            entity.HasIndex(e => e.WorkerUsername, "fk_Task_User1_idx");

            entity.Property(e => e.IdTask).HasColumnName("idTask");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.WorkerUsername).HasMaxLength(45);

            entity.HasOne(d => d.WorkerUsernameNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.WorkerUsername)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Task_User1");
        });

        modelBuilder.Entity<Todolist>(entity =>
        {
            entity.HasKey(e => e.IdTodolist).HasName("PRIMARY");

            entity.ToTable("todolist");

            entity.HasIndex(e => e.WorkerUsername, "fk_TODOList_User1_idx");

            entity.Property(e => e.IdTodolist).HasColumnName("idTODOList");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.WorkerUsername).HasMaxLength(45);

            entity.HasOne(d => d.WorkerUsernameNavigation).WithMany(p => p.Todolists)
                .HasForeignKey(d => d.WorkerUsername)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TODOList_User1");
        });

        modelBuilder.Entity<Todolistitem>(entity =>
        {
            entity.HasKey(e => new { e.IdTodolistItem, e.IdTodolist })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("todolistitem");

            entity.HasIndex(e => e.IdTodolist, "fk_TODOListItem_TODOList1_idx");

            entity.Property(e => e.IdTodolistItem)
                .ValueGeneratedOnAdd()
                .HasColumnName("idTODOListItem");
            entity.Property(e => e.IdTodolist).HasColumnName("idTODOList");
            entity.Property(e => e.Content).HasColumnType("text");

            entity.HasOne(d => d.IdTodolistNavigation).WithMany(p => p.Todolistitems)
                .HasForeignKey(d => d.IdTodolist)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TODOListItem_TODOList1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Username, "Username_UNIQUE").IsUnique();

            entity.HasIndex(e => e.IdSector, "fk_User_Sector1_idx");

            entity.Property(e => e.Username).HasMaxLength(45);
            entity.Property(e => e.AccountType).HasMaxLength(45);
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.HourlyRateWorkerSpecific).HasPrecision(5, 2);
            entity.Property(e => e.IdSector).HasColumnName("idSector");
            entity.Property(e => e.Image).HasColumnType("blob");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.OvertimeRateWorkerSpecific).HasPrecision(5, 2);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(45);
            entity.Property(e => e.Surname).HasMaxLength(45);

            entity.HasOne(d => d.IdSectorNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdSector)
                .HasConstraintName("fk_User_Sector1");
        });

        modelBuilder.Entity<Worksession>(entity =>
        {
            entity.HasKey(e => e.IdSession).HasName("PRIMARY");

            entity.ToTable("worksession");

            entity.HasIndex(e => e.WorkerUsername, "fk_WorkSession_User1_idx");

            entity.Property(e => e.IdSession).HasColumnName("idSession");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.WorkedHours).HasColumnType("time");
            entity.Property(e => e.WorkerUsername).HasMaxLength(45);

            entity.HasOne(d => d.WorkerUsernameNavigation).WithMany(p => p.Worksessions)
                .HasForeignKey(d => d.WorkerUsername)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WorkSession_User1");
        });

        modelBuilder.Entity<Worksessionreport>(entity =>
        {
            entity.HasKey(e => e.WorkSessionIdSession).HasName("PRIMARY");

            entity.ToTable("worksessionreport");

            entity.Property(e => e.WorkSessionIdSession)
                .ValueGeneratedNever()
                .HasColumnName("WorkSession_idSession");
            entity.Property(e => e.HourlyRate).HasPrecision(5, 2);
            entity.Property(e => e.OvertimeHourlyRate).HasPrecision(5, 2);
            entity.Property(e => e.OvertimeHours).HasColumnType("time");
            entity.Property(e => e.WorkedHours).HasColumnType("time");

            entity.HasOne(d => d.WorkSessionIdSessionNavigation).WithOne(p => p.Worksessionreport)
                .HasForeignKey<Worksessionreport>(d => d.WorkSessionIdSession)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WorkSessionReport_WorkSession1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
