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
    public class CustomerController : ControllerBase
    {
        private restaurantdbContext _dbContext;
        private IMapper _mapper;
        public CustomerController(restaurantdbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetCustemer()
        {
            _dbContext.IgnoreFilterCust = true;
            var result = _dbContext.Customers.ToList();
            var resultView = _mapper.Map<IEnumerable<CustomerMV>>(result);
            return Ok(resultView);
          
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetCustID(int id)
        {
            var resultId = _dbContext.Customers.Find(id);
            var resultView = _mapper.Map<CustomerMV>(resultId);
            return Ok(resultView);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult creatCustomer([FromBody] CustomerMV CustomerMV)
        {

            var rest = _dbContext.Customers.FirstOrDefault(x => x.Id == CustomerMV.Id);
            if (rest != null)
            {
                return BadRequest("Customer alredy exist");
            }

            var result = _mapper.Map<CustomerMV>(rest);

            rest = _dbContext.Customers.Add(new Customer
            {
                Id = CustomerMV.Id ,
                FirstName = CustomerMV.FirstName.ToCapCase(),
                LastName = CustomerMV.LastName.ToCapCase(),
                CreatedDate = CustomerMV.CreatedDate ,
                UpdatedDate = CustomerMV.UpdatedDate ,
                Archived = CustomerMV.Archived
            }).Entity;
            _dbContext.SaveChanges();
            return Ok(rest);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(CustomerMV CustomerMV)
        {
            var rest = _dbContext.Customers.FirstOrDefault(x => x.Id == CustomerMV.Id);

            if (rest != null)
            {
                return BadRequest("Id not found");
            }
            var result = _mapper.Map<CustomerMV>(rest);

            rest.Id = CustomerMV.Id;
            rest.FirstName = CustomerMV.FirstName.ToCapCase();
            rest.LastName = CustomerMV.LastName.ToCapCase();
            rest.CreatedDate = CustomerMV.CreatedDate;
            rest.UpdatedDate = CustomerMV.UpdatedDate;
            rest.Archived = CustomerMV.Archived;
           
            _dbContext.Entry(rest).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(rest);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(CustomerMV CustomerMV)
        {
            var rest = _dbContext.Customers.FirstOrDefault(x => x.Id == CustomerMV.Id);

            if (rest != null)
            {
                return BadRequest("Id not found");
            }
            var result = _mapper.Map<CustomerMV>(rest);
            _dbContext.Customers.Remove(rest);
            _dbContext.SaveChanges();

            return Ok("Done!!");
        }
    }
}
