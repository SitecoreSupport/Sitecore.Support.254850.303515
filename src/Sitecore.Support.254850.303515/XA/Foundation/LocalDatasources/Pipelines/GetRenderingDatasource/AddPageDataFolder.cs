namespace Sitecore.Support.XA.Foundation.LocalDatasources.Pipelines.GetRenderingDatasource
{
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Pipelines.GetRenderingDatasource;
  using Sitecore.XA.Foundation.LocalDatasources;
  using System.Linq;

  public class AddPageDataFolder : Sitecore.XA.Foundation.LocalDatasources.Pipelines.GetRenderingDatasource.AddPageDataFolder
  {
    public new void Process(GetRenderingDatasourceArgs args)
    {
      Assert.IsNotNull(args, "args");

      Item contextItem = args.ContentDatabase.GetItem(args.ContextItemPath, args.ContentLanguage);
      if (contextItem != null)
      {
        if (base.PageRelativeLocationAllowed(contextItem, args.RenderingItem.ID))
        {
          if (args.DatasourceRoots.Any(base.IsPageDataFolder))
          {
            return;
          }

          Item pageDataFolder = contextItem.GetChildren().FirstOrDefault(base.IsPageDataFolder);
          #region Changed code for 303515
          if (pageDataFolder != null) 
          #endregion
          {
            args.DatasourceRoots.Insert(args.DatasourceRoots.Count, pageDataFolder);
          }
          else
          {
            TemplateItem pageDataFolderTemplate = args.ContentDatabase.GetItem(Items.VirtualPageData, args.ContentLanguage);
            if (pageDataFolderTemplate?.StandardValues != null)
            {
              args.DatasourceRoots.Insert(args.DatasourceRoots.Count, pageDataFolderTemplate.StandardValues);
              args.CustomData[Sitecore.XA.Foundation.LocalDatasources.Constants.LocalDataFolderParent] = contextItem.Uri.ToString();
            }
          }
        }
      }
    }
  }
}