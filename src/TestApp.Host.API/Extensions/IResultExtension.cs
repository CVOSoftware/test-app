using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TestApp.Host.API.Models;

namespace TestApp.Host.API.Extensions;

internal static class IResultExtension
{
    public static ActionResult AddPaginationMetadata(this ActionResult result, PaginationQuery paginationQuery, int totalCount, int selectedCount)
    {
        var metadata = new PaginationMetadata(paginationQuery, totalCount, selectedCount);

        return new PaginationResult(result, metadata);
    }

    #region Types

    private sealed class PaginationResult : ActionResult
    {
        private readonly ActionResult _result;

        private readonly PaginationMetadata _paginationMetadata;

        public PaginationResult(ActionResult result, PaginationMetadata paginationMetadata)
        {
            _result = result;
            _paginationMetadata = paginationMetadata;
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            ExecuteResult(context);
            await _result.ExecuteResultAsync(context);
        }

        public override void ExecuteResult(ActionContext context)
        {
            var jsonOptions = context.HttpContext.RequestServices?.GetService<IOptions<JsonOptions>>()!.Value.JsonSerializerOptions;
            var json = JsonSerializer.Serialize(_paginationMetadata, jsonOptions);

            context.HttpContext.Response.Headers.Append("X-Pagination-Metadata", json);
        }
    }

    #endregion
}