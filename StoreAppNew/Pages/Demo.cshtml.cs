using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreAppNew2.Pages
{
    public class DemoModel : PageModel
    {
        public string? FullName => HttpContext.Session.GetString("name");

        public void OnGet()
        {
        }

        public void OnPost([FromForm]string name)
        {
            //FullName = name;
            //To keep the name information through the session not temporarily.
            HttpContext.Session.SetString("name", name);
        }
    }
}
