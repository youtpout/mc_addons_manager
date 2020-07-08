using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace MC_Addons_Manager
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Maps : Page
    {
        public string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString() + @"\AppData\Local\Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang\minecraftWorlds";

        public ObservableCollection<ResourceData> MapsCollection { get; set; }

        public RelayCommand<ResourceData> DeleteCommand { get; set; }

        public Maps()
        {
            this.InitializeComponent();
            MapsCollection = new ObservableCollection<ResourceData>();
            DeleteCommand = new RelayCommand<ResourceData>(DeleteMap);
            GetMaps();
            this.DataContext = this;
        }

        public async void GetMaps()
        {
            try
            {
                StorageFolder mapsFolder = await StorageFolder.GetFolderFromPathAsync(folderPath);
                IReadOnlyList<StorageFolder> maps = await mapsFolder.GetFoldersAsync();

                foreach (var mapItem in maps)
                {
                    StorageFile MapName = await StorageFile.GetFileFromPathAsync(Path.Combine(folderPath + @"\" + mapItem.DisplayName + @"\levelname.txt"));

                    string name = await FileIO.ReadTextAsync(MapName);
                    MapsCollection.Add(new ResourceData() { Name = name, DisplayName = mapItem.DisplayName, Date = MapName.DateCreated.DateTime.ToString() });
                }
            }
            catch (Exception ex)
            {
                ShowBox("Error", ex.Message + "\n\nPath: " + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Packages\Microsoft.MinecraftUWP_8wekyb3d8bbwe\LocalState\games\com.mojang\minecraftWorlds").ToString());
            }
        }

        public async void DeleteMap(ResourceData item)
        {

            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "Delete this world ?",
                Content = "MESSAGE DE CONFIRMATION ENCORE BOURRE DE BUG MERDE",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    StorageFolder mapsFolder = await StorageFolder.GetFolderFromPathAsync(folderPath);
                    IReadOnlyList<StorageFolder> maps = await mapsFolder.GetFoldersAsync();

                    StorageFolder MapToDelete = await StorageFolder.GetFolderFromPathAsync(folderPath + @"\" + item.DisplayName);
                    await MapToDelete.DeleteAsync();

                    MapsCollection.Remove(item);
                }
                catch (Exception ex)
                {
                    ShowBox("Can't delete this world \n Error: ", ex.Message);
                }
            }
        }


        public static async void ShowBox(string title, string content)
        {
            MessageDialog dialog = new MessageDialog(content, title);
            await dialog.ShowAsync();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
