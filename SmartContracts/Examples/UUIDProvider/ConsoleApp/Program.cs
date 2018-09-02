using System;
using Nethereum.Hex.HexTypes;
using System.Threading.Tasks;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Geth;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        /// <summary>
        /// Simple ConsoleApp to connect to a Ethereum Blockchain to deploy a contract and run functions and transactions.
        /// This project depends on the Solidity project. So make sure to run `npm run build`.
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
            Console.WriteLine("DeployContract");
            string contractAddress = await UUIDProviderContractService.DeployContractAsync(web3, fromAddress, null, gas);

            IUUIDProviderContractService service = new UUIDProviderContractService(web3, contractAddress);

            byte[] uuid1 = await service.GenerateUUID4CallAsync(fromAddress, gas);
            var guid1 = new Guid(uuid1);
            Console.WriteLine($"guid1 = {guid1}");

            string trans = await service.GenerateUUID4Async(fromAddress, gas);

            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(trans);
            while (receipt == null)
            {
                Thread.Sleep(1000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(trans);
            }

            byte[] uuid2 = await service.GenerateUUID4CallAsync(fromAddress, gas);
            var guid2 = new Guid(uuid2);
            Console.WriteLine($"guid2 = {guid2}");
        }
    }
}