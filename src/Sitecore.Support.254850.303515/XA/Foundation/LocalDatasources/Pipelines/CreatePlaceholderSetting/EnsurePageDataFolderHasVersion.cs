#region Added code for 303515
namespace Sitecore.XA.Foundation.LocalDatasources.Pipelines.CreatePlaceholderSetting
{
  using Sitecore.XA.Foundation.PlaceholderSettings.Pipelines.CreatePlaceholderSetting;
  using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;

  public class EnsurePageDataFolderHasVersion
  {
    public void Process(CreatePlaceholderSettingGetArgs args)
    {
      if (args.SettingsRoot.InheritsFrom(Templates.PageData.ID) && args.SettingsRoot.Versions.Count == 0)
      {
        args.SettingsRoot.Versions.AddVersion();
      }
    }
  }
} 
#endregion