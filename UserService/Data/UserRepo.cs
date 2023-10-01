using Core.Repository;
using UserService.Objects;

namespace UserService.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }

        public void Create(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Users.Add(entity);
        }

        public void Delete(int entity)
        {
            _context.Users.Remove(Details(entity));
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
        }

        public User Details(int id)
        {
            return _context.Users.FirstOrDefault(x=>x.Id == id);  
        }

        public bool Exists(int id)
        {
            return _context.Users.Any(x => x.Id == id);
        }
    }
}
