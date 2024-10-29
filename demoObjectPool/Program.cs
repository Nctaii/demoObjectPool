using System;
using System.Collections.Generic;

// Đối tượng có thể tái sử dụng
public class ReusableObject
{
    public int Id { get; }

    public ReusableObject(int id)
    {
        Id = id;
        Console.WriteLine($"Tao doi tuong co Id: {Id}");
    }

    public void DoSomething()
    {
        Console.WriteLine($"Doi tuong {Id} dang thuc hien cong viec");
    }
}

// Object Pool
public class SimpleObjectPool
{
    private List<ReusableObject> _availableObjects;
    private int _nextId = 1;

    public SimpleObjectPool(int size)
    {
        _availableObjects = new List<ReusableObject>();
        for (int i = 0; i < size; i++)
        {
            _availableObjects.Add(new ReusableObject(_nextId++));
        }
    }

    public ReusableObject AcquireObject()
    {
        if (_availableObjects.Count > 0)
        {
            ReusableObject obj = _availableObjects[0];
            _availableObjects.RemoveAt(0);
            Console.WriteLine($"Lay doi tuong {obj.Id} tu pool");
            return obj;
        }
        else
        {
            ReusableObject obj = new ReusableObject(_nextId++);
            Console.WriteLine($"Pool trong. Tao doi tuong moi co Id: {obj.Id}");
            return obj;
        }
    }

    public void ReleaseObject(ReusableObject obj)
    {
        Console.WriteLine($"Tra doi tuong {obj.Id} ve pool");
        _availableObjects.Add(obj);
    }
}

// Sử dụng Object Pool
class Program
{
    static void Main(string[] args)
    {
        SimpleObjectPool pool = new SimpleObjectPool(2);

        Console.WriteLine("Lay va su dung doi tuong 1:");
        ReusableObject obj1 = pool.AcquireObject();
        obj1.DoSomething();
        pool.ReleaseObject(obj1);

        Console.WriteLine("\nLay va su dung doi tuong 2:");
        ReusableObject obj2 = pool.AcquireObject();
        obj2.DoSomething();
        pool.ReleaseObject(obj2);

        Console.WriteLine("\nLay va su dung doi tuong 3:");
        ReusableObject obj3 = pool.AcquireObject();
        obj3.DoSomething();
        pool.ReleaseObject(obj3);

        Console.ReadLine();
    }
}