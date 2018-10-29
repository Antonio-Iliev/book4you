using System;
using LibrarySystem.Data.Context;
using LibrarySystem.Services.Abstract.Contracts;

namespace LibrarySystem.Services.Abstract
{
    public abstract class BaseServicesClass
    {
        protected ILibrarySystemContext context;
        protected IValidations validations;

        protected BaseServicesClass(ILibrarySystemContext context, IValidations validations)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.validations = validations ?? throw new ArgumentNullException(nameof(validations));
        }

    }
}
