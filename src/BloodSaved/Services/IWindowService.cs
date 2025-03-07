using Avalonia.Controls;

namespace BloodSaved.Services
{
  public interface IWindowService
  {
    void ShowWindow<T>() where T : Window;
    void ShowDialog<T>() where T : Window;
  }
}