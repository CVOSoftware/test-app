using TestApp.Common.Types;
using TestApp.Domain.Abstraction.Core.Models;

namespace TestApp.Domain.Abstraction.Core;

public interface IPatientService
{
    Task<PatientModel> Get(Guid id, CancellationToken token);

    Task<(int TotalCount, PatientModel[] SelectedItems)> Get(int offset, int limit, FilterPrefix doBFilter, DateTime? doB, CancellationToken token);

    Task<PatientModel> Create(PatientModel model, CancellationToken token);

    Task Update(PatientModel model, CancellationToken token);

    Task Delete(Guid id, CancellationToken token);
}