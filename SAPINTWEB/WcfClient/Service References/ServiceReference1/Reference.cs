﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfClient.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IMessageHello")]
    public interface IMessageHello {
        
        // CODEGEN: 消息 HelloGreetingMessage 的包装名称(HelloGreetingMessage)以后生成的消息协定与默认值(Hello)不匹配
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageHello/Hello", ReplyAction="http://tempuri.org/IMessageHello/HelloResponse")]
        WcfClient.ServiceReference1.HelloResponseMessage Hello(WcfClient.ServiceReference1.HelloGreetingMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessageHello/Hello", ReplyAction="http://tempuri.org/IMessageHello/HelloResponse")]
        System.Threading.Tasks.Task<WcfClient.ServiceReference1.HelloResponseMessage> HelloAsync(WcfClient.ServiceReference1.HelloGreetingMessage request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="HelloGreetingMessage", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class HelloGreetingMessage {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://localhost", Order=0)]
        public string Salutations;
        
        public HelloGreetingMessage() {
        }
        
        public HelloGreetingMessage(string Salutations) {
            this.Salutations = Salutations;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="HelloResponseMessage", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class HelloResponseMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://localhost")]
        public string OutOfBandData;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://localhostl", Order=0)]
        public string ResponseToGreeting;
        
        public HelloResponseMessage() {
        }
        
        public HelloResponseMessage(string OutOfBandData, string ResponseToGreeting) {
            this.OutOfBandData = OutOfBandData;
            this.ResponseToGreeting = ResponseToGreeting;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessageHelloChannel : WcfClient.ServiceReference1.IMessageHello, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MessageHelloClient : System.ServiceModel.ClientBase<WcfClient.ServiceReference1.IMessageHello>, WcfClient.ServiceReference1.IMessageHello {
        
        public MessageHelloClient() {
        }
        
        public MessageHelloClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MessageHelloClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessageHelloClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessageHelloClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WcfClient.ServiceReference1.HelloResponseMessage WcfClient.ServiceReference1.IMessageHello.Hello(WcfClient.ServiceReference1.HelloGreetingMessage request) {
            return base.Channel.Hello(request);
        }
        
        public string Hello(string Salutations, out string ResponseToGreeting) {
            WcfClient.ServiceReference1.HelloGreetingMessage inValue = new WcfClient.ServiceReference1.HelloGreetingMessage();
            inValue.Salutations = Salutations;
            WcfClient.ServiceReference1.HelloResponseMessage retVal = ((WcfClient.ServiceReference1.IMessageHello)(this)).Hello(inValue);
            ResponseToGreeting = retVal.ResponseToGreeting;
            return retVal.OutOfBandData;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WcfClient.ServiceReference1.HelloResponseMessage> WcfClient.ServiceReference1.IMessageHello.HelloAsync(WcfClient.ServiceReference1.HelloGreetingMessage request) {
            return base.Channel.HelloAsync(request);
        }
        
        public System.Threading.Tasks.Task<WcfClient.ServiceReference1.HelloResponseMessage> HelloAsync(string Salutations) {
            WcfClient.ServiceReference1.HelloGreetingMessage inValue = new WcfClient.ServiceReference1.HelloGreetingMessage();
            inValue.Salutations = Salutations;
            return ((WcfClient.ServiceReference1.IMessageHello)(this)).HelloAsync(inValue);
        }
    }
}