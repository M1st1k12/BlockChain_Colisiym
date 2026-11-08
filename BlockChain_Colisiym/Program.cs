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

var blockChainService = new BlockChain();
var displayService = new DisplayService();

blockChainService.AddBlock("Register user: Bob");
blockChainService.AddBlock("Bob buy iPhone 16");
blockChainService.AddBlock("Bob send 1000 USD to AppleStore");
blockChainService.AddBlock("Sign in user: Bob");
blockChainService.AddBlock("Create order: Bob, iPhone 16, 1000 USD");

if (blockChainService.IsValid())
{
    Console.WriteLine("The blockchain is valid.");
}
else
{
    Console.WriteLine("The blockchain is not valid.");
}

displayService.DisplayChain(blockChainService);
Console.WriteLine(new string('-', 30));
blockChainService.SearchBlockByIndex(2);
blockChainService.SearchEventByText("iPhone 16");
