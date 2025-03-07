using System;
using Avalonia.Controls;
using BloodSaved.ViewModels;

namespace BloodSaved.Views;

public partial class MainView : Window
{
  private const double MinScale = .25d;
  private const double MaxScale = 10d;
  private const double ScalingDeltaStep = .25d;

  //only for designer purposes
#if DEBUG
  public MainView()
  {
    InitializeComponent();
  }
#endif

  public MainView(MainViewModel dataContext)
  {
    DataContext = dataContext;
    InitializeComponent();
  }

  private void MapPointerWheelChanged(object? sender, Avalonia.Input.PointerWheelEventArgs e)
  {
    if (e.KeyModifiers == Avalonia.Input.KeyModifiers.Control)
    {
      MainViewModel mainViewModel = (MainViewModel)DataContext;

      double scale = mainViewModel.MapScale;
      double delta = e.Delta.Y * ScalingDeltaStep;

      scale += delta;

      scale = Math.Max(scale, MinScale);
      scale = Math.Min(scale, MaxScale);

      if (mainViewModel.MapScale != scale)
      {
        mainViewModel.MapScale = scale;
      }

      e.Handled = true;
    }
  }
}