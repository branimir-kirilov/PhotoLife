using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoLife.Models
{
    public class Vote
    {
        public Vote()
        {

        }

        public Vote(int postId, string userId)
        {
            this.PostId = postId;
            this.UserId = userId;
        }

        [Key]
        public int VoteId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}
