using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception($"Telefono no encontrado con este id: {id}");
    }

    public async Task<List<GetPhone>> GetByUserId(string userId, CancellationToken cancellationToken = default)
    {
        var phones = await _repository.GetByUserId(userId, cancellationToken);

        return phones.Select(PhoneProjection.GetAll).ToList();
    }


    public async Task<int> AddAsync(CreatePhone create, CancellationToken cancellationToken = default)
    {
        create.Id = Guid.NewGuid().ToString();

        if (string.IsNullOrEmpty(create.Number))
            throw new Exception("El numero de telefono no debe estar vacio");

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

