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

blockChainService.AddBlock("Register user: Bob", "System");
blockChainService.AddBlock("Bob buy iPhone 16", "Bob");
blockChainService.AddBlock("Bob send 1000 USD to AppleStore", "Bob");
blockChainService.AddBlock("Sign in user: Bob", "Bob");
blockChainService.AddBlock("Create order: Bob, iPhone 16, 1000 USD", "AppleStore");

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

// Проверяем, что хеш учитывает поле Author (пробуем подменить автора)
Console.WriteLine(new string('-', 30));
blockChainService.Chain[1].Author = "Sasha";
Console.WriteLine("Changed Block 1 Author to Sasha");
if (blockChainService.IsValid())
{
    Console.WriteLine("The blockchain is valid.");
}
else
{
    Console.WriteLine("The blockchain is not valid.");
}
