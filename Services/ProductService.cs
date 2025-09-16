using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.API.Data;
using CustomerOrders.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrders.API.Services;

public class ProductService
{
        private readonly AppDbContext _appDbContext;

    public ProductService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Products> AddProduct(Products newProduct)
    {
        try
        {
            _appDbContext.products.Add(newProduct);
            await _appDbContext.SaveChangesAsync();

            var productSave = await _appDbContext.products.FirstOrDefaultAsync(
                p => p.NameProduct == newProduct.NameProduct);

            if (productSave == null)
            {
                throw new Exception("Erro ao inserir um novo produto");
            }

            return productSave;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Products>> GetProducts()
    {
        return await _appDbContext.products.ToListAsync();
    }

    public async Task<Products> GetProductById(int id)
    {
        return await _appDbContext.products.FindAsync(id);
    }


    public async Task<Products> UpdateProduct(int id, Products productsUpdate)
    {
        try
        {
            var existingProduct = await _appDbContext.products.FindAsync(id);

            if (existingProduct == null)
            {
                throw new Exception("Product not's valid");
            }

            if (existingProduct.NameProduct != productsUpdate.NameProduct)
            {
                if (await ProductsVerify(productsUpdate.NameProduct))
                {
                    throw new Exception("Já existe um produto com esse nome cadastrado");
                }
            }

            existingProduct.NameProduct = productsUpdate.NameProduct;
            existingProduct.TypeProduct = productsUpdate.TypeProduct;
            existingProduct.PriceProduct = productsUpdate.PriceProduct;
            existingProduct.Description = productsUpdate.Description;

            await _appDbContext.SaveChangesAsync();

            return existingProduct;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> ProductsVerify(string nameProduct)
    {
        try
        {
            return await _appDbContext.products
                .AnyAsync(products => products.NameProduct == nameProduct);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteProduct(int id)
    {
        try
        {
            var product = await _appDbContext.products.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            _appDbContext.products.Remove(product);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
