using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChain_Colisiym.Models;

namespace BlockChain_Colisiym.Services
{
    public class BlockChain
    {
        private int _difficulty = 1;
        public readonly decimal _targetBlockTime = 2; 
        public readonly int _adjustEveryNBlocks = 3;
        private readonly decimal _miningReward = 10; // Reward for mining a block


        public List<Block> Chain { get; set; }
        private readonly HashingService _hashingService;
        private readonly MiningService _miningService;
        private readonly WalletService _walletService;
        private readonly BalanceService _balanceService;
        private readonly TransactionService _transactionService;

        public int Difficulty { get; private set; } = 10;

        public BlockChain(int difficulty = 1)
        {
            Chain = new List<Block>();
            _hashingService = new HashingService();
            this._difficulty = difficulty;
            this.Difficulty = difficulty;
            _miningService = new MiningService(_hashingService);
            _transactionService = new TransactionService();
            _walletService = new WalletService();
            _balanceService = new BalanceService();
            AddGenesisBlock("Genesis Block");
        }

        public string TargetPrefix { get; set; } = "alex";
        public BlockChain(string targetPrefix)
        {
            Chain = new List<Block>();
            _hashingService = new HashingService();
            this.TargetPrefix = targetPrefix;
            _miningService = new MiningService(_hashingService);
            AddGenesisBlock("Genesis Block");
        }

        public void AddGenesisBlock(string data = "Genesis Block")
        {
            var genesisBlock = new Block
            {
                Index = 0,
                Data = "Genesis Block",
                Transactions = new List<Transaction>(),
                PreviousHash = "0",
                TimeStamp = DateTime.Parse("2026-08-11"),
                Author = "Colisiym",
                DifficultyMining = 1
            };
           //_miningService.MineBlock(genesisBlock, 1);
           _miningService.MineBlockAsync(genesisBlock, 1).Wait();
           //_miningService.MineBlock(genesisBlock, TargetPrefix);
            Chain.Add(genesisBlock);

        }

        //public void AddBlock(string data, string author = "Colisiym")
        //{
        //    var lastBlock = Chain[Chain.Count - 1];
        //    var newBlock = new Block
        //    {
        //        Index = lastBlock.Index + 1,
        //        Data = data,
        //        PreviousHash = lastBlock.Hash,
        //        TimeStamp = DateTime.Now,
        //        Author = author,
        //        DifficultyMining = _difficulty
        //    };
        //    //_miningService.MineBlock(newBlock, _difficulty);
        //    _miningService.MineBlockAsync(newBlock, _difficulty).Wait();
        //    //_miningService.MineBlock(newBlock, TargetPrefix);
        //    Chain.Add(newBlock);
        //    AdjustDifficulty();
        //}

        public void AddBlock(List<Transaction> transactions, string author = "Colisiym")
        {
            foreach (var tx in transactions)
            {
                var isValid = _transactionService.ValidateTransaction(tx);
                if (!isValid.isValid)
                {
                    throw new ArgumentException($"Invalid transaction: {isValid.errorMessage}");
                }
            }

            var transactionCopy = new List<Transaction>(transactions);

            var lastBlock = Chain[Chain.Count - 1];
            var newBlock = new Block
            {
                Index = lastBlock.Index + 1,
                Transactions = transactionCopy,
                PreviousHash = lastBlock.Hash,
                TimeStamp = DateTime.Now,
                Author = author,
                DifficultyMining = _difficulty
            };
            //_miningService.MineBlock(newBlock, _difficulty);
            _miningService.MineBlockAsync(newBlock, _difficulty).Wait();
            Chain.Add(newBlock);
            AdjustDifficulty();
        }

        // Асинхронное добавление блока со списком транзакций (многопоточный майнинг)
        public async Task AddBlockAsync(List<Transaction> transactions, string author = "Colisiym")
        {
            foreach (var tx in transactions)
            {
                var isValid = _transactionService.ValidateTransaction(tx);
                if (!isValid.isValid)
                {
                    throw new ArgumentException($"Invalid transaction: {isValid.errorMessage}");
                }
            }

            var transactionCopy = new List<Transaction>(transactions);

            var lastBlock = Chain[Chain.Count - 1];
            var newBlock = new Block
            {
                Index = lastBlock.Index + 1,
                Transactions = transactionCopy,
                PreviousHash = lastBlock.Hash,
                TimeStamp = DateTime.Now,
                Author = author,
                DifficultyMining = _difficulty
            };
            await _miningService.MineBlockAsync(newBlock, _difficulty);
            Chain.Add(newBlock);
            AdjustDifficulty();
        }

