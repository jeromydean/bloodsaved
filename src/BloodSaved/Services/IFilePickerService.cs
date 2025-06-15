using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace BloodSaved.Services
{
  public interface IFilePickerService
  {
    Task<IReadOnlyList<IStorageFile>> OpenFilePickerAsync(string title,
      bool allowMultiple = false,
      string? suggestedStartLocation = null,
      IEnumerable<FilePickerFileType>? filters = null);

    Task<IStorageFile?> SaveFilePickerAsync(string title,
      bool showOverwritePrompt = true,
      string? suggestedStartLocation = null,
      IEnumerable<FilePickerFileType>? filters = null);
  }
}
