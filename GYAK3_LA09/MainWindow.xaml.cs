using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GYAK3_LA09;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    string BackgroundName = "background.jpg";
    string[] ImageNames = {"grabowski.jpg", "kukori.jpg",
                            "kuldonc.jpg", "vili.jpg"};

    BitmapImage biBackground;
    BitmapImage[] biImages = new BitmapImage[8];

    Image[] imImages;

    Random rnd = new Random();
    private DispatcherTimer dt;

    public MainWindow()
    {
        InitializeComponent();

        imImages = new Image[] { 
            im10, im11, im12, im13,
            im20, im21, im22, im23
        };

        dt = new DispatcherTimer { 
            Interval = new TimeSpan(0,0,0,0,3000),
            IsEnabled = false
        };

        dt.Tick += Dt_Tick;

        LoadImage();
        ShowImage();
        dt.Start();
    }

    private void LoadImage()
    {
        try {
            biBackground = new BitmapImage(new Uri(@"Images/"+
                BackgroundName, UriKind.Relative));
            for (int i = 0; i < 4; i++)
            {
                biImages[i] = new BitmapImage(new Uri(@"Images/" +
                    ImageNames[i], UriKind.Relative));
                biImages[i + 4] = biImages[i];
            }
        }
        catch (Exception) { }
    }

    private void ShowImage()
    {
        for (int i = 0; i < 8; i++)
        {
            imImages[i].Source = biImages[i];
        }
    }

    private void Dt_Tick(object? sender, EventArgs e)
    {
        ShowBackground();
        dt.Stop();
    }

    private void ShowBackground()
    {
        for (int i = 0; i < 8; i++)
        {
            imImages[i].Source = biBackground;
        }
    }

    private void miExit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void miMix_Click(object sender, RoutedEventArgs e)
    {
        Randomize();
        ShowImage();
        dt.Start();
    }

    private void Randomize()
    {
        List<BitmapImage> ImageList = new List<BitmapImage>();
        ImageList.AddRange(biImages);
        for (int i = 0; i < 8; i++)
        {
            int no = rnd.Next(0, ImageList.Count);
            biImages[i] = ImageList[no];
            ImageList.RemoveAt(no);

        }
    }

    private void miGuess_Click(object sender, RoutedEventArgs e)
    {

    }
}