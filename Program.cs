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

    string input = UserInput();
    switch (input)
    {
        case "0":
            Console.WriteLine("Successfully exited.");
            return;
        case "1":
            Console.WriteLine("=== Show All Tasks ===");
            if (!HasNoTasks(tasks))
            {
                PrintAllTasks(tasks);
            }
            else
            {
                Console.WriteLine("No task here.");
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
        PrintTask(tasks[i], i);
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
    string task = UserInput();
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
    if (HasNoTasks(tasks))
    {
        Console.WriteLine("No task here.");
        return;
        // 離開目前的方法；若在主程式最外層會結束程式
    }
    PrintAllTasks(tasks);

    Console.WriteLine("Enter task number to change status:");
    string taskNum = UserInput();
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
    if (HasNoTasks(tasks))
    {
        Console.WriteLine("No task here.");
        return;
    }
    PrintAllTasks(tasks);

    Console.WriteLine("Enter task number to delete:");
    string taskNum = UserInput();
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
    if (HasNoTasks(tasks))
    {
        Console.WriteLine("No task here.");
        return;
    }
    PrintAllTasks(tasks);

    Console.WriteLine("Enter task number to edit:");
    string taskNum = UserInput();
    if (TryGetTaskIndex(tasks, taskNum, out int index))
    {
        Console.WriteLine("Enter new title:");
        string newTitle = UserInput();

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
    string clearInput = UserInput();
    if (string.IsNullOrWhiteSpace(clearInput))
    {
        Console.WriteLine("Invalid input.");
    }
    else
    {
        clearInput = clearInput.ToLower();
        if (clearInput == "y")
        {
            if (HasNoTasks(tasks))
            {
                Console.WriteLine("No task here.");
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
    Console.WriteLine("=== Filter The Task ===");
    if (HasNoTasks(tasks))
    {
        Console.WriteLine("No task here.");
        return;
    }

    Console.WriteLine("Enter filter type:\n0 = All\n1 = Completed\n2 = Uncompleted\n3 = Keyword\nSelect the option:");
    string filterInput = UserInput();
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
                PrintTaskByStatus(true, "No completed task.");
                break;

            case "2":
                PrintTaskByStatus(false, "No uncompleted task.");
                break;

            case "3":
                PrintTasksByKeywords();
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }

    void PrintTaskByStatus(bool targetStatus, string noResultMessage)
    // 方法寫在 FilterTasks 裡面，不用再傳 List<TodoItem> tasks，因為 local function 可以直接使用外層 FilterTasks 的 tasks
    {
        bool hasResult = false;
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].IsCompleted == targetStatus)
            {
                PrintTask(tasks[i], i);
                hasResult = true;
            }
        }
        if (!hasResult)
        {
            Console.WriteLine(noResultMessage);
        }
    }

    void PrintTasksByKeywords()
    {
        Console.WriteLine("Enter keywords to find the task:");
        string keywordInput = UserInput();

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
                    PrintTask(tasks[i], i);
                    hasResult = true;
                }
            }
            if (!hasResult)
            {
                Console.WriteLine("No matching task.");
            }
        }

    }
}

static bool HasNoTasks(List<TodoItem> tasks)
{
    // if (tasks.Count == 0)
    // {
    //     return true;
    // }

    // return false;

    return tasks.Count == 0;
}

static void PrintTask(TodoItem task, int index)
{

    string status = task.IsCompleted ? "Completed" : "Uncompleted";
    Console.WriteLine($"{index + 1}.[{status}] {task.TaskTitle}");
}

static string UserInput()
{
    return Console.ReadLine() ?? "";
}

class TodoItem
{
    public string TaskTitle { get; set; } = "";
    public bool IsCompleted { get; set; }
}