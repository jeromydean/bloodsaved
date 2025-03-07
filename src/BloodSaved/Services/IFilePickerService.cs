using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace BloodSaved.Services
{
  public interface IFilePickerService
  {
    Task<IReadOnlyList<IStorageFile>> Show(string title,
      bool allowMultiple = false,
      string? suggestedStartLocation = null,
      IEnumerable<FilePickerFileType>? filters = null);
  }
}
