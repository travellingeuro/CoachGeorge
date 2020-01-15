using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace SGXC.ViewModels
{
    public class SetLogoPageViewModel : BindableBase, INavigatedAware
    {
        public INavigationService NavigationService { get; set; }

        public DelegateCommand LoadImageCommand { get; set; }

        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand<object> ImageSavedCommand { get; set; }

        private ImageSource teamlogo;
        public ImageSource TeamLogo
        {
            get { return teamlogo; }
            set { SetProperty(ref teamlogo, value); }
        }

        private string teamname;
        public string TeamName
        {
            get { return teamname; }
            set { SetProperty(ref teamname, value); }
        }


        public SetLogoPageViewModel(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
            LoadImageCommand = new DelegateCommand(LoadImageMEthod);
            SaveCommand = new DelegateCommand(SaveMethod);
            ImageSavedCommand = new DelegateCommand<object>(ImageSavedMethod);

        }

        private void ImageSavedMethod(object obj)
        {
            var loc = obj as string;
            AppSettings.TeamLogo = loc;
        }

        private void SaveMethod()
        {
            AppSettings.TeamName = TeamName;
            NavigationService.NavigateAsync("/MainPage");

        }

        private async void LoadImageMEthod()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions

            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,

            });

            if (file!=null)

            {
                AppSettings.TeamLogo = file.Path;
                TeamLogo = ImageSource.FromFile(file.Path);
            }
            return;

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            AppSettings.TeamName = TeamName;
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            TeamName = AppSettings.TeamName;
            TeamLogo = ImageSource.FromFile(AppSettings.TeamLogo);
        }
    }
}
