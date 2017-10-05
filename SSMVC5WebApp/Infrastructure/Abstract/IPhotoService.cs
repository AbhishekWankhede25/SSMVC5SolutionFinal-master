using System.Threading.Tasks;

using System.Web;

namespace SSMVC5WebApp.Infrastructure.Abstract
{
    public interface IPhotoService
    {
        Task<string> UploadPhotoAsync(string category, HttpPostedFileBase photoToUpload);

        Task<bool> DeletePhotoAsync(string category, string photoUrl);
    }
}
