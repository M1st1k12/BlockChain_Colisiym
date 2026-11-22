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
                Console.WriteLine($"Author: {block.Author}");
                Console.WriteLine("Transaction:");
                foreach (var transaction in block.Transactions)
                {
                    Console.WriteLine($"\t{transaction}");
                }
                Console.WriteLine($"Previous Hash: {block.PreviousHash}");
                Console.WriteLine($"Timestamp: {block.TimeStamp}");
                Console.WriteLine($"DifficultyAtMining: {block.DifficultyMining}");
                Console.WriteLine($"Nonce: {block.Nonce}");
                Console.WriteLine($"Mining Duration: {block.MiningDuration} seconds");
                Console.WriteLine(new string('-', 44));
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
