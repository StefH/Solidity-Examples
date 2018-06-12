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
        private const string Endpoint = "http://localhost:7545";

        // Note that the password does not matter when connecting to local Ganache
        private static readonly ManagedAccount Account = new ManagedAccount("0xe2e6a2994D00BDEcd1A4280EE9859793d950e16A", "test");

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