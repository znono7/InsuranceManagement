using InsuranceManagement.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Container = InsuranceManagement.API.Container;

namespace InsuranceManagement.API
{
    /// <summary>
    /// Manages the Web API calls
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        #region Protected Members

        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected EFDbContext mContext;

        /// <summary>
        /// The manager for handling user creation, deletion, searching, roles etc...
        /// </summary>
        protected UserManager<ApplicationUser> mUserManager;

        /// <summary>
        /// The manager for handling signing in and out for our users
        /// </summary>
        protected SignInManager<ApplicationUser> mSignInManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The injected context</param>
        /// <param name="signInManager">The Identity sign in manager</param>
        /// <param name="userManager">The Identity user manager</param>
        public APIController(
            EFDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        #endregion

        /// <summary>
        /// Logs in a user using token-based authentication
        /// </summary>
        [Route("api/login")]
        public async Task<ApiResponse<LoginResultModel>> LogInAsync([FromBody]LoginModel loginModel)
        {
            // The message when we fail to login
            var ErrorMessage = "Invalid username or password";

            // The error response for a failed login
            var errorResponse = new ApiResponse<LoginResultModel>
            {
                // Set error message
                ErrorMessage = ErrorMessage
            };

            if ( string.IsNullOrWhiteSpace(loginModel.UsernameOrEmail) || string.IsNullOrWhiteSpace(loginModel.Password))
                // Return error message to user
                return errorResponse;

            // Is it an email?
            var isEmail = loginModel.UsernameOrEmail.Contains("@");

            // Get the user details
            var user = isEmail ? 
                await mUserManager.FindByEmailAsync(loginModel.UsernameOrEmail) : 
                await mUserManager.FindByNameAsync(loginModel.UsernameOrEmail);

            // If we failed to find a user...
            if (user == null)
                // Return error message to user
                return errorResponse;

            // Get if password is valid
            var isValidPassword = await mUserManager.CheckPasswordAsync(user, loginModel.Password);

            // If the password was wrong
            if (!isValidPassword)
                // Return error message to user
                return errorResponse;

            // Get username
            var username = user.UserName;

            // Set our tokens claims
            var claims = new[]
            {
                // Unique ID for this token
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),

                //The username using the Identity name so it fills out the HttpContext.User.Identity.Name value
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
            };

            // Create the credentials used to generate the token
            var credentials = new SigningCredentials(
                // Get the secret key from configuration
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Container.Configuration["Jwt:SecretKey"])),
                // Use HS256 algorithm
                SecurityAlgorithms.HmacSha256
                );

            // Generate the Jwt Token
            var token = new JwtSecurityToken(
                issuer: Container.Configuration["Jwt:Issuer"],
                audience: Container.Configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,
                // Expire if not used for 3 months
                expires:DateTime.Now.AddMonths(3)
                );


            // Pass back the user details and the token
            var userdetails = new LoginResultModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                StateOfBirth = user.State.Name,
                NIdentification = user.NIdentification,
                Phone = user.Phone,
                CCPpayment = user.CCPpayment,
                Username = user.UserName,
                Email = user.Email,
                LicenseNumber = user.LicenseNumber,
                LicenseExpiryDate = user.LicenseExpiryDate,
                LicenseIssuingState = user.LicenseIssuingState,
                Agency = user.Agency.Name,
                AgencyAddress = user.Agency.Address,
                AgencyState = user.Agency.State.Name,
            };

            // Provide the geographical area for the work of both the expert and the mechanic
            if (user.Municipality != null)
            {
                userdetails.Municipality = user.Municipality.Name;
                userdetails.District = user.Municipality.District.Name;
                userdetails.State = user.Municipality.District.State.Name;
            }

            // Return token to user
            return new ApiResponse<LoginResultModel>
            {
                Response = userdetails
            };
        }
    }
}
