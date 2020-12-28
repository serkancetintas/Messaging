using Armut.Messaging.Core.Entities;
using Armut.Messaging.Core.Repositories;
using Armut.Messaging.Infrastructure.Mongo.Documents;
using System;
using System.Threading.Tasks;

namespace Armut.Messaging.Infrastructure.Mongo.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly IMongoRepository<UserDocument, Guid> _repository;
        public UserRepository(IMongoRepository<UserDocument, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _repository.GetAsync(x => x.Email == email.ToLowerInvariant());

            return user?.AsEntity();
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await _repository.GetAsync(x => x.UserName == userName.ToLowerInvariant());

            return user?.AsEntity();
        }

        public Task AddAsync(User user) => _repository.AddAsync(user.AsDocument());
    }
}
