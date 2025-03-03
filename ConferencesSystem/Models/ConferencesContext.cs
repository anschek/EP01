using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConferencesSystem.Models;

public partial class ConferencesContext : DbContext
{
    public ConferencesContext()
    {
    }

    public ConferencesContext(DbContextOptions<ConferencesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivitiyType> ActivitiyTypes { get; set; }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CityEmblem> CityEmblems { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DirectionType> DirectionTypes { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Juror> Jurors { get; set; }

    public virtual DbSet<Moderator> Moderators { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Conferences;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivitiyType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("activitiy_types_pk");

            entity.ToTable("activitiy_types");

            entity.HasIndex(e => e.Name, "activitiy_types_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("activities_pk");

            entity.ToTable("activities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityType)
                .ValueGeneratedOnAdd()
                .HasColumnName("activity_type");
            entity.Property(e => e.City)
                .ValueGeneratedOnAdd()
                .HasColumnName("city");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.DurationInDays).HasColumnName("duration_in_days");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.Winner).HasColumnName("winner");

            entity.HasOne(d => d.ActivityTypeNavigation).WithMany(p => p.Activities)
                .HasForeignKey(d => d.ActivityType)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("activities_activitiy_types_fk");

            entity.HasOne(d => d.CityNavigation).WithMany(p => p.Activities)
                .HasForeignKey(d => d.City)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("activities_cities_fk");

            entity.HasOne(d => d.WinnerNavigation).WithMany(p => p.Activities)
                .HasForeignKey(d => d.Winner)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("activities_users_fk");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cities_pk");

            entity.ToTable("cities");

            entity.HasIndex(e => e.Name, "cities_unique_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<CityEmblem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("city_emblems_pk");

            entity.ToTable("city_emblems");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .ValueGeneratedOnAdd()
                .HasColumnName("city");
            entity.Property(e => e.Emblem)
                .HasColumnType("character varying")
                .HasColumnName("emblem");

            entity.HasOne(d => d.CityNavigation).WithMany(p => p.CityEmblems)
                .HasForeignKey(d => d.City)
                .HasConstraintName("city_emblems_cities_fk");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("countries_pk");

            entity.ToTable("countries");

            entity.HasIndex(e => e.LiteralCode, "countries_unique_literal").IsUnique();

            entity.HasIndex(e => e.NameEng, "countries_unique_name_eng").IsUnique();

            entity.HasIndex(e => e.NameRu, "countries_unique_name_ru").IsUnique();

            entity.HasIndex(e => e.NumericCode, "countries_unique_numeric").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LiteralCode)
                .HasColumnType("character varying")
                .HasColumnName("literal_code");
            entity.Property(e => e.NameEng)
                .HasColumnType("character varying")
                .HasColumnName("name_eng");
            entity.Property(e => e.NameRu)
                .HasColumnType("character varying")
                .HasColumnName("name_ru");
            entity.Property(e => e.NumericCode).HasColumnName("numeric_code");
        });

        modelBuilder.Entity<DirectionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("direction_types_pk");

            entity.ToTable("direction_types");

            entity.HasIndex(e => e.Name, "direction_types_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("events_pk");

            entity.ToTable("events");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activity)
                .ValueGeneratedOnAdd()
                .HasColumnName("activity");
            entity.Property(e => e.DayNumber).HasColumnName("day_number");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Moderator)
                .ValueGeneratedOnAdd()
                .HasColumnName("moderator");
            entity.Property(e => e.StartTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_time");

            entity.HasOne(d => d.ActivityNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.Activity)
                .HasConstraintName("events_activities_fk");

            entity.HasOne(d => d.ModeratorNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.Moderator)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("events_moderators_fk");

            entity.HasMany(d => d.Juries).WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "EventsJuror",
                    r => r.HasOne<Juror>().WithMany()
                        .HasForeignKey("Jury")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("events_jurors_jury_fk"),
                    l => l.HasOne<Event>().WithMany()
                        .HasForeignKey("Event")
                        .HasConstraintName("events_jurors_events_fk"),
                    j =>
                    {
                        j.HasKey("Event", "Jury").HasName("events_jurors_pk");
                        j.ToTable("events_jurors");
                        j.IndexerProperty<int>("Event")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("event");
                        j.IndexerProperty<int>("Jury")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("jury");
                    });
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genders_pk");

            entity.ToTable("genders");

            entity.HasIndex(e => e.Name, "genders_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Juror>(entity =>
        {
            entity.HasKey(e => e.User).HasName("jury_pk");

            entity.ToTable("jurors");

            entity.Property(e => e.User)
                .HasDefaultValueSql("nextval('jury_user_seq'::regclass)")
                .HasColumnName("user");
            entity.Property(e => e.Direction)
                .HasDefaultValueSql("nextval('jury_direction_seq'::regclass)")
                .HasColumnName("direction");

            entity.HasOne(d => d.DirectionNavigation).WithMany(p => p.Jurors)
                .HasForeignKey(d => d.Direction)
                .HasConstraintName("jury_direction_types_fk");

            entity.HasOne(d => d.UserNavigation).WithOne(p => p.Juror)
                .HasForeignKey<Juror>(d => d.User)
                .HasConstraintName("jury_users_fk");
        });

        modelBuilder.Entity<Moderator>(entity =>
        {
            entity.HasKey(e => e.User).HasName("moderators_pk");

            entity.ToTable("moderators");

            entity.Property(e => e.User)
                .ValueGeneratedOnAdd()
                .HasColumnName("user");
            entity.Property(e => e.ActivityType)
                .ValueGeneratedOnAdd()
                .HasColumnName("activity_type");
            entity.Property(e => e.DirectionType)
                .ValueGeneratedOnAdd()
                .HasColumnName("direction_type");

            entity.HasOne(d => d.ActivityTypeNavigation).WithMany(p => p.Moderators)
                .HasForeignKey(d => d.ActivityType)
                .HasConstraintName("moderators_activitiy_types_fk");

            entity.HasOne(d => d.DirectionTypeNavigation).WithMany(p => p.Moderators)
                .HasForeignKey(d => d.DirectionType)
                .HasConstraintName("moderators_direction_types_fk");

            entity.HasOne(d => d.UserNavigation).WithOne(p => p.Moderator)
                .HasForeignKey<Moderator>(d => d.User)
                .HasConstraintName("moderators_users_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pk");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "roles_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("participants_pk");

            entity.ToTable("users");

            entity.HasIndex(e => e.Mail, "participants_unique_mail").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('participants_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Country)
                .HasDefaultValueSql("nextval('participants_country_seq'::regclass)")
                .HasColumnName("country");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.FullName)
                .HasColumnType("character varying")
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasDefaultValueSql("nextval('participants_gender_seq'::regclass)")
                .HasColumnName("gender");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.Mail)
                .HasColumnType("character varying")
                .HasColumnName("mail");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phone_number");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.CountryNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Country)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participants_countries_fk");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("participants_genders_fk");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("users_roles_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
