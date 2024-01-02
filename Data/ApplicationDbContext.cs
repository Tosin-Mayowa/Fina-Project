using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WebApplication2.Model;
using WebApplication2.Model.ReportTables;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<APP_Report> APP_Reports2 { get; set; }
        public DbSet<DateSettings> DateSettings { get; set; }

        public DbSet<ReportCategory> APP_ReportCategories{ get; set; }

        public DbSet<LoanUpload1> LoanUploads1 { get; set; }
        public DbSet<LoanUploads2> LoanUpload2 { get; set; }
        public DbSet<CashFlowUpload> CashFlows { get; set; }
        public DbSet<DailyTable> DailyUploads { get; set; }
        public DbSet<StatesBond> StateUploads { get; set; }
        public DbSet<TBillFmdq> TBills { get; set; }
        public DbSet<Coupons> CouponUploads { get; set; }

        public DbSet<DailyDate> U_Daily { get; set; }
        public DbSet<APP_GlobalSetting> APP_GlobalSettings { get; set; }
        public DbSet<aMBR300> MBR300 { get; set; }
        public DbSet<aMBR302> MBR302 {  get; set; }





        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("dbo");
            builder.Entity<ApplicationUser>(
                entity =>
                {
                    entity.ToTable("User");
                }
                );


            builder.Entity<aMBR300>()
                .Property(x => x.Amount)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed

            
           
            builder.Entity<IdentityRole>(
               entity =>
               {
                   entity.ToTable("Role");
               }
               );
            builder.Entity<IdentityUserRole<string>>(
               entity =>
               {
                   entity.ToTable("UserRoles");
               }
               );
            builder.Entity<IdentityUserClaim<string>>(
               entity =>
               {
                   entity.ToTable("UserClaims");
               }
               );
            builder.Entity<IdentityUserLogin<string>>(
               entity =>
               {
                   entity.ToTable("UserLogin");
               }
               );
            builder.Entity<IdentityRoleClaim<string>>(
               entity =>
               {
                   entity.ToTable("RoleClaims");
               }
               );
            builder.Entity<IdentityUserToken<string>>(
               entity =>
               {
                   entity.ToTable("UserTokens");
               }
               );
        }
    }
}