using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace BloodSaved.Services
{
  public class FilePickerService : IFilePickerService
  {
    public async Task<IReadOnlyList<IStorageFile>> OpenFilePickerAsync(string title,
      bool allowMultiple = false,
      string? suggestedStartLocation = null,
      IEnumerable<FilePickerFileType>? filters = null)
    {
      if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp
        && desktopApp.MainWindow is not null)
      {
        TopLevel topLevel = TopLevel.GetTopLevel(desktopApp.MainWindow);

        IStorageFolder? startLocation = null;
        if (!string.IsNullOrEmpty(suggestedStartLocation)
          && Directory.Exists(suggestedStartLocation))
        {
          startLocation = await topLevel.StorageProvider.TryGetFolderFromPathAsync(suggestedStartLocation);
        }

        //topLevel.StorageProvider.SaveFilePickerAsync

        return await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
          Title = title,
          AllowMultiple = allowMultiple,
          SuggestedStartLocation = startLocation,
          FileTypeFilter = filters?.ToArray()
        });
      }
      return new IStorageFile[0];
    }

    public async Task<IStorageFile?> SaveFilePickerAsync(string title,
      string? defaultExtension = null,
      bool showOverwritePrompt = true,
      string? suggestedStartLocation = null,
      IEnumerable<FilePickerFileType>? filters = null)
    {
      if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp
        && desktopApp.MainWindow is not null)
      {
        TopLevel topLevel = TopLevel.GetTopLevel(desktopApp.MainWindow);

        IStorageFolder? startLocation = null;
        if (!string.IsNullOrEmpty(suggestedStartLocation)
          && Directory.Exists(suggestedStartLocation))
        {
          startLocation = await topLevel.StorageProvider.TryGetFolderFromPathAsync(suggestedStartLocation);
        }

        return await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
          DefaultExtension = defaultExtension,
          Title = title,
          ShowOverwritePrompt = showOverwritePrompt,
          SuggestedStartLocation = startLocation
        });
      }

      return null;
    }
  }
}
