using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading;
using BackgroundComponent;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BackgroundTaskDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <summary> 
    /// An empty page that can be used on its own or navigated to within a Frame. 
    /// </summary>

    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }
         

        public static async Task<BackgroundTaskRegistration> RegisterBackgroundTask(IBackgroundTrigger trigger)
        {
            var task = new BackgroundTaskBuilder
            {
                Name = "DynamicWall Process",
                TaskEntryPoint = typeof(BackgroundComponent.Class1).ToString()
            };
            task.SetTrigger(trigger);

            var Trigger = new ApplicationTrigger();
            TimeTrigger hourlyTrigger = new TimeTrigger(60, false);
            task.SetTrigger(hourlyTrigger);


            var requestStatus = await Windows.ApplicationModel.Background.BackgroundExecutionManager.RequestAccessAsync();
            if (requestStatus != BackgroundAccessStatus.AlwaysAllowed)
            {
                // Depending on the value of requestStatus, provide an appropriate response
                // such as notifying the user which functionality won't work as expected
            }

            BackgroundTaskRegistration backtask = task.Register();
            return backtask;
        }
        public static async void MainAsync()
        {
            var t = await Class1.SetWallpaperAsync()
                { }
                
        }



    }
}
