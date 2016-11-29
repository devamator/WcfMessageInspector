using System.Runtime.Serialization;

namespace WcfMessageInspector
{
   [DataContract]
   public class CustomDataHeader
   {
      [DataMember]
      public string Value { get; set; }

      public CustomDataHeader Clone()
      {
         return new CustomDataHeader
         {
            Value = Value
         };
      }
   }
}