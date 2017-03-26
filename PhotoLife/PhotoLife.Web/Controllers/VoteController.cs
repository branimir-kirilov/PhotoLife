using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoLife.Authentication.Providers;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Controllers
{
    public class VoteController : Controller
    {
        private readonly IVotingService voteService;
        private readonly IAuthenticationProvider authenticationProvider;

        public VoteController(IVotingService voteService, IAuthenticationProvider authenticationProvider)
        {
            if (voteService == null)
            {
                throw new ArgumentNullException(nameof(voteService));
            }

            if (authenticationProvider == null)
            {
                throw new ArgumentNullException(nameof(authenticationProvider));
            }

            this.voteService = voteService;
            this.authenticationProvider = authenticationProvider;
        }
        
        [HttpPost]
        public ActionResult Vote(int logId, int currentVoteCount)
        {
            var userId = this.authenticationProvider.CurrentUserId;

            var rating = this.voteService.Vote(logId, userId);

            if (rating < 0)
            {
                rating = currentVoteCount;
            }

            return this.PartialView(rating);
        }
    }
}