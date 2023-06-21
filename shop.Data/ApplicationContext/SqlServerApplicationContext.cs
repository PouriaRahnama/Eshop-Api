using System.Data.Common;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using shop.Data.Extension;
using Microsoft.Extensions.Options;

namespace shop.Data.ApplicationContext
{
    public class SqlServerApplicationContext : DbContext, IApplicationContext
    {
        public SqlServerApplicationContext(DbContextOptions<SqlServerApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerApplicationContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }


        //Store Procedure
        public List<T> RunSp<T>(string StoreName, List<DbParamter> ListParamert) where T : new()
        {
            Database.OpenConnection();
            DbCommand cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = StoreName;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var item in ListParamert)
            {
                cmd.Parameters.Add(new SqlParameter { ParameterName = item.ParametrName, Value = item.Value });
            }
            List<T> list = new List<T>();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader != null && reader.HasRows)
                {
                    var entity = typeof(T);
                    var propDict = new Dictionary<string, PropertyInfo>();
                    var props = entity.GetProperties
           (BindingFlags.Instance | BindingFlags.Public);
                    propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);
                    while (reader.Read())
                    {
                        T newobject = new T();

                        for (int index = 0; index < reader.FieldCount; index++)
                        {
                            if (propDict.ContainsKey(reader.GetName(index).ToUpper()))
                            {
                                var info = propDict[reader.GetName(index).ToUpper()];
                                if (info != null && info.CanWrite)
                                {
                                    var val = reader.GetValue(index);
                                    info.SetValue(newobject, val == DBNull.Value ? null : val, null);
                                }
                            }
                        }
                        list.Add(newobject);
                    }
                }
                Database.CloseConnection();
                return list;
            }

        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                CleanContext();
                throw ex;
            }
        }

        
        private void CleanContext()
        {
            if (ChangeTracker.HasChanges())
            {
                var _list = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified || p.State == EntityState.Added || p.State == EntityState.Deleted).ToList();
                foreach (var item in _list)
                {
                    item.State = EntityState.Unchanged;
                }
            }
        }
    }
}
