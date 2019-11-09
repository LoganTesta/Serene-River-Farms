using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SereneRiverFarms.Areas.Identity.Data;


namespace SereneRiverFarms.Areas.Identity.Pages.Account
{
    [Authorize]  //Restricts access to this page to only signed in users.
    public class AccountHomeModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
