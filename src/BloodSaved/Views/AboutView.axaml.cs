using Avalonia.Controls;
using BloodSaved.ViewModels;

namespace BloodSaved;

public partial class AboutView : Window
{
  //only for designer purposes
#if DEBUG
  public AboutView()
  {
    InitializeComponent();
  }
#endif

  public AboutView(AboutViewModel dataContext)
  {
    DataContext = dataContext;
    InitializeComponent();
  }
}