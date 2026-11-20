using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChain_Colisiym.Models;

namespace BlockChain_Colisiym.Services
{
    public class DisplayService
    {
        public void DisplayChain(BlockChain blockChain)
        {
            foreach (var block in blockChain.Chain)
            {
                Console.WriteLine($"Index: {block.Index}");
                Console.WriteLine($"Hash: {block.Hash}");
                Console.WriteLine($"PrevHash: {block.PreviousHash}");
                Console.WriteLine($"TimeStamp: {block.TimeStamp}");
                Console.WriteLine($"Nonce: {block.Nonce}");
                Console.WriteLine($"Difficulty: {block.DifficultyMining}");
                Console.WriteLine($"Mining Duration: {block.MiningDuration} seconds");
                foreach (var transaction in block.Transactions)
                {
                    Console.WriteLine(transaction);
                }
                Console.WriteLine(new string('-', 30));
            }
        }

        public void DisplayTransactions(List<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction);
            }
        }
    }
}
