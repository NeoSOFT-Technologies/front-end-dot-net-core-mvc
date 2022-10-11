using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Boilerplate.Application.Helper.ApiHelper
{
    public interface IApiClient<T>
    {
        Task<IEnumerable<T>> GetAllAsync(string apiUrl);
        Task<T> GetByIdAsync(string apiUrl);
        Task<T> PostAsync(string apiUrl, T entity);
        Task<T> PutAsync(string apiUrl, T entity);
        Task<string> DeleteAsync(string apiUrl);
    }
}
