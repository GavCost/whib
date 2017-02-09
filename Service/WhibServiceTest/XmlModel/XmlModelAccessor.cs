namespace WhibServiceTest
{
  using System;
  using System.IO;
  using System.Xml.Serialization;

  internal static class XmlModelAccessor
  {
    internal static Region LoadRegions()
    {
      Region world = null;

      try
      {
        Type[] subTypes = new Type[] { typeof(Statistic) };
        XmlSerializer serializer = new XmlSerializer(typeof(Region), subTypes);
        using (TextReader reader = new StreamReader(@"Data\Regions.xml"))
        {
          world = (Region)serializer.Deserialize(reader);
        }
      }
      catch
      { }

      return world;
    }
  }
}
