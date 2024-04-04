using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using System.Linq;
using Azure.Core;

namespace SuperfonMobileAPI.Shared
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly CustomJWTValidator customJWTValidator;
        public RequestResponseMiddleware(RequestDelegate next, CustomJWTValidator customJWTValidator)
        {
            this.next = next;
            this.customJWTValidator = customJWTValidator;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var auth = context.Request.Headers["Authorization"].FirstOrDefault();
            string token = auth?.Split(" ").Last();
            if (!string.IsNullOrWhiteSpace(token) && auth.StartsWith("bearer", StringComparison.OrdinalIgnoreCase) && !customJWTValidator.ExistsInCache(token))
            {
                context.Response.StatusCode = 401;
            }
            else
            {
                await next(context);
            }
            
        }

        private async Task<Dictionary<string,string>> GetRequestData(HttpRequest request)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            // Leave the body open so the next middleware can read it.
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            // Do some processing with body…

            var formattedRequest = $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {body}";
            values["Scheme"] = request.Scheme;
            values["Host"] = request.Host.ToString();
            values["Path"] = request.Path.ToString();
            values["QueryString"] = request.QueryString.ToString();
            values["Method"] = request.Method;
            values["Body"] = body;
            foreach (var h in request.Headers)
                values[h.Key] = h.Value;
            // Reset the request body stream position so the next middleware can read it
            request.Body.Position = 0;

            return values;
        }

        private async Task<Dictionary<string, string>> GetResponseData(HttpResponse response)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            values["Body"] = await new StreamReader(response.Body).ReadToEndAsync();
            values["StatusCode"] = response.StatusCode.ToString();
            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return values;
        }




        private Stream ReplaceBody(HttpResponse response)
        {
            var originBody = response.Body;
            response.Body = new MemoryStream();
            return originBody;
        }

        private void ReturnBody(HttpResponse response, Stream originBody)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            response.Body.CopyTo(originBody);
            response.Body = originBody;
        }
        //private async Task ReturnBody(HttpResponse response, Stream originBody)
        //{
        //    response.Body.Seek(0, SeekOrigin.Begin);
        //    await response.Body.CopyToAsync(originBody);
        //    response.Body = originBody;
        //}

    }
}
