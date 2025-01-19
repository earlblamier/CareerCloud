using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {
        private readonly CareerCloudContext _context;

        // Default constructor for backward compatibility
        public EFGenericRepository()
        {
            var options = new DbContextOptionsBuilder<CareerCloudContext>()
                .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True")
                .Options;
            _context = new CareerCloudContext(options);
        }

        // Constructor accepting CareerCloudContext via dependency injection
        public EFGenericRepository(CareerCloudContext context)
        {
            _context = context;
        }

        public void Add(params T[] items)
        {
            _context.Set<T>().AddRange(items);
            _context.SaveChanges();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var property in navigationProperties)
            {
                query = query.Include(property);
            }
            return query.ToList();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var property in navigationProperties)
            {
                query = query.Include(property);
            }
            return query.Where(where).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var property in navigationProperties)
            {
                query = query.Include(property);
            }
            return query.FirstOrDefault(where);
        }

        public void Remove(params T[] items)
        {
            _context.Set<T>().RemoveRange(items);
            _context.SaveChanges();
        }

        public void Update(params T[] items)
        {
            _context.Set<T>().UpdateRange(items);
            _context.SaveChanges();
        }
    }
}