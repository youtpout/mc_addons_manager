using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace MC_Addons_Manager
{
    public sealed partial class ResourceBox : UserControl
    {
        public ResourceBox()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }


        public ResourceData ResourceData
        {
            get { return (ResourceData)GetValue(ResourceDataProperty); }
            set { SetValue(ResourceDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResourceData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResourceDataProperty =
            DependencyProperty.Register("ResourceData", typeof(ResourceData), typeof(ResourceBox), new PropertyMetadata(new ResourceData()));


        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeleteCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(ResourceBox), new PropertyMetadata(null));




        private void Button_Click(object send, RoutedEventArgs e)
        {
            if (DeleteCommand != null)
            {
                DeleteCommand.Execute(ResourceData);
            }
        }

        private void Btn_Export_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
