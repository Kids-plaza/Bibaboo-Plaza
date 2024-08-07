﻿using BPA.BusinessObject.Dtos.Account;
using BPA.BusinessObject.Entities;
using BPA.Repository.IRepositories;
using BPA.Service.IServices;

namespace BPA.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Add(Account account)
        {
            _accountRepository.Add(account);
        }

        public void Delete(Account account)
        {
            _accountRepository.Delete(account);
        }

        public Account? GetAccountByEmailAndPassword(LoginRequest accountLogin)
        {
            try
            {
                return _accountRepository.GetAll()
                    .FirstOrDefault(x => x.email!.Equals(accountLogin.Email) && x.password!.Equals(accountLogin.Password));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IEnumerable<Account> SearchByName(string name)
        {
            return _accountRepository.GetAll().Where(x => x.fullname!.Contains(name, StringComparison.OrdinalIgnoreCase) && x.is_deleted == false).ToList();
        }

        public IEnumerable<Account> GetAll()
        {
            return _accountRepository.GetAll();
        }

        public Account? GetById(Guid id)
        {
            return _accountRepository.GetById(id);
        }

        public void Update(Account account)
        {
            _accountRepository.Update(account);
        }
        public void Update2(Account account)
        {
            _accountRepository.Update2(account);
        }
    }
}
