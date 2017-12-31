pragma solidity ^0.4.18;


import "./Common.sol";


contract Authorization is Common {
    address internal owner;

    // The function body is inserted where the special symbol "_;" in the definition of a modifier appears.
    // This means that if the owner calls this function, the function is executed and otherwise, an exception is thrown.
    modifier onlyContractUploader() {
        require(msg.sender == owner);
        _;
    }
}