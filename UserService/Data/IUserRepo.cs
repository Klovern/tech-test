using Core.Repository;
using UserService.Objects;

namespace UserService.Data
{
    public interface IUserRepo : ICruddable<User>, ISaveable
    {
    }
}
