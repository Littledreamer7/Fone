using AutoMapper;
using FoneApi.Data;
using FoneApi.Model;
using FoneApi.Service.Interface;
using FoneApi.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace FoneApi.Service
{
    public class FoneService : IFoneService
    {
        private readonly IMapper _mapper;
        private readonly FoneDb _foneDb;
        //private readonly ApplicationDbContext _appDbContext;
        public FoneService(
            IMapper mapper,
            FoneDb foneDb)
           // ApplicationDbContext appDbContext)
        {
            _mapper = mapper;
            _foneDb = foneDb;
            //_appDbContext = appDbContext;
        }

        public async Task<FoneVM> GetFoneDetailAsync(int Id)
        {
            var result = await _foneDb.FoneApi_Tbl.FindAsync(Id);
            if (result == null) new Exception("No Data Bro!!!");
            return (_mapper.Map<FoneVM>(result));
        }

        public async Task<List<FoneVM>> GetFoneListAsync()
        {
            try
            {
                var See = await _foneDb.FoneApi_Tbl.ToListAsync();
                return (_mapper.Map<List<FoneVM>>(See));
            }
            catch
            {
                throw new NullReferenceException("!!!Oops GetFoneListAsync Failed ");
            }

        }

        public async Task<bool> CreateFoneAsync(Create_FoneVM AddfoneVM)
        {
            var Addnew = _mapper.Map<Fone_Model>(AddfoneVM);
            _foneDb.FoneApi_Tbl.AddAsync(Addnew);
            await _foneDb.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateFoneAsync(Update_FoneVM editfoneVM)
        {
            try
            {
                var result = await _foneDb.FoneApi_Tbl.FindAsync(editfoneVM.Id);
                _foneDb.Entry<Fone_Model>(result).State = EntityState.Detached;
                result = _mapper.Map<Fone_Model>(editfoneVM);
                _foneDb.FoneApi_Tbl.Update(result);
                await _foneDb.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new NullReferenceException("!!!Oops UpdateFone Failed ");
            }
        }

        public async Task<bool> DeleteFoneAsync(int Id)
        {
            var result = await _foneDb.FoneApi_Tbl.FindAsync(Id);
            if (result == null) return false;
            try
            {
                _foneDb.Remove(result);
                await _foneDb.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new NullReferenceException("!!!Oops UpdateFone Failed ");
            }
        }

    }
}
