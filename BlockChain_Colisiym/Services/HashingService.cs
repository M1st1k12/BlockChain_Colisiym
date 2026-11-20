using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChain_Colisiym.Models;

namespace BlockChain_Colisiym.Services
{
    public class HashingService
    {
        public string ComputeBlockHash(Block block)
        {
            //формируем строку из данных блока для хеширования
            //var blockData = $"{block.Index}{block.PreviousHash}{block.TimeStamp}{block.Data}{block.Author}{block.DifficultyMining}{block.Nonce}";
            var stringTransactionRaw = string.Concat(block.Transactions.Select(t => t.ToRawString()));
            var blockData = $"{block.Index}{block.PreviousHash}{block.TimeStamp}{stringTransactionRaw}{block.Nonce}{block.DifficultyMining}";
            return ComputeHash(blockData);
        }

        //вычисляем хеш блока с отдельным nonce для потоков
        public string ComputeBlockHash(Block block, int nonce)
        {
            //var blockData = $"{block.Index}{block.PreviousHash}{block.TimeStamp}{block.Data}{block.Author}{block.DifficultyMining}{nonce}";
            var stringTransactionRaw = string.Concat(block.Transactions.Select(t => t.ToRawString()));
            var blockData = $"{block.Index}{block.PreviousHash}{block.TimeStamp}{stringTransactionRaw}{nonce}{block.DifficultyMining}";
            return ComputeHash(blockData);
        }

        private string ComputeHash(string data)
        {
            //используем SHA256 для вычисления хеша
            var bytes = Encoding.UTF8.GetBytes(data);
            var hashBytes = System.Security.Cryptography.SHA256.HashData(bytes);
            //return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return Convert.ToHexString(hashBytes).ToLower();
        }

        //хеш в Base64 
        private string ComputeHashBase64(string data)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
