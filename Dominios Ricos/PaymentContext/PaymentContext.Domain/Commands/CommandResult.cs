using PaymentContext.Shared.Commands;

namespace PaymentContext.Domais.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {
        }

        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; } 
        public string Message{ get; set; } 
    }
}