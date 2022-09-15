using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant.project.Models;
using restaurant.project.ModelView;
using System.Collections.Generic;
using System.Linq;

namespace restaurant.project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestautantMenuController : ControllerBase
    {
        private restaurantdbContext _dbContext;
        private IMapper _mapper;
        public RestautantMenuController(restaurantdbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetRestMenu()
        {
            _dbContext.IgnoreFilterMenu = true;
            var result = _dbContext.Restaurantmenus.ToList();
            var resultView = _mapper.Map<IEnumerable<RestautantMenuMV>>(result);
            return Ok(resultView);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetRestMenuId(int id)
        {
            var result = _dbContext.Restaurantmenus.ToList();
            var resultView = _mapper.Map<IEnumerable<RestautantMenuMV>>(result);
            return Ok(resultView);
        }

       
        [HttpPost]
        public IActionResult creatMenu([FromBody] RestautantMenuMV menu)
        {
            var restMen = _dbContext.Restaurantmenus.FirstOrDefault(x => x.Id == menu.Id);
            if (restMen != null)
            {
                return BadRequest("the menu alredy exist");
            }
            restMen = _dbContext.Restaurantmenus.Add(new Restaurantmenu
            {
                Id = menu.Id,
                MaelName = menu.MaelName,
                Quatity = menu.Quatity,
                PriceNis = menu.PriceNis,
                PriceUsd = (float)(menu.PriceNis * 3.50),
                ResurantId = menu.ResurantId,
                CreatedDate = menu.CreatedDate,
                UpdatedDate = menu.UpdatedDate,
                Archived = menu.Archived
            }).Entity;
       
            _dbContext.SaveChanges();
            return Ok(restMen);
        }





        [HttpPut("{id}")]
        public IActionResult UpdateRestarntMenu(RestautantMenuMV menu)
        {
            var rest = _dbContext.Restaurantmenus.FirstOrDefault(x => x.Id == menu.Id);

            if (rest != null)
            {
                return BadRequest("Id not found");
            }
            var result = _mapper.Map<RestautantMenuMV>(rest);

            rest.Id = menu.Id;
            rest.MaelName = menu.MaelName;
            rest.Quatity = menu.Quatity;
            rest.PriceNis = menu.PriceNis;
            rest.PriceUsd = (float)(menu.PriceNis * 3.50);
            rest.ResurantId = menu.ResurantId;
            rest.CreatedDate = menu.CreatedDate;
            rest.UpdatedDate = menu.UpdatedDate;
            rest.Archived = menu.Archived;

            _dbContext.Entry(rest).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(rest);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(RestautantMenuMV menu)
        {
            var rest = _dbContext.Restaurantmenus.FirstOrDefault(x => x.Id == menu.Id);

            if (rest != null)
            {
                return BadRequest("Id not found");
            }
            var result = _mapper.Map<RestautantMenuMV>(rest);
            _dbContext.Restaurantmenus.Remove(rest);
            _dbContext.SaveChanges();

            return Ok("Done!!");
        }
    }
}
