using LibrarySystem.Data.Context;
using LibrarySystem.Data.Repository;
using LibrarySystem.Data.Repository.Contracts;
using System;
using System.Collections.Generic;

public class UnitOfWork
{
    private LibrarySystemContext context;
    private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

    public UnitOfWork(LibrarySystemContext context)
    {
        this.context = context;
    }

    public IRepository<T> GetRepo<T>() where T : class
    {
        var repoType = typeof(Repository<T>);

        if (!repositories.ContainsKey(repoType))
        {
            var repo = Activator.CreateInstance(repoType, this.context);
            repositories[repoType] = repo;
        }

        return (IRepository<T>)repositories[repoType];
    }

    public int SaveChanges()
    {
        return this.context.SaveChanges();
    }
}