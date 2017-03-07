using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PhotoLife.Models;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;


namespace PhotoLife.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            BackgroundUploader bgUploader = GetUploader();

            if (bgUploader.Progress < 100)
                return PartialView("Upload", bgUploader);

            // The whole uploader object will be passed to view as model
            // This is to show usage of some API helpers that builds HTML or URL
            return PartialView("Show", bgUploader);
        }

        /// <summary>
        /// Gets uploading progress
        /// </summary>
        public ActionResult Progress()
        {
            BackgroundUploader bgUploader = GetUploader();

            return Json(new { progress = bgUploader.Progress }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets uploader from session or creates a new one
        /// </summary>
        private BackgroundUploader GetUploader()
        {
            BackgroundUploader bgUploader = Session["bguploader"] as BackgroundUploader;

            if (bgUploader == null)
            {
                bgUploader = new BackgroundUploader();
                Session.Add("bguploader", bgUploader);
                bgUploader.Upload();
                return bgUploader;
            }

            return bgUploader;
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email, Description = model.Description, Name = model.Name };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }

    /// <summary>
    /// We can inherit API class to make more convenient for us
    /// These fields will not be passes to cloudinary
    /// They will be used just at showing images after uploading
    /// </summary>
    class ImageUploadParamsExt : ImageUploadParams
    {
        /// <summary>
        /// Image caption to show
        /// </summary>
        public string Caption;

        /// <summary>
        /// Transformation that will be applied at showing image, not at uploading
        /// </summary>
        public Transformation ShowTransform;
    }

    /// <summary>
    /// Model that will be passed to view
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Image caption to show
        /// </summary>
        public string Caption;

        /// <summary>
        /// Transformation that will be applied at showing image, not at uploading
        /// </summary>
        public Transformation ShowTransform;

        /// <summary>
        /// Cloudinary image URL
        /// </summary>
        public string Url;

        /// <summary>
        /// Image format
        /// </summary>
        public string Format;

        /// <summary>
        /// Cloudinary public ID of image
        /// </summary>
        public string PublicId;
    }

    public class BackgroundUploader
    {
        // Cloudinary API object
        Cloudinary m_cloudinary;

        // Parameters of uploading tasks
        List<ImageUploadParamsExt> m_uploadParams;

        // Results of uploading tasks
        List<Image> m_images;

        // Background uploading task
        Task<bool> m_task;
        int m_progress = 0;
        object m_sync = new object();

        public List<Image> Images
        {
            get { return m_images; }
        }

        public Api CloudinaryApi
        {
            get { return m_cloudinary.Api; }
        }

        public BackgroundUploader()
        {
            // Set up cloudinary object

            Account acc = new Account(
                "djga3zgwr",
                "786453771238183",
                "Lts6xv-uEkffhjUqJS0xThKrsI0");


            m_cloudinary = new Cloudinary(acc);

            // Check that application is properly installed in IIS
            //string fileToCheck = HttpContext.Current.Server.MapPath("/Images/pizza.jpg");
            //if (!File.Exists(fileToCheck))
            //    throw new ApplicationException(String.Format("Can't find file {0}!", fileToCheck));

            // Set up parameters of uploading tasks

            m_uploadParams = new List<ImageUploadParamsExt>();

            //// Upload local image, public_id will be generated on Cloudinary's backend.
            //m_uploadParams.Add(new ImageUploadParamsExt()
            //{
            //    File = new FileDescription(HttpContext.Current.Server.MapPath("/Images/pizza.jpg")),
            //    Tags = "basic_mvc4",

            //    Caption = "Local file, Fill 200x150",
            //    ShowTransform = new Transformation().Width(200).Height(150).Crop("fill")
            //});

            //// Upload local image, uploaded with a public_id.
            //m_uploadParams.Add(new ImageUploadParamsExt()
            //{
            //    File = new FileDescription(HttpContext.Current.Server.MapPath("/Images/pizza.jpg")),
            //    PublicId = "custom_name",
            //    Tags = "basic_mvc4",

            //    Caption = "Local file, custom public ID, Fit into 200x150",
            //    ShowTransform = new Transformation().Width(200).Height(150).Crop("fit")
            //});

            //// Eager transformations are applied as soon as the file is uploaded, instead of waiting
            //// for a user to request them. 
            //m_uploadParams.Add(new ImageUploadParamsExt()
            //{
            //    File = new FileDescription(HttpContext.Current.Server.MapPath("/Images/lake.jpg")),
            //    PublicId = "eager_custom_name",
            //    Tags = "basic_mvc4",
            //    EagerTransforms = new List<Transformation>()
            //    {
            //        new EagerTransformation().Width(200).Height(150).Crop("scale")
            //    },

            //    Caption = "Local file, Eager transformation of scaling to 200x150",
            //    ShowTransform = new Transformation().Width(200).Height(150).Crop("scale")
            //});

            // In the two following examples, the file is fetched from a remote URL and stored in Cloudinary.
            // This allows you to apply the same transformations, and serve those using Cloudinary's CDN layer.
            m_uploadParams.Add(new ImageUploadParamsExt()
            {
                File = new FileDescription("http://res.cloudinary.com/demo/image/upload/couple.jpg"),
                Tags = "basic_mvc4",

                Caption = "Uploaded remote image, Face detection based 200x150 thumbnail",
                ShowTransform = new Transformation().Width(200).Height(150).Crop("thumb").Gravity("faces")
            });

            m_uploadParams.Add(new ImageUploadParamsExt()
            {
                File = new FileDescription("http://res.cloudinary.com/demo/image/upload/couple.jpg"),
                Tags = "basic_mvc4",
                Transformation = new Transformation().Width(500).Height(500).Crop("fit").Effect("saturation:-70"),

                Caption = "Uploaded remote image, Fill 200x150, round corners, apply the sepia effect",
                ShowTransform =
                    new Transformation().Width(200).Height(150).Crop("fill").Gravity("face").Radius(10).Effect("sepia")
            });

            m_images = new List<Image>();
        }

        public int Progress
        {
            get
            {
                lock (m_sync)
                {
                    return m_progress;
                }
            }
        }

        // Performs all uploading tasks and fills results (model to apply to view)
        public void Upload()
        {
            // Upload images in background to allow user to see the progress
            m_task = Task.Factory.StartNew(() =>
            {
                try
                {
                    for (int i = 0; i < m_uploadParams.Count; i++)
                    {
                        // Using Cloudinary API to upload images
                        ImageUploadResult result = m_cloudinary.Upload(m_uploadParams[i]);

                        m_images.Add(new Image()
                        {
                            // Copy predefined caption and transformation
                            Caption = m_uploadParams[i].Caption,
                            ShowTransform = m_uploadParams[i].ShowTransform,

                            // Load data from Cloudinary response
                            PublicId = result.PublicId,
                            Url = result.Uri.ToString(),
                            Format = result.Format
                        });

                        lock (m_sync)
                        {
                            m_progress = (i + 1) * 100 / m_uploadParams.Count;
                        }
                    }

                    return true;
                }
                catch
                {
                    lock (m_sync)
                    {
                        m_progress = 100;
                    }

                    return false;
                }
            });
        }
    }
}