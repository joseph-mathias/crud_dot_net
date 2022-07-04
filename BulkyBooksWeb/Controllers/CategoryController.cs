using BulkyBooksWeb.Data;
using BulkyBooksWeb.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
namespace BulkyBooksWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _db.Categories;
        // var objCategoryList = _db.Categories.ToList();
        return View(objCategoryList);
    }
    // GET
    public IActionResult Create()
    {
        IEnumerable<Category> objCategoryList = _db.Categories;
        // var objCategoryList = _db.Categories.ToList();
        return View();
    }
    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if(obj.Name == obj.DisplayOrder.ToString()){
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
        }
        if(ModelState.IsValid){
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    public IActionResult Edit(int? id)
    {
        if(id == null || id == 0){
            return NotFound();
        }
        var categoryFromDb = _db.Categories.Find(id);
        // var categoryFromDb = _db.Categories.FindOrDefault(id);
        // var categoryFromDb = _db.Categories.SinglrOrDefault(id);

        if(categoryFromDb == null){
            return NotFound();
        }
        return View(categoryFromDb);
    }
    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if(obj.Name == obj.DisplayOrder.ToString()){
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
        }
        if(ModelState.IsValid){
            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    public IActionResult Delete(int? id)
    {
        if(id == null || id == 0){
            return NotFound();
        }
        var categoryFromDb = _db.Categories.Find(id);
        // var categoryFromDb = _db.Categories.FindOrDefault(id);
        // var categoryFromDb = _db.Categories.SinglrOrDefault(id);

        if(categoryFromDb == null){
            return NotFound();
        }
        return View(categoryFromDb);
    }
    // POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj= _db.Categories.Find(id);
        if(obj == null){
            return NotFound();
        }
        _db.Categories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}