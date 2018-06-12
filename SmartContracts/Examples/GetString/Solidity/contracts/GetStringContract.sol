pragma solidity ^0.4.18; // solhint-disable-line


contract GetStringContract {
    string private myString = "someString";

    function getMyString() public view returns (bytes) {
        bytes memory b3 = bytes(myString);
        return b3;
    }
}