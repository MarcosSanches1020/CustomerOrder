using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.API.Data;
using CustomerOrders.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrders.API.Services;

public class CustomerService
{

    private readonly AppDbContext _appDbContext;

    public CustomerService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Customer> AddCustomer(Customer newCustomer)
    {
        try
        {
            _appDbContext.customers.Add(newCustomer);
            await _appDbContext.SaveChangesAsync();

            var customerSave = await _appDbContext.customers.FirstOrDefaultAsync(
                c => c.Cpf == newCustomer.Cpf);

            if (customerSave == null)
            {
                throw new Exception("Erro ao inserir");
            }

            return customerSave;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Customer>> GetCustumers()
    {
        return await _appDbContext.customers.ToListAsync();
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        return await _appDbContext.customers.FindAsync(id);
    }


    public async Task<Customer> UpdateCustomer(int id, Customer customerUpdate)
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
