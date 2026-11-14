using BlockChain_Colisiym;

//var blockChainService = new BlockChain();
//var displayService = new DisplayService();

//blockChainService.AddBlock("alex send bob 100");
//blockChainService.AddBlock("bob send alex 50");
//blockChainService.AddBlock("Eva send frank 25");
//blockChainService.AddBlock("Dave send eva 10");

//displayService.DisplayChain(blockChainService);
//var isValid = blockChainService.IsValid();
//if (isValid)
//{
//    Console.WriteLine("The blockchain is valid.");
//}
//else
//{
//    Console.WriteLine("The blockchain is not valid.");
//}


//blockChainService.Chain[2].Data = "bob send alex 500"; // Изменяем данные блока с индексом 2

//displayService.DisplayChain(blockChainService);

//isValid = blockChainService.IsValid();
//if (isValid)
//{
//    Console.WriteLine("The blockchain is valid.");
//}
//else
//{
//    Console.WriteLine("The blockchain is not valid.");
//}

//var blockChainService = new BlockChain(5);
//var blockChainService = new BlockChain("Sasha");
//DisplayService displayService = new DisplayService();
//
//blockChainService.AddBlock("alex send 1 BTC Bob ");
//
//displayService.DisplayChain(blockChainService);
//Console.WriteLine($"Is blockchain valid? {blockChainService.IsValid()}");

using System.Diagnostics;

var blockChainService = new BlockChain("Sash");
DisplayService displayService = new DisplayService();

Stopwatch sw1 = Stopwatch.StartNew();
blockChainService.AddBlock("alex send 1 BTC Bob ");
blockChainService.AddBlock("Bob 0.5 BTC to Charlie");
blockChainService.AddBlock("Charlie 0.2 BTC to Dave");
sw1.Stop();

var blockChainService2 = new BlockChain("Sash");

Stopwatch sw2 = Stopwatch.StartNew();
await blockChainService2.AddBlockAsync("alex send 1 BTC Bob ");
await blockChainService2.AddBlockAsync("Bob 0.5 BTC to Charlie");
await blockChainService2.AddBlockAsync("Charlie 0.2 BTC to Dave");
sw2.Stop();

displayService.DisplayChain(blockChainService2);
Console.WriteLine($"Is blockchain valid? {blockChainService2.IsValid()}");
Console.WriteLine($"Normal mining time: {sw1.ElapsedMilliseconds} ms");
Console.WriteLine($"Async multithreaded mining time: {sw2.ElapsedMilliseconds} ms");


