using ObserverStandard;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCoreTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowVM vm = new MainWindowVM() { Greeting = "Go Birds!!!" };
            this.DataContext = vm;
            MyObs obs = new MyObs();
            vm.Notifier = new Subject();
            vm.Notifier.Attach(obs);
            
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            
        }
    }

    public class MyObs : Observer
    {
        public override void Update(string mode, string msg)
        {
            MessageBox.Show(msg, mode);
        }
    }
}
