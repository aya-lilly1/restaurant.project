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
    public class OrderController : ControllerBase
    {
        private restaurantdbContext _dbContext;
        private IMapper _mapper;
        public OrderController(restaurantdbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        private string IsAvilable( object v )
        {
            var result = _dbContext.Restaurantmenus.ToList();
            var res = _mapper.Map<OrderMV>(result);
            if (res.Quatity > 0)
            {
                return ("done!!");
            }
            return ("Finished");
        }
        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult GetOrder()
        {
            _dbContext.IgnoreFilterRestaurnt = true;  
            var result = _dbContext.Orders.ToList();
            var resultView = _mapper.Map<IEnumerable<OrderMV>>(result);
            return Ok(resultView);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IActionResult GetOrderId(int id)
        {
            var resultId = _dbContext.Orders.Find(id);
            var resultView = _mapper.Map<OrderMV>(resultId);
            return Ok(resultView);
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult CreatOrder([FromBody] OrderMV OrderMV)
        {
            var rest = _dbContext.Orders.FirstOrDefault(x => x.Id == OrderMV.Id);
            if (rest != null)
            {
                return BadRequest("Order alredy exist");
            }

            var result = _mapper.Map<OrderMV>(rest);
            var COrder = new Order
            {
                Id = OrderMV.Id,
                MealId = OrderMV.MealId,
                CustomerId = OrderMV.CustomerId,
                RestaurantId = OrderMV.RestaurantId,
                Quatity = OrderMV.Quatity,
                CreatedDate = OrderMV.CreatedDate,
                UpdatedDate = OrderMV.UpdatedDate,
                Archived = OrderMV.Archived,
            };

                rest = _dbContext.Orders.Add(COrder).Entity;

            string IsAvilable( )
            {
                var result = _dbContext.Restaurantmenus.ToList();
                var res = _mapper.Map<OrderMV>(result);
                if (res.Quatity > 0)
                {
                    _dbContext.SaveChanges();
                }
                return ("Sorry Not Avilable");
            }
            IsAvilable( );
            return Ok ("Done");
        }


        [HttpPut("{id}")]
        public IActionResult UpdateOrder(OrderMV OrderMV)
        {
            var rest = _dbContext.Orders.FirstOrDefault(x => x.Id == OrderMV.Id);

            if (rest != null)
            {
                return BadRequest("Id not found");
            }
            var result = _mapper.Map<OrderMV>(rest);

            rest.Id = OrderMV.Id;
            rest.MealId = OrderMV.MealId;
            rest.CustomerId = OrderMV.CustomerId;
            rest.RestaurantId = OrderMV.RestaurantId;
            rest.Quatity = OrderMV.Quatity;
            rest.CreatedDate = OrderMV.CreatedDate;
            rest.UpdatedDate = OrderMV.UpdatedDate;
            rest.Archived = OrderMV.Archived;
            _dbContext.Entry(rest).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(rest);

        }

            // DELETE api/<OrderController>/5
            [HttpDelete("{id}")]
            public IActionResult Delete(OrderMV OrderMV)
            {
            var rest = _dbContext.Orders.FirstOrDefault(x => x.Id == OrderMV.Id);

            if (rest != null)
            {
                return BadRequest("Id not found");
            }
            var result = _mapper.Map<OrderMV>(rest);
           _dbContext.Orders.Remove(rest);
            _dbContext.SaveChanges();

            return Ok("Done!!");
        }
        
    }
}
