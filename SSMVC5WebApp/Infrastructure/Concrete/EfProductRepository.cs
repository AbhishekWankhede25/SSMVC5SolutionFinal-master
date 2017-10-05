using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

using SSMVC5WebApp.Infrastructure.Abstract;
using SSMVC5WebApp.Infrastructure.Entities;

namespace SSMVC5WebApp.Infrastructure.Concrete
{
    public class EfProductRepository : IProductRepository, IDisposable
    {
        SportsStoreDbContext _context;
        ILogger _log;

        public EfProductRepository(ILogger logger)
        {
            _context = new SportsStoreDbContext();
            _log = logger;
        }

        #region IProductRespository Members

        #region With Time taken to execute
        //async public Task CreateAsync(Product product)
        //{
        //    Stopwatch timeSpan = Stopwatch.StartNew();
        //    try
        //    {
        //        _context.Products.Add(product);
        //        await _context.SaveChangesAsync();
        //        timeSpan.Stop();
        //        _log.TraceApi("SQL Database", "ProductRepository.CreateAsync", timeSpan.Elapsed, "product={0}", product);
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex, "Errror in ProductRepository.CreateAsync(product={0})", product);
        //        throw;
        //    }
        //} 
        #endregion

        public async Task CreateAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                _log.TraceApi("SQL Database", "ProductRepository.CreateAsync", "product={0}", product);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error in ProductRepository.CreateAsync(product={0})", product);
                throw;
            }
        }

        public async Task DeleteAsync(int productId)
        {
            try
            {
                Product prod = await _context.Products.FindAsync(productId);
                _context.Products.Remove(prod);
                await _context.SaveChangesAsync();
                _log.TraceApi("SQL Database", "ProductRepository.DeleteAsync", "productId={0}", productId);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Errror in ProductRepository.DeleteAsync(productId={0})", productId);
                throw;
            }
        }

        public async Task<Product> FindProductByIDAsync(int productId)
        {
            Product product = null;
            try
            {
                product = await _context.Products.FindAsync(productId);
                _log.TraceApi("SQL Database", "ProductRepository.FindProductBuIDAsync", "productId={0}", productId);
                return product;
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error in  ProductRepository.FindProductBuIDAsync(productId={0})", productId);
                throw;
            }

        }

        public async Task<List<Product>> FindProductsByCategoryAsync(string category)
        {
            try
            {
                var result = await _context.Products.Where(p => p.Category == category).ToListAsync();
                _log.TraceApi("SQL Database", "ProductRepository.FindProductsByCategoryAsync", "category={0}", category);
                return result;
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Errror in ProductRepository.FindProductsByCategoryAsync(category={0})", category);
                throw;
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                var productsList = await _context.Products.ToListAsync();
                _log.TraceApi("SQL Database", "ProductRepository.GetAllProductsAsync");
                return productsList;
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error in  ProductRepository.GetAllProductsAsync");
                throw;
            }

        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _log.TraceApi("SQL Database", "ProductRepository.UpdateAsync", "product={0}", product);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Errror in ProductRepository.UpdateAsync(product={0})", product);
                throw;
            }
        }

        #endregion

        #region IDisposable Memeber
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Free managed resources
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

    }
}