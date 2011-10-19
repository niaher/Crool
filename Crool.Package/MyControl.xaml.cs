namespace Microsoft.Crool_Package
{
    using System.Windows.Controls;
    using Crool.Business.Managers;
    using Crool.DataAccess.Repositories;
    using Crool.Domain.Entities;

    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class MyControl : UserControl
    {
        public MyControl()
        {
            InitializeComponent();

            // For some really strage reason only if we use something from an assembly, will that assembly be loaded.
            // If we don't have the following 2 lines, at some point we might get "type <something> not found in assembly...".
            // TODO: Find out why this happens and fix this behavior.
            new ReviewRepository();
            new Review();
        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var fileReviews = new ReviewManager().GetByFileId(1);
        }
    }
}