using App1.Interfaces;
using App1.Managers;
using App1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    internal class Startup
    {
        public Startup ConfigureServices()
        {
            ServiceManager.AddService<IDatabase, Database>();
            ServiceManager.AddService<IAuthenticationService, AuthenticationService>();
            return this;
        }

        public Startup Configure()
        {
            return this;
        }
    }
}
