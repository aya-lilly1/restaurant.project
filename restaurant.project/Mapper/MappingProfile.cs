using AutoMapper;
using restaurant.project.Models;
using restaurant.project.ModelView;

namespace restaurant.project.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, RestaurantModelView>().ReverseMap();
            CreateMap<Order, OrderMV>().ReverseMap();
            CreateMap<Restaurantmenu, RestautantMenuMV>().ReverseMap();
            CreateMap<Customer, CustomerMV>().ReverseMap();
            CreateMap<CsvView, CsvViewMV>().ReverseMap();
        }
    }
}
