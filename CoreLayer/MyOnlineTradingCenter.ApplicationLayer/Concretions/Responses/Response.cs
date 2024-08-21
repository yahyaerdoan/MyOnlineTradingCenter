using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Responses;

public class Response<T>
{
    public bool IsSuccessful { get; }
    public string Message { get; }
    public int StatusCode { get; }
    public T? Data { get; }
    public List<string>? Errors { get; }

    private Response(bool isSuccessful, string message, int statusCode, T? data = default, List<string>? errors = null)
    {
        IsSuccessful = isSuccessful;
        Message = message;
        StatusCode = statusCode;
        Data = data;
        Errors = errors;
    }

    public static Response<T> Success(T data, string message = "", int statusCode = 200)
    {
        return new Response<T>(true, message, statusCode, data);
    }

    public static Response<T> Failure(List<string> errors, string message = "", int statusCode = 400)
    {
        return new Response<T>(false, message, statusCode, default, errors);
    }

    public static Response<T> Failure(string error, string message = "", int statusCode = 400)
    {
        return new Response<T>(false, message, statusCode, default, new List<string> { error });
    }
}
