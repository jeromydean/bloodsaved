using System;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BloodSaved.Services;
using BloodSaved.ViewModels;
using BloodSaved.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BloodSaved
{
  public partial class App : Application
  {
    private IServiceProvider _serviceProvider;

    public App()
    {
      ServiceCollection serviceCollection = new ServiceCollection();
      ConfigureServices(serviceCollection);
      _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public override void Initialize()
    {
      AvaloniaXamlLoader.Load(this);
    }

    private void ConfigureServices(IServiceCollection services)
    {
      services.AddTransient<IFilePickerService, FilePickerService>();
      services.AddTransient<IWindowService, WindowService>();

      //viewmodels
      Type viewModelBaseType = typeof(ViewModelBase);
      Assembly executingAssembly = Assembly.GetExecutingAssembly();
      foreach (Type viewModelType in executingAssembly.GetTypes().Where(t => t != viewModelBaseType
        && viewModelBaseType.IsAssignableFrom(t)))
      {
        services.AddTransient(viewModelType);
      }

      //views
      Type viewBaseType = typeof(Window);
      foreach (Type viewType in executingAssembly.GetTypes().Where(t => t != viewBaseType
        && viewBaseType.IsAssignableFrom(t)))
      {
        services.AddTransient(viewType);
      }
    }

    public override void OnFrameworkInitializationCompleted()
    {
      if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
      {
        desktop.MainWindow = _serviceProvider.GetRequiredService<MainView>();
      }

      base.OnFrameworkInitializationCompleted();
    }
  }
}