using System.Data.Common;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using shop.Data.Extension;
using Microsoft.Extensions.Options;
using shop.Core.Domain.Category;
using shop.Core.Domain.Comment;
using shop.Core.Domain.Order;
using shop.Core.Domain.Product;
using shop.Core.Domain.Role;
using shop.Core.Domain.Seller;
using shop.Core.Domain.Slider;
using shop.Core.Domain.User;

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

        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderAddress> OrderAddress { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductPicture> ProductPicture { get; set; }
        public DbSet<ProductSpecification> ProductSpecification { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SellerInventory> SellerInventory { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserToken> UserToken { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
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
