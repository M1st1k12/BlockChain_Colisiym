using BlockChain_Colisiym.Models;
using BlockChain_Colisiym.Services;

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
////var blockChainService = new BlockChain("Sash");
//var blockChainService = new BlockChain(5);
//DisplayService displayService = new DisplayService();
//
//Stopwatch sw1 = Stopwatch.StartNew();
//await blockChainService.AddBlockAsync("alex send 1 BTC Bob ");
//await blockChainService.AddBlockAsync("Bob 0.5 BTC to Charlie");
//await blockChainService.AddBlockAsync("Charlie 0.2 BTC to Dave");
//sw1.Stop();
//
//displayService.DisplayChain(blockChainService);
//Console.WriteLine($"Is blockchain valid? {blockChainService.IsValid()}");
//Console.WriteLine($"time: {sw1.ElapsedMilliseconds} ms");

//var displayService = new DisplayService();
//var blockChainService = new BlockChain();
//
//while (true)
//{
//    Console.ReadLine();
//    Console.Clear();
//    Console.WriteLine("Select an option:");
//    Console.WriteLine("1. Add Block");
//    Console.WriteLine("2. Display Chain");
    //    Console.WriteLine("3. Validate Chain");
//    Console.WriteLine("4. Add 8 Blocks");
//    Console.WriteLine("5. Difficulty History");
//    Console.WriteLine("0. Exit");
//    var enter = Console.ReadLine();
//    switch (enter)
//    {
//        case "1":
//            await blockChainService.AddBlockAsync("Bob send 100 coins to Alice");
//            break;
//        case "2":
//            displayService.DisplayChain(blockChainService);
//            break;
//        case "3":
//            var isValid = blockChainService.IsValid();
//            Console.WriteLine($"Is blockchain valid? {isValid}");
//            break;
//        case "4":
//            for (int i = 0; i < 8; i++)
//            {
//                await blockChainService.AddBlockAsync($"Block {i + 1} data");
//            }
//            break;
//        case "5":
//            blockChainService.PrintDifficultyHistory();
//            break;
//        case "0":
//            return; // Exit the application
//        default:
//            Console.WriteLine("Invalid option.");
//            break;
//    }
//}

