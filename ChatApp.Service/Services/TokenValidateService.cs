using ChatApp.Core.Repositories;
using ChatApp.Core.Services;
using ChatApp.Core.UnifOfWorks;
using ChatApp.Entities.Entities;
using ChatApp.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Service.Services
{
    public class TokenValidateService : GenericService<User>, IValidateTokenService
    {
        public TokenValidateService(IGenericRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public bool ValidateToken(User user)
        {
            throw new UnauthorizedException();
        }
    }
}
