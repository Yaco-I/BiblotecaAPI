using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Biblioteca.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, IAuditableEntity, new() 
    {
        private readonly BibilotecaDbContext _bibilotecaDbContext;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Repository(BibilotecaDbContext bibilotecaDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _bibilotecaDbContext = bibilotecaDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {

            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value; 
            entity.LastModifiedBy = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            entity.LastModifiedDate = DateTime.Now;
            await _bibilotecaDbContext.Set<T>().AddAsync(entity);
            await _bibilotecaDbContext.SaveChangesAsync();
            return entity;
            }
            catch(Exception e)
            {
                throw new Exception("No se pudo insertar el registro", e);
            }
        }
        public async Task<T> UpdateAsync(T entity)
        {
            var result = await _bibilotecaDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result == null)
            {
                throw new Exception("No se encontró el registro para actualizar");
            }

            entity.LastModifiedBy = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            entity.LastModifiedDate = DateTime.Now;
            entity.CreatedDate = result.CreatedDate;
            entity.CreatedBy = result.CreatedBy;
            _bibilotecaDbContext.Update(entity);
            await _bibilotecaDbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = _bibilotecaDbContext.Set<T>().FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                //TODO: Hacer un log y exception 
                return false;
            }

            _bibilotecaDbContext.Set<T>().Remove(entity);
            _bibilotecaDbContext.SaveChanges();
            return true;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _bibilotecaDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
            //var result = await _bibilotecaDbContext.Set<T>().ToListAsync();
                return await _bibilotecaDbContext.Set<T>().ToListAsync();

            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<T>> SearchASync(Expression<Func<T, bool>> predicate)
        {
            var result = await _bibilotecaDbContext.Set<T>().Where(predicate).ToListAsync();
            return result;
        }


    }
}
