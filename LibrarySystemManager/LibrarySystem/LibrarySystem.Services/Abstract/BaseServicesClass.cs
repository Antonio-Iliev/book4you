using System;
using LibrarySystem.Services.Abstract.Contracts;

namespace LibrarySystem.Services.Abstract
{
    public abstract class BaseServicesClass
    {
        protected UnitOfWork unitOfWork;
        protected IValidations validations;

        protected BaseServicesClass(UnitOfWork unitOfWork, IValidations validations)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(validations));
            this.validations = validations ?? throw new ArgumentNullException(nameof(validations));
        }

    }
}