//var displayService = new DisplayService();
//var blockChainService = new BlockChain();
//var tempListTransaction = new List<Transaction>();
//var transactionService = new TransactionService();
//
//// Стартовые 4 блока и 12 разных транзакций
//await blockChainService.AddBlockAsync(new List<Transaction>
//{
//    transactionService.CreateTransaction("Alex", "Bob", 50),
//    transactionService.CreateTransaction("Bob", "Eva", 20),
//    transactionService.CreateTransaction("Eva", "Dave", 15)
//});
//
//await blockChainService.AddBlockAsync(new List<Transaction>
//{
//    transactionService.CreateTransaction("Dave", "Alex", 10),
//    transactionService.CreateTransaction("Alex", "Eva", 35),
//    transactionService.CreateTransaction("Bob", "Dave", 40)
//});
//
//await blockChainService.AddBlockAsync(new List<Transaction>
//{
//    transactionService.CreateTransaction("Eva", "Alex", 25),
//    transactionService.CreateTransaction("Dave", "Bob", 60),
//    transactionService.CreateTransaction("Alex", "Dave", 12)
//});
//
//await blockChainService.AddBlockAsync(new List<Transaction>
//{
//    transactionService.CreateTransaction("Bob", "Alex", 30),
//    transactionService.CreateTransaction("Eva", "Bob", 18),
//    transactionService.CreateTransaction("Dave", "Eva", 45)
//});
//
//while (true)
//{
//    Console.WriteLine();
//    Console.WriteLine("Select an option:");
//    Console.WriteLine("1. Add Block");
//    Console.WriteLine("2. Add Transaction");
//    Console.WriteLine("3. Display Chain");
//    Console.WriteLine("4. Display Transaction");
//    Console.WriteLine("5. Chain Valid");
//    Console.WriteLine("6. Add 20 Blocks");
//    Console.WriteLine("7. Search Transaction by Id");
//    Console.WriteLine("8. Search Transactions by User");
//    Console.WriteLine("0. Exit");
//    Console.Write("Choice: ");
//    var enter = Console.ReadLine();
//    Console.WriteLine();
//
//    switch (enter)
//    {
//        case "1":
//            await blockChainService.AddBlockAsync(tempListTransaction);
//            tempListTransaction.Clear();
//            Console.WriteLine("Block added.");
//            break;
//        case "2":
//            Console.Write("From: ");
//            var from = Console.ReadLine();
//            Console.Write("To: ");
//            var to = Console.ReadLine();
//            Console.Write("Amount: ");
//            var amount = decimal.Parse(Console.ReadLine());
//            tempListTransaction.Add(transactionService.CreateTransaction(from, to, amount));
//            Console.WriteLine("Transaction added.");
//            break;
//        case "3":
//            displayService.DisplayChain(blockChainService);
//            break;
//        case "4":
//            displayService.DisplayTransactions(tempListTransaction);
//            break;
//        case "5":
//            var isValid = blockChainService.IsValid();
//            Console.WriteLine($"Is chain valid? {isValid}");
//            break;
//        case "6":
//            for (int i = 0; i < 20; i++)
//            {
//                await blockChainService.AddBlockAsync(tempListTransaction);
//                tempListTransaction.Clear();
//            }
//            Console.WriteLine("20 blocks added.");
//            break;
//        case "7":
//            Console.Write("Enter Transaction Id: ");
//            var txId = Console.ReadLine();
//            blockChainService.SearchTransactionById(txId);
//            break;
//        case "8":
//            Console.Write("Enter user name: ");
//            var user = Console.ReadLine();
//            Console.WriteLine("Filter: 1 - All, 2 - Sent only, 3 - Received only");
//            Console.Write("Select filter: ");
//            var filter = Console.ReadLine();
//            blockChainService.SearchTransactionsByUser(user, filter);
//            break;
//        case "0":
//            return;
//        default:
//            Console.WriteLine("Invalid option. Please try again.");
//            break;
//    }
//}

var displayService = new DisplayService();
var blockChainService = new BlockChain();
var tempListTransaction = new List<Transaction>();
var transactionService = new TransactionService();
var balanceService = new BalanceService();

var walletService = new WalletService();

var bobwallet = walletService.CreateWallet("Bob");
var alicewallet = walletService.CreateWallet("Alice");
var charliewallet = walletService.CreateWallet("Charlie");

decimal GetAvailableBalance(string address)
{
    var balance = balanceService.GetBalance(address, blockChainService.Chain);
    foreach (var tx in tempListTransaction)
    {
        if (tx.From == address) balance -= tx.Amount;
    }
    return balance;
}

Wallet SelectWallet(string prompt)
{
    Console.WriteLine(prompt);
    Console.WriteLine($"1. Alice ({alicewallet.Address}) - Balance: {GetAvailableBalance(alicewallet.Address)}");
    Console.WriteLine($"2. Bob ({bobwallet.Address}) - Balance: {GetAvailableBalance(bobwallet.Address)}");
    Console.WriteLine($"3. Charlie ({charliewallet.Address}) - Balance: {GetAvailableBalance(charliewallet.Address)}");
    Console.Write("Select: ");
    var choice = Console.ReadLine();
    return choice switch
    {
        "2" => bobwallet,
        "3" => charliewallet,
        _ => alicewallet
    };
}

