using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string _filePath;

    public UserFileRepository()
    {
        var dataDir = Path.Combine(AppContext.BaseDirectory, "data");
        if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);
        _filePath = Path.Combine(dataDir, "users.json");
        if (!File.Exists(_filePath)) File.WriteAllText(_filePath, "[]");
    }

    public void Add(User user)
    {
        var list = FileHelper.LoadList<User>(_filePath);
        user.Id = list.Any() ? list.Max(u => u.Id) + 1 : 1;
        list.Add(user);
        FileHelper.SaveList(_filePath, list);
    }

    public void Update(User user)
    {
        var list = FileHelper.LoadList<User>(_filePath);
        var ex = list.FirstOrDefault(u => u.Id == user.Id);
        if (ex != null)
        {
            list.Remove(ex);
            list.Add(user);
            FileHelper.SaveList(_filePath, list);
        }
    }

    public void Delete(int id)
    {
        var list = FileHelper.LoadList<User>(_filePath);
        list.RemoveAll(u => u.Id == id);
        FileHelper.SaveList(_filePath, list);
    }

    public User? GetById(int id)
    {
        var list = FileHelper.LoadList<User>(_filePath);
        return list.FirstOrDefault(u => u.Id == id);
    }

    public List<User> GetAll()
    {
        return FileHelper.LoadList<User>(_filePath);
    }
}
