using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VideoControl
{
    /// <summary>
    /// Interaktionslogik für Logo.xaml
    /// </summary>
    public partial class Logo : Window
    {
        public Logo(string stImage = "", int iScreen = 0)
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(
                new Uri(
                   stImage));
            myBrush.ImageSource = image.Source;
            this.Background = myBrush;
            System.Windows.Forms.Screen Screen = GetScreenPosition(iScreen);
            if (Screen != null)
            {
                System.Drawing.Rectangle wa = Screen.WorkingArea;
                this.WindowState = WindowState.Normal;
                this.Top = wa.Y;
                this.Left = wa.X;
                this.Width = wa.Width;
                this.Height = wa.Height;
            }
        }

        private System.Windows.Forms.Screen GetScreenPosition(int iScreen)
        {
            string stDeviceName = "";
            var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            if (allScreens.Count >= iScreen)
            {
                return allScreens[iScreen];
            }
            else
            {
                return null;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            this.WindowState = WindowState.Maximized;
        }
    }
}
