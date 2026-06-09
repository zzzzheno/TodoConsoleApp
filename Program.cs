using System.Text.Json;

string filePath = "todos.json";
List<TodoItem> tasks = LoadTasks(filePath);
// 程式啟動時讀取檔案

while (true)
// 永遠重複執行這行程式，直到遇到break
{
    Console.WriteLine("=== Todo List ===");
    Console.WriteLine("0. Exit");
    Console.WriteLine("1. Show All Tasks");
    Console.WriteLine("2. Add New Task");
    Console.WriteLine("3. Change Status");
    Console.WriteLine("4. Delete Task");
    Console.WriteLine("5. Edit Task");
    Console.WriteLine("6. Show Statistics");
    Console.WriteLine("7. Filter");
    Console.WriteLine("8. Clear All");
    Console.Write("Please Select An Option:");

    string input = Console.ReadLine() ?? "";
    switch (input)
    {
        case "0":
            Console.WriteLine("Successfully exited.");
            return;
        case "1":
            Console.WriteLine("=== Show All Tasks ===");
            if (!NoTask(tasks))
            {
                PrintAllTasks(tasks);
            }
            break;

        case "2":
            AddNewTask(tasks, filePath);
            break;

        case "3":
            ChangeTaskStatus(tasks, filePath);
            break;

        case "4":
            DeleteTask(tasks, filePath);
            break;

        case "5":
            EditTask(tasks, filePath);
            break;

        case "6":
            ShowStatistics(tasks);
            break;

        case "7":
            FilterTasks(tasks);
            break;

        case "8":
            ClearAllTasks(tasks, filePath);
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
    Console.Clear();
}

static void SaveTasks(List<TodoItem> tasks, string filePath)
// 方法的 () 裡面寫的東西叫參數(parameter)
// 方法的 () 裡面 = 這個方法需要外面給它的資料
// static 代表這個方法可以直接在目前的 Programs.cs 裡呼叫，不需要建立物件
// void 代表這個方法不會回傳值
{
    string json = JsonSerializer.Serialize(tasks);
    File.WriteAllText(filePath, json);
}
// 把 List<TodoItem> 轉成 JSON 字串
// 然後寫入 todos.json

static List<TodoItem> LoadTasks(string filePath)
{
    if (!File.Exists(filePath))
    {
        return new List<TodoItem>();
    }
    // 如果todos.json不存 => 回傳空清單

    string json = File.ReadAllText(filePath);

    if (string.IsNullOrWhiteSpace(json))
    {
        return new List<TodoItem>();
    }
    // 如果檔案是空的 => 回傳空清單

    return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
    // 如果有內容 => 把 JSON 轉回 List<TodoItem>
}

static void PrintAllTasks(List<TodoItem> tasks)
{
    for (int i = 0; i < tasks.Count; i++)
    // 設定 i = 0，只要 i < Tasks.Count 成立，就執行迴圈內容；每執行完一輪，i 就加 1。
    {
        string status = tasks[i].IsCompleted ? "Completed" : "Uncompleted";
        Console.WriteLine($"{i + 1}.[{status}] {tasks[i].TaskTitle}");
    }
}

static bool TryGetTaskIndex(List<TodoItem> tasks, string taskNum, out int index)
{
    if (int.TryParse(taskNum, out int taskNumber))
    // TryParse => 嘗試將字串轉成指定型態，成功回傳 true，失敗回傳 false。若轉換失敗不會讓程式當掉
    // [型態].TryParse(要轉換的字串, out 轉換成功後存放結果的變數)
    {
        index = taskNumber - 1;
        return index >= 0 && index < tasks.Count;
        // 檢查 index 是否有效，會回傳 true 或 false
    }

    index = -1;
    // 若轉換失敗不會進到 if ，所以給 -1 代表沒有有效的index
    return false;
    // 回傳 false
}

static void AddNewTask(List<TodoItem> tasks, string filePath)
{
    Console.WriteLine("=== Add New Task ===");
    Console.WriteLine("Enter task:");
    string task = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(task))
    {
        Console.WriteLine("Task cannot be empty.");
    }
    else
    {
        tasks.Add(new TodoItem
        {
            TaskTitle = task,
            IsCompleted = false,
        });
        SaveTasks(tasks, filePath);
        Console.WriteLine("Add task successfully.");
    }
}

static void ChangeTaskStatus(List<TodoItem> tasks, string filePath)
{
    Console.WriteLine("=== Change Status ===");

    if (NoTask(tasks))
    {
        return;
        // 離開目前的方法；若在主程式最外層會結束程式
    }
    PrintAllTasks(tasks);

    Console.WriteLine("Enter task number to change status:");
    string taskNum = Console.ReadLine() ?? "";
    if (TryGetTaskIndex(tasks, taskNum, out int index))
    {
        tasks[index].IsCompleted = !tasks[index].IsCompleted;
        // 把目前的bool值反過來
        SaveTasks(tasks, filePath);

        string status = tasks[index].IsCompleted ? "completed" : "uncompleted";
        Console.WriteLine($"{tasks[index].TaskTitle} is now {status}.");
    }
    else
    {
        Console.WriteLine("Task number doesn't exist or invalid insert.");
    }

}


