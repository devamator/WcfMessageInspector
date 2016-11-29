using System.ServiceModel;

namespace WcfMessageInspector
{
   [ServiceContract]
   public interface ISampleService
   {
      [OperationContract]
      string GetData(string value);
   }

   public class SampleService : ISampleService
   {
      public string GetData(string value)
      {
         return value;
      }
   }
}
