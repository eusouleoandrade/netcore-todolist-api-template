using Core.Application.Resources;
using Infra.Notification.Models;
using System.Text.Json.Serialization;

namespace Core.Application.Dtos.Wrappers
{
    public class Response<TData>
        where TData : class

    {
        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<NotificationMessage>? Errors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TData? Data { get; set; }

        public Response(TData data, bool succeeded, string? message = null, IEnumerable<NotificationMessage>? errors = null)
        {
            Succeeded = succeeded;

            Errors = errors;

            Data = data;

            if (string.IsNullOrWhiteSpace(message))
                Message = succeeded ? Msg.RESPONSE_SUCCEEDED_MESSAGE : Msg.RESPONSE_FAILED_MESSAGE;
            else
                Message = message;
        }
    }

    public class Response<TData, TErrors>
        where TData : class
        where TErrors : class

    {
        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TErrors? Errors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TData? Data { get; set; }

        public Response(TData data, bool succeeded, string? message = null, TErrors? errors = null)
        {
            Succeeded = succeeded;

            Errors = errors;

            Data = data;

            if (string.IsNullOrWhiteSpace(message))
                Message = succeeded ? Msg.RESPONSE_SUCCEEDED_MESSAGE : Msg.RESPONSE_FAILED_MESSAGE;
            else
                Message = message;
        }
    }

    public class Response
    {
        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<NotificationMessage>? Errors { get; set; }

        public Response(bool succeeded, string? message = null, IEnumerable<NotificationMessage>? errors = null)
        {
            Succeeded = succeeded;

            Errors = errors;

            if (string.IsNullOrWhiteSpace(message))
                Message = succeeded ? Msg.RESPONSE_SUCCEEDED_MESSAGE : Msg.RESPONSE_FAILED_MESSAGE;
            else
                Message = message;
        }
    }
}