static void DeleteTask(List<TodoItem> tasks, string filePath)
{
    Console.WriteLine("=== Delete Task ===");
    if (NoTask(tasks))
    {
        return;
    }
    PrintAllTasks(tasks);

    Console.WriteLine("Enter task number to delete:");
    string taskNum = Console.ReadLine() ?? "";
    if (TryGetTaskIndex(tasks, taskNum, out int index))
    {
        string removedTask = tasks[index].TaskTitle;
        tasks.RemoveAt(index);
        SaveTasks(tasks, filePath);
        Console.WriteLine($"Remove {removedTask} success!");
    }
    else
    {
        Console.WriteLine("Task number doesn't exist or invalid insert.");
    }
}

static void EditTask(List<TodoItem> tasks, string filePath)
{
    Console.WriteLine("=== Edit Task ===");
    if (NoTask(tasks))
    {
        return;
    }
    PrintAllTasks(tasks);

    Console.WriteLine("Enter task number to edit:");
    string taskNum = Console.ReadLine() ?? "";
    if (TryGetTaskIndex(tasks, taskNum, out int index))
    {
        Console.WriteLine("Enter new title:");
        string newTitle = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(newTitle))
        {
            Console.WriteLine("New Title can't be empty.");
        }
        else
        {
            tasks[index].TaskTitle = newTitle;
            SaveTasks(tasks, filePath);
            Console.WriteLine($"Edit successfully.");
        }
    }
    else
    {
        Console.WriteLine("Task number doesn't exist or invalid insert.");
    }
}

static void ShowStatistics(List<TodoItem> tasks)
{
    Console.WriteLine("=== Show Statistics ===");

    int totalCount = tasks.Count;
    int completedCount = 0;
    int uncompletedCount = 0;

    for (int i = 0; i < tasks.Count; i++)
    {
        if (tasks[i].IsCompleted)
        {
            completedCount++;
        }
        else
        {
            uncompletedCount++;
        }
    }

    if (totalCount == 0)
    {
        Console.WriteLine("No task.");
    }
    else
    {
        double completionRate = (double)completedCount / totalCount * 100;
        Console.WriteLine($"Total tasks: {totalCount}\nCompleted: {completedCount}\nUncompleted: {uncompletedCount}\nCompletion Rate: {completionRate:F2}%");
    }
}

static void ClearAllTasks(List<TodoItem> tasks, string filePath)
{
    Console.WriteLine("=== Clear All Tasks ===\nEnter y to clear all tasks:");
    string clearInput = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(clearInput))
    {
        Console.WriteLine("Invalid input.");
    }
    else
    {
        clearInput = clearInput.ToLower();
        if (clearInput == "y")
        {
            if (NoTask(tasks))
            {
                return;
            }
            tasks.Clear();
            SaveTasks(tasks, filePath);
            Console.WriteLine("Clear all tasks successfully!");
        }
        else
        {
            Console.WriteLine("Clear cancelled");
        }
    }
}

static void FilterTasks(List<TodoItem> tasks)
{
    if (NoTask(tasks))
    {
        return;
    }
    Console.WriteLine("=== Filter ===\nEnter filter type:\n0 = All\n1 = Completed\n2 = Uncompleted\n3 = Keyword\nSelect the option:");

    string filterInput = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(filterInput))
    {
        Console.WriteLine("Invalid option.");
    }
    else
    {
        switch (filterInput)
        {
            case "0":
                PrintAllTasks(tasks);
                break;

            case "1":
                {
                    bool hasResult = false;
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        if (tasks[i].IsCompleted)
                        {
                            Console.WriteLine($"{i + 1}.{tasks[i].TaskTitle}");
                            hasResult = true;
                        }
                    }
                    if (!hasResult)
                    {
                        Console.WriteLine("No completed task.");
                    }
                    break;
                }

            case "2":
                {
                    bool hasResult = false;
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        if (!tasks[i].IsCompleted)
                        {
                            Console.WriteLine($"{i + 1}.{tasks[i].TaskTitle}");
                            hasResult = true;
                        }
                    }
                    if (!hasResult)
                    {
                        Console.WriteLine("No uncompleted task.");
                    }
                    break;
                }

            case "3":
                {
                    Console.WriteLine("Enter keywords to find the task:");
                    string keywordInput = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(keywordInput))
                    {
                        Console.WriteLine("Keyword can't be empty.");
                    }
                    else
                    {
                        bool hasResult = false;
                        string keyword = keywordInput.ToLower();

                        for (int i = 0; i < tasks.Count; i++)
                        {
                            string taskTitle = tasks[i].TaskTitle.ToLower();

                            if (taskTitle.Contains(keyword))
                            {
                                string status = tasks[i].IsCompleted ? "Completed" : "Uncompleted";
                                Console.WriteLine($"{i + 1}.[{status}] {tasks[i].TaskTitle}");
                                hasResult = true;
                            }
                        }
                        if (!hasResult)
                        {
                            Console.WriteLine("No matching task.");
                        }
                    }
                    break;
                }

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
}

static bool NoTask(List<TodoItem> tasks)
{
    if (tasks.Count == 0)
    {
        Console.WriteLine("No task heres.");
        return true;
    }

    return false;
}

class TodoItem
{
    public string TaskTitle { get; set; } = "";
    public bool IsCompleted { get; set; }
}