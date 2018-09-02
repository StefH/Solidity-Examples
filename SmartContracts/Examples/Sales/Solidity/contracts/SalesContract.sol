pragma solidity ^0.4.20;

contract WorkbenchBase {

    event WorkbenchContractCreated(string applicationName, string workflowName, address originatingAddress);
    event WorkbenchContractUpdated(string applicationName, string workflowName, string action, address originatingAddress);
    
    string internal ApplicationName;
    string internal WorkflowName;
    
    function WorkbenchBase(string applicationName, string workflowName) internal {
        ApplicationName = applicationName;
        WorkflowName = workflowName;
    }

    function ContractCreated() internal {
        WorkbenchContractCreated(ApplicationName, WorkflowName, msg.sender);
    }
    function ContractUpdated(string action) internal {
        WorkbenchContractUpdated(ApplicationName, WorkflowName, action, msg.sender);
    }
}

contract SalesContract is WorkbenchBase("SalesContract", "SalesContract"){
    enum StateType {BankGuaranteeRequested, CustomerAuthorized, FailedCustomerAuthorization, BankGuaranteeIssued, BankGuaranteeApproved, BankGuaranteeRejected, OrderConfirmed, ATCReleased, OrderHeld, OrderVoided, OrderFulfilled}

    StateType public State;

    address public BankAdmin;
    address public CorporateSales;
    address public BankManager;
    address public CorporateSalesDirector;
    address public FactoryManager;

    string public ATC;
    string public Request;
    string public BankGuarantee;

    function SalesContract(string request, address corporateSales, address bankManager) public {
        BankAdmin=msg.sender;
        CorporateSales=corporateSales;
        BankManager = bankManager;
        Request=request;
        State=StateType.BankGuaranteeRequested;
        ContractCreated();
    }

    function AuthorizeCustomer(address corporateSalesDirector) public {
        CorporateSalesDirector=corporateSalesDirector;
        if(msg.sender!=CorporateSales){
            revert();
        }
        State=StateType.CustomerAuthorized;
        ContractUpdated("AuthorizeCustomer");
    }

    function RejectCustomerAuth() public {
        if(msg.sender!=CorporateSales||msg.sender!=BankManager){
            revert();
        }
        State=StateType.FailedCustomerAuthorization;
        ContractUpdated("RejectCustomerAuth");
    }

    function IssueBankGuarantee(string bankGuarantee) public {
        if(msg.sender!=BankManager){
            revert();
        }
        BankGuarantee=bankGuarantee;
        State=StateType.BankGuaranteeIssued;
        ContractUpdated("IssueBankGuarantee");

    }
    function ApproveBankGuarantee() public {
        if(msg.sender!=CorporateSalesDirector){
            revert();
        }
        State=StateType.BankGuaranteeApproved;
        ContractUpdated("ApproveBankGuarantee");
    }
    function RejectBankGuarantee() public {
        if(msg.sender!=CorporateSalesDirector){
            revert();
        }
        State=StateType.BankGuaranteeRejected;
        ContractUpdated("RejectBankGuarantee");
    }
    function ConfirmOrder(address factoryManager) public {
        if(msg.sender!=CorporateSales){
            revert();
        }
        FactoryManager=factoryManager;
        State=StateType.OrderConfirmed;
        ContractUpdated("ConfirmOrder");
    }
    function ReleaseATC(string atc) public {
        if(msg.sender!=CorporateSales){
            revert();
        }
        ATC=atc;
        State=StateType.ATCReleased;
        ContractUpdated("ReleaseATC");
    }
    function HoldOrder() public {
        if(msg.sender!=CorporateSales){
            revert();
        }
        State=StateType.OrderHeld;
        ContractUpdated("HoldOrder");
    }
    function VoidOrder() public {
        if(msg.sender!=CorporateSales){
            revert();
        }
        State=StateType.OrderVoided;
        ContractUpdated("VoidOrder");
    }
    function FulfillOrder() public {
        if(msg.sender!=FactoryManager){
            revert();
        }
        State=StateType.OrderFulfilled;
        ContractUpdated("FulfillOrder");
    }

}