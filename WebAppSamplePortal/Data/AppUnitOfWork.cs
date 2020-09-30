using System;
using System.Data;
using System.Data.Common;
using WebAppSamplePortal.Data.Abstractions;

namespace WebAppSamplePortal.Data
{
    public class AppUnitOfWork : UnitOfWorkProperty
    {
        public override void SaveWork()
        {
            var connection = Property<DbConnection>() ?? throw new NullReferenceException("DbConnection não configurado");
            var transaction = Property<DbTransaction>() ?? throw new NullReferenceException("DbTransaction não configurado");

            if (connection.State != ConnectionState.Open)
                throw new InvalidOperationException();

            transaction.Commit();
        }
    }
}
