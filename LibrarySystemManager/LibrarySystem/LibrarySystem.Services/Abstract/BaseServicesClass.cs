using System;
using LibrarySystem.Data.Context;

namespace LibrarySystem.Services.Abstract
{
    public abstract class BaseServicesClass
    {
        protected ILibSysContext context;

        protected BaseServicesClass(ILibSysContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
