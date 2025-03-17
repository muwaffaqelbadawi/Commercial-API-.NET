using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.Mappers;
using Api.DTOs.Stock;


namespace Api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
            .Select(stock => stock.toStockDTO());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.toStockDTO());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDTO stockDTO)
        {
            var stockModel = stockDTO.toStockFromCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.toStockDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO UpdateDTO)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }

            stockModel.Symbol = UpdateDTO.Symbol;
            stockModel.CompanyName = UpdateDTO.CompanyName;
            stockModel.Purchase = UpdateDTO.Purchase;
            stockModel.LastDiv = UpdateDTO.LastDiv;
            stockModel.Industry = UpdateDTO.Industry;
            stockModel.MarketCap = UpdateDTO.MarketCap;

            _context.SaveChanges();
            return Ok(stockModel.toStockDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stockModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();
            return NoContent();
        }
    }
}