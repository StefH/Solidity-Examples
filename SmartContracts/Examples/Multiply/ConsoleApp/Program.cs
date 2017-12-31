using System;
using System.Threading;
using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Geth;

namespace ConsoleApp
{
    class Program
    {
        /// <summary>
        /// Simple ConsoleApp to connect to your local Ethereum to deploy a contract and run functions and transactions. This project depends on the Solidity project.
        /// So make sure to run `npm run build`.
        /// 
        /// Also make sure to start `\SupplyChainLog\Blockchain\testchain.bat`.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Blockchain - Ethereum - ConsoleApp");

            string senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            string password = "password";

            Console.WriteLine(new string('-', 80));

            TestService(senderAddress, password).Wait(60000);

            Console.WriteLine(new string('_', 80));
        }

        private static async Task TestService(string fromAddress, string password)
        {
            var account = new ManagedAccount(fromAddress, password);

            var web3 = new Web3Geth(account);

            var gas = new HexBigInteger(900000);
            Console.WriteLine("DeployContractWithMultiplyContractService");
            string contractAddress = await MultiplyContractService.DeployContractAsync(web3, fromAddress, 4, null, gas);

            IMultiplyContractService service = new MultiplyContractService(web3, contractAddress);

            var addEvent = service.GetAddEvent();
            var addFilterAll = await addEvent.CreateFilterAsync();

            var resultServiceMultiply = await service.MultiplyCallAsync(fromAddress, 9, gas);
            Console.WriteLine($"resultServiceMultiply = {resultServiceMultiply}");

            var resultServiceAdd1 = await service.AddCallAsync(fromAddress, 10, 20, gas);
            Console.WriteLine($"resultServiceAdd1 = {resultServiceAdd1}");

            var resultServiceAdd2 = await service.AddCallAsync(fromAddress, 1, -2, gas);
            Console.WriteLine($"resultServiceAdd2 = {resultServiceAdd2}");

            var transactionHash = await service.AddAsync(fromAddress, 5, 7, gas);
            Console.WriteLine($"transactionHash for Add = {transactionHash}");

            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            while (receipt == null)
            {
                Thread.Sleep(1000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }
            Console.WriteLine("receipt: " + JsonConvert.SerializeObject(receipt, Formatting.Indented));

            var addLog = await addEvent.GetFilterChanges<AddEvent>(addFilterAll);
            Console.WriteLine("addLog:");
            foreach (var log in addLog)
            {
                Console.WriteLine("log: " + JsonConvert.SerializeObject(log, Formatting.Indented));
            }

            var t1 = await service.AddOrderAsync(fromAddress, "new 1", gas);
            var t2 = await service.AddOrderAsync(fromAddress, "new 2", gas);
            var t3 = await service.AddOrderAsync(fromAddress, "new 3", gas);
            var t4 = await service.AddOrderAsync(fromAddress, "", gas);

            receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(t4);
            while (receipt == null)
            {
                Thread.Sleep(1000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(t4);
            }

            var count = await service.CountOrdersCallAsync(fromAddress);
            Console.WriteLine("count: " + count);

            for (int idx = 0; idx < count; idx++)
            {
                var orderId = await service.GetOrderIdAtIndexCallAsync(fromAddress, idx, gas);
                var o = await service.GetOrderByIdCallAsync(fromAddress, orderId, gas);
                Console.WriteLine("order[" + idx + "] = " + JsonConvert.SerializeObject(new { order = o, Guid = new Guid(o.Id) }, Formatting.Indented));
            }

            var orderGuid = await service.GetOrderIdAtIndexCallAsync(fromAddress, 0, gas);
            var existingOrder = await service.GetOrderByIdCallAsync(fromAddress, orderGuid, gas);
            Console.WriteLine("existingOrder: " + JsonConvert.SerializeObject(existingOrder, Formatting.Indented));

            Guid g = Guid.NewGuid();
            var notfoundOrder = await service.GetOrderByIdCallAsync(fromAddress, g.ToByteArray(), gas);
            Console.WriteLine("notfoundOrder: " + JsonConvert.SerializeObject(notfoundOrder, Formatting.Indented));

            var closeResult = await service.CloseAsync(fromAddress, gas);
            Console.WriteLine($"closeResult = {closeResult}");

            var transactionHash2 = await service.AddAsync(fromAddress, 12, 13, gas);
            Console.WriteLine($"transactionHash2 for Add = {transactionHash2}");
        }
    }
}