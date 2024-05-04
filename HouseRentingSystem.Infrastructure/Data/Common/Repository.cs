﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Infrastructure.Data.Common
{
    public class Repository : IRepository
    {
        private readonly DbContext context;

        public Repository(HouseRentingSystemDbContext _context)
        {
            context = _context;
        }

        private DbSet<T> DbSet<T>() where T : class 
        {
            return context.Set<T>();
        }

        public IQueryable<T> All<T>() where T : class 
        {
            return DbSet<T>();
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return DbSet<T>()
                .AsNoTracking();
        }
    }
}
