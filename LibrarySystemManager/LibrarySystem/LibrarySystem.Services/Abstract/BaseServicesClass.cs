using System;
using LibrarySystem.Data.Contracts;

namespace LibrarySystem.Services.Abstract
{
    public abstract class BaseServicesClass
    {
        protected ILibrarySystemContext context;

        protected BaseServicesClass(ILibrarySystemContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

    }
}
