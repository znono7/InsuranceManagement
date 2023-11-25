using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceManagement.API
{
   
    public class ExpertController : ControllerBase
    {

        #region Protected Members
        // The scoped Application context
        protected EFDbContext _context;

        // The manager for handling user creation, deletion, searching, roles etc...
        protected UserManager<ApplicationUser> _userManager;

        // The manager for handling signing in and out for our users
        protected SignInManager<ApplicationUser> _signInManager;


        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public ExpertController(EFDbContext context,
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion
       
    }
}
