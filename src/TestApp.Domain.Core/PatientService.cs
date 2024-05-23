using Microsoft.EntityFrameworkCore;
using TestApp.Common.Types;
using TestApp.Data;
using TestApp.Data.Entities;
using TestApp.Domain.Abstraction.Core;
using TestApp.Domain.Abstraction.Core.Models;
using TestApp.Domain.Abstraction.Exceptions;

namespace TestApp.Domain.Core;

public sealed class PatientService : IPatientService
{
    private readonly DataContext _dataContext;

    public PatientService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }


    public async Task<PatientModel> Get(Guid id, CancellationToken token)
    {
        var entity = await _dataContext.Patients.FirstOrDefaultAsync(e => e.Id == id, token)
            ?? throw DomainException.NotFound();

        return Map(entity);
    }

    public async Task<(int TotalCount, PatientModel[] SelectedItems)> Get(int offset, int limit, FilterPrefix doBFilter, DateTime? doB, CancellationToken token)
    {
        var query = _dataContext.Patients.AsNoTracking();

        if (doB.HasValue)
        {
            query = doBFilter switch
            {
                FilterPrefix.eq => query.Where(e => e.DoB == doB.Value),
                FilterPrefix.ne => query.Where(e => e.DoB != doB.Value),
                FilterPrefix.gt => query.Where(e => e.DoB > doB.Value),
                FilterPrefix.lt => query.Where(e => e.DoB < doB.Value),
                FilterPrefix.ge => query.Where(e => e.DoB >= doB.Value),
                FilterPrefix.le => query.Where(e => e.DoB <= doB.Value),
                _ => throw new NotImplementedException()
            };
        }

        var totalCount = await query.CountAsync(token);

        if (totalCount == 0)
        {
            return (totalCount, Array.Empty<PatientModel>());
        }

        var entities = await query
            .OrderByDescending(e => e.DoB)
            .Skip(offset)
            .Take(limit)
            .ToArrayAsync(token);

        return (totalCount, Map(entities));
    }

    public async Task<PatientModel> Create(PatientModel model, CancellationToken token)
    {
        var id = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;
        var entity = new Patient
        {
            Id = id,
            Use = model.Name.Use?.Trim(),
            Name = model.Name.Given[0].Trim(),
            Family = model.Name.Family.Trim(),
            Patronymic = model.Name.Given[1].Trim(),
            Gender = (byte)model.Gender,
            DoB = model.BirthDate,
            IsActive = model.Active,
            CreatedAt = timestamp,
            UpdatedAt = timestamp
        };


        await _dataContext.Patients.AddAsync(entity, token);
        await _dataContext.SaveChangesAsync(token);

        return model with { Name = model.Name with { Id = id }};
    }

    public async Task Update(PatientModel model, CancellationToken token)
    {
        if (model.Name.Id.HasValue == false)
        {
            throw DomainException.NotFound();
        }

        var entity = await _dataContext.Patients.FirstOrDefaultAsync(e => e.Id == model.Name.Id.Value, token)
            ?? throw DomainException.NotFound();

        entity.Use = model.Name.Use?.Trim();
        entity.Name = model.Name.Given[0].Trim();
        entity.Family = model.Name.Family.Trim();
        entity.Patronymic = model.Name.Given[1].Trim();
        entity.Gender = (byte)model.Gender;
        entity.DoB = model.BirthDate;
        entity.IsActive = model.Active;
        entity.UpdatedAt = DateTime.UtcNow;


        _dataContext.Patients.Update(entity);
        await _dataContext.SaveChangesAsync(token);
    }

    public async Task Delete(Guid id, CancellationToken token)
    {
        var entity = await _dataContext.Patients.FirstOrDefaultAsync(e => e.Id == id, token) 
            ?? throw DomainException.NotFound();

        _dataContext.Patients.Remove(entity);
        await _dataContext.SaveChangesAsync(token);
    }

    private PatientModel Map(Patient entity)
    {
        var nameModel = new PatientNameModel(entity.Id, entity.Use, entity.Family, new[] { entity.Name, entity.Patronymic });

        return new(nameModel, (Gender)entity.Gender, entity.DoB, entity.IsActive);
    }

    private PatientModel[] Map(Patient[] entities) => entities.Select(Map).ToArray();
}