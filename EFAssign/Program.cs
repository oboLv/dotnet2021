using System;
using EFAssign.DataModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFAssign
{
    class Program
    {
        static void Main(string[] args)
        {
            var exit = false;
            while(!exit)
            {
                Console.Clear();
                System.Console.WriteLine("1. Display Blogs");
                System.Console.WriteLine("2. Add Blog");
                System.Console.WriteLine("3. Display Posts");
                System.Console.WriteLine("4. Add Post");
                var choice = Console.ReadLine();
                // display blogs
                if(choice == "1")
                {
                    Console.Clear();
                    using (var db = new BloggingContext())
                    {
                        var blogs = db.Blogs;
                        foreach(var blog in blogs)
                        {
                            System.Console.WriteLine("{0}. {1}", blog.BlogId, blog.Url);
                        }
                    }
                    Console.ReadKey();
                }
                // add blog
                else if(choice == "2")
                {
                    Console.Clear();
                    System.Console.WriteLine("Name of new Blog: ");
                    var name = Console.ReadLine();
                    using (var db = new BloggingContext())
                    {
                        var blog = new Blog()
                        {
                            Rating = 1,
                            Url = name
                        };
                        db.Blogs.Add(blog);
                        db.SaveChanges();
                    }
                    System.Console.WriteLine("Blog Added!");
                    Console.ReadKey();
                }
                // display posts
                else if(choice == "3")
                {
                    Console.Clear();
                    using (var db = new BloggingContext())
                    {
                        var blogs = db.Blogs
                                        .Include(b => b.Posts);

                        foreach (var blog in blogs)
                        {
                            System.Console.WriteLine("Blog");
                            System.Console.WriteLine("{0, -3} {1}", blog.BlogId, blog.Url);
                            foreach (var post in blog.Posts)
                            {
                                System.Console.WriteLine("\tPosts");
                                System.Console.WriteLine("\t{0, -3} {1}", post.PostId, post.Title);
                                System.Console.WriteLine("\t\t{0}", post.Content);
                            }
                        }
                    }
                    Console.ReadKey();
                }
                // add post
                else if(choice == "4")
                {
                    Console.Clear();
                    using (var db = new BloggingContext())
                    {
                        System.Console.WriteLine("Choose blog to add to:");
                        var blogs = db.Blogs;
                        foreach (var blog in blogs)
                        {
                            System.Console.WriteLine("{0}. {1}", blog.BlogId, blog.Url);
                        }
                        var blogChoice = Console.ReadLine();
                        System.Console.WriteLine("New post title:");
                        var newTitle = Console.ReadLine();
                        System.Console.WriteLine("New post content:");
                        var newContent = Console.ReadLine();
                        var newPost = new Post()
                        {
                            Title = newTitle,
                            Content = newContent,
                            BlogId = Convert.ToInt32(blogChoice)
                        };
                        db.Posts.Add(newPost);
                        db.SaveChanges();
                    }
                }
                else
                {
                    exit = true;
                }
            }
        }
    }
}
