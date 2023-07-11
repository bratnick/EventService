using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventService.Common.Constants
{
    public class AppConfiguration
    {
        public string EventURL { get; set; }
        public int  RetryCount { get; set; }
        public List<string> ValidEmailIds { get; set; }
    }
}
