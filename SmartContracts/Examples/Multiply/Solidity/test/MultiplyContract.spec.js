// var UUIDProviderContract = artifacts.require("../../UUIDProvider/Solidity/contracts/UUIDProviderContract.sol");
var MultiplyContract = artifacts.require("./MultiplyContract.sol");

contract('MultiplyContract', (accounts) => {
    var contract;

    beforeEach(async () => {
        // await UUIDProviderContract.deployed();
        contract = await MultiplyContract.deployed();
    });

    it("multiply", async () => {
        var result = await contract.multiply(7);
        assert.equal(result.valueOf(), 28);
    });

    it("add", async () => {
        var result = await contract.add(1, 9);
        assert.isTrue(result !== null)

        var log = result.logs[0];
        assert.isTrue(log !== null)

        var event = log.event;
        assert.equal(event, 'AddEvent');

        var eventArgs = log.args;
        assert.isTrue(eventArgs.from !== null);
        assert.equal(eventArgs.value1, 1);
        assert.equal(eventArgs.value2, 9);
    });

    it("add order", async () => {
        // Act
        var result = await contract.addOrder('order 1');

        // Assert
        assert.isTrue(result !== null)

        var log = result.logs[0];
        assert.isTrue(log !== null)

        var event = log.event;
        assert.equal(event, 'AddOrderEvent');

        var eventArgs = log.args;
        assert.isTrue(eventArgs.from.length > 0);
        assert.isTrue(eventArgs.id.length > 0);
        assert.isTrue(eventArgs.name.length > 0);
    });

    it("add order with empty name - fail", async () => {
        // Act + Assert
        try {
            await contract.addOrder('');
            assert.fail();
        }
        catch (err) {
        }
    });
});
