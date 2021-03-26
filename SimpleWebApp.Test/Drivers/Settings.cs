using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleWebApp.Test.Drivers
{
  public static class Settings
  {
    public static IConfiguration GetConfiguration()
    {
      string basePath = Directory.GetCurrentDirectory();
      string filePath = Path.Combine(basePath, @"..\SimpleWebApp.Test\appsettings.json");
      try
      {
        var config = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", false, true)
              .Build();
        return new ConfigurationBuilder()
          .SetBasePath(basePath)
          .AddJsonFile(@"..\SimpleWebApp.Test\appsettings.json").Build();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
