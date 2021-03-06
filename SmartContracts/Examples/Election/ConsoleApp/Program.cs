﻿using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;

namespace ConsoleApp
{
    class Program
    {
        private static int timeoutInMinutes = 5;

        private static string Account1 = "0xd0352781461f5e06329ea6b9d002b67e475c0669";
        private static string Password = "password";

        /// <summary>
        /// Simple ConsoleApp to connect to a Ethereum to deploy a contract and run functions and transactions.
        /// This project depends on the Solidity project. So make sure to run `npm run build`.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Blockchain - Ethereum - ConsoleApp - Election");
            Console.WriteLine(new string('-', 80));

            try
            {
                TestService().Wait(TimeSpan.FromMinutes(timeoutInMinutes));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine(new string('_', 80));
        }

        private static async Task TestService()
        {
            var account = new ManagedAccount(Account1, Password);

            var web3 = new Web3Geth(account, "http://127.0.0.1:3100");
            web3.TransactionManager.DefaultGasPrice = BigInteger.Zero; // Needed for connecting to private network (gas has no real meaning there)

            // sample smart contract from https://github.com/jpmorganchase/quorum-examples/blob/master/examples/7nodes/private-contract.js
            var abi = @"[{""constant"":true,""inputs"":[],""name"":""storedData"",""outputs"":[{""name"":"""",""typeprivate-contract.js"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""x"",""type"":""uint256""}],""name"":""set"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""get"",""outputs"":[{""name"":""retVal"",""type"":""uint256""}],""payable"":false,""type"":""function""},{""inputs"":[{""name"":""initVal"",""type"":""uint256""}],""payable"":false,""type"":""constructor""}]";
            var bytecode = "0x6060604052341561000f57600080fd5b604051602080610149833981016040528080519060200190919050505b806000819055505b505b610104806100456000396000f30060606040526000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680632a1afcd914605157806360fe47b11460775780636d4ce63c146097575b600080fd5b3415605b57600080fd5b606160bd565b6040518082815260200191505060405180910390f35b3415608157600080fd5b6095600480803590602001909190505060c3565b005b341560a157600080fd5b60a760ce565b6040518082815260200191505060405180910390f35b60005481565b806000819055505b50565b6000805490505b905600a165627a7a72305820d5851baab720bba574474de3d09dbeaabc674a15f4dd93b974908476542c23f00029";

            Console.WriteLine("start 'private-contract.js' DeployContract.SendRequestAndWaitForReceiptAsync(...)");
            await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi, bytecode, Account1, new HexBigInteger(4700000), null);
            Console.WriteLine("end   'private-contract.js' DeployContract.SendRequestAndWaitForReceiptAsync(...)");

            Console.WriteLine("start ElectionContractService.DeployContractAsync(...)");
            string contractAddress = await ElectionContractService.DeployContractAsync(web3, Account1);
            Console.WriteLine("end   ElectionContractService.DeployContractAsync(...)");

            IElectionContractService service = new ElectionContractService(web3, contractAddress);

            BigInteger votesForAlice;
            Console.WriteLine($"votesForAlice before = {votesForAlice}");

            await service.ExecuteTransactionAsync((srv) => srv.VoteAsync(Account1, "Alice"));

            votesForAlice = await service.GetVoteCallAsync(Account1, "Alice");
            Console.WriteLine($"votesForAlice after = {votesForAlice}");

            try
            {
                // voting for Bob should fail !
                await service.ExecuteTransactionAsync((srv) => srv.VoteAsync(Account1, "Bob"));
            }
            catch (Exception)
            {
                Console.WriteLine("Account1 voting for Bob has failed (as expected)");
            }

            try
            {
                // Recursion should fail !
                await service.RecursionAsync(Account1, BigInteger.One);
            }
            catch (Exception)
            {
                Console.WriteLine("Recursion failed (as expected)");
            }
        }
    }
}