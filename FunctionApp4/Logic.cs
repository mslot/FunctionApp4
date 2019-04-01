using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp4
{
    public class Logic : ILogic
    {
        public ILogger logger;

        public Logic(ILogger logger)
        {
            this.logger = logger;
        }

        public void Method()
        {
            this.logger.LogInformation("Method with logging lol");
        }
    }
}
