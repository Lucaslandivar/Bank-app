using _7Bank.Api.DTOs;
using _7Bank.Api.Models;
using _7Bank.Api.Services.AccountService;
using Microsoft.AspNetCore.Mvc;

namespace _7Bank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (account == null)
                return NotFound("Conta não encontrada!");

            return Ok(account);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var account = await _accountService.GetByUserIdAsync(userId);

            if (account == null)
                return NotFound("Conta não encontrada para este usuário!");

            return Ok(account);
        }

        [HttpGet("saldo/{accountId}")]
        public async Task<IActionResult> GetBalance(int accountId)
        {
            var balance = await _accountService.GetBalanceAsync(accountId);
            return Ok(balance);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountService.GetAllAsync();
            return Ok(accounts);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountUpdateDto account)
        {
            var updated = await _accountService.UpdateAccountAsync(account);
            return Ok(updated);
        }

        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            try
            {
                var result = await _accountService.DeleteAccountAsync(accountId);

                if (!result)
                    return BadRequest("Não foi possível excluir a conta!");

                return Ok("Conta excluída com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("inativar/{accountId}")]
        public async Task<IActionResult> Inactivate(int accountId)
        {
            var success = await _accountService.InactiveAccountAsync(accountId);

            if (!success)
                return BadRequest("Erro ao inativar conta!");

            return Ok("Conta inativada com sucesso!");
        }
    }
}