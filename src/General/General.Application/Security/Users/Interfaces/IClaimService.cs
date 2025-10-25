using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Security.Users.Interfaces
{
    public interface IClaimService
    {
         Task<List<Claim>> GetUserClaimsAsync(Guid userId);
    }
}
