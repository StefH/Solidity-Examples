var UUIDProviderContract = artifacts.require("./UUIDProviderContract.sol");

contract('UUIDProviderContract', (accounts) => {
    var contract;

    beforeEach(async () => {
        // Assign
        contract = await UUIDProviderContract.deployed();
    });

    it("generate", async () => {
        // Act
        var result = await contract.generateUUID4();

        // Assert
        assert.isTrue(result !== null);

        var log = result.logs[0];
        assert.isTrue(log !== null)

        var event = log.event;
        assert.equal(event, 'UUIDEvent');

        var eventArgs = log.args;
        assert.isTrue(eventArgs.uuid !== null);
    });
});
