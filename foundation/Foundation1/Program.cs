using System;
using System.Collections.Generic;

class Program
{

    public class Comment
    {
        public string CommenterName { get; set; }
        public string Text { get; set; }


        public Comment (string commenterName, string text)
        {
            CommenterName = commenterName;
            Text = text;

        }
    }

    public class Video
    {

        public string Title { get; set; }

        public string Author { get; set; }

        public int LengthInSeconds { get; set; }

        public List<Comment> Comments { get; set; }
        public int lengthInSeconds { get; private set; }

        public Video(string title, string author, int LengthInSeconds)
        {
            Title = title;
            Author = author;
            LengthInSeconds = lengthInSeconds;
            Comments = new List<Comment>();

        }

        public int GetNumberOfComments()
        {
            return Comments.Count;
        }
    }

    static void Main(string[] args)
    {
        Video video1 = new Video("How to Program in C#", "John Hall", 600);
        Video video2 = new Video("C# Basic Tutorial", "Jane Smith", 450);
        Video video3 = new Video("Advanced C# Programming", "Alex Brown", 1200);

        video1.Comments.Add(new Comment("Alice", "Great video, Very informative!"));
        video1.Comments.Add(new Comment("Bob", "I learned a lot from this tutorial."));
        video1.Comments.Add(new Comment("Charlie", "Clear explanation, thanks!"));

        video2.Comments.Add(new Comment("David", "Perfect for beginners, thanks!"));
        video2.Comments.Add(new Comment("Emma", "I liked the examples you used."));
        video2.Comments.Add(new Comment("Fay", "Would love more practice exercises"));

        video3.Comments.Add(new Comment("George", "This is more advanced but very helpful."));
        video3.Comments.Add(new Comment("Hellen", "I can apply this knowledge to my work now."));
        video3.Comments.Add(new Comment("Ian", "Fantastic Content, keep it up!"));

        List<Video> videos = new List<Video> { video1, video2, video3 };

        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}\n");

            foreach (var comment in video.Comments)
            {
                Console.WriteLine($"Comment by {comment.CommenterName}: {comment.Text}");
            }
        }



    }


}
