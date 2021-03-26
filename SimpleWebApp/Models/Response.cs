using EnumsNET;
using SimpleWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Models
{
  public class Response<T>
  {
    #region Constructors
    public Response() { }
    public Response(ResponseStatus status, string message) : this(status, message, string.Empty, default, null) { }
    public Response(ResponseStatus status, string message, string details) : this(status, message, details, default, null) { }
    public Response(ResponseStatus status, string message, T data) : this(status, message, string.Empty, data, null) { }
    public Response(ResponseStatus status, string message, Exception exception) : this(status, message, string.Format("Details: {0}", exception.Message), default, exception) { }
    public Response(ResponseStatus status, string message, string details, T data) : this(status, message, details, data, null) { }
    public Response(ResponseStatus status, string message, string details, T data, Exception exception)
    {
      Status = EnumHelper.GetDescription(status);
      Message = message;
      Details = details;
      Data = data;
      TimeSpan = DateTime.Now;
      Exception = exception != null ? exception.ToString() : string.Empty;
    }

    #endregion

    #region Properties

    public string Status { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
    public DateTime TimeSpan { get; set; }
    public T Data { get; set; }
    public string Exception { get; set; }

    #endregion
  }

  public enum ResponseStatus
  {
    [Description("success")]
    SUCCESS,
    [Description("error")]
    ERROR,
    [Description("warning")]
    WARNING,
    [Description("fatal error")]
    FATAL_ERROR
  }

  public static class ResponseMessages
  {
    public const string SUCCESS_REQUESTS = "The request has been processed successfully! (◠‿◠)✌";
    public const string SUCCESS_CREATED = "The record was created successfully! ٩(˘◡˘)۶";
    public const string SUCCESS_UPDATED = "The record was updated successfully! ٩(˘◡˘)۶";
    public const string SUCCESS_DELETED = "The record was deleted successfully! ٩(˘◡˘)۶";

    public const string ERROR = "Something went wrong! (ಥ_ಥ)";
    public const string ERROR_NOT_FOUND = "Resource not found! (◐.̃◐)";

    public const string WARNING_VALIDATION = "Some fields did not pass validations! ^( '-' )^";
    public const string WARNING_PARAMETER_LOSE = "Some parameters were not received! (ಠ_ಠ)";

    public const string FATAL_ERROR = "Something really bad happened! (╥﹏╥)";
  }
}
