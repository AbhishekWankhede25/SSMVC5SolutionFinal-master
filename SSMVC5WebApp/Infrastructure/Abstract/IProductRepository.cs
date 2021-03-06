﻿using System.Threading.Tasks;
using System.Collections.Generic;

using SSMVC5WebApp.Infrastructure.Entities;

namespace SSMVC5WebApp.Infrastructure.Abstract
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<Product> FindProductByIDAsync(int productId);

        Task<List<Product>> FindProductsByCategoryAsync(string category);

        Task CreateAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(int productId);
    }
}
