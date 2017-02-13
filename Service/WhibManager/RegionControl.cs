namespace WhibManager
{
  using System;
  using System.IO;
  using System.Net;
  using System.Windows.Forms;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;
  using WhibModel;

  public partial class RegionControl : UserControl
  {
    public RegionControl()
    {
      InitializeComponent();
    }

    public void  LoadRegions()
    {
      try
      {
        WebRequest request = WebRequest.Create("http://localhost:59998/api/Region");
        request.Method = "GET";
        request.ContentType = "application/json";

        WebResponse response = request.GetResponse();

        string result = string.Empty;
        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
        {
          result = sr.ReadToEnd();
          sr.Close();
        }

        JArray jsonList = (JArray)JsonConvert.DeserializeObject(result);
        foreach (var item in jsonList)
        {
          Region region = item.ToObject<Region>();
          treeView1.Nodes.Add(region.EnglishName);
        }
      }
      catch { }
    }
  }
}
