namespace AppBackend.Services
{
    public class AppConfiguration : IAppConfiguration
    {
        public AppConfiguration() {

        }

        public string GetStaticFileServerEndpoint(){
            return "localhost:9000";
        }

        public string GetStaticFileServerAccessKey() {
            return "AKIAIOSFODNN7NOOOOOO";
        }
        public string GetStaticFileServerSecret() {
            return "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";
        }

        public string GetStaticFileBucketName() {
            return "cigna";
        }

        public string GetStaticFileServerLocation() {
            return "ap-southeast-1";
        }
    }
}