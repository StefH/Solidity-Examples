pragma solidity ^0.4.18;


contract Common {
    uint constant internal MINIMUM_TIMESTAMP = 1483228800;  // Must be in 2017

    function stringEquals(string value1, string value2) internal pure returns (bool) {
       return keccak256(value1) == keccak256(value2);
    }
}