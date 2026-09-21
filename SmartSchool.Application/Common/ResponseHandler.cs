using Microsoft.Extensions.Localization;
using SmartSchool.Application.Resources;
using SmartSchool.Application.Resources.Common;

namespace SmartSchool.Application.Common
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ResponseHandler(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public Response<T> Deleted<T>(string? Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = Message ?? _stringLocalizer[SharedResourcesKeys.DeletedSuccessfully]
            };
        }
        public Response<T> Success<T>(T entity, object? Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = _stringLocalizer[SharedResourcesKeys.Success],
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = _stringLocalizer[SharedResourcesKeys.UnAuthorized]
            };
        }
        public Response<T> BadRequest<T>(string? Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message ?? _stringLocalizer[SharedResourcesKeys.BadRequest]
            };
        }
        public Response<T> UnprocessableEntity<T>(string? Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message ?? _stringLocalizer[SharedResourcesKeys.UnprocessableEntity]
            };
        }

        public Response<T> NotFound<T>(string? message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message ?? _stringLocalizer[SharedResourcesKeys.NotFound]
            };
        }

        public Response<T> Created<T>(T entity, object? Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = _stringLocalizer[SharedResourcesKeys.Created],
                Meta = Meta
            };
        }

        public Response<T> Created<T>(string? message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = message ?? _stringLocalizer[SharedResourcesKeys.Created]
            };
        }
    }
}
