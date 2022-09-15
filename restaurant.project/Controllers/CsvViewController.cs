using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using restaurant.project.Models;
using restaurant.project.ModelView;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


namespace restaurant.project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvViewController : ControllerBase
    {
        public class OrderController : ControllerBase
        {
            private restaurantdbContext _dbContext;
            private IMapper _mapper;
            public OrderController(restaurantdbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            [HttpGet]
            public IActionResult Get()
            {
                var res = _dbContext.CsvViews.ToList();
                var resultView = _mapper.Map<IEnumerable<CsvViewMV>>(res);
                using(var writer = new StreamWriter("D:\\csv") )
                using (var csvwriter = new CsvWriter(writer,CultureInfo.InvariantCulture))
                { csvwriter.WriteRecords(resultView); }
                    return Ok();
            }


        }
    }
}

