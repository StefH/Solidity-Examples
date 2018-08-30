pragma solidity ^0.4.24; // solhint-disable-line


contract MultiplyContract {
    address private owner;
    int private multiplier;
    int private storedValue;
    mapping (bytes16 => Order) private orders;
    bytes16[] private orderIdentifiers;

    struct Order {
        bytes16 id;
        string name;
        uint index;
    }

    event AddEvent (
        address indexed from,
        int indexed value1,
        int indexed value2
    );

    event StoreValueEvent (
        address indexed from,
        int indexed value
    );

    event AddOrderEvent (
        address indexed from,
        bytes16 indexed id,
        string name
    );

    // The function body is inserted where the special symbol "_;" in the definition of a modifier appears.
    // This means that if the owner calls this function, the function is executed and otherwise, an exception is thrown.
    modifier onlyOwner() {
        require(msg.sender == owner, "Invalid owner");
        _;
    }

    // Constructor
    constructor(int _multiplier) public {
        owner = msg.sender;
        multiplier = _multiplier;
    }

    // Using the "onlyOwner"-modifier causes that calls to "close" only have an effect if they are made by the stored owner.
    function close() public onlyOwner {
        selfdestruct(owner);
    }

    function multiply(int val) public view returns (int result) {
        return val * multiplier;
    }

    function add(int val1, int val2) public returns (int result) {
        emit AddEvent(msg.sender, val1, val2);

        return val1 + val2;
    }

    function storeValue(int val) public {
        storedValue = val;

        emit StoreValueEvent(msg.sender, val);
    }

    function addOrder(string name) public {
        require(bytes(name).length > 0, "Bad Request: 'name' is empty");

        uint currentTime = now; // solhint-disable-line
        bytes32 hashedValue = keccak256(abi.encodePacked(orderIdentifiers.length, name, currentTime));
        bytes16 uuid = bytes16(hashedValue);

        Order storage newOrder = orders[uuid];
        newOrder.id = uuid;
        newOrder.name = name;
        newOrder.index = orderIdentifiers.length;

        orderIdentifiers.push(uuid);

        emit AddOrderEvent(msg.sender, uuid, name);
    }

    function isExistingOrder(bytes16 id) public view returns(bool existing) {
        if (orderIdentifiers.length == 0) {
            return false;
        }

        return orderIdentifiers[orders[id].index] == id;
    }

    function countOrders() public view returns (uint count) {
        return orderIdentifiers.length;
    }

    function getOrderIdAtIndex(uint index) public view returns(bytes16 orderId) {
        require(index >= 0, "Bad Request: 'index' should be >= 0");

        return orderIdentifiers[index];
    }

    function getOrderById(bytes16 orderId) public view returns (bytes16 id, string name) {
        assert(isExistingOrder(orderId));

        Order storage foundOrder = orders[orderId];

        return (foundOrder.id, foundOrder.name);
    }
}