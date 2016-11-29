using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WcfMessageInspector
{
   public class CustomMessageInspector : IDispatchMessageInspector, IClientMessageInspector
   {
      private const string HeaderName = "token-header";
      private const string HeaderNamespace = "ns";
      private const string MessageProperty = "TokenHeader";

      object IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
      {
         var header = MessageHeader.CreateHeader(HeaderName, HeaderNamespace, CustomDataContext.Header);
         request.Headers.Add(header);
         return null;
      }

      object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
      {
         var header = request.Headers.GetHeader<CustomDataHeader>(HeaderName, HeaderNamespace);
         if (header == null)
            return null;

         OperationContext.Current.IncomingMessageProperties.Add(MessageProperty, header);
         CustomDataContext.Header = header;
         return null;
      }

      void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
      {
      }

      void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
      {
      }
   }
}