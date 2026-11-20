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

//using System.Diagnostics;
//
//var blockChainService = new BlockChain("Sash");
//DisplayService displayService = new DisplayService();
//
//Stopwatch sw1 = Stopwatch.StartNew();
//blockChainService.AddBlock("alex send 1 BTC Bob ");
//blockChainService.AddBlock("Bob 0.5 BTC to Charlie");
//blockChainService.AddBlock("Charlie 0.2 BTC to Dave");
//sw1.Stop();
//
//var blockChainService2 = new BlockChain("Sash");
//
//Stopwatch sw2 = Stopwatch.StartNew();
//await blockChainService2.AddBlockAsync("alex send 1 BTC Bob ");
//await blockChainService2.AddBlockAsync("Bob 0.5 BTC to Charlie");
//await blockChainService2.AddBlockAsync("Charlie 0.2 BTC to Dave");
//sw2.Stop();
//
//displayService.DisplayChain(blockChainService2);
//Console.WriteLine($"Is blockchain valid? {blockChainService2.IsValid()}");
//Console.WriteLine($"Normal mining time: {sw1.ElapsedMilliseconds} ms");
//Console.WriteLine($"Async multithreaded mining time: {sw2.ElapsedMilliseconds} ms");

var blockChainService = new BlockChain("Sash");
var displayService = new DisplayService();


await blockChainService.AddBlockAsync("Register user: Bob", "System");
await blockChainService.AddBlockAsync("Bob buy iPhone 16", "Bob");
await blockChainService.AddBlockAsync("Bob send 1000 USD to AppleStore", "Bob");
await blockChainService.AddBlockAsync("Sign in user: Bob", "Bob");
await blockChainService.AddBlockAsync("Create order: Bob, iPhone 16, 1000 USD", "AppleStore");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Select an option:");
    Console.WriteLine("1. Add Block");
    Console.WriteLine("2. Display Chain");
    Console.WriteLine("3. Search Block by Index");
    Console.WriteLine("4. Search Event by Text");
    Console.WriteLine("5. Validate Chain");
    Console.WriteLine("6. Change Data in Block 2 (Hack Test)");
    Console.WriteLine("0. Exit");
    Console.Write("Choice: ");
    var enter = Console.ReadLine();
    Console.WriteLine();

    switch (enter)
    {
        case "1":
            Console.Write("Enter event data: ");
            var data = Console.ReadLine();
            Console.Write("Enter author: ");
            var author = Console.ReadLine();
            await blockChainService.AddBlockAsync(data, author);
            Console.WriteLine("Block added.");
            break;
        case "2":
            displayService.DisplayChain(blockChainService);
            break;
        case "3":
            Console.Write("Enter block index: ");
            var index = int.Parse(Console.ReadLine());
            blockChainService.SearchBlockByIndex(index);
            break;
        case "4":
            Console.Write("Enter text to search: ");
            var text = Console.ReadLine();
            blockChainService.SearchEventByText(text);
            break;
        case "5":
            if (blockChainService.IsValid())
            {
                Console.WriteLine("The blockchain is valid.");
            }
            else
            {
                Console.WriteLine("The blockchain is not valid.");
            }
            break;
        case "6":
            Console.Write("Enter new data for Block 2: ");
            var newData = Console.ReadLine();
            blockChainService.Chain[2].Data = newData;
            Console.WriteLine("Block 2 data changed.");
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}


