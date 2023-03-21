using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using treni_contact.Models.Contact;
using treni_contact.Models.Entity.Email;
using treni_contact.Models.Entity.Phone;
using treni_contact.Models.Entity.Role;
using treni_contact.Models.Entity.User;

namespace treni_contact.Configs.DataBase;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
       
        
        modelBuilder.Entity<Contact>().HasKey(x => x.Id);
        // modelBuilder.Entity<Contact>().HasMany(typeof(Phone));
        // modelBuilder.Entity<Contact>().HasMany(typeof(Email));
        
        modelBuilder.Entity<Email>().HasKey(x => x.id);
        modelBuilder.Entity<Email>().HasIndex(x => x.email).IsUnique();
        // modelBuilder.Entity<Email>().HasOne(typeof(Models.ViewModel.Contact));
        
        modelBuilder.Entity<Phone>().HasKey(x => x.id);
        modelBuilder.Entity<Phone>().HasIndex(x => x.phone).IsUnique();
        // modelBuilder.Entity<Phone>().HasOne(typeof(Models.ViewModel.Contact));
        
       

    }

    public DbSet<Contact> contacts { get; set; }
    public DbSet<Email> Email { get; set; }
    public DbSet<Phone> Phone { get; set; }
}