using Microsoft.AspNetCore.Mvc;
using OlağanWeb.Models;
using System.Data.SqlClient;
using System.Reflection;

namespace OlağanWeb.Controllers;

public class OlaganSiirController : Controller
{
    readonly IConfiguration _configuration;

    public OlaganSiirController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        List<TextModel> texts = new List<TextModel>();

        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:sql"]))
        {
            connection.Open();
            var command = new SqlCommand("select TextId, Title, CategoryName, WriteFullName, Number, Picture, PublishedInOlaganSiir,Text,Referances, Summary, CreatedTime,UpdatedTime from texts as te join Categories as ca on te.CategoryId = ca.CategoryId join Writers as wr on te.WriterId = wr.WriterId", connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                TextModel model = new TextModel();
                model.Id = reader.GetInt32(0);
                model.Title = reader.GetString(1);
                model.Category = reader.GetString(2);
                model.Writer = reader.GetString(3);
                model.picture = reader.GetString(5);
                model.Text = reader.GetString(7);
                model.Referances = reader.GetString(8);
                model.Summary = reader.GetString(9);
                texts.Add(model);
            }

        }
        return View(texts);
    }

    public IActionResult Content(int a)
    {


        List<TextModel> texts = new List<TextModel>();
        TextModel model = new TextModel();
        List<CommentModel> comments = new List<CommentModel>();


        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:sql"]))
        {
            connection.Open();
            var command2 = new SqlCommand("select TextId, Title, CategoryName, WriteFullName, Number, Picture, PublishedInOlaganSiir,Text,Referances, Summary, CreatedTime,UpdatedTime from texts as te join Categories as ca on te.CategoryId = ca.CategoryId join Writers as wr on te.WriterId = wr.WriterId", connection);
            var reader2 = command2.ExecuteReader();

            while (reader2.Read())
            {
                TextModel model2 = new TextModel();
                model2.Id = reader2.GetInt32(0);
                model2.Title = reader2.GetString(1);
                model2.Category = reader2.GetString(2);
                model2.Writer = reader2.GetString(3);
                model2.picture = reader2.GetString(5);
                model2.Text = reader2.GetString(7);
                model2.Referances = reader2.GetString(8);
                model2.Summary = reader2.GetString(9);
                texts.Add(model2);
            }
            connection.Close();
            connection.Open();


            var command = new SqlCommand("select TextId, Title, CategoryName, WriteFullName, Number, Picture, PublishedInOlaganSiir,Text,Referances, Summary, CreatedTime,UpdatedTime from texts as te join Categories as ca on te.CategoryId = ca.CategoryId join Writers as wr on te.WriterId = wr.WriterId where TextId = @TextId", connection);
            command.Parameters.AddWithValue("@TextId", a);
            var reader = command.ExecuteReader();
            reader.Read();
            model.Id = reader.GetInt32(0);
            model.Title = reader.GetString(1);
            model.Writer = reader.GetString(3);
            model.picture = reader.GetString(5);
            model.Text = reader.GetString(7);
            model.Referances = reader.GetString(8);
            model.Summary = reader.GetString(9);
            connection.Close();


            connection.Open();
            CommentModel comment = new();
            var commentCommand = new SqlCommand("select * from comments where TextId = @TextId and IsActive = 1", connection);
            commentCommand.Parameters.AddWithValue("@TextId", a);
            var reader3 = commentCommand.ExecuteReader();
            if (reader3.HasRows)
            {
                while (reader3.Read())
                {
                    comment.CommentId = reader3.GetInt32(0);
                    comment.Comments = reader3.GetString(1);
                    comment.Name = reader3.GetString(3);
                    comment.CommentDate = reader3.GetDateTime(4);
                    comment.IsActive = reader3.GetBoolean(5);
                    comments.Add(comment);
                }
            }
        }
        var ViewModels = new ContentVM {
            CommentModels = comments,
            TextModels = texts

        };

        ViewBag.Model = model;

        return View(ViewModels);
    }

    [HttpGet]
    public IActionResult Category(string a)
    {
        List<TextModel> CategoryTexts = new List<TextModel>();

        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:sql"]))
        {
            connection.Open();
            var command = new SqlCommand("select TextId, Title, CategoryName, WriteFullName, Number, Picture, PublishedInOlaganSiir,Text,Referances, Summary, CreatedTime,UpdatedTime from texts as te join Categories as ca on te.CategoryId = ca.CategoryId join Writers as wr on te.WriterId = wr.WriterId where CategoryName = @Category", connection);
            command.Parameters.AddWithValue("@Category", a);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                TextModel model = new TextModel();
                model.Id = reader.GetInt32(0);
                model.Title = reader.GetString(1);
                model.Category = reader.GetString(2);
                model.Writer = reader.GetString(3);
                model.picture = reader.GetString(5);
                model.Text = reader.GetString(7);
                model.Referances = reader.GetString(8);
                model.Summary = reader.GetString(9);
                CategoryTexts.Add(model);
            }
        }
        return View("index", CategoryTexts);
    }

    [HttpGet]
    public IActionResult Number(int a)
    {
        List<TextModel> CategoryTexts = new List<TextModel>();

        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:sql"]))
        {
            connection.Open();
            var command = new SqlCommand("select TextId, Title, CategoryName, WriteFullName, Number, Picture, PublishedInOlaganSiir,Text,Referances, Summary, CreatedTime,UpdatedTime from texts as te join Categories as ca on te.CategoryId = ca.CategoryId join Writers as wr on te.WriterId = wr.WriterId where Number = @Number", connection);
            command.Parameters.AddWithValue("@Number", a);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                TextModel model = new TextModel();
                model.Id = reader.GetInt32(0);
                model.Title = reader.GetString(1);
                model.Category = reader.GetString(2);
                model.Writer = reader.GetString(3);
                model.picture = reader.GetString(5);
                model.Text = reader.GetString(7);
                model.Referances = reader.GetString(8);
                model.Summary = reader.GetString(9);
                CategoryTexts.Add(model);
            }
        }
        return View("index", CategoryTexts);
    }

    [HttpPost]
    public IActionResult PostComment(CommentModel Comment)
    {
        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:sql"]))
        {
            connection.Open();
            var command = new SqlCommand("INSERT INTO Comments (Comment, TextId, Name, CommentDate) VALUES (@Comment, @TextId, @Name, @CommentDate)", connection);
            command.Parameters.AddWithValue("@Comment", Comment.Comments);
            command.Parameters.AddWithValue("@TextId", Convert.ToInt16(Comment.TextId));
            command.Parameters.AddWithValue("@Name", Comment.Name);
            command.Parameters.AddWithValue("@CommentDate", DateTime.Now);
            command.ExecuteNonQuery();
        }
        ModelState.Clear();
        return NoContent();
    }
}

