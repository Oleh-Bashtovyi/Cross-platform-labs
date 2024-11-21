using Lab6.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public ApplicationDbContext()
        {
        }

        public DbSet<DiveOrganisation> DiveOrganisations { get; set; }
        public DbSet<LevelOfCertification> LevelsOfCertification { get; set; }
        public DbSet<Diver> Divers { get; set; }
        public DbSet<DiverCertification> DiverCertifications { get; set; }
        public DbSet<DiveSiteType> DiveSiteTypes { get; set; }
        public DbSet<DiveSite> DiveSites { get; set; }
        public DbSet<Dive> Dives { get; set; }
        public DbSet<Wreck> Wrecks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite keys
            modelBuilder.Entity<DiverCertification>()
                .HasKey(dc => new { dc.DiverId, dc.CertificationCode });

            // Configure relationships
            modelBuilder.Entity<LevelOfCertification>()
                .HasOne(lc => lc.DiveOrganisation)
                .WithMany(d => d.Certifications)
                .HasForeignKey(lc => lc.OrganisationCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiverCertification>()
                .HasOne(dc => dc.Diver)
                .WithMany(d => d.Certifications)
                .HasForeignKey(dc => dc.DiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiverCertification>()
                .HasOne(dc => dc.Certification)
                .WithMany(lc => lc.DiverCertifications)
                .HasForeignKey(dc => dc.CertificationCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiveSite>()
                .HasOne(ds => ds.DiveSiteType)
                .WithMany(dst => dst.DiveSites)
                .HasForeignKey(ds => ds.DiveSiteCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Dive>()
                .HasOne(d => d.Diver)
                .WithMany(dv => dv.Dives)
                .HasForeignKey(d => d.DiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Dive>()
                .HasOne(d => d.DiveSite)
                .WithMany(ds => ds.Dives)
                .HasForeignKey(d => d.DiveSiteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wreck>()
                .HasOne(w => w.DiveSite)
                .WithOne(ds => ds.Wreck)
                .HasForeignKey<Wreck>(w => w.DiveSiteId)
                .OnDelete(DeleteBehavior.Cascade);

        }


        public void SeedData()
        {
            if (Divers.Any() || DiveSiteTypes.Any() || DiveSites.Any() || Dives.Any()) return;

            var divers = new List<Diver>
            {
                new Diver
                {
                    DiverId = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                    DiverName = "Jhon Doe",
                    DiverDetails = "Experienced diver"
                },
                new Diver
                {
                    DiverId = Guid.NewGuid(),
                    DiverName = "France Kafka",
                    DiverDetails = "Beginner diver"
                },
                new Diver
                {
                    DiverId = Guid.NewGuid(),
                    DiverName = "Geralt from Rivia",
                    DiverDetails = "Intermediate diver"
                },
                new Diver
                {
                    DiverId = Guid.NewGuid(),
                    DiverName = "Some Guy",
                    DiverDetails = "Advanced diver"
                },
                new Diver
                {
                    DiverId = Guid.NewGuid(),
                    DiverName = "Henry Martines",
                    DiverDetails = "Expert diver"
                }
            };
            Divers.AddRange(divers);

            var diveSiteTypes = new List<DiveSiteType>
            {
                new DiveSiteType
                {
                    DiveSiteCode = "CAVE",
                    DiveSiteDetails = "Cave diving site"
                },
                new DiveSiteType
                {
                    DiveSiteCode = "REEF",
                    DiveSiteDetails = "Coral reef site"
                },
                new DiveSiteType
                {
                    DiveSiteCode = "WRECK",
                    DiveSiteDetails = "Shipwreck diving site"
                },
                new DiveSiteType
                {
                    DiveSiteCode = "SHORE",
                    DiveSiteDetails = "Shore diving site"
                },
                new DiveSiteType
                {
                    DiveSiteCode = "OPEN",
                    DiveSiteDetails = "Open water site"
                }
            };
            DiveSiteTypes.AddRange(diveSiteTypes);

            var diveSites = new List<DiveSite>
            {
                new DiveSite
                {
                    DiveSiteId = Guid.NewGuid(),
                    DiveSiteCode = "CAVE",
                    DiveSiteName = "Mystery Cave",
                    DiveSiteDescription = "Deep cave with tunnels",
                    OtherDetails = "Requires advanced certification"
                },
                new DiveSite
                {
                    DiveSiteId = new Guid("d27ca42c-58cc-4372-a567-0e02b2c3d479"),
                    DiveSiteCode = "REEF",
                    DiveSiteName = "Coral Garden",
                    DiveSiteDescription = "Colorful coral reef",
                    OtherDetails = "Good for beginners"
                },
                new DiveSite
                {
                    DiveSiteId = Guid.NewGuid(),
                    DiveSiteCode = "WRECK",
                    DiveSiteName = "Sunken Ship",
                    DiveSiteDescription = "Historic wreck site",
                    OtherDetails = "Advanced level"
                },
                new DiveSite
                {
                    DiveSiteId = Guid.NewGuid(),
                    DiveSiteCode = "SHORE",
                    DiveSiteName = "Blue Beach",
                    DiveSiteDescription = "Shallow shore dive",
                    OtherDetails = "Good visibility"
                },
                new DiveSite
                {
                    DiveSiteId = Guid.NewGuid(),
                    DiveSiteCode = "OPEN",
                    DiveSiteName = "Open Sea",
                    DiveSiteDescription = "Deep open water",
                    OtherDetails = "Requires open water certification"
                }
            };
            DiveSites.AddRange(diveSites);

            var dives = new List<Dive>
            {
                new Dive
                {
                    DiveId = Guid.NewGuid(),
                    DiverId = divers[0].DiverId,
                    DiveSiteId = diveSites[0].DiveSiteId,
                    DiveDate = DateTime.UtcNow.AddDays(-30),  
                    NightDiveYn = true,
                    OtherDetails = "Night dive in the cave"
                },
                new Dive
                {
                    DiveId = Guid.NewGuid(),
                    DiverId = divers[1].DiverId,
                    DiveSiteId = diveSites[1].DiveSiteId,
                    DiveDate = DateTime.UtcNow.AddDays(-20),  
                    NightDiveYn = false,
                    OtherDetails = "Coral reef exploration"
                },
                new Dive
                {
                    DiveId = Guid.NewGuid(),
                    DiverId = divers[2].DiverId,
                    DiveSiteId = diveSites[2].DiveSiteId,
                    DiveDate = DateTime.UtcNow.AddDays(-10),  
                    NightDiveYn = true,
                    OtherDetails = "Exploring the wreck"
                },
                new Dive
                {
                    DiveId = Guid.NewGuid(),
                    DiverId = divers[3].DiverId,
                    DiveSiteId = diveSites[3].DiveSiteId,
                    DiveDate = DateTime.UtcNow.AddDays(-5),  
                    NightDiveYn = false,
                    OtherDetails = "Shore diving"
                },
                new Dive
                {
                    DiveId = Guid.NewGuid(),
                    DiverId = divers[4].DiverId,
                    DiveSiteId = diveSites[4].DiveSiteId,
                    DiveDate = DateTime.UtcNow,  
                    NightDiveYn = true,
                    OtherDetails = "Deep open sea dive"
                }
            };
            Dives.AddRange(dives);

            var wrecks = new List<Wreck>
            {
                new Wreck
                {
                    DiveSiteId = diveSites[2].DiveSiteId,
                    WreckDate = DateTime.UtcNow.AddDays(-10),
                    WreckDetails = "Historic shipwreck from World War II, located at a depth of 40 meters",
                },
                new Wreck
                {
                    DiveSiteId = diveSites[4].DiveSiteId,
                    WreckDate = DateTime.UtcNow.AddDays(-100),
                    WreckDetails = "Historic shipwreck, located at a depth of 20 meters",
                }
            };
            Wrecks.AddRange(wrecks);

            SaveChanges();
        }
    }
}
