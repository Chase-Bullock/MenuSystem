using Microsoft.AspNetCore.Identity;
using PizzaButt.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaButt.Security
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserClaimStore<User>, IUserRoleStore<User>, IUserEmailStore<User>
    {
        private ICathedralKitchenRepository repository;
        private CathedralKitchenContext _ctx;
        public UserStore(ICathedralKitchenRepository repo, CathedralKitchenContext ctx)
        {
            repository = repo;
            _ctx = ctx;
        }

        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            user.CreateTime = DateTime.UtcNow;
            user.UpdateTime = DateTime.UtcNow;
            _ctx.User.Add(user);
            _ctx.SaveChanges();
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return _ctx.User.SingleOrDefault(x => x.Email.ToUpper() == normalizedEmail);
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            long result = 0;
            long.TryParse(userId, out result);
            return _ctx.User.SingleOrDefault(x => x.Id == result);
        }

        public async Task<User> FindByNameAsync(string email, CancellationToken cancellationToken)
        {
            return _ctx.User.SingleOrDefault(x => x.Email.ToUpper() == email);
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {
            return new List<Claim>();
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return user.Email;
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return user.Email;
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return user.Email;
        }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            user = _ctx.User.SingleOrDefault(x => x.Id == user.Id);
            return user.Hash;
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            return new List<string>();
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return user.Id.ToString();
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return user.Email;
        }

        public Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.Email = normalizedEmail;
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
           
        }

        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.SetHash(passwordHash);
            //await repository.UpdateAsync(user);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var oldUser = _ctx.User.SingleOrDefault(x => x.Id == user.Id);
            oldUser = user;
            _ctx.SaveChanges();
            return IdentityResult.Success;
        }
    }
}
