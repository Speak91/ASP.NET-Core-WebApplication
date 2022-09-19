﻿namespace HomeWork1
{
    public class PostModel
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public override string ToString()
        {
            return $" {UserId}\n {Id}\n {Title}\n {Body = Body.Replace("\n", @"\n")}\n";
        }
    }
}
