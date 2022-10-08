using BpInterface.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BpInterface.Infrastructure.Data
{
    public class BpContext : DbContext
    {
        #region Properties
        //private readonly IConnectionStringService _connectionStringService;
        #endregion
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Cuenta> Cuentas { get; set; } = null!;
        public DbSet<Movimiento> Movimientos { get; set; } = null!;

        #region Constructor

        public BpContext(DbContextOptions<BpContext> options) : base(options)
        {
            
        }

        #endregion

        #region Protected Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connectionStringService.GetClarityConnectionString());
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            /*
            modelBuilder.Entity<F_GetDateTZResult>()
                 .HasNoKey();

            modelBuilder.Entity<GetLastTransplantCandidateResult>()
                 .HasNoKey();
            
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientIDNumber)
                    .HasName("UPKCI_Patient");

                entity.Property(e => e.PatientIDNumber)
                    .HasColumnName("PatientIDNumber");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Last Name");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("First Name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Middle Name");

                entity.Property(e => e.BirthDate)
                    .IsUnicode(false)
                    .HasComment("Birth Date");

                entity.Property(e => e.DateFirstDialysis)
                    .IsUnicode(false)
                    .HasComment("Date First Dialysis");

                entity.Property(e => e.DateFirstDialysisCurrentUnit)
                    .IsUnicode(false)
                    .HasComment("Date First Dialysis Current Unit");

                entity.Property(e => e.Suffix)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Suffix");

                entity.Property(e => e.Sex)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Sex");

                entity.Property(e => e.Address1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Address1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Address2");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("City");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("State");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("ZipCode");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Phone");

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Phone2");

                entity.Property(e => e.Degree)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Degree");

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("MobilePhone");

                entity.Property(e => e.Prefix)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Prefix");

                entity.Property(e => e.CauseofRenalFailure)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("CauseofRenalFailure");

                entity.Property(e => e.PrimaryModality)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("PrimaryModality");

                entity.Property(e => e.DialysisPatientID)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("DialysisPatientID")
                    .IsRequired(false);

                entity.Property(e => e.PreferredHospitalIDNumber)
                    .HasComment("DialysisPatientID")
                    .IsRequired(false);
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.HasKey(e => e.HospitalIDNumber)
                    .HasName("UPKCI_Hospital");

                entity.Property(e => e.HospitalIDNumber)
                    .HasColumnName("HospitalIDNumber");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Phone");
            });

            modelBuilder.Entity<Pharmacy>(entity =>
            {
                entity.HasKey(e => e.PharmacyIDNumber)
                    .HasName("UPKCI_Pharmacy");

                entity.Property(e => e.PharmacyIDNumber)
                    .HasColumnName("PharmacyIDNumber");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Phone");
            });

            modelBuilder.Entity<PatientAllergy>(entity =>
            {
                entity.HasKey(e => e.PatientAlergyIDNumber)
                    .HasName("UPKCI_PatientAllergy");

                entity.Property(e => e.PatientAlergyIDNumber)
                    .HasColumnName("PatientAlergyIDNumber");

                entity.Property(e => e.MedicationName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("MedicationName")
                    .IsRequired();

                entity.Property(e => e.TypeofReaction)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("TypeofReaction");

                entity.Property(e => e.PatientIDNumber)
                   .HasComment("PatientIDNumber")
                   .IsRequired(false);

                entity.HasAlternateKey(e => e.PatientIDNumber);
            });

            modelBuilder.Entity<PicList>(entity =>
            {
                entity.HasKey(e => e.PicListIDNumber)
                    .HasName("UPKCI_PicList");

                entity.Property(e => e.PicListIDNumber)
                    .HasColumnName("PicListIDNumber");

                entity.Property(e => e.Value)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Name");

                entity.Property(e => e.ListType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("ListType");

                entity.Property(e => e.Text)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(space((1)))")
                    .HasComment("Text");
            });

            modelBuilder.Entity<PatientPhysician>(entity =>
            {
                entity.ToTable("PatientPhysician");

                entity.HasComment("Care Teams for patients");

                entity.HasKey(e => e.PatientPhysicianIDNumber)
                    .HasName("UPKCI_Patient");

                entity.Property(e => e.PatientPhysicianIDNumber)
                    .HasColumnName("PatientPhysicianIDNumber")
                    .HasComment("Identifier of the table");

                entity.Property(e => e.PatientIDNumber)
                    .HasDefaultValueSql("((-1))")
                    .HasColumnName("PatientIDNumber")
                    .HasComment("The identifier of the patient who had the care team");

                entity.Property(e => e.UserIDNumber)
                    .HasColumnName("UserIDNumber")
                    .HasComment("The identifier of the clarity user that matches the care team");

                entity.Property(e => e.PhysicianIDNumber)
                    .HasDefaultValueSql("((-1))")
                    .HasColumnName("PhysicianIDNumber")
                    .HasComment("The identifier of the care team who had physician role");

                entity.Property(e => e.CareTeamMemberRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Care Team Member Role");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date Record was discontinued");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("User");

                entity.Property(e => e.UserIDNumber).HasColumnName("UserIDNumber");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Titles)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });*/

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
