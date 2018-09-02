// Copied from https://ethereum.stackexchange.com/questions/11081/install-solc-compiler-on-windows-8/11086#11086
// and
// https://github.com/Nethereum/abi-code-gen

var solc = require('solc');
var fs = require('fs');
var glob = require('glob');
var ejs = require('ejs');
var path = require('path');

var inputContractFilename = process.argv[2];
var outputPath = process.argv[3];
var ns = process.argv.length > 4 ? process.argv[4] : 'CustomNameSpace';

var contractBasename = path.basename(inputContractFilename);

var input = {};
input[contractBasename] = fs.readFileSync(inputContractFilename, 'utf-8');

function solidityFindImports(contractFilename) {
    return { contents: fs.readFileSync(path.join(path.dirname(inputContractFilename), contractFilename), 'utf-8') };
}

// process.chdir(path.dirname(inputContractFilename));

console.log('Compiling contracts');
var optimizeBinaryCode = 1;
var output = solc.compile({ sources: input }, optimizeBinaryCode, solidityFindImports);
if (output.errors && output.errors.length > 0) {
    console.log('ERRORS found !');
    console.log(JSON.stringify(output.errors, null, 4));
}

for (var contractName in output.contracts) {
    var abi = output.contracts[contractName].interface;
    var code = output.contracts[contractName].bytecode;

    // Remove the : at the start from the contractName
    var contractFilename = contractName.split(':')[0];
    contractName = contractName.split(':')[1];

    var abiFilename = path.join(outputPath, contractName + '.abi');

    console.log(contractName + ': generate ABI and ByteCode');
    fs.writeFileSync(abiFilename, abi, 'utf-8');
    fs.writeFileSync(path.join(outputPath, contractName + '.bin'), code, 'utf-8');

    console.log(contractName + ': generate C# classes');
    fs.writeFileSync(path.join(outputPath, contractName + '.Generated.cs'), generateContractClass(contractName, ns, abi, code), 'utf-8');

    generateContractService(inputContractFilename, outputPath, contractName, ns, abi, code, 'cs-service');
}

function generateContractClass(contractName, ns, abi, code) {
    var c = `namespace ` + ns + `
{
    public class ` + contractName + `
    {
        public const string ABI = @"` + abi.replace(/\"/g, '""') + `";
        public const string ByteCode = "0x` + code + `";
    }
}`;

    return c;
}

function generateContractService(inputPath, outputPath, contractName, ns, abi, bytecode, generatorName) {
    var serviceFilename = path.join('../../../Common/Solidity/templates/' + generatorName + '.ejs');
    var interfaceFilename = path.join('../../../Common/Solidity/templates/' + generatorName + '-interface.ejs');

    var combinedInput = {
        _contractName: contractName,
        abi: JSON.parse(abi),
        bytecode: bytecode,
        namespace: ns
    };

    var templateService = ejs.compile(fs.readFileSync(serviceFilename, 'utf8'));
    fs.writeFileSync(path.join(outputPath, contractName + 'Service.Generated.cs'), templateService(combinedInput));

    var templateInterface = ejs.compile(fs.readFileSync(interfaceFilename, 'utf8'));
    fs.writeFileSync(path.join(outputPath, 'I' + contractName + 'Service.Generated.cs'), templateInterface(combinedInput));
}