using KOULIBALY.Models.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    Id = 1,
                    Name = "FC Barcelona",
                    Strength = 9
                },
                new Team
                {
                    Id = 2,
                    Name = "Liverpool FC",
                    Strength = 8
                },
                new Team
                {
                    Id = 3,
                    Name = "Borussia Dortmund",
                    Strength = 8
                },
                new Team
                {
                    Id = 4,
                    Name = "AC Milan",
                    Strength = 7
                }
                );

        }
    }
}
