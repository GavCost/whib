namespace WhibManager
{
  using System;
  using System.IO;
  using System.Net;
  using System.Windows.Forms;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;
  using WhibModel;

  public partial class RegionsControl : UserControl
  {
    public RegionsControl()
    {
      InitializeComponent();
    }

    public void LoadRegions()
    {
      Cursor currentCursor = Cursor.Current;

      try
      {
        Cursor.Current = Cursors.WaitCursor;

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

        trvRegions.Nodes.Clear();
        trvRegions.BeginUpdate();

        JArray jsonList = (JArray)JsonConvert.DeserializeObject(result);
        foreach (var item in jsonList)
        {
          Region region = item.ToObject<Region>();
          AddRegionNode(region);
        }

        trvRegions.Nodes[0].Expand();
        trvRegions.EndUpdate();
      }
      catch { }
      finally
      {
        Cursor.Current = currentCursor;
      }
    }

    public Region LoadRegion(int id)
    {
      Region region = null;
      Cursor currentCursor = Cursor.Current;

      try
      {
        Cursor.Current = Cursors.WaitCursor;

        WebRequest request = WebRequest.Create(string.Format("http://localhost:59998/api/Region/{0}", id));
        request.Method = "GET";
        request.ContentType = "application/json";

        WebResponse response = request.GetResponse();

        string result = string.Empty;
        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
        {
          result = sr.ReadToEnd();
          sr.Close();
        }

        region = JsonConvert.DeserializeObject<Region>(result);
      }
      catch { }
      finally
      {
        Cursor.Current = currentCursor;
      }

      return region;
    }

    private void AddRegionNode(Region region)
    {
      // The first case is simple, the item has no parent so it has to go in the root.
      if (!region.ParentId.HasValue)
      {
        trvRegions.Nodes.Add(region.Id.ToString(), region.EnglishName);
      }
      else
      {
        // The second case is also fairly easy, we have the parent in the tree so we just attach to it.
        TreeNode[] parentNodes = trvRegions.Nodes.Find(region.ParentId.ToString(), true);
        if (parentNodes != null && parentNodes.Length == 1)
        {
          parentNodes[0].Nodes.Add(region.Id.ToString(), region.EnglishName);
        }
        else
        {
          // Here we have a node who's parent does not exist yet.
          trvRegions.Nodes.Add(region.Id.ToString(), region.EnglishName);
        }
      }
    }

    private void trvRegions_AfterSelect(object sender, TreeViewEventArgs e)
    {
      try
      {
        string nodeName = e.Node.Name;
        int id = int.Parse(nodeName);
        Region region = LoadRegion(id);
      }
      catch { }
    }
  }
}
