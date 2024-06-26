using System;

namespace SO2M.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }

        public Utilisateur Utilisateur { get; set; }
    }
}