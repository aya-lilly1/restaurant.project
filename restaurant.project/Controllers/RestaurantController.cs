using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant.project.Extensions;
using restaurant.project.Models;
using restaurant.project.ModelView;
using System.Collections.Generic;
using System.Linq;

namespace restaurant.project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {

        private restaurantdbContext _dbContext;
        private IMapper _mapper;
        public RestaurantController(restaurantdbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetRest()
        {
            _dbContext.IgnoreFilterRestaurnt = true;
            var result = _dbContext.Restaurants.ToList();
            var resultView = _mapper.Map<IEnumerable<RestaurantModelView>>(result);
            return Ok(resultView);
        }


        [HttpGet("{id}")]
        public IActionResult GetRestID(int id)
        {
            var resultId = _dbContext.Restaurants.Find(id);
            var resultView = _mapper.Map<RestaurantModelView>(resultId);
            return Ok(resultView);
        }


        [HttpPost]
        public IActionResult creatRes([FromBody] RestaurantModelView restView)
        {
            var rest = _dbContext.Restaurants.FirstOrDefault(x => x.Id == restView.Id);
            if (rest != null)
            {
                return BadRequest("restaurant alredy exist");
            }
            rest = _dbContext.Restaurants.Add(new Restaurant
            {
                Id = restView.Id,
                Phone = restView.Phone,
                RestaurantName = restView.RestaurantName.ToCapCase(),
                CreatedDate = restView.CreatedDate,
                UpdatedDate = restView.UpdatedDate,
                Archived = restView.Archived
            }).Entity;
            _dbContext.SaveChanges();
            return Ok(rest);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateRestarnt(RestaurantModelView restView)
        {
            var rest = _dbContext.Restaurants.FirstOrDefault(x => x.Id == restView.Id);

            if (rest != null)
            {
                return BadRequest("Id not found");
            }
            var result = _mapper.Map<RestaurantModelView>(rest);

            rest.Id = restView.Id;
            rest.Phone = restView.Phone;
            rest.RestaurantName = restView.RestaurantName.ToCapCase();
            rest.CreatedDate = restView.CreatedDate;
            rest.UpdatedDate = restView.UpdatedDate;
            rest.Archived = restView.Archived;

            _dbContext.Entry(rest).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(rest);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(RestaurantModelView restView)
        {
            var rest = _dbContext.Restaurants.FirstOrDefault(x => x.Id == restView.Id);

            if (rest != null)
            {
                return BadRequest("Id not found");
            }
            var result = _mapper.Map<RestaurantModelView>(rest);
            _dbContext.Restaurants.Remove(rest);
            _dbContext.SaveChanges();

            return Ok("Done!!");
        }
    }
}
