using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Roster.Data.DBAccessor
{
    public partial class DataClassesDataContext : DbContext, IDbContext
    {
        private DbContextTransaction transaction;
            
        IDbSet<T> IDbContext.Set<T>()
        {
            return base.Set<T>();
        }

        //new void Dispose()
        //{
        //    base.Dispose();
        //}

        int IDbContext.SaveChanges()
        {
            int result = base.SaveChanges();
            return result;
        }      

        public DataClassesDataContext(string sqlConnection)
            : base(sqlConnection)
        {
        }

        DbEntityEntry<T> IDbContext.Entry<T>(T t)
        {
            return base.Entry<T>(t);
        }

        public void BeginTransaction()
        {
            this.transaction = this.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        //public int ExecuteSqlCommmant(string sql, object[] parameters)
        //{
        //   int result = 0;
        //   string connectionString = this.Database.Connection.ConnectionString;
        //   using (SqlConnection connection =
        //       new SqlConnection(connectionString))
        //   {
        //       // Create the Command and Parameter objects.
        //       SqlCommand command = new SqlCommand(sql, connection);
        //       command.CommandTimeout = 3000;
        //       command.CommandType = System.Data.CommandType.StoredProcedure;
        //       command.Parameters.AddRange(parameters);
        //       // Open the connection in a try/catch block. 
        //       // Create and execute the DataReader, writing the result
        //       // set to the console window.
        //       try
        //       {
        //           connection.Open();
        //           result = command.ExecuteNonQuery();
        //           connection.Close();
        //       }
        //       catch 
        //       {
        //           result = 0;
        //       }
        //   }
        //   return result;
        //}

        public int ExecuteSqlCommmant(string sql, object[] parameters)
        {
            return this.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IEnumerable<T> ExecWithStoreProcedure<T>(string query, params object[] parameters)
        {
            return this.Database.SqlQuery<T>(query, parameters);
        }


        public void Commit()
        {
            if (this.transaction != null)
            {
                this.transaction.Commit();
                this.transaction = null;
            }
        }

        public void Rollback()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
                this.transaction = null;
            }
        }


       
    }
}
