using AutoMapper.Configuration;
using LabClothingCollectionAPI.Entities;
using Microsoft.EntityFrameworkCore;
using LabClothingCollectionAPI.Enums;

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
                new User("João dos Santos", "Masculino", "joao@joao.com", "098.098.349-48", "04832631026", "Administrador", "Ativo")
                {
                    Id = 1,
                    BirthDate = new DateTime(1989, 09, 06),
                },
                new User("Jose dos Santos", "Masculino", "jose@jose.com", "043.098.349-48", "04832631526", "Administrador", "Ativo")
                {
                    Id = 2,
                    BirthDate = new DateTime(1990, 09, 06),
                },
                new User("John dos Santos", "Masculino", "john@john.com", "098.054.349-48", "04742631026", "Administrador", "Ativo")
                {
                    Id = 3,
                    BirthDate = new DateTime(1989, 10, 06),
                },
                new User("Jonas dos Santos", "Masculino", "jonas@jonas.com", "098.098.398-48", "04832631666", "Administrador", "Ativo")
                {
                    Id = 4,
                    BirthDate = new DateTime(1989, 09, 06),
                },
                new User("Jordão dos Santos", "Masculino", "jordao@jordao.com", "054.098.349-48", "05632631026", "Administrador", "Ativo")
                {
                    Id = 5,
                    BirthDate = new DateTime(1989, 09, 15),
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
