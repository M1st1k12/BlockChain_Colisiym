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

        public BlockChain()
        {
            Chain = new List<Block>();
            _hashingService = new HashingService();
            AddGenesisBlock("Genesis Block");
        }

        public void AddGenesisBlock(string data)
        {
            var genesisBlock = new Block
            {
                Index = 0,
                Data = "Genesis Block",
                PreviousHash = "0",
                TimeStamp = DateTime.Parse("2026-08-11")
            };
            genesisBlock.Hash = _hashingService.ComputeBlockHash(genesisBlock);
            Chain.Add(genesisBlock);

        }

        public void AddBlock(string data)   
        {
            var lastBlock = Chain[Chain.Count-1];
            var newBlock = new Block
            {
                Index = lastBlock.Index + 1,
                Data = data,
                PreviousHash = lastBlock.Hash,
                TimeStamp = DateTime.Now
            };
            newBlock.Hash = _hashingService.ComputeBlockHash(newBlock);
            Chain.Add(newBlock);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                var currentBlock = Chain[i];
                var previousBlock = Chain[i - 1];

                // Проверяем, что хеш текущего блока совпадает с вычисленным хешем
                if (currentBlock.Hash != _hashingService.ComputeBlockHash(currentBlock))
                {
                    return false;
                }
                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
    }

}
