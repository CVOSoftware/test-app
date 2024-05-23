using System.Net.Http.Json;
using TestApp.Common.Types;
using TestApp.Domain.Abstraction.Core;
using TestApp.Domain.Abstraction.Core.Models;

namespace TestApp.Data.Bootstrap.Service;

internal sealed class PatientService : IPatientService, IDisposable
{
    private readonly string _host;

    private readonly HttpClient _httpClient;

    public PatientService(string host)
    {
        _host = host;

        var handler = new HttpClientHandler()
        {
            // только в рамках тестового задания и тестирования
            ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
            {
                return true;
            }
        };

        _httpClient = new HttpClient(handler);
    }

    public Task<PatientModel> Get(Guid id, CancellationToken token) => throw new NotImplementedException();

    public Task<(int TotalCount, PatientModel[] SelectedItems)> Get(int offset, int limit, FilterPrefix doBFilter, DateTime? doB, CancellationToken token) => throw new NotImplementedException();

    public async Task<PatientModel> Create(PatientModel model, CancellationToken token)
    {
        var uri = $"{_host}/api/v1/patients";
        var json = JsonContent.Create(model);
        await _httpClient.PostAsync(uri, json, token);

        return model;
    }

    public Task Delete(Guid id, CancellationToken token) => throw new NotImplementedException();

    public Task Update(PatientModel model, CancellationToken token) => throw new NotImplementedException();

    public void Dispose() => _httpClient.Dispose();
}