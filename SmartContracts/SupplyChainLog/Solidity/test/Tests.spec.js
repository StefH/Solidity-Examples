// const helper = require('./helper');
const chai = require('chai'); // http://chaijs.com/
const chaiAsPromised = require('chai-as-promised'); // http://chaijs.com/plugins/chai-as-promised/

chai.use(chaiAsPromised);
chai.should();
const assert = chai.assert;

const SupplyChainLogContract = artifacts.require('SupplyChainLogContract.sol');

contract('SupplyChainLogContract', (accounts) => {
    const owner = accounts[0];

    let contract;

    beforeEach('create new contract before each test', async () => {
        contract = await SupplyChainLogContract.new({ from: owner });
    });

    it('close', async () => {
        // Act
        const result = await contract.close({ from: owner });

        // Assert
        assert.isNotNull(result);
    });
});
