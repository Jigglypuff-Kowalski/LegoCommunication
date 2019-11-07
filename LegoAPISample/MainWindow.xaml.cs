using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lego.Ev3.Core;
using Lego.Ev3.Desktop; 

namespace LegoAPISample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Brick _brick; 
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _brick = new Brick(new UsbCommunication());
            _brick.BrickChanged += _brick_BrickChanged;
            
            await _brick.ConnectAsync();

            await _brick.DirectCommand.PlayToneAsync(100, 1000, 300);
            _brick.Ports[InputPort.Two].SetMode(TouchMode.Touch);
            Debug.WriteLine(_brick.Ports[InputPort.Two].SIValue);
        }

        private void _brick_BrickChanged(object sender, BrickChangedEventArgs e)
        {
            Debug.WriteLine("Brick Changed!"); 
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(_brick.Ports[InputPort.Two].SIValue);
            await _brick.SystemCommand.CopyFileAsync("Hello.txt", "../prjs/rc-data/Hello.txt");
            await _brick.DirectCommand.TurnMotorAtPowerForTimeAsync(OutputPort.D, 50, 3000, false); 
        }
    }
}
