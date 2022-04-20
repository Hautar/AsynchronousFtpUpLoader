namespace FtpGazApp
{
    public class Settings
    {
        public string UserName;
        public string Password;
        
        public string HostUrl;
        public string RemotePath;

        public string LocalPath;
        public string LocalPathUploaded;

        public Settings()
        {
            UserName = default;
            Password = default;

            HostUrl = default;
            RemotePath = default;

            LocalPath = default;
            LocalPathUploaded = default;
        }
    }
}