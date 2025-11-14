using _7Bank.Api.DTOs;
using _7Bank.Api.DTOs.Transaction;
using _7Bank.Api.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;

namespace _7Bank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("pix/{fromUserId}")]
        public async Task<IActionResult> PixTransfer(int fromUserId, [FromBody] PixRequestDto dto)
        {
            if (dto.Amount <= 0)
                return BadRequest("O valor deve ser maior que zero!");

            if (string.IsNullOrEmpty(dto.Identifier))
                return BadRequest("Informe o CPF ou Email do destinatário!");

            var result = await _transactionService.TransferAsync(
                fromUserId,
                dto.Identifier,
                "cpf",
                dto.Amount
            );

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var transactions = await _transactionService.GetByUserIdAsync(userId);

            if (transactions == null || !transactions.Any())
                return NotFound("Nenhuma transação encontrada para este usuário!");

            return Ok(transactions);
        }

        [HttpGet("last3months/{userId}")]
        public async Task<IActionResult> GetLast3Months(int userId)
        {
            var transactions = await _transactionService.GetLast3MonthsAsync(userId);

            if (transactions == null || !transactions.Any())
                return NotFound("Nenhuma transação nos últimos 3 meses!");

            return Ok(transactions);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync();

            return Ok(transactions);
        }

        [HttpPost("pix")]
        public async Task<IActionResult> TransferPix([FromBody] PixRequestDto model)
        {
            if (model == null)
                return BadRequest("Dados inválidos.");

            var result = await _transactionService.TransferAsync(
                model.FromUserId,
                model.Identifier,
                model.IdentifierType,
                model.Amount
            );

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
