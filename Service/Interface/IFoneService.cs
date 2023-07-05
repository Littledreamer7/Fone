using FoneApi.ViewModel;

namespace FoneApi.Service.Interface
{
    public interface IFoneService
    {
        Task<List<FoneVM>> GetFoneListAsync();
        Task<FoneVM> GetFoneDetailAsync(int Id);
        Task<bool> CreateFoneAsync(Create_FoneVM AddfoneVM);
        Task<bool> UpdateFoneAsync(Update_FoneVM editfoneVM);
        Task<bool> DeleteFoneAsync(int Id);
    }
}
