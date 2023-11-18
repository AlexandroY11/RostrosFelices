﻿using Microsoft.EntityFrameworkCore;
using RostrosFelices.Models;

namespace RostrosFelices.Data
{
    public class RostrosFelicesContext : DbContext
    {
        public RostrosFelicesContext(DbContextOptions options) : base(options)
        { 
        }

        public DbSet <Client> Clients { get; set; }
       
    }
}
