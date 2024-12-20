using System;
using System.Linq.Expressions;
using CarRental.Application.Interfaces.Repository;
using CarRental.Application.Interfaces.Repository.Spesifications;
using CarRental.Application.Interfaces.Services;
using CarRental.Application.Specifications.Base;
using CarRental.Domain.DTO;
using CarRental.Domain.Entities.Base;
using Microsoft.Extensions.Logging;

namespace CarRental.Application.Services;

public abstract class CrudService<T> : ICrudService<T> where T : BaseEntity
{
    private readonly ILogger<CrudService<T>> _logger;
    private readonly IGenericRepository<T> _repository;
    private readonly string _className;
    public CrudService(ILogger<CrudService<T>> logger, IGenericRepository<T> repository)
    {
        _logger = logger;
        _repository = repository;

        _className = $"CrudService for {typeof(T).Name}";
    }
    public async Task CreateAsync(AddGeneralDTO<T> dto)
    {
        _logger.LogInformation("{methodName} started in {className}", nameof(CreateAsync), _className);

        try
        {
            await _repository.AddAsync(dto.Entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something unexpected happened while executing {methodName} in {className}", nameof(GetAsync), _className);
            _logger.LogTrace("Id: {id}", dto.Entity.Id);

            return;
        }

        _logger.LogDebug("{methodName} finished in {className}", nameof(CreateAsync), _className);
    }

    public async Task DeleteAsync(DeleteGeneralDTO dto)
    {
        _logger.LogInformation("{methodName} started in {className}", nameof(DeleteAsync), _className);

        try
        {
            T? entity = await _repository.GetFirstOrDefaultAsync(e => e.Id == dto.Id);

            if (entity is null)
            {
                throw new ArgumentNullException("Entity was not found");
            }

            await _repository.DeleteAsync(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something unexpected happened while executing {methodName} in {className}", nameof(GetAsync), _className);
            _logger.LogTrace("Id: {id}", dto.Id);

            return;
        }

        _logger.LogDebug("{methodName} finished in {className}", nameof(DeleteAsync), _className);
    }

    public async Task DeleteAsync(ISpecification<T, T> specification)
    {
        _logger.LogInformation("{methodName} started in {className}", nameof(DeleteAsync), _className);

        try
        {
            await _repository.DeleteAsync(specification);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something unexpected happened while executing {methodName} in {className}", nameof(GetAsync), _className);
            _logger.LogTrace("Criteria: {criteria}", specification.Criteria);

            return;
        }

        _logger.LogDebug("{methodName} finished in {className}", nameof(DeleteAsync), _className);
    }


    public async Task<IEnumerable<T>?> GetAllAsync()
    {
        _logger.LogInformation("{methodName} started in {className}", nameof(GetAllAsync), _className);

        IEnumerable<T>? entities = null;

        try
        {
            entities = await _repository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something unexpected happened while executing {methodName} in {className}", nameof(GetAllAsync), _className);

            return entities;
        }

        _logger.LogDebug("{methodName} finished in {className}", nameof(GetAllAsync), _className);
        return entities;
    }

    public async Task<T?> GetAsync(GetGeneralDTO dto)
    {
        _logger.LogInformation("{methodName} started in {className}", nameof(GetAsync), _className);

        T? entity = null;

        try
        {
            entity = await _repository.GetFirstOrDefaultAsync(e => e.Id == dto.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something unexpected happened while executing {methodName} in {className}", nameof(GetAsync), _className);
            _logger.LogTrace("Id: {id}", dto.Id);

            return entity;
        }

        _logger.LogDebug("{methodName} finished in {className}", nameof(GetAsync), _className);
        return entity;
    }


    public async Task<TResult?> GetFirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification)
    {
        _logger.LogInformation("{methodName} started in {className}", nameof(GetFirstOrDefaultAsync), _className);

        TResult? entity = default;

        try
        {
            entity = await _repository.GetFirstOrDefaultAsync(specification);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something unexpected happened while executing {methodName} in {className}", nameof(GetFirstOrDefaultAsync), _className);
            _logger.LogTrace("Criteria: {criteria}", specification.Criteria);

            return entity;
        }

        _logger.LogDebug("{methodName} finished in {className}", nameof(GetFirstOrDefaultAsync), _className);

        return entity;
    }

    public async Task<IEnumerable<TResult>?> GetRangeAsync<TResult>(ISpecification<T, TResult> specification)
    {
        _logger.LogInformation("{methodName} started in {className}", nameof(GetRangeAsync), _className);

        IEnumerable<TResult>? entities = null;

        try
        {
            entities = await _repository.GetRangeAsync(specification);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something unexpected happened while executing {methodName} in {className}", nameof(GetRangeAsync), _className);
            _logger.LogTrace("Criteria: {criteria}", specification.Criteria);

            return entities;
        }

        _logger.LogDebug("{methodName} finished in {className}", nameof(GetRangeAsync), _className);

        return entities;
    }

    public async Task<TResult?> GetSingleOrDefaultAsync<TResult>(ISpecification<T, TResult> specification)
    {
        _logger.LogInformation("{methodName} started in {className}", nameof(GetSingleOrDefaultAsync), _className);

        TResult? entity = default;

        try
        {
            entity = await _repository.GetSingleOrDefaultAsync(specification);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something unexpected happened while executing {methodName} in {className}", nameof(GetSingleOrDefaultAsync), _className);
            _logger.LogTrace("Criteria: {criteria}", specification.Criteria);

            return entity;
        }

        _logger.LogDebug("{methodName} finished in {className}", nameof(GetSingleOrDefaultAsync), _className);

        return entity;
    }

    public async Task UpdateAsync(UpdateGeneralDTO<T> dto)
    {
        _logger.LogInformation("{methodName} started in {className}", nameof(UpdateAsync), _className);

        try
        {
            await _repository.UpdateAsync(dto.Entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something unexpected happened while executing {methodName} in {className}", nameof(UpdateAsync), _className);
            _logger.LogTrace("Id: {id}", dto.Entity.Id);

            return;
        }

        _logger.LogDebug("{methodName} finished in {className}", nameof(UpdateAsync), _className);
    }
}
