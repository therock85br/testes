using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Attiva.Freight.Authorization.Domain.Entities;
using Attiva.Freight.Authorization.Domain.Interfaces.Repository;
using Attiva.Freight.Authorization.Infrastructure.Persistence;


namespace Attiva.Freight.Authorization.Infrastructure.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository, IDisposable
    {
        public UserApp Login(UserApp item)
        {
            UserApp result = null;

            using (DataContext dataContext = new DataContext())
            {
                /*result = dataContext.UserApp
                    .Where(w => w.UserId == item.UserId && w.SecretKey == item.SecretKey)
                    .AsNoTracking()
                    .FirstOrDefault();*/

                if (item.UserId == "5a" && item.SecretKey == "07A9999BA37290F9BF60618760266FD0A83505E3AD6EFD6CC47C772445590A65789E25F0CF476A07DA1CF6AE598762C9F86DCAE2B5C271A68960BB05AA249923")
                {
                    result = item;
                }
            }

            return result;
        }

        public void Dispose()
        {
        }
    }
}