        public void AddBlock(List<Transaction> transactions, string minerAddress, List<Transaction> mempool)
        {
            // Проверка валидности транзакций перед добавлением блока
            foreach (var tx in transactions)
            {
                var isValid = _transactionService.ValidateTransaction(tx);
                if (!isValid.isValid)
                {
                    throw new ArgumentException($"Invalid transaction: {isValid.errorMessage}");
                }

                var balanceCheck = _transactionService.ValidateBalance(tx, Chain, mempool);
                if (!balanceCheck.isValid)
                {
                    throw new ArgumentException($"Invalid transaction: {balanceCheck.errorMessage}");
                }
            }

            var transactionCopy = new List<Transaction>(transactions);
            var coinbaseTransaction = Transaction.CreateCoinbaseTransaction(minerAddress, _miningReward);
            transactionCopy.Insert(0, coinbaseTransaction);

            var lastBlock = Chain[Chain.Count - 1];
            var newBlock = new Block
            {
                Index = lastBlock.Index + 1,
                Transactions = transactionCopy,
                PreviousHash = lastBlock.Hash,
                TimeStamp = DateTime.Now,
                Author = "Colisiym",
                DifficultyMining = _difficulty
            };

            _miningService.MineBlockAsync(newBlock, _difficulty).Wait();
            Chain.Add(newBlock);
            AdjustDifficulty();
        }

        public async Task AddBlockAsync(List<Transaction> transactions, string minerAddress, List<Transaction> mempool)
        {
            // Проверка валидности транзакций перед добавлением блока
            var checkedFromMempool = new List<Transaction>();
            foreach (var tx in transactions)
            {
                var isValid = _transactionService.ValidateTransaction(tx);
                if (!isValid.isValid)
                {
                    throw new ArgumentException($"Invalid transaction: {isValid.errorMessage}");
                }

                var balanceCheck = _transactionService.ValidateBalance(tx, Chain, checkedFromMempool);
                if (!balanceCheck.isValid)
                {
                    throw new ArgumentException($"Invalid transaction: {balanceCheck.errorMessage}");
                }
                checkedFromMempool.Add(tx);
            }

            var transactionCopy = new List<Transaction>(transactions);
            var coinbaseTransaction = Transaction.CreateCoinbaseTransaction(minerAddress, _miningReward);
            transactionCopy.Insert(0, coinbaseTransaction);

            var lastBlock = Chain[Chain.Count - 1];
            var newBlock = new Block
            {
                Index = lastBlock.Index + 1,
                Transactions = transactionCopy,
                PreviousHash = lastBlock.Hash,
                TimeStamp = DateTime.Now,
                Author = "Colisiym",
                DifficultyMining = _difficulty
            };

            await _miningService.MineBlockAsync(newBlock, _difficulty);
            Chain.Add(newBlock);
            AdjustDifficulty();
        }

        public decimal GetBalance(string address)
        {
            return _balanceService.GetBalance(address, Chain);
        }

        private void AdjustDifficulty()
        {
            if ((Chain.Count - 1) % _adjustEveryNBlocks != 0) return;

            var recentBlock = Chain.Where(b => b.Index > 0).TakeLast(_adjustEveryNBlocks).ToList();
            if (recentBlock.Count == 0) return;

            var avgTime = recentBlock.Average(b => b.MiningDuration);
            if (avgTime < _targetBlockTime)
            {
                _difficulty++;
                Console.WriteLine($"Difficulty increased to {_difficulty}");
            }
            else if (avgTime > _targetBlockTime)
            {
                _difficulty = Math.Max(1, _difficulty - 1);
                Console.WriteLine($"Difficulty decreased to {_difficulty}");
            }
        }

        public void PrintDifficultyHistory()
        {
            foreach (var block in Chain)
            {
                Console.WriteLine($"Block: {block.Index} | Difficulty: {block.DifficultyMining} | Attempts: {block.Nonce} | MiningDuration: {block.MiningDuration} seconds");
            }
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                var currentBlock = Chain[i];
                var previousBlock = Chain[i - 1];
                
                if (currentBlock.Index == 0)
                {
                    continue; // Пропускаем проверку для генезис-блока
                }

                // Проверяем, что хеш текущего блока совпадает с вычисленным хешем
                if (currentBlock.Hash != _hashingService.ComputeBlockHash(currentBlock))
                {
                    return false;
                }
                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
                if (currentBlock.Hash.Substring(0, currentBlock.DifficultyMining) != new string('0', currentBlock.DifficultyMining))
                {
                    return false;
                }
                //if (!currentBlock.Hash.StartsWith(TargetPrefix, StringComparison.OrdinalIgnoreCase))
                //{
                //    return false;
                //}
            }
            return true;
        }


        public void SearchBlockByIndex(int index)
        {
            var block = Chain.FirstOrDefault(b => b.Index == index);
            if (block != null)
            {
                Console.WriteLine($"Index: {block.Index}");
                Console.WriteLine($"Hash: {block.Hash}");
                Console.WriteLine($"Data: {block.Data}");
                Console.WriteLine($"Previous Hash: {block.PreviousHash}");
                Console.WriteLine($"Timestamp: {block.TimeStamp}");
                Console.WriteLine($"Author: {block.Author}");
            }
            else
            {
                Console.WriteLine($"Block with index {index} not found.");
            }
        }


