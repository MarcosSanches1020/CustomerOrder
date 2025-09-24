
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
            var result = new ServiceResultSellers<SellersResponseDto>();

            var entityExisting = await _appDbContext.sellers
                   .AnyAsync(sellers => sellers.Cpf == newSeller.Cpf);
            try
            {
                if (entityExisting.Equals(true))
                {
                    return new ServiceResultSellers<SellersResponseDto>
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = "Já existe um cliente cadastrado com este CPF"
                    };
                }

                var entity = newSeller.ToEntity();
                var saved = _appDbContext.sellers.Add(entity);
                await _appDbContext.SaveChangesAsync();

                return new ServiceResultSellers<SellersResponseDto>
                {
                    Success = true,
                    StatusCode = 201,
                    Datas = saved.Entity.ToResponseDto(),
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

        public async Task<List<SellersResponseDto>> GetCustomersAll()
        {
            var sellers = await _appDbContext.sellers.ToListAsync();
            return sellers.ConvertAll(s => s.ToResponseDto());
        }

        public async Task<ServiceResultSellers<SellersResponseDto>> GetSellerId(int id)
        {
            try
            {
                var sellers = await _appDbContext.sellers.FindAsync(id);
                if (sellers == null)
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
                    Datas = sellers.ToResponseDto()
                };
            }
            catch (Exception ex)
            {
                return new ServiceResultSellers<SellersResponseDto>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
    }
}