while (true)
{
    Console.WriteLine("1. Add Block");
    Console.WriteLine("2. Add Transaction");
    Console.WriteLine("3. Display Chain");
    Console.WriteLine("4. Display Transaction");
    Console.WriteLine("5. Chain Valid");
    Console.WriteLine("6. Add 20 Blocks");
    Console.WriteLine("7. Display Wallet Balances");
    Console.WriteLine("8. Print Wallet Transaction History");
    Console.WriteLine("9. Search Transaction by Id");
    Console.WriteLine("10. Search Transactions by User");
    Console.WriteLine("11. Audit Blockchain");
    Console.WriteLine("12. Tamper Transaction Amount (Hack Test)");
    Console.WriteLine("0. Exit");

    var enter = Console.ReadLine();
    Console.Clear();
    switch (enter)
    {
        case "1":
            var miner = SelectWallet("Who will mine the block (+10 coins)?");
            try
            {
                await blockChainService.AddBlockAsync(tempListTransaction, miner.Address, tempListTransaction);
                tempListTransaction.Clear();
                Console.WriteLine($"Block mined! {miner.Name} received 10 coins.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                tempListTransaction.Clear();
            }
            break;
        case "2":
            //tempListTransaction.Add(transactionService.CreateTransaction(alicewallet.Address, bobwallet.Address, 10, alicewallet));
            var sender = SelectWallet("From:");
            var receiver = SelectWallet(" To:");
            Console.Write("Enter Amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                try
                {
                    var tx = transactionService.CreateTransaction(sender.Address, receiver.Address, amount, sender);
                    var balanceCheck = transactionService.ValidateBalance(tx, blockChainService.Chain, tempListTransaction);
                    if (!balanceCheck.isValid)
                    {
                        Console.WriteLine($"Error: {balanceCheck.errorMessage}");
                    }
                    else
                    {
                        tempListTransaction.Add(tx);
                        Console.WriteLine($"Transaction added: {sender.Name} -> {receiver.Name} ({amount} coins)");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            break;
        case "3":
            displayService.DisplayChain(blockChainService);
            break;
        case "4":
            displayService.DisplayTransactions(tempListTransaction);
            break;
        case "5":
            var isValid = blockChainService.IsValid();
            Console.WriteLine($"Is chain valid? {isValid}");
            break;
        case "6":
            for (int i = 0; i < 20; i++)
            {
                await blockChainService.AddBlockAsync(tempListTransaction, alicewallet.Address, tempListTransaction);
                tempListTransaction.Clear();
            }
            break;
        case "7":
            Console.WriteLine("Wallet Balances:");
            Console.WriteLine($"Bob ({bobwallet.Address}): {balanceService.GetBalance(bobwallet.Address, blockChainService.Chain)}");
            Console.WriteLine($"Alice ({alicewallet.Address}): {balanceService.GetBalance(alicewallet.Address, blockChainService.Chain)}");
            Console.WriteLine($"Charlie ({charliewallet.Address}): {balanceService.GetBalance(charliewallet.Address, blockChainService.Chain)}");
            break;
        case "8":
            var selectedWallet = SelectWallet("Select wallet to view transaction history:");
            displayService.PrintTransactionHistory(blockChainService, selectedWallet);
            break;
        case "9":
            Console.Write("Enter Transaction Id: ");
            var txId = Console.ReadLine();
            blockChainService.SearchTransactionById(txId);
            break;
        case "10":
            var searchWallet = SelectWallet("Select user wallet to search:");
            Console.WriteLine("Filter: 1 - All, 2 - Sent only, 3 - Received only");
            Console.Write("Select filter: ");
            var filter = Console.ReadLine();
            blockChainService.SearchTransactionsByUser(searchWallet.Address, filter);
            break;
        case "11":
            blockChainService.AuditBlockchain();
            break;
        case "12":
            if (blockChainService.Chain.Count > 1 && blockChainService.Chain[1].Transactions.Count > 0)
            {
                blockChainService.Chain[1].Transactions[0].Amount = 999;
                Console.WriteLine("Changed Block 1 transaction Amount to 999");
            }
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}



