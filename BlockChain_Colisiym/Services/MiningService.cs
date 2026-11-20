using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockChain_Colisiym.Models;

namespace BlockChain_Colisiym.Services
{
    public class MiningService
    {
        private readonly HashingService _hashingService;
        public MiningService(HashingService hashingService)
        {
            _hashingService = hashingService;
        }
        public void MineBlock(Block block, int difficulty)
        {
            // Создаем строку, состоящую из '0', длиной равной сложности
            string target = new string('0', difficulty);
            // Продолжаем изменять nonce и вычислять хеш, пока не найдем подходящий хеш
            var startTime = Stopwatch.StartNew();
            while (true)
            {
                block.Hash = _hashingService.ComputeBlockHash(block);
                if (block.Hash.Substring(0, difficulty) == target) // Проверяем, что хеш начинается с нужного количества нулей
                {
                    startTime.Stop();
                    block.MiningDuration = (decimal)startTime.Elapsed.TotalSeconds;
                    //Console.WriteLine($"Hash found: {block.Hash} with Nonce: {block.Nonce}");
                    break;
                }
                //if (block.Nonce % 100000 == 0) // Каждые 100000 итераций выводим текущий хеш и nonce
                //{
                //    Console.WriteLine($"Current Hash: {block.Hash}, Nonce: {block.Nonce}");
                //}
                block.Nonce++;
            }
        }

        // Асинхронный многопоточный майнинг (по количеству нулей)
        public async Task MineBlockAsync(Block block, int difficulty)
        {
            string target = new string('0', difficulty);
            int threadCount = Environment.ProcessorCount;
            bool isFound = false;
            object lockObj = new object();
            var tasks = new List<Task>();
            var startTime = Stopwatch.StartNew();

            for (int i = 0; i < threadCount; i++)
            {
                int threadIndex = i;
                tasks.Add(Task.Run(() =>
                {
                    // Каждый поток проверяет свои значения Nonce
                    int nonce = threadIndex;

                    while (!isFound)
                    {
                        string hash = _hashingService.ComputeBlockHash(block, nonce);

                        if (hash.Substring(0, difficulty) == target)
                        {
                            lock (lockObj)
                            {
                                if (!isFound)
                                {
                                    isFound = true;
                                    startTime.Stop();
                                    block.MiningDuration = (decimal)startTime.Elapsed.TotalSeconds;
                                    block.Nonce = nonce;
                                    block.Hash = hash;
                                    //Console.WriteLine($"Hash found: {block.Hash} with Nonce: {block.Nonce}");
                                }
                            }
                            break;
                        }

                        //if (nonce % 1000000 == 0)
                        //{
                        //    Console.WriteLine($"Current Hash: {hash}, Nonce: {nonce}");
                        //}

                        nonce += threadCount;
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }

        // Майнинг по имени (префиксу)
        public void MineBlock(Block block, string targetPrefix)
        {
            while (true)
            {
                block.Hash = _hashingService.ComputeBlockHash(block);
                // Проверяем, что хеш начинается с нужного имени
                if (block.Hash.StartsWith(targetPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Hash found: {block.Hash} with Nonce: {block.Nonce}");
                    break;
                }
                if (block.Nonce % 100000 == 0)
                {
                    Console.WriteLine($"Current Hash: {block.Hash}, Nonce: {block.Nonce}");
                }
                block.Nonce++;
            }
        }

        // Асинхронный многопоточный майнинг
        public async Task MineBlockAsync(Block block, string targetPrefix)
        {
            int threadCount = Environment.ProcessorCount;
            bool isFound = false;
            object lockObj = new object();
            var tasks = new List<Task>();

            for (int i = 0; i < threadCount; i++)
            {
                int threadIndex = i;
                tasks.Add(Task.Run(() =>
                {
                    // Каждый поток проверяет свои значения Nonce
                    int nonce = threadIndex;

                    while (!isFound)
                    {
                        string hash = _hashingService.ComputeBlockHash(block, nonce);

                        if (hash.StartsWith(targetPrefix, StringComparison.OrdinalIgnoreCase))
                        {
                            lock (lockObj)
                            {
                                if (!isFound)
                                {
                                    isFound = true;
                                    block.Nonce = nonce;
                                    block.Hash = hash;
                                    Console.WriteLine($"Hash found: {block.Hash} with Nonce: {block.Nonce}");
                                }
                            }
                            break;
                        }

                        if (nonce % 1000000 == 0)
                        {
                            Console.WriteLine($"Current Hash: {hash}, Nonce: {nonce}");
                        }

                        nonce += threadCount;
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }
    }
}