        public void SearchEventByText(string text)
        {
            var blocks = Chain.FindAll(b => b.Data.Contains(text));
            if (blocks.Count > 0)
            {
                foreach (var block in blocks)
                {
                    Console.WriteLine($"Block Index: {block.Index}");
                    Console.WriteLine($"Block Hash: {block.Hash}");
                    Console.WriteLine($"Previous Hash: {block.PreviousHash}");
                    Console.WriteLine($"Data: {block.Data}");
                    Console.WriteLine($"Timestamp: {block.TimeStamp}");
                    Console.WriteLine($"Author: {block.Author}");
                    Console.WriteLine(new string('-', 30));
                }
            }
            else
            {
                Console.WriteLine($"No blocks found containing the text '{text}'.");
            }
        }

        // Поиск транзакции по Id
        public void SearchTransactionById(string id)
        {
            foreach (var block in Chain)
            {
                var tx = block.Transactions.FirstOrDefault(t => t.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
                if (tx != null)
                {
                    Console.WriteLine($"Transaction found in Block #{block.Index}:");
                    Console.WriteLine(tx);
                    return;
                }
            }
            Console.WriteLine($"Transaction with Id '{id}' not found.");
        }

        // Поиск транзакций по пользователю (1 - все, 2 - только отправленные, 3 - только полученные)
        public void SearchTransactionsByUser(string user, string filter = "1")
        {
            bool found = false;

            foreach (var block in Chain)
            {
                foreach (var tx in block.Transactions)
                {
                    bool isSender = tx.From.Equals(user, StringComparison.OrdinalIgnoreCase);
                    bool isReceiver = tx.To.Equals(user, StringComparison.OrdinalIgnoreCase);

                    bool match = filter switch
                    {
                        "2" => isSender,
                        "3" => isReceiver,
                        _ => isSender || isReceiver
                    };

                    if (match)
                    {
                        Console.WriteLine($"[Block #{block.Index}] {tx}");
                        found = true;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine($"No transactions found for user '{user}'.");
            }
        }

        // Рівень 3. Blockchain Audit
        public void AuditBlockchain()
        {
            bool isChainValid = true;
            var seenTransactionIds = new List<string>();
            var userOps = new Dictionary<string, int>();

            int totalBlocks = Chain.Count;
            int totalTransactions = 0;
            decimal totalAmount = 0;

            for (int i = 1; i < Chain.Count; i++)
            {
                var currentBlock = Chain[i];
                var previousBlock = Chain[i - 1];

                if (currentBlock.Hash != _hashingService.ComputeBlockHash(currentBlock))
                {
                    Console.WriteLine($"Invalid block #{currentBlock.Index}: wrong Hash");
                    isChainValid = false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    Console.WriteLine($"Invalid block #{currentBlock.Index}: wrong PreviousHash");
                    isChainValid = false;
                }

                foreach (var tx in currentBlock.Transactions)
                {
                    totalTransactions++;
                    totalAmount += tx.Amount;

                    if (tx.From != "COINBASE")
                    {
                        if (!userOps.ContainsKey(tx.From)) userOps[tx.From] = 0;
                        userOps[tx.From]++;
                    }
                    if (!userOps.ContainsKey(tx.To)) userOps[tx.To] = 0;
                    userOps[tx.To]++;

                    if (seenTransactionIds.Contains(tx.Id))
                    {
                        Console.WriteLine($"Invalid block #{currentBlock.Index}: duplicate Transaction Id {tx.Id}");
                        isChainValid = false;
                    }
                    else
                    {
                        seenTransactionIds.Add(tx.Id);
                    }

                    if (!tx.IsCoinbase)
                    {
                        var check = _transactionService.ValidateTransaction(tx);
                        if (!check.isValid)
                        {
                            Console.WriteLine($"Invalid block #{currentBlock.Index}: transaction error ({check.errorMessage})");
                            isChainValid = false;
                        }
                    }
                }
            }

            string topUser = "None";
            if (userOps.Count > 0)
            {
                var max = userOps.OrderByDescending(x => x.Value).First();
                topUser = $"{max.Key} ({max.Value} tx)";
            }

            Console.WriteLine("------------------------------");
            Console.WriteLine($"Blocks count: {totalBlocks}");
            Console.WriteLine($"Transactions count: {totalTransactions}");
            Console.WriteLine($"Total amount: {totalAmount}");
            Console.WriteLine($"Most active user: {topUser}");
            Console.WriteLine($"Blockchain valid: {isChainValid}");
            Console.WriteLine("------------------------------");
        }

    }
}