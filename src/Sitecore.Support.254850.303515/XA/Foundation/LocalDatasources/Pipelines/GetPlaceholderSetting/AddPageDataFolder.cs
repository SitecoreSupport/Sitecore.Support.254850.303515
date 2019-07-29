namespace Sitecore.Support.XA.Foundation.LocalDatasources.Pipelines.GetPlaceholderSetting
{
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.XA.Foundation.LocalDatasources;
  using Sitecore.XA.Foundation.PlaceholderSettings.Pipelines.GetPlaceholderSetting;
  using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
  using System.Linq;

  public class AddPageDataFolder
  {
    public void Process(GetPlaceholderSettingArgs args)
    {
      Assert.IsNotNull(args, "args");

      if (args.ContextItem != null)
      {

        if (args.SettingsRoots.Any(r => args.ContextItem.Axes.IsDescendantOf(r)))
        {
          return;
        }

        if (args.SettingsRoots.Any(IsPageDataFolder))
        {
          return;
        }

        Item pageDataFolder = args.ContextItem.GetChildren().FirstOrDefault(IsPageDataFolder);
        #region Changed code for 303515
        if (pageDataFolder != null) 
        #endregion
        {
          args.SettingsRoots.Insert(args.SettingsRoots.Count, pageDataFolder);
        }
        else
        {
          TemplateItem pageDataFolderTemplate = args.ContextItem.Database.GetItem(Items.VirtualPageData, args.ContextItem.Language);
          if (pageDataFolderTemplate?.StandardValues != null)
          {
            args.SettingsRoots.Insert(args.SettingsRoots.Count, pageDataFolderTemplate.StandardValues);
          }
        }
      }
    }

    private bool IsPageDataFolder(Item item)
    {
      return item.InheritsFrom(Sitecore.XA.Foundation.Editing.Templates.PageDataFolder);
    }
  }
}