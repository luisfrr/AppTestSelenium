using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebApp.XUnit.Test.Common
{
  public static class AppSettings
  {
    public static IConfiguration GetConfiguration()
    {
      return new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", false, true)
        .Build();
    }
  }
}
