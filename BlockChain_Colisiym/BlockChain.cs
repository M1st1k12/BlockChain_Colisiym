using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain_Colisiym
{
    public class BlockChain
    {
        public List<Block> Chain { get; set; }
        private readonly HashingService _hashingService;
        private readonly MiningService _miningService;

        //public int Difficulty { get; set; } = 4; 
        //public BlockChain(int difficulty)
        //{
        //    Chain = new List<Block>();
        //    _hashingService = new HashingService();
        //    this.Difficulty = difficulty;
        //    _miningService = new MiningService(_hashingService);
        //    AddGenesisBlock("Genesis Block");
        //}

        public string TargetPrefix { get; set; } = "alex";
        public BlockChain(string targetPrefix)
        {
            Chain = new List<Block>();
            _hashingService = new HashingService();
            this.TargetPrefix = targetPrefix;
            _miningService = new MiningService(_hashingService);
            AddGenesisBlock("Genesis Block");
        }

        public void AddGenesisBlock(string data)
        {
            var genesisBlock = new Block
            {
                Index = 0,
                Data = "Genesis Block",
                PreviousHash = "0",
                TimeStamp = DateTime.Parse("2026-08-11"),
                Author = "Colisiym"
            };
           //_miningService.MineBlock(genesisBlock, 1);
           _miningService.MineBlock(genesisBlock, TargetPrefix);
            Chain.Add(genesisBlock);

        }

        public void AddBlock(string data, string author = "Colisiym")
        {
            var lastBlock = Chain[Chain.Count - 1];
            var newBlock = new Block
            {
                Index = lastBlock.Index + 1,
                Data = data,
                PreviousHash = lastBlock.Hash,
                TimeStamp = DateTime.Now,
                Author = author
            };
            //_miningService.MineBlock(newBlock, Difficulty);
            _miningService.MineBlock(newBlock, TargetPrefix);
            Chain.Add(newBlock);
        }

        // Асинхронное добавление блока (многопоточный майнинг)
        public async Task AddBlockAsync(string data, string author = "Colisiym")
        {
            var lastBlock = Chain[Chain.Count - 1];
            var newBlock = new Block
            {
                Index = lastBlock.Index + 1,
                Data = data,
                PreviousHash = lastBlock.Hash,
                TimeStamp = DateTime.Now,
                Author = author
            };
            await _miningService.MineBlockAsync(newBlock, TargetPrefix);
            Chain.Add(newBlock);
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
                //if (currentBlock.Hash.Substring(0, Difficulty) != new string('0', Difficulty))
                //{
                //    return false;
                //}
                if (!currentBlock.Hash.StartsWith(TargetPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
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

    }
}