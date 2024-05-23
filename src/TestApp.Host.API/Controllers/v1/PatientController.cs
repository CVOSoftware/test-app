using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using TestApp.Domain.Abstraction.Core;
using TestApp.Domain.Abstraction.Core.Models;
using TestApp.Host.API.Extensions;
using TestApp.Host.API.Models;
using TestApp.Host.API.Utilities;

namespace TestApp.Host.API.Controllers.v1;

[Tags("Пациенты")]
[EndpointGroupName("v1")]
[ApiController, Route("api/v1/patients")]
public sealed class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet, Route("{id:guid}")]
    [SwaggerOperation(Summary = "Получение пациента по id")]
    [SwaggerResponse(StatusCodes.Status200OK,                  ContentTypes = new[] { MediaTypeNames.Application.Json }, Type = typeof(PatientModel))]
    [SwaggerResponse(StatusCodes.Status404NotFound,            ContentTypes = new[] { MediaTypeNames.Application.Json })]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, ContentTypes = new[] { MediaTypeNames.Application.Json })]
    public async Task<ActionResult<PatientModel>> Get(Guid id, CancellationToken token)
    {
        var model = await _patientService.Get(id, token);

        return Ok(model);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Получение списка пациента с опциональным параметром фильтра по дате рождения")]
    [SwaggerResponse(StatusCodes.Status200OK,                  ContentTypes = new[] { MediaTypeNames.Application.Json }, Type = typeof(PatientModel[]))]
    [SwaggerResponse(StatusCodes.Status400BadRequest,          ContentTypes = new[] { MediaTypeNames.Application.Json }, Type = typeof(ValidationProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, ContentTypes = new[] { MediaTypeNames.Application.Json })]
    public async Task<ActionResult<PatientModel[]>> Get([FromQuery] PaginationQuery paginationQuery, [FromQuery(Name = "birthDate")] string? birthDate, CancellationToken token)
    {
        var parser = new DateFilterParser(birthDate);
        var (TotalCount, SelectedItems) = await _patientService.Get(paginationQuery.Offset, paginationQuery.Limit, parser.Prefix, parser.Date, token);
        
        return Ok(SelectedItems).AddPaginationMetadata(paginationQuery, TotalCount, SelectedItems.Length);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Создание записи о пациенте")]
    [SwaggerResponse(StatusCodes.Status201Created,             ContentTypes = new[] { MediaTypeNames.Application.Json }, Type = typeof(PatientModel))]
    [SwaggerResponse(StatusCodes.Status400BadRequest,          ContentTypes = new[] { MediaTypeNames.Application.Json }, Type = typeof(ValidationProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, ContentTypes = new[] { MediaTypeNames.Application.Json })]
    public async Task<ActionResult<PatientModel>> Create(PatientModel model, CancellationToken token)
    {
        var createdModel = await _patientService.Create(model, token);

        return Created($"api/v1/patients/{createdModel.Name.Id}", createdModel);

    }

    [HttpPut]
    [SwaggerOperation(Summary = "Обновление записи о пациенте")]
    [SwaggerResponse(StatusCodes.Status200OK,                  ContentTypes = new[] { MediaTypeNames.Application.Json }, Type = typeof(PatientModel))]
    [SwaggerResponse(StatusCodes.Status400BadRequest,          ContentTypes = new[] { MediaTypeNames.Application.Json }, Type = typeof(ValidationProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound,            ContentTypes = new[] { MediaTypeNames.Application.Json })]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, ContentTypes = new[] { MediaTypeNames.Application.Json })]
    public async Task<ActionResult<PatientModel>> Update(PatientModel model, CancellationToken token)
    {
        await _patientService.Update(model, token);

        return Ok(model);
    }

    [HttpDelete, Route("{id:guid}")]
    [SwaggerOperation(Summary = "Удаление записи о пациенте по id")]
    [SwaggerResponse(StatusCodes.Status204NoContent,           ContentTypes = new[] { MediaTypeNames.Application.Json })]
    [SwaggerResponse(StatusCodes.Status404NotFound,            ContentTypes = new[] { MediaTypeNames.Application.Json })]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, ContentTypes = new[] { MediaTypeNames.Application.Json })]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _patientService.Delete(id, token);

        return NoContent();
    }
}