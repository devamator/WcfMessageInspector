using System;

namespace WcfMessageInspector
{

   public static class CustomDataContext
   {
      [ThreadStatic]
      private static CustomDataHeader _header;

      static CustomDataContext()
      {
         _header = new CustomDataHeader();
      }

      public static CustomDataHeader Header
      {
         get { return _header.Clone(); }
         set { _header = value.Clone(); }
      }
   }
}