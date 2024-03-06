using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazet.Infrastructure.Configuration
{
    public class DatabaseSettings
    {
        public const string Key = nameof(DatabaseSettings);
        public string DBProvider { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }
}
