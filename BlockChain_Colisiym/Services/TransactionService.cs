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
        private readonly WalletService _walletService = new WalletService();

        //public Transaction CreateTransaction(string from, string to, decimal amount)
        //{
        //    var tx = new Transaction(from, to, amount);
        //    var validationResult = ValidateTransaction(tx);
        //    if (!validationResult.isValid)
        //    {
        //        throw new ArgumentException(validationResult.errorMessage);
        //    }
        //    return tx;
        //}

        public Transaction CreateTransaction(string from, string to, decimal amount, Wallet wallet)
        {
            var tx = new Transaction(from, to, amount, wallet.PublicKey);
            tx.Signature = wallet.Sign(tx.GetDataToSign());

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
            if (transaction.PublicKey == null || transaction.PublicKey.Length == 0)
            {
                return (false, "Public key is required.");
            }
            if (transaction.Signature == null || transaction.Signature.Length == 0)
            {
                return (false, "Signature is required.");
            }
            if (!_walletService.VerifySignature(transaction.GetDataToSign(), transaction.Signature, transaction.PublicKey))
            {
                return (false, "Invalid signature.");
            }
            return (true, string.Empty);
        }
    }
}
