using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB01.ACCOUNTING2023.CORE.Interfaces.Configurations
{
    public class RedisConfiguration
    {
        public bool Enable { get; set; }
        public string ConnectionString { get; set; }
    }
}
