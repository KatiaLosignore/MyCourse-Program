using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyCourse.Models.Entities;


namespace MyCourse.Models.Entities
{
    //classe tramite cui potrò eseguire le operazioni CRUD tra le classi entità e il database
    public partial class MyCourseDbContext : DbContext
    {
        public MyCourseDbContext()
        {
        }

        public MyCourseDbContext(DbContextOptions<MyCourseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }//oggetto tramite cui potrò eseguire le operazioni CRUD con la tabella COurses del db
        public virtual DbSet<Lesson> Lessons { get; set; }//oggetto tramite cui potrò eseguire le operazioni CRUD con la tabella Lessons del db

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //istruzione che mi connette al database tramite connection string
            optionsBuilder.UseSqlite("Data Source=Data/MyCourse.db");

        }

        //metodo che mi permette di configurare tutte le classi entità con le tabelle del database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //setto tutti i matching tramite l'oggetto modelBuilder
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");//tramite ToTable indico che la classe entità Course mappa la tabella Courses del db
                entity.HasKey(course => course.Id);//tramite HasKey indico che la proprietà Id è la chiave primaria della tabella   

                entity.OwnsOne(course => course.CurrentPrice, builder =>
                {
                    builder.Property(money => money.Currency)
                    .HasConversion<string>()
                    .HasColumnName("CurrentPrice_Currency"); //Questo è superfluo perché le nostre colonne seguono già la convenzione di nomi
                    builder.Property(money => money.Amount).HasColumnName("CurrentPrice_Amount"); //Questo è superfluo perché le nostre colonne seguono già la convenzione di nomi
                });
                entity.OwnsOne(course => course.FullPrice, builder =>
                {
                    builder.Property(money => money.Currency).HasConversion<string>();
                });

                //codice per inserire la relazione uno a molti tra la lista di Lesson nell'entità Course
                //e il singolo oggetto Course nell'entità Lesson
                entity.HasMany(course => course.Lessons)
                .WithOne(lesson => lesson.Course)
                .HasForeignKey(lesson => lesson.CourseId);//indico che la chiave esterna è il campo CourseId dell'entità Lesson


                modelBuilder.Entity<Lesson>(e =>
                {
                    e.ToTable("Lessons");//associo l'entità Lesson alla tabella Lessons del db
                    e.HasKey(lesson => lesson.Id);//indicco la chiave primaria di Lesson

                    //mappatuta della proprietà Title di Lesson superflua
                    //perchè se la proprietà dell'entità ha lo stesso identico nome del campo nella tabella del db
                    //la mappatura avviene automaticamente
                    //in caso contrario (come con FullPrice e currentPrice) dovete utilizzare il metodo Property
                    //e indicare tramite HasColumnName a quale colonna della tabella del db fa riferimento la proprietà dell'entità
                    //entity.Property(entity => entity.Title).HasColumnName("Title").HasColumnType("TEXT");

                    //la proprietà Course nella classe Lesson rappresenta il lato 1
                    e.HasOne(lesson => lesson.Course)
                        .WithMany(course => course.Lessons)//che si riferisce lato N alla proprietà Lessons dell'entità Course
                        .HasForeignKey(lesson => lesson.CourseId);
                });
            });
        }
    }
}
