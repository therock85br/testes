using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Communication.Enums;
using System.Net;

namespace Attiva.Communication
{
    public class Response<T> where T : class
    {
        public bool Success { get; set; }
        public List<T> Items { get; set; }
        public List<Error> Errors { get; set; }
        public string ResponseTime { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public Response()
        {
            Items = new List<T>();
            Errors = new List<Error>();
        }

        public void AddError(Exception ex)
        {
            Errors.Add(new Error()
            {
                Type = ErrorType.Exception,
                Exception = ex.GetType().Name,
                Description = ex.Message,
                Details = ex.InnerException?.Message
            });
            HttpStatusCode = HttpStatusCode.InternalServerError;
        }

        public void AddError(string description, string details)
        {
            Success = false;
            Errors.Add(new Error()
            {
                Type = ErrorType.Validation,
                Description = description,
                Details = details
            });
            HttpStatusCode = HttpStatusCode.InternalServerError;
        }

        public void AddError(string description)
        {
            Success = false;
            Errors.Add(new Error()
            {
                Type = ErrorType.Validation,
                Description = description,
            });
            HttpStatusCode = HttpStatusCode.InternalServerError;
        }
    }
}
