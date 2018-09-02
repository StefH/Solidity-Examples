// var ElectionContract = artifacts.require("./ElectionContract.sol");

// contract('ElectionContract', (accounts) => {
//     var contract;

//     beforeEach(async () => {
//         contract = await ElectionContract.deployed();
//     });

//     it("getVote", async () => {
//         // Act
//         var resultAlice = await contract.getVote('Alice');
//         var resultBob = await contract.getVote('Bob');

//         // Assert
//         assert.equal(resultAlice.valueOf(), 0);
//         assert.equal(resultBob.valueOf(), 0);
//     });

//     it("vote and getVote multiple times", async () => {
//         // Act
//         await contract.vote('Alice');
//         await contract.vote('Alice', { from: accounts[1] });
//         await contract.vote('Alice', { from: accounts[2] });

//         // Act
//         var resultAlice = await contract.getVote('Alice');

//         // Assert
//         assert.equal(resultAlice.valueOf(), 3);
//     });

//     // it("vote and getVote", async () => {
//     //     // Assign
//     //     await contract.vote('Alice', { from: accounts[1] });
//     //     await contract.vote('Bob', { from: accounts[2] });

//     //     // Act
//     //     var resultAlice = await contract.getVote('Alice');
//     //     var resultBob = await contract.getVote('Bob');

//     //     // Assert
//     //     assert.equal(resultAlice.valueOf(), 1);
//     //     assert.equal(resultBob.valueOf(), 1);
//     // });

//     // it("vote multiple times on same name - fails", async () => {
//     //     // Assign
//     //     await contract.vote('Alice');

//     //     // Act and Assert
//     //     try {
//     //         await contract.vote('Alice');
//     //         assert.fail();
//     //     }
//     //     catch (err) {
//     //     }
//     // });

//     // it("getVote with empty name - fail", async () => {
//     //     // Act + Assert
//     //     try {
//     //         await contract.getVote('');
//     //         assert.fail();
//     //     }
//     //     catch (err) {
//     //     }
//     // });

//     // it("vote with empty name - fail", async () => {
//     //     // Act + Assert
//     //     try {
//     //         await contract.vote('');
//     //         assert.fail();
//     //     }
//     //     catch (err) {
//     //     }
//     // });
// });
