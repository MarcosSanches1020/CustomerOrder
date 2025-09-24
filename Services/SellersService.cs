
using System;
using System.Net.Http;
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
            var result = new ServiceResultSellers<SellersResponseDto>();
            try
            {
                if (await SellerVerifyExisting(newSeller.Cpf))
                {
                    return new ServiceResultSellers<SellersResponseDto>
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = "Já existe um cliente cadastrado com este CPF"
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
            catch (Exception ex)
            {
                return new ServiceResultSellers<SellersResponseDto>
                {
                    Success = false,
                    StatusCode = 400,
                    Message = ex.Message
                };
            }
        }
        
        public async Task<bool> SellerVerifyExisting(string cpf)
        {
            try
            {
                return await _appDbContext.sellers
                    .AnyAsync(sellers => sellers.Cpf == cpf);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Sellers> SaveSellerAsync(Sellers newSeller)
        {
            try
            {
                _appDbContext.sellers.Add(newSeller);
                await _appDbContext.SaveChangesAsync();

                return newSeller;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

