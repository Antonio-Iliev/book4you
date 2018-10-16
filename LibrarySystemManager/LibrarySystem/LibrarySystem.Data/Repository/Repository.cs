﻿using LibrarySystem.Data.Context;
using LibrarySystem.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace LibrarySystem.Data.Repository
{
    public class Repository<T> : IRepository<T>
       where T : class
    {
        private readonly LibrarySystemContext context;

        public Repository(LibrarySystemContext context)
        {
            this.context = context;
        }

        public IQueryable<T> All()
        {
            return this.context.Set<T>();
        }


        public void Add(T entity)
        {
            EntityEntry entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.context.Set<T>().Add(entity);
            }
        }

        public void Update(T entity)
        {
            EntityEntry entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
