using System;

namespace WebAppSamplePortal.Data.Abstractions
{
    public class UnitOfWorkProperty<T>
        where T : class
    {
        private readonly T _property;

        public UnitOfWorkProperty(UnitOfWorkProperty uow)
        {
            _property = uow.Property<T>()
                ?? throw new NullReferenceException();
        }

        public static implicit operator T(UnitOfWorkProperty<T> p)
        {
            return p._property;
        }
    }
}
