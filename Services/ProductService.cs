using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.API.Data;
using CustomerOrders.API.Models;
using Microsoft.EntityFrameworkCore;

using CustomerOrders.API.DTOs.Product;
using CustomerOrders.API.Mappings;


namespace CustomerOrders.API.Services
{
    public class ServiceProductResult<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
    }

    public class ProductService
    {
        public async Task<ServiceProductResult<ProductResponseDto>> AddProductAsync(ProductCreateDto newProduct)
        {
            if (await ProductsVerify(newProduct.NameProduct))
            {
                return new ServiceProductResult<ProductResponseDto>
                {
                    Success = false,
                    StatusCode = 400,
                    Message = "A product with this name already exists"
                };
            }
            var entity = newProduct.ToEntityProduct();
            var saved = await AddProduct(entity);
            return new ServiceProductResult<ProductResponseDto>
            {
                Success = true,
                StatusCode = 201,
                Data = saved.ToResponseProductDto(),
                Message = "Product created successfully"
            };
        }

        public async Task<List<ProductResponseDto>> GetProductsAll()
        {
            var products = await GetProducts();
            return products.ConvertAll(p => p.ToResponseProductDto());
        }

        public async Task<ServiceProductResult<ProductResponseDto>> GetProductById(int id)
        {
            var product = await GetProductId(id);
            if (product == null)
            {
                return new ServiceProductResult<ProductResponseDto>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Product not found"
                };
            }
            return new ServiceProductResult<ProductResponseDto>
            {
                Success = true,
                StatusCode = 200,
                Data = product.ToResponseProductDto()
            };
        }

        public async Task<ServiceProductResult<ProductResponseDto>> UpdateProductAsync(int id, ProductUpdateDto productUpdate)
        {
            var existing = await GetProductId(id);
            if (existing == null)
            {
                return new ServiceProductResult<ProductResponseDto>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Product not found"
                };
            }
            productUpdate.ApplyToEntity(existing);
            var updatedProduct = await UpdateProduct(id, existing);
            return new ServiceProductResult<ProductResponseDto>
            {
                Success = true,
                StatusCode = 200,
                Data = updatedProduct.ToResponseProductDto(),
                Message = "Product updated successfully"
            };
        }

        public async Task<ServiceProductResult<object>> DeleteProductById(int id)
        {
            var deleted = await DeleteProduct(id);
            if (!deleted)
            {
                return new ServiceProductResult<object>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Product not found"
                };
            }
            return new ServiceProductResult<object>
            {
                Success = true,
                StatusCode = 200,
                Message = "Product removed successfully"
            };
        }
    private readonly AppDbContext _appDbContext;

    public ProductService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Products> AddProduct(Products newProduct)
    {
        _appDbContext.products.Add(newProduct);
        await _appDbContext.SaveChangesAsync();
        var productSave = await _appDbContext.products.FirstOrDefaultAsync(
            p => p.NameProduct == newProduct.NameProduct);
        if (productSave == null)
        {
            throw new Exception("Error inserting new product");
        }
        return productSave;
    }

    public async Task<List<Products>> GetProducts()
    {
        return await _appDbContext.products.ToListAsync();
    }

    public async Task<Products> GetProductId(int id)
    {
        return await _appDbContext.products.FindAsync(id);
    }


    public async Task<Products> UpdateProduct(int id, Products productsUpdate)
    {
        var existingProduct = await _appDbContext.products.FindAsync(id);
        if (existingProduct == null)
        {
            throw new Exception("Product is not valid");
        }
        if (existingProduct.NameProduct != productsUpdate.NameProduct)
        {
            if (await ProductsVerify(productsUpdate.NameProduct))
            {
                throw new Exception("A product with this name already exists");
            }
        }
        existingProduct.NameProduct = productsUpdate.NameProduct;
        existingProduct.TypeProduct = productsUpdate.TypeProduct;
        existingProduct.PriceProduct = productsUpdate.PriceProduct;
        existingProduct.Description = productsUpdate.Description;
        await _appDbContext.SaveChangesAsync();
        return existingProduct;
    }

    public async Task<bool> ProductsVerify(string nameProduct)
    {
        return await _appDbContext.products.AnyAsync(products => products.NameProduct == nameProduct);
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var product = await _appDbContext.products.FindAsync(id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        _appDbContext.products.Remove(product);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}
}
