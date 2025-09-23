using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.API.Data;
using CustomerOrders.API.Models;
using Microsoft.EntityFrameworkCore;
using CustomerOrders.API.DTOs.Customer;
using CustomerOrders.API.Mappings;


namespace CustomerOrders.API.Services
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
    }

    public class CustomerService
    {

        public async Task<ServiceResult<CustomerResponseDto>> AddCustomerAsync(CustomerCreateDto newCustomer)
        {
            try
            {
                if (await CustomerVerify(newCustomer.Cpf))
                {
                    return new ServiceResult<CustomerResponseDto>
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = "Já existe um cliente cadastrado com este CPF"
                    };
                }
                var entity = newCustomer.ToEntity();
                var saved = await SaveCustomerAsync(entity);
                return new ServiceResult<CustomerResponseDto>
                {
                    Success = true,
                    StatusCode = 201,
                    Data = saved.ToResponseDto(),
                    Message = "Cliente criado com sucesso"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<CustomerResponseDto>
                {
                    Success = false,
                    StatusCode = 400,
                    Message = ex.Message
                };
            }
        }

        public async Task<List<CustomerResponseDto>> GetCustomersAll()
        {
            var customers = await AllCustumers();
            return customers.ConvertAll(c => c.ToResponseDto());
        }


        public async Task<ServiceResult<CustomerResponseDto>> GetCustomerById(int id)
        {
            try
            {
                var customer = await GetCustomerId(id);
                if (customer == null)
                {
                    return new ServiceResult<CustomerResponseDto>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = "Cliente não encontrado"
                    };
                }
                return new ServiceResult<CustomerResponseDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Data = customer.ToResponseDto()
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<CustomerResponseDto>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }


        public async Task<ServiceResult<CustomerResponseDto>> UpdateCustomerId(int id, CustomerUpdateDto customerUpdate)
        {
            try
            {
                var existing = await GetCustomerId(id);
                if (existing == null)
                {
                    return new ServiceResult<CustomerResponseDto>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = "Cliente não encontrado"
                    };
                }
                customerUpdate.ApplyToEntity(existing);
                var updatedCustomer = await UpdateCustomerAsync(id, existing);
                return new ServiceResult<CustomerResponseDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Data = updatedCustomer.ToResponseDto(),
                    Message = "Cliente atualizado com sucesso"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<CustomerResponseDto>
                {
                    Success = false,
                    StatusCode = 400,
                    Message = ex.Message
                };
            }
        }


        public async Task<ServiceResult<object>> DeleteCustomeID(int id)
        {
            try
            {
                var deleted = await DeleteCustomer(id);
                if (!deleted)
                {
                    return new ServiceResult<object>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = "Cliente não encontrado"
                    };
                }
                return new ServiceResult<object>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Cliente removido com sucesso"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<object>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        private readonly AppDbContext _appDbContext;

        public CustomerService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Customer> SaveCustomerAsync(Customer newCustomer)
        {
            try
            {
                _appDbContext.customers.Add(newCustomer);
                await _appDbContext.SaveChangesAsync();

                return newCustomer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Customer>> AllCustumers()
        {
            return await _appDbContext.customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerId(int id)
        {
            return await _appDbContext.customers.FindAsync(id);
        }


        public async Task<Customer> UpdateCustomerAsync(int id, Customer customerUpdate)
        {
            try
            {
                var existingCustomer = await _appDbContext.customers.FindAsync(id);

                if (existingCustomer == null)
                {
                    return null;
                }

                if (existingCustomer.Cpf != customerUpdate.Cpf)
                {
                    if (await CustomerVerify(customerUpdate.Cpf))
                    {
                        throw new Exception("Já existe um cliente cadastrado com este CPF");
                    }
                }

                existingCustomer.Name = customerUpdate.Name;
                existingCustomer.Cpf = customerUpdate.Cpf;

                await _appDbContext.SaveChangesAsync();

                return existingCustomer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CustomerVerify(string cpf)
        {
            try
            {
                return await _appDbContext.customers
                    .AnyAsync(customer => customer.Cpf == cpf);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _appDbContext.customers.FindAsync(id);

                if (customer == null)
                {
                    return false;
                }

                _appDbContext.customers.Remove(customer);
                await _appDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
