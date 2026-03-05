using Microsoft.EntityFrameworkCore;
using StashBankApplication.Model;
using StashBankApplication.Model.Context;

namespace StashBankApplication.Repository.Impl
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly MSSQLContext _context;

        public UserRepository(MSSQLContext context) : base(context)
        {
            _context = context;
        }

        public User FindByUsername(string username)
        {
            return _context.Users
                .SingleOrDefault(u => u.username == username);
        }
    }
}
