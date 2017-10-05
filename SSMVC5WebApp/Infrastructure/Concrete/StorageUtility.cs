using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;

namespace SSMVC5WebApp.Infrastructure.Concrete
{
    public class StorageUtility
    {
        public static CloudStorageAccount StorageAccount
        {
            get
            {
                string account = CloudConfigurationManager.GetSetting("StorageAccountName");
                if (account == "{StorageAccountName}")
                {
                    return CloudStorageAccount.DevelopmentStorageAccount;
                }
                string key = CloudConfigurationManager.GetSetting("StorageAccountAccessKey");
                string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", account, key);
                return CloudStorageAccount.Parse(connectionString);
            }
        }

    }
}