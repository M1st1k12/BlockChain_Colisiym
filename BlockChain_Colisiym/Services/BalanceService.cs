using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChain_Colisiym.Models;

namespace BlockChain_Colisiym.Services
{
    public class BalanceService
    {
        // Метод для получения баланса определенного адреса на основе списка блоков в блокчейне
        public decimal GetBalance(string address, List<Block> chain)
        {
            decimal balance = 0; // Инициализируем баланс на 0
            foreach (var block in chain)
            {
                foreach (var transaction in block.Transactions)
                {
                    if (transaction.From == address)
                    {
                        balance -= transaction.Amount; // Если адрес является отправителем, уменьшаем баланс на сумму транзакции
                    }
                    if (transaction.To == address)
                    {
                        balance += transaction.Amount; // Если адрес является получателем, увеличиваем баланс на сумму транзакции
                    }
                }
            }
            return balance;
        }

        // Метод для проверки, имеет ли определенный адрес достаточно баланса для совершения транзакции
        public bool HasSufficientBalance(string address, decimal amount, List<Block> chain)
        {
            var balance = GetBalance(address, chain);
            return balance >= amount; // Проверяем, достаточно ли баланса для совершения транзакции
        }

        // Метод для проверки, имеет ли определенный адрес достаточно баланса для совершения транзакции с учетом мемпула
        public bool HasSufficientBalance(string address, decimal amount, List<Block> chain, List<Transaction> mempool)
        {
            var balance = GetBalance(address, chain);
            foreach (var transaction in mempool)
            {
                if (transaction.From == address)
                {
                    balance -= transaction.Amount; // Учитываем транзакции из мемпула, которые еще не добавлены в блокчейн
                }
            }
            return balance >= amount; // Проверяем, достаточно ли баланса для совершения транзакции
        }
    }
}
