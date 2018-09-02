using System;
using System.Numerics;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;

namespace ConsoleApp
{
    class Program
    {
        private static int timeoutInMinutes = 5;

        /// <summary>
        /// Simple ConsoleApp to connect to a Ethereum Blockchain to deploy a contract and run functions and transactions.
        /// This project depends on the Solidity project. So make sure to run `npm run build`.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Blockchain - Ethereum - ConsoleApp - Multiply");

            string senderAddress = "0xd0352781461f5e06329ea6b9d002b67e475c0669";
            string password = "password";

            Console.WriteLine(new string('-', 80));

            try
            {
                TestService(senderAddress, password).Wait(TimeSpan.FromMinutes(timeoutInMinutes));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine(new string('_', 80));
        }

        private static async Task TestService(string fromAddress, string password)
        {
            var account = new ManagedAccount(fromAddress, password);

            var web3 = new Web3Geth(account, "http://127.0.0.1:3100");
            web3.TransactionManager.DefaultGasPrice = BigInteger.Zero; // Needed for connecting to private network (gas has no real meaning there)

            var gas = new HexBigInteger(900000);
            Console.WriteLine("MultiplyContractService.DeployContractAsync");
            string contractAddress = await MultiplyContractService.DeployContractAsync(web3, fromAddress, 4, null, gas);

            IMultiplyContractService service = new MultiplyContractService(web3, contractAddress);

            var addEvent = service.GetAddEvent();
            var addFilterAll = await addEvent.CreateFilterAsync();

            var resultServiceMultiply = await service.MultiplyCallAsync(fromAddress, 9);
            Console.WriteLine($"resultServiceMultiply = {resultServiceMultiply}");

            var resultServiceAdd1 = await service.AddCallAsync(fromAddress, 10, 20);
            Console.WriteLine($"resultServiceAdd1 = {resultServiceAdd1}");

            var resultServiceAdd2 = await service.AddCallAsync(fromAddress, 1, -2);
            Console.WriteLine($"resultServiceAdd2 = {resultServiceAdd2}");

            var receipt = await service.ExecuteTransactionAsync((srv) => srv.AddAsync(fromAddress, 5, 7));
            Console.WriteLine("receipt: " + JsonConvert.SerializeObject(receipt, Formatting.Indented));

            var addLog = await addEvent.GetFilterChanges<AddEvent>(addFilterAll);
            Console.WriteLine("addLog:");
            foreach (var log in addLog)
            {
                Console.WriteLine("log: " + JsonConvert.SerializeObject(log, Formatting.Indented));
            }

            await service.ExecuteTransactionAsync((srv) => srv.AddOrderAsync(fromAddress, "new 1"));
            await service.ExecuteTransactionAsync((srv) => srv.AddOrderAsync(fromAddress, "new 2"));
            await service.ExecuteTransactionAsync((srv) => srv.AddOrderAsync(fromAddress, "new 3"));
            try
            {
                await service.ExecuteTransactionAsync((srv) => srv.AddOrderAsync(fromAddress, ""));
            }
            catch
            {
                Console.WriteLine("Adding order with empty name fails (as expected)");
            }

            var count = await service.CountOrdersCallAsync(fromAddress);
            Console.WriteLine("count: " + count);

            for (int idx = 0; idx < count; idx++)
            {
                var orderId = await service.GetOrderIdAtIndexCallAsync(fromAddress, idx);
                var o = await service.GetOrderByIdCallAsync(fromAddress, orderId);
                Console.WriteLine("order[" + idx + "] = " + JsonConvert.SerializeObject(new { order = o, Guid = new Guid(o.Id) }, Formatting.Indented));
            }

            var orderGuid = await service.GetOrderIdAtIndexCallAsync(fromAddress, 0);
            var existingOrder = await service.GetOrderByIdCallAsync(fromAddress, orderGuid);
            Console.WriteLine("existingOrder: " + JsonConvert.SerializeObject(existingOrder, Formatting.Indented));

            Guid g = Guid.NewGuid();
            try
            {
                await service.GetOrderByIdCallAsync(fromAddress, g.ToByteArray());
            }
            catch
            {
                Console.WriteLine("Order not found by " + g + ", (as expected)");
            }
        }
    }
}