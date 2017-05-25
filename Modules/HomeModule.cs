using Nancy;
using ToDo.Objects;
using System.Collections.Generic;

namespace ToDoList
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/categories"] = _ => {
        List<Category> allCategories = Category.GetAll();
        return View["categories.cshtml", allCategories];
      };
      Get["/categories/new"] = _ => {
        return View["category_form.cshtml"];
      };
      Post["/categories"] = _ => {
        Category newCategory = new Category(Request.Form["category-name"]);
        List<Category> allCategories = Category.GetAll();
        return View["categories.cshtml", allCategories];
      };
      Get["/categories/{id}"]= parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category selectedCategory = Category.Find(parameters.id);
        List<Task> categoryTasks = selectedCategory.GetTasks();
        model.Add("category", selectedCategory);
        model.Add("tasks", categoryTasks);
        return View["category.cshtml", model];
      };
      Get["/categories/{id}/tasks/new"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category selectedCategory = Category.Find(parameters.id);
        List<Task> allTasks = selectedCategory.GetTasks();
        model.Add("category", selectedCategory);
        model.Add("tasks", allTasks);
        return View["category_tasks_form.cshtml", model];
      };
      Post["/tasks"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category selectedCategory = Category.Find(Request.Form["category-id"]);
        List<Task> categoryTasks = selectedCategory.GetTasks();
        string taskDescription = Request.Form["task-description"];
        Task newTask = new Task(taskDescription);
        categoryTasks.Add(newTask);
        model.Add("tasks", categoryTasks);
        model.Add("category", selectedCategory);
        return View["category.cshtml", model];
      };
      Post["/tasks/update"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category selectedCategory = Category.Find(Request.Form["category-id"]);
        int toDelete = Request.Form["task-id"];
        selectedCategory.DeleteTask(toDelete);
        List<Task> categoryTasks = selectedCategory.GetTasks();
        model.Add("tasks", categoryTasks);
        model.Add("category", selectedCategory);
        return View["category.cshtml", model];
      };
      // Get["/"] = _ => {
      //   return View["index.cshtml"];
      // };
      // Get["/tasks"] = _ => {
      //   List<Task> allTasks = Task.GetAll();
      //   return View["tasks.cshtml", allTasks];
      // };
      // Get["/tasks/new"] = _ => {
      //   return View["task_form.cshtml"];
      // };
      // Post["/tasks/confirm"] = _ => {
      //   Task newTask = new Task(Request.Form["new-task"]);
      //   return View["task_confirmation", newTask];
      // };
      // Get["/tasks"] = _ => {
      //   List<Task> allTasks = Task.GetAll();
      //   return View["tasks.cshtml", allTasks];
      // };
      // Get["/tasks/{id}"] = parameters => {
      //   Task task = Task.Find(parameters.id);
      //   return View["task.cshtml", task];
      // };
      // Post["/tasks/cleared"] = _ => {
      //   Task.ClearAll();
      //   return View["task_form.cshtml"];
      // };
    }
  }
}
