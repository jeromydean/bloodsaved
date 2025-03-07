using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.DependencyInjection;

namespace BloodSaved.Services
{
  public class WindowService : IWindowService
  {
    private readonly IServiceProvider _serviceProvider;

    public WindowService(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public void ShowDialog<T>() where T : Window
    {
      if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
        && desktop.MainWindow is not null)
      {
        var view = _serviceProvider.GetRequiredService<T>();
        view.ShowDialog(desktop.MainWindow);
      }
    }

    public void ShowWindow<T>() where T : Window
    {
      var view = _serviceProvider.GetRequiredService<T>();
      view.Show();
    }
  }
}