﻿namespace BloggingApp.Models.ReplyDTOs
{
    public class RetweetReplyDetailsReturnDTO
    {
        public int Id { get; set; }
        public string ReplyType { get; set; }
        public string ReplyContent { get; set; }
        public int UserId { get; set; }
        public DateTime ReplyDateTime { get; set; }

        // ForeignKey - Comment ID
        public int CommentId { get; set; }
        public string UserName { get; set; }
        public string PUserId { get; set; }
        public string UserProfileImageLink { get; set; }
        public int LikedCount { get; set; }
        public string IsLikedByUser { get; set; }
    }
}
