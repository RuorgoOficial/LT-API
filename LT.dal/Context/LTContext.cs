﻿using LT.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Context
{
    public class LTDBContext : DbContext
    {
        public LTDBContext(DbContextOptions<LTDBContext> options) : base(options) { 
        }

        public DbSet<EntityTest> Test { get; set; }
        public DbSet<EntityScore> Score { get; set; }
    }
}
