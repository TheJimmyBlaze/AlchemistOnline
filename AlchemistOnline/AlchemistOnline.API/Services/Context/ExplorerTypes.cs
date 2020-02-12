using AlchemistOnline.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Context
{
    public class ExplorerTypes : IDataGenerator
    {
        public const int CARTOGRAPHER = 1;
        public const int TRACKER = 2;
        public const int HUNTER = 3;

        private readonly List<ExplorerType> definitions = new List<ExplorerType>
        {
            new ExplorerType
            {
                ExplorerTypeID = CARTOGRAPHER,
                Name = "Cartographer",
                ImagePath = "Cartographer.png"
            },
            new ExplorerType
            {
                ExplorerTypeID = TRACKER,
                Name = "Tracker",
                ImagePath = "Tracker.png"
            },
            new ExplorerType
            {
                ExplorerTypeID = HUNTER,
                Name = "Hunter",
                ImagePath = "Hunter.png"
            }
        };

        public void Generate(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExplorerType>().HasData(definitions);
        }
    }
}
