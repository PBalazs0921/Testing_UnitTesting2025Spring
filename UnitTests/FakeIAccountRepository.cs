using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class FakeIAccountRepository : IAccountRepository
    {
        private readonly List<Account> _accounts = new List<Account>();

        public bool Add(Account account)
        {
            _accounts.Add(account);
            return true;
        }

        public bool Exists(int accountId)
        {
            return _accounts.Any(Account => Account.Id == accountId);
        }

        public Account Get(int accountId)
        {
            return _accounts.FirstOrDefault(Account => Account.Id == accountId);
        }

        public IEnumerable<Account> GetAll()
        {
            return _accounts.ToList();
        }

        public bool Remove(int accountId)
        {
            _accounts.RemoveAll(Account => Account.Id == accountId);
            return true;
        }
    }


}

