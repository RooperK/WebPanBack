using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserService
    {
        public Task<bool> ContainsUserById(string id);
    }
}