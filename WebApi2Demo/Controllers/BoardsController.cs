using DemoWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi2Demo.Data;

namespace WebApi2Demo.Controllers
{
    [EnableCors("*","*", "GET")]
    public class BoardsController : ApiController
    {
        // GET api/boards
        public IEnumerable<Board> Get()
        {
            IEnumerable<Board> boards;
            using (var ctx = new DemoContext())
            {
                boards = ctx.Boards.ToList();    
            }

            return boards;
        }

        
        public IHttpActionResult Get(int id)
        {
            Board b;

            using (var ctx = new DemoContext())
            {
                b = ctx.Boards.SingleOrDefault(x => x.Id == id);
            }

            var result = Ok(b);
            
            return result;
        }

        [Route("api/boards/{id}/cards")]
        public IEnumerable<Card> GetCards(int id)
        {
            IEnumerable<Card> cards;
            using (var ctx = new DemoContext())
            {
                cards = ctx.Cards.Where(x => x.BoardId == id).ToList();
            }

            return cards;
        }

        // POST api/boards
        public void Post(Board board)
        {
            using (var ctx = new DemoContext())
            {
                ctx.Boards.Add(board);
                ctx.SaveChanges();
            }

            #region IHttpActionResult

            //var location = Request.RequestUri.ToString()+ "/" + board.Id;
            //return Created(location, board);

            #endregion
        }
    }
}
