﻿using Microsoft.EntityFrameworkCore;
using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Application.Phones.Services.Projections;
using UserManagementApp.Application.Users.Interfaces;
using UserManagementApp.Domain.Entities.Phones;
using UserManagementApp.Domain.Interfaces.Repositories.Phones;

namespace UserManagementApp.Application.Phones.Services;

public class PhoneService : IPhoneService
{
    private readonly IPhoneRepository _repository;

    public PhoneService(IPhoneRepository repository)
    { 
        _repository = repository;
    }

    public async Task<List<GetPhone>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.Queryable(cancellationToken).AsNoTracking()
            .Select(PhoneProjection.GetAll)
            .ToListAsync(cancellationToken);
    }

    public async Task<GetPhone> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _repository.Queryable(cancellationToken)
            .AsNoTracking()
            .Where(st => st.Id == id)
            .Select(PhoneProjection.GetAll)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception($"No Phone found with id: {id}");
    }

    public async Task<List<Phone>> GetByUserId(string userId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetByUserId(userId, cancellationToken);
    }


    public async Task<int> AddAsync(CreatePhone create, CancellationToken cancellationToken = default)
    {
        create.Id = Guid.NewGuid().ToString();

        await _repository.AddAsync(create, cancellationToken);

        return await _repository.SaveChangesAsync(cancellationToken);

    }


    public async Task<int> UpdateAsync(string id, UpdatePhone update, CancellationToken cancellationToken = default)
    {
        var phone = await _repository.GetByIdAsync(id);

        if (!string.IsNullOrEmpty(update.Number))
            phone.Number = update.Number;

        if (!string.IsNullOrEmpty(update.ContryCode))
            phone.ContryCode = update.ContryCode;

        if (!string.IsNullOrEmpty(update.CityCode))
            phone.CityCode = update.CityCode;

        await _repository.UpdateAsync(phone, cancellationToken);

        return await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var phone = await _repository.GetByIdAsync(id);

        await _repository.DeleteAsync(phone, cancellationToken);

        return await _repository.SaveChangesAsync(cancellationToken);
    }

}
