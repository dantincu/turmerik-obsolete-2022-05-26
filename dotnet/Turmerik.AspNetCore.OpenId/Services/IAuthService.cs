using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.OpenId.Services
{
    public interface IAuthService
    {
        Task LogIn();
        Task LogOut();
    }
}
