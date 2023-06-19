using AutoMapper.Configuration;
using LabClothingCollectionAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabClothingCollectionAPI.DbContexts
{
    public class LabClothingContext: DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Collection> Collections { get; set; } = null!;
        public DbSet<Model> Models { get; set; } = null!;

        public LabClothingContext(DbContextOptions<LabClothingContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User("João dos Santos", "Masculino", "joao@joao.com", "098.098.349-48", "04832631026")
                {
                    Id = 1,
                    BirthDate = new DateTime(1989, 09, 06),
                    Type = UserType.Administrador,
                    Status = Status.Ativo
                },
                new User("Jose dos Santos", "Masculino", "jose@jose.com", "043.098.349-48", "04832631526")
                {
                    Id = 2,
                    BirthDate = new DateTime(1990, 09, 06),
                    Type = UserType.Administrador,
                    Status = Status.Ativo
                },
                new User("John dos Santos", "Masculino", "john@john.com", "098.054.349-48", "04742631026")
                {
                    Id = 3,
                    BirthDate = new DateTime(1989, 10, 06),
                    Type = UserType.Administrador,
                    Status = Status.Ativo
                },
                new User("Jonas dos Santos", "Masculino", "jonas@jonas.com", "098.098.398-48", "04832631666")
                {
                    Id = 4,
                    BirthDate = new DateTime(1989, 09, 06),
                    Type = UserType.Administrador,
                    Status = Status.Ativo
                },
                new User("Jordão dos Santos", "Masculino", "jordao@jordao.com", "054.098.349-48", "05632631026")
                {
                    Id = 5,
                    BirthDate = new DateTime(1989, 09, 15),
                    Type = UserType.Administrador,
                    Status = Status.Ativo
                });
        }
    }
}
