using System;
using System.Threading.Tasks;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Geth;

namespace ConsoleApp
{
    class Program
    {
        private const string Endpoint = "http://localhost:7545";

        // Note that the password does not matter when connecting to local Ganache
        private static readonly ManagedAccount Account = new ManagedAccount("0xe2e6a2994D00BDEcd1A4280EE9859793d950e16A", "test");

        /// <summary>
        /// Simple ConsoleApp to connect to a Ethereum Blockchain to deploy a contract and run functions and transactions.
        /// This project depends on the Solidity project. So make sure to run `npm run build`.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Blockchain - Ganache - ConsoleApp");

            Console.WriteLine(new string('_', 80));

            TestService().Wait(30000);

            Console.WriteLine(new string('_', 80));
        }

        private static async Task TestService()
        {
            var web3 = new Web3Geth(Account, Endpoint);

            Console.WriteLine("DeployContractAsync");
            string contractAddress = await GetStringContractService.DeployContractAsync(web3, Account.Address);

            IGetStringContractService service = new GetStringContractService(web3, contractAddress);

            Console.WriteLine("Call GetMyStringCallAsync");
            byte[] result = await service.GetMyStringCallAsync();
            string str = System.Text.Encoding.UTF8.GetString(result);
            Console.WriteLine("result = `" + str + "`");
        }
    }
}