using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace shop.Web.Controllers.Comments
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        [HttpGet]
        public ActionResult get()
        {
            var a = "dfg";
            var b = Convert.ToInt32(a);
            return Ok();
        }
    }
}
