using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChain_Colisiym.Models;

namespace BlockChain_Colisiym.Services
{
    public class TransactionService
    {
        public Transaction CreateTransaction(string from, string to, decimal amount)
        {
            var tx = new Transaction(from, to, amount);
            var validationResult = ValidateTransaction(tx);
            if (!validationResult.isValid)
            {
                throw new ArgumentException(validationResult.errorMessage);
            }
            return tx;
        }

        public (bool isValid, string errorMessage) ValidateTransaction(Transaction transaction)
        {
            if (string.IsNullOrWhiteSpace(transaction.From))
            {
                return (false, "Sender address is required.");
            }
            if (string.IsNullOrWhiteSpace(transaction.To))
            {
                return (false, "Recipient address is required.");
            }
            if (transaction.Amount <= 0)
            {
                return (false, "Transaction amount must be greater than zero.");
            }
            return (true, string.Empty);
        }
    }
}
