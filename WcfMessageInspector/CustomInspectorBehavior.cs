using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WcfMessageInspector
{
   [AttributeUsage(AttributeTargets.Class)]
   public class CustomInspectorBehavior : Attribute, IEndpointBehavior, IServiceBehavior
   {
      void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
      {
      }

      void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
      {
         clientRuntime.MessageInspectors.Add(new CustomMessageInspector());
      }

      void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
      {
         var channelDispatcher = endpointDispatcher.ChannelDispatcher;
         if (channelDispatcher == null)
            return;
         channelDispatcher.Endpoints
            .Select(e => e.DispatchRuntime)
            .Select(r => r.MessageInspectors)
            .ToList()
            .ForEach(m => m.Add(new CustomMessageInspector()));
      }

      void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
      {
      }

      void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
      {
      }

      void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
      {
         serviceHostBase
            .ChannelDispatchers
            .OfType<ChannelDispatcher>()
            .SelectMany(c => c.Endpoints)
            .Select(e => e.DispatchRuntime)
            .Select(r => r.MessageInspectors)
            .ToList()
            .ForEach(m => m.Add(new CustomMessageInspector()));
      }

      void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
      {
      }
   }
}