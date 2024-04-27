using Dapper;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using LT.dal.Abstractions;
using LT.dal.Context;
using LT.model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using static Dapper.SqlMapper;

namespace LT.Integration.Test
{
    public class DatabaseFixture : IAsyncLifetime, ILTRepository<EntityBase>
    {
        private IContainer? _container;
        public string ConnectionString { 
            get
            {
                return "Server=localhost;Initial Catalog=master;Persist Security Info=true;User ID=sa;Password=yourStrong(!)Password;Trust Server Certificate=true";
            }
        }

        public async Task InitializeAsync()
        {
            _container = new ContainerBuilder()
                .WithImage("mcr.microsoft.com/azure-sql-edge:latest")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("SA_PASSWORD", "yourStrong(!)Password")
                .WithPortBinding(1433, 1433)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
                .Build();

            await _container.StartAsync();

            await using var db = CreateDBContext();
            await db.Database.MigrateAsync();
        }

        public LTDBContext CreateDBContext()
        {
            ArgumentNullException.ThrowIfNull(_container);
            return new LTDBContext(
                new DbContextOptionsBuilder<LTDBContext>()
                    .UseSqlServer(ConnectionString)
                    .Options);
        }

        public async Task<int> EnsureAsync<TEntity>(TEntity entity)
            where TEntity : EntityBase
        {
            using var dbContext = CreateDBContext();
            dbContext.Set<TEntity>().Add(entity);
            return await dbContext.SaveChangesAsync();
        }

        public Task<TEntity[]> GetAllAsync<TEntity>(CancellationToken cancellationToken, bool asNoTracking = false)
            where TEntity : EntityBase
        {
            using var dbContext = CreateDBContext();
            var query = dbContext.Set<TEntity>();
            return query.ToArrayAsync(cancellationToken);
        }

        public async Task<int> RemoveAllAsync<TEntity>()
            where TEntity : EntityBase
        {
            using var dbContext = CreateDBContext();
            foreach(var item in dbContext.Set<TEntity>())
            {
                dbContext.Remove(item);
            }

            return await dbContext.SaveChangesAsync();
        }

        private async Task<DbConnection> OpenConnectionAsync()
        {
            var connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync();
            return connection;
        }

        public async Task<bool> RepeatScalarQueryUntilAsync<T>(CommandDefinition cmd, Func<T, bool> predicate, CancellationToken cancellationToken)
        {
            while(true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var conn = await OpenConnectionAsync())
                {
                    var result = await conn.ExecuteScalarAsync<T>(cmd);
                    if(result is not null)
                    {
                        if (predicate(result))
                        {
                            return true;
                        }
                    }
                }

                await Task.Delay(200, cancellationToken);
            }
        }

        public async Task DisposeAsync()
        {
            if(_container is not null) { 
                await _container.DisposeAsync();
            }
        }

        public async Task Add(EntityBase entity)
        {
            using var dbContext = CreateDBContext();
            await dbContext.Set<EntityBase>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return;
        }

        public async void Add(IEnumerable<EntityBase> entities)
        {
            using var dbContext = CreateDBContext();
            await dbContext.Set<EntityBase>().AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
            return;
        }

        public void Remove(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(IEnumerable<EntityBase> entities)
        {
            throw new NotImplementedException();
        }

        public Task Update(EntityBase entity)
        {
            throw new NotImplementedException();
        }

        public ValueTask<EntityBase?> FindByIdAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<EntityBase[]> FindAsync(ISpecification<EntityBase> spec, CancellationToken cancellationToken, bool asNoTracking = false, string? includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<EntityBase[]> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            throw new NotImplementedException();
        }

        Task ILTRepository<EntityBase>.Remove(EntityBase entity)
        {
            throw new NotImplementedException();
        }
    }
}
