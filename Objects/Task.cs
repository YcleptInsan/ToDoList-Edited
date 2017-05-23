using System.Collections.Generic;

namespace ToDo.Objects
{
  public class Task
  {
    private string _description;
    private int _id;
    private static List<Task> _instances = new List<Task> {};

    public Task(string newDescription)
    {
      _description = newDescription;
      _id = _instances.Count;
      _instances.Add(this);
    }
    public string GetDescription()
    {
      return _description;
    }
    public int GetId()
    {
      return _id;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public static List<Task> GetAll()
    {
      return _instances;
    }
    public static void ClearAll()
    {
      _instances.Clear();
    }
    public static Task Find(int SearchId)
    {
      return _instances[SearchId];
    }
  }
}
