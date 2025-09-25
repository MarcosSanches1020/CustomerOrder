
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.API.Data;
using CustomerOrders.API.DTOs.Sellers;
using CustomerOrders.API.Mappings;
using CustomerOrders.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrders.API.Services
{
    public class ServiceResultSellers<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Datas { get; set; }
    }

    public class SellersService
    {
        private readonly AppDbContext _appDbContext;

        public SellersService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ServiceResultSellers<SellersResponseDto>> createNewSellerAsync(SellersCreateDto newSeller)
        {
            if (await SellerCpfExists(newSeller.Cpf))
            {
                return new ServiceResultSellers<SellersResponseDto>
                {
                    Success = false,
                    StatusCode = 400,
                    Message = "A seller with this CPF already exists"
                };
            }
            var entity = newSeller.ToEntity();
            var saved = await SaveSellerAsync(entity);
            return new ServiceResultSellers<SellersResponseDto>
            {
                Success = true,
                StatusCode = 201,
                Datas = saved.ToResponseDto(),
                Message = "Seller created successfully"
            };
        }

        public async Task<List<SellersResponseDto>> GetCustomersAll()
        {
            var sellers = await GetAllSellers();
            return sellers.ConvertAll(s => s.ToResponseDto());
        }

        public async Task<ServiceResultSellers<SellersResponseDto>> GetSellerId(int id)
        {
            var seller = await GetSellerById(id);
            if (seller == null)
            {
                return new ServiceResultSellers<SellersResponseDto>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Seller not found"
                };
            }
            return new ServiceResultSellers<SellersResponseDto>
            {
                Success = true,
                StatusCode = 200,
                Datas = seller.ToResponseDto()
            };
        }

        public async Task<ServiceResultSellers<SellersResponseDto>> updateSellerId(int id, SellersUpdateDto sellersUpdateDto)
        {
            var sellerExisting = await GetSellerById(id);
            if (sellerExisting == null)
            {
                return new ServiceResultSellers<SellersResponseDto>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Seller not found"
                };
            }
            if (sellerExisting.Cpf == sellersUpdateDto.Cpf)
            {
                return new ServiceResultSellers<SellersResponseDto>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Seller with this CPF already exists"
                };
            }
            sellersUpdateDto.ApplyToEntity(sellerExisting);
            var updatedSeller = await UpdateSellerAsync(id, sellerExisting);
            return new ServiceResultSellers<SellersResponseDto>
            {
                Success = true,
                StatusCode = 200,
                Datas = updatedSeller.ToResponseDto(),
                Message = "Seller updated successfully"
            };
        }

        public async Task<ServiceResultSellers<Object>> DeleteSellerId(int id)
        {
            var sellerExisting = await GetSellerById(id);
            if (sellerExisting == null)
            {
                return new ServiceResultSellers<Object>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Seller not found"
                };
            }
            await DeleteSellerAsync(sellerExisting);
            return new ServiceResultSellers<Object>
            {
                Success = true,
                StatusCode = 200,
                Message = "Seller deleted successfully"
            };
        }

        private async Task<bool> SellerCpfExists(string cpf)
        {
            return await _appDbContext.sellers.AnyAsync(s => s.Cpf == cpf);
        }

        private async Task<Sellers> SaveSellerAsync(Sellers seller)
        {
            _appDbContext.sellers.Add(seller);
            await _appDbContext.SaveChangesAsync();
            return seller;
        }

        private async Task<List<Sellers>> GetAllSellers()
        {
            return await _appDbContext.sellers.ToListAsync();
        }

        private async Task<Sellers> GetSellerById(int id)
        {
            return await _appDbContext.sellers.FindAsync(id);
        }

        private async Task<Sellers> UpdateSellerAsync(int id, Sellers sellerUpdate)
        {
            var existingSeller = await _appDbContext.sellers.FindAsync(id);
            if (existingSeller == null)
            {
                return null;
            }
            
            existingSeller.Name = sellerUpdate.Name;
            existingSeller.Cpf = sellerUpdate.Cpf;
            existingSeller.Email = sellerUpdate.Email;
            existingSeller.Phone = sellerUpdate.Phone;
            existingSeller.DataUpdate = DateTime.UtcNow;
            await _appDbContext.SaveChangesAsync();
            return existingSeller;
        }

        private async Task DeleteSellerAsync(Sellers seller)
        {
            _appDbContext.sellers.Remove(seller);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

