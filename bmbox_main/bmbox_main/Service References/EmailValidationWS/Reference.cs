﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bmbox_main.EmailValidationWS {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.webservicex.net", ConfigurationName="EmailValidationWS.ValidateEmailSoap")]
    public interface ValidateEmailSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.webservicex.net/IsValidEmail", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool IsValidEmail(string Email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.webservicex.net/IsValidEmail", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> IsValidEmailAsync(string Email);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ValidateEmailSoapChannel : bmbox_main.EmailValidationWS.ValidateEmailSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ValidateEmailSoapClient : System.ServiceModel.ClientBase<bmbox_main.EmailValidationWS.ValidateEmailSoap>, bmbox_main.EmailValidationWS.ValidateEmailSoap {
        
        public ValidateEmailSoapClient() {
        }
        
        public ValidateEmailSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ValidateEmailSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ValidateEmailSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ValidateEmailSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool IsValidEmail(string Email) {
            return base.Channel.IsValidEmail(Email);
        }
        
        public System.Threading.Tasks.Task<bool> IsValidEmailAsync(string Email) {
            return base.Channel.IsValidEmailAsync(Email);
        }
    }
}
