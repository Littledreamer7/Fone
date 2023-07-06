using AutoMapper;
using FoneApi.Data;
using FoneApi.Service.Interface;
using FoneApi.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoneApi.Controllers
{
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class Fone_Controller : ControllerBase
    {
        private readonly IFoneService _IFoneService;
        private readonly IMapper _mapper;
        private readonly FoneDb _foneDb;
        //private readonly ApplicationDbContext _appDbContext;
        public Fone_Controller(IFoneService foneService,
            IMapper mapper,
            FoneDb foneDb)
            //ApplicationDbContext appDbContext)
        {
            _IFoneService = foneService;
            _mapper = mapper;
            _foneDb = foneDb;
            //_appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetFoneLists()
        {
            var result = await _IFoneService.GetFoneListAsync();
            return Ok(result);
        }

        [HttpGet("GetFoneListDetailById/{Id}")]
        public async Task<IActionResult> GetFoneById(int Id)
        {
            var result = await _IFoneService.GetFoneDetailAsync(Id);
            return Ok(result);
        }

        [HttpPost("CreateFone")]
        public async Task<IActionResult> CreateFone([FromBody] Create_FoneVM AddfoneVM)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Invalid Model");
                var result = await _IFoneService.CreateFoneAsync(AddfoneVM);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Something wrong");
            }
        }

        [HttpPost("UpdateFone")]
        public async Task<IActionResult> UpdateFone([FromBody] Update_FoneVM editfoneVM)
        {
            try
            {
                var result = await _IFoneService.UpdateFoneAsync(editfoneVM);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("DeleteFoneById/{Id}")]
        public async Task<IActionResult> DeleteFone(int Id)
        {
            var result = await _IFoneService.DeleteFoneAsync(Id);
            return Ok(result);
        }


    }
}
