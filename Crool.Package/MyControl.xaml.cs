namespace Microsoft.Crool_Package
{
    using System.Windows;
    using System.Windows.Controls;
    using Crool.Business.Managers;

    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class MyControl : UserControl
    {
        public MyControl()
        {
            InitializeComponent();

            this.dataGrid1.DataContext = new ReviewManager().GetByFileId(1);
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}