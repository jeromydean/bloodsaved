using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using BloodSaved.ViewModels;

namespace BloodSaved.Views;

public partial class MainView : Window
{
  private const double MinScale = .25d;
  private const double MaxScale = 10d;
  private const double ScalingDeltaStep = .25d;

  private bool _mapPanning;
  private Point _mapPanPointerStart;
  private Vector _mapPanOffsetStart;

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
    Title = $"BloodSaved (v{Assembly.GetExecutingAssembly().GetName().Version})";
  }

  private void MapPointerWheelChanged(object? sender, Avalonia.Input.PointerWheelEventArgs e)
  {
    if (e.KeyModifiers != Avalonia.Input.KeyModifiers.Control)
    {
      return;
    }

    MainViewModel mainViewModel = (MainViewModel)DataContext!;

    double oldScale = mainViewModel.MapScale;
    double newScale = oldScale + e.Delta.Y * ScalingDeltaStep;
    newScale = Math.Clamp(newScale, MinScale, MaxScale);

    if (Math.Abs(newScale - oldScale) < 1e-9)
    {
      e.Handled = true;
      return;
    }

    // Keep the point under the cursor fixed while zooming (layout scale is from top-left).
    Point contentPos = e.GetPosition(mapLayoutRoot);
    Vector oldOffset = mapScrollViewer.Offset;
    double viewportX = contentPos.X - oldOffset.X;
    double viewportY = contentPos.Y - oldOffset.Y;
    double ratio = newScale / oldScale;

    mainViewModel.MapScale = newScale;

    double newOffsetX = contentPos.X * ratio - viewportX;
    double newOffsetY = contentPos.Y * ratio - viewportY;

    Dispatcher.UIThread.Post(
      () => mapScrollViewer.Offset = new Vector(newOffsetX, newOffsetY),
      DispatcherPriority.Loaded);

    e.Handled = true;
  }

  private void MapPointerPressed(object? sender, PointerPressedEventArgs e)
  {
    if (!e.GetCurrentPoint(mapControl).Properties.IsLeftButtonPressed)
    {
      return;
    }

    _mapPanning = true;
    _mapPanPointerStart = e.GetPosition(mapScrollViewer);
    _mapPanOffsetStart = mapScrollViewer.Offset;
    e.Pointer.Capture(mapControl);
    e.Handled = true;
  }

  private void MapPointerMoved(object? sender, PointerEventArgs e)
  {
    if (!_mapPanning)
    {
      return;
    }

    Point current = e.GetPosition(mapScrollViewer);
    Vector drag = current - _mapPanPointerStart;
    mapScrollViewer.Offset = _mapPanOffsetStart - drag;
    e.Handled = true;
  }

  private void MapPointerReleased(object? sender, PointerReleasedEventArgs e)
  {
    if (!_mapPanning || e.InitialPressMouseButton != MouseButton.Left)
    {
      return;
    }

    _mapPanning = false;
    e.Pointer.Capture(null);
    e.Handled = true;
  }

  private void MapPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
  {
    _mapPanning = false;
  }
}