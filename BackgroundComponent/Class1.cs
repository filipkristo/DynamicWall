using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using Windows.System.UserProfile;
using Windows.Storage;
using System;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;


namespace BackgroundComponent
{
    public class Class1 : IBackgroundTask
    {
        
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            SendToast("Background task active");
        }

        public static void SendToast(string message)
        {
            var template = ToastTemplateType.ToastText01;
            var xml = ToastNotificationManager.GetTemplateContent(template);
            var elements = xml.GetElementsByTagName("Test");
            var text = xml.CreateTextNode(message);

            elements[0].AppendChild(text);
            var toast = new ToastNotification(xml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }


        // Pass in a relative path to a file inside the local appdata folder 
        async Task<bool> SetWallpaperAsync()
        {
            bool success = false;
            if (UserProfilePersonalizationSettings.IsSupported())

            {
                var imageId = DateTime.Now.Hour;
                var uri = new Uri($"ms-appx:///Dynamic/Dynamic-{imageId}.jpg");
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
                success = await UserProfilePersonalizationSettings.Current.TrySetWallpaperImageAsync(file);
                success = await UserProfilePersonalizationSettings.Current.TrySetLockScreenImageAsync(file);
            }
            return success;
        }
    }
}
