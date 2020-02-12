using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Context
{
    public interface IDataGenerator
    {
        public void Generate(ModelBuilder modelBuilder);
    }
}
