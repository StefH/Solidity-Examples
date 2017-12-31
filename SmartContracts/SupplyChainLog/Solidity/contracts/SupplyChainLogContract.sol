pragma solidity ^0.4.18; // solhint-disable-line


import "./Authorization.sol";


contract SupplyChainLogContract is Authorization {
    // address constant public CONTRACT_UPLOADER = 0x12890d2cce102216644c59dae5baed380d84830c;

    // Constructor
    function SupplyChainLogContract() public {
        // require(msg.sender == CONTRACT_UPLOADER);
        owner = msg.sender;
    }

    function close() public onlyContractUploader {
        selfdestruct(owner);
    }
}