using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OlağanWeb.Models;
using System.Data.SqlClient;
using System.Reflection;

namespace OlağanWeb.Controllers;
[Authorize(AuthenticationSchemes = "user")]
public class AdminController : Controller
{
    readonly IConfiguration _configuration;

    public AdminController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult TextAdded()
    {

        return View();
    }

    public IActionResult AddText()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddText(PostTextModel Model)
    {
        ViewBag.hata = null;

        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:sql"]))
        {
            try
            {
                connection.Open();
                var command2 = new SqlCommand("select * from writers where WriteFullName = @Writer", connection);
                command2.Parameters.AddWithValue("@Writer", Model.Writer);
                var reader2 = command2.ExecuteReader();
                reader2.Read();
                int a = reader2.GetInt32(0);
                connection.Close();

                connection.Open();
                var command3 = new SqlCommand("select * from Categories where CategoryName = @CategoryName", connection);
                command3.Parameters.AddWithValue("@CategoryName", Model.Category);
                var reader3 = command3.ExecuteReader();
                reader3.Read();
                int b = reader3.GetInt32(0);
                connection.Close();

                connection.Open();
                var command = new SqlCommand("INSERT INTO Texts (Title, CategoryId, WriterId, Number, Picture, PublishedInOlaganSiir, [Text], Referances, Summary, CreatedTime,UpdatedTime) VALUES (@Title, @CategoryId, @WriterId, @Number, @Picture, @PublishedInOlaganSiir ,@Text, @Referances, @Summary, @CreatedTime, @UpdatedTime)", connection);
                command.Parameters.AddWithValue("@Title", Model.Title);
                command.Parameters.AddWithValue("@CategoryId", a);
                command.Parameters.AddWithValue("@WriterId", b);
                command.Parameters.AddWithValue("@Number", Model.number);
                command.Parameters.AddWithValue("@Picture", Model.picture);
                command.Parameters.AddWithValue("@PublishedInOlaganSiir", Model.PublishedInOlaganSiir);
                command.Parameters.AddWithValue("@Text", Model.Text);
                command.Parameters.AddWithValue("@Referances", Model.Referances);
                command.Parameters.AddWithValue("@Summary", Model.Summary);
                command.Parameters.AddWithValue("@CreatedTime", DateTime.Now);
                command.Parameters.AddWithValue("@UpdatedTime", DateTime.Now);
                // connection.Close();
                command.ExecuteNonQuery();

                return RedirectToAction("TextAdded");
            }
            catch ( Exception Exception)
            {

                ViewBag.hata = "İçerik Eklenemedi";
                return View();
            }
        }
    }


    public IActionResult TextDeleted()
    {

        return View();
    }

    public IActionResult DeleteText()
    {

        return View();
    }

    [HttpPost]
    public IActionResult DeleteText(DeleteText delete)
    {
        ViewBag.hata = null;
        try
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:sql"]))
            {
                connection.Open();
                var command = new SqlCommand("delete from texts where Title = @Title", connection);
                command.Parameters.AddWithValue("@Title", delete.Title);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("TextDeleted");
        }
        catch (Exception)
        {

            ViewBag.hata = "Metin Silinemedi";
            return View();
        }
    }

    public IActionResult TextUpdated()
    {

        return View();
    }

    public IActionResult UpdateText()
    {

        return View();
    }

    [HttpPost]
    public IActionResult UpdateText(PostTextModel Model, string Title)
    {

        ViewBag.hata = null;

        using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:sql"]))
        {
            try
            {
                connection.Open();
                var command2 = new SqlCommand("select * from writers where WriteFullName = @Writer", connection);
                command2.Parameters.AddWithValue("@Writer", Model.Writer);
                var reader2 = command2.ExecuteReader();
                reader2.Read();
                int a = reader2.GetInt32(0);
                connection.Close();

                connection.Open();
                var command3 = new SqlCommand("select * from Categories where CategoryName = @CategoryName", connection);
                command3.Parameters.AddWithValue("@CategoryName", Model.Category);
                var reader3 = command3.ExecuteReader();
                reader3.Read();
                int b = reader3.GetInt32(0);
                connection.Close();

                connection.Open();
                var command = new SqlCommand("UPDATE Texts SET Title = @Title, CategoryId = @CategoryId, WriterId = @WriterId, Number = @Number, Picture = @Picture, PublishedInOlaganSiir = @PublishedInOlaganSiir, [Text]= @Text, Referances= @Referances, Summary= @Summary, CreatedTime=@CreatedTime,UpdatedTime= @UpdatedTime WHERE Title = '"+Title+"'", connection);
                command.Parameters.AddWithValue("@Title", Model.Title);
                command.Parameters.AddWithValue("@CategoryId", a);
                command.Parameters.AddWithValue("@WriterId", b);
                command.Parameters.AddWithValue("@Number", Model.number);
                command.Parameters.AddWithValue("@Picture", Model.picture);
                command.Parameters.AddWithValue("@PublishedInOlaganSiir", Model.PublishedInOlaganSiir);
                command.Parameters.AddWithValue("@Text", Model.Text);
                command.Parameters.AddWithValue("@Referances", Model.Referances);
                command.Parameters.AddWithValue("@Summary", Model.Summary);
                command.Parameters.AddWithValue("@CreatedTime", DateTime.Now);
                command.Parameters.AddWithValue("@UpdatedTime", DateTime.Now);
                command.ExecuteNonQuery();

                return RedirectToAction("TextUpdated");
            }
            catch (Exception Exception)
            {
                ViewBag.hata = "İçerik Güncellenemedi";
                return View();
            }
        }
    }

}
