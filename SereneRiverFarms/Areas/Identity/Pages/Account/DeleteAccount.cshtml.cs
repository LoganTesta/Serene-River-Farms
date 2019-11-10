using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SereneRiverFarms.Areas.Identity.Data;


namespace SereneRiverFarms.Areas.Identity.Pages.Account
{

    [Authorize]
    public class DeleteAccountModel : PageModel
    {
        private readonly SignInManager<SereneRiverFarmsUser> _signInManager;
        private readonly UserManager<SereneRiverFarmsUser> _userManager;
        private readonly ILogger<DeleteAccountModel> _logger;

        public DeleteAccountModel(
            UserManager<SereneRiverFarmsUser> userManager,
            SignInManager<SereneRiverFarmsUser> signInManager,
            ILogger<DeleteAccountModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Required]
            [DataType(DataType.Password)]
            public string userPassword { get; set; }

        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'."); 
            }

            if(!await _userManager.CheckPasswordAsync(user, "" + Input.userPassword))
            {
                ModelState.AddModelError(string.Empty, "Password not correct.");
                return Page();
            }
            

            if(Input.userPassword.Length > 2)
            {
            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occured while deleting user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User with ID '{UserId}' deleted their account.", userId);
                                          
            }

            return Redirect("~/");
        }




    }
}