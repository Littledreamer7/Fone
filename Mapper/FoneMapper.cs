using AutoMapper;
using FoneApi.Model;
using FoneApi.ViewModel;

namespace FoneApi.Mapper
{
    public class FoneMapper : Profile
    {
        public FoneMapper()
        {
            CreateMap<Fone_Model, FoneVM>().ReverseMap();

            CreateMap<Fone_Model, Update_FoneVM>().ReverseMap();

            CreateMap<Fone_Model, Create_FoneVM>().ReverseMap();
        }
    }
}
