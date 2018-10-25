using System;

namespace AppBackend.Services {
    public interface IAppConfiguration
    {
        string GetStaticFileServerEndpoint();
        string GetStaticFileServerAccessKey();
        string GetStaticFileServerSecret();
        string GetStaticFileBucketName();
        string GetStaticFileServerLocation();
    }
}