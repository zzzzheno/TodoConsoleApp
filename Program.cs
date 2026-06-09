using System.Text.Json;

string filePath = "todos.json";
List<TodoItem> newTasks = LoadTasks(filePath);
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


    if (input == "0")
    {
        Console.WriteLine("Successfully exited.");
        break;
    }

    else if (input == "1")
    {
        Console.WriteLine("=== Show All Tasks ===");

        if (newTasks.Count == 0)
        {
            Console.WriteLine("No task.");
        }
        else
        {
            for (int i = 0; i < newTasks.Count; i++)
            // 設定 i = 0，只要 i < newTasks.Count 成立，就執行迴圈內容；每執行完一輪，i 就加 1。
            {
                string status = newTasks[i].IsCompleted ? "Completed" : "Uncompleted";
                Console.WriteLine($"{i + 1}.[{status}] {newTasks[i].TaskTitle}");
            }
        }
    }

    else if (input == "2")
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
            newTasks.Add(new TodoItem
            {
                TaskTitle = task,
                IsCompleted = false,
            });
            SaveTasks(newTasks, filePath);
            Console.WriteLine("Add task successfully.");
        }
    }

    else if (input == "3")
    {
        Console.WriteLine("=== Change Status ===");

        if (newTasks.Count == 0)
        {
            Console.WriteLine("No task.");
        }
        else
        {
            for (int i = 0; i < newTasks.Count; i++)
            {
                string status = newTasks[i].IsCompleted ? "Completed" : "Uncompleted";
                Console.WriteLine($"{i + 1}.[{status}] {newTasks[i].TaskTitle}");
            }

            Console.WriteLine("Enter task number to change status:");
            string changeInput = Console.ReadLine() ?? "";
            if (int.TryParse(changeInput, out int taskNumber))
            {
                int index = taskNumber - 1;
                if (index >= 0 && index < newTasks.Count)
                {
                    newTasks[index].IsCompleted = !newTasks[index].IsCompleted;
                    // 把目前的bool值反過來
                    SaveTasks(newTasks, filePath);

                    string status = newTasks[index].IsCompleted ? "completed" : "uncompleted";
                    Console.WriteLine($"{newTasks[index].TaskTitle} is now {status}.");
                }
                else
                {
                    Console.WriteLine("Task number doesn't exist or invalid insert.");
                }
            }
            else
            {
                Console.WriteLine("Task number doesn't exist or invalid insert.");
            }
        }
    }

    else if (input == "4")
    {
        Console.WriteLine("=== Delete Task ===");
        if (newTasks.Count == 0)
        {
            Console.WriteLine("No task.");
        }
        else
        {
            for (int i = 0; i < newTasks.Count; i++)
            {
                string status = newTasks[i].IsCompleted ? "Completed" : "Uncompleted";
                Console.WriteLine($"{i + 1}.[{status}] {newTasks[i].TaskTitle}");
            }

            Console.WriteLine("Enter task number to delete:");
            string deleteInput = Console.ReadLine() ?? "";

            if (int.TryParse(deleteInput, out int taskNumber))
            // TryParse => 嘗試將字串轉成指定型態，成功回傳 true，失敗回傳 false。若轉換失敗不會讓程式當掉
            // [型態].TryParse(要轉換的字串, out 轉換成功後存放結果的變數)
            {
                int index = taskNumber - 1;

                if (index >= 0 && index < newTasks.Count)
                {
                    string removedTask = newTasks[index].TaskTitle;
                    newTasks.RemoveAt(index);
                    SaveTasks(newTasks, filePath);
                    Console.WriteLine($"Remove {removedTask} success!");
                }
                else
                {
                    Console.WriteLine("Task number doesn't exist or invalid insert.");
                }
            }
            else
            {
                Console.WriteLine("Task number doesn't exist or invalid insert.");
            }
        }
    }

    else if (input == "5")
    {
        Console.WriteLine("=== Edit Task ===");
        if (newTasks.Count == 0)
        {
            Console.WriteLine("No task.");
        }
        else
        {
            for (int i = 0; i < newTasks.Count; i++)
            {
                string status = newTasks[i].IsCompleted ? "Completed" : "Uncompleted";
                Console.WriteLine($"{i + 1}.[{status}] {newTasks[i].TaskTitle}");
            }

            Console.WriteLine("Enter task number to edit:");
            string editInput = Console.ReadLine() ?? "";

            if (int.TryParse(editInput, out int taskNumber))
            {
                int index = taskNumber - 1;

                if (index >= 0 && index < newTasks.Count)
                {
                    Console.WriteLine("Enter new title:");
                    string newTitle = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(newTitle))
                    {
                        Console.WriteLine("New Title can't be empty.");
                    }
                    else
                    {
                        newTasks[index].TaskTitle = newTitle;
                        SaveTasks(newTasks, filePath);
                        Console.WriteLine($"Edit successfully.");
                    }
                }
                else
                {
                    Console.WriteLine("Task number doesn't exist or invalid insert.");
                }
            }
            else
            {
                Console.WriteLine("Task number doesn't exist or invalid insert.");
            }
        }
    }

    else if (input == "6")
    {
        Console.WriteLine("=== Show Statistics ===");

        int totalCount = newTasks.Count;
        int completedCount = 0;
        int uncompletedCount = 0;

        for (int i = 0; i < newTasks.Count; i++)
        {
            if (newTasks[i].IsCompleted)
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
            Console.WriteLine($"Total Tasks: {totalCount}\nCompleted: {completedCount}\nUncompleted: {uncompletedCount}\nCompletion Rate: {completionRate:F2}%");
        }
    }

    else if (input == "7")
    {
        Console.WriteLine("=== Filter ===\nEnter filter type:\n0 = All\n1 = Completed\n2 = Uncompleted\n3 = Keyword\nSelect the option:");
        string filterInput = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(filterInput))
        {
            Console.WriteLine("Invalid option.");
        }
        else
        {
            if (filterInput == "0")
            {
                if (newTasks.Count == 0)
                {
                    Console.WriteLine("No task.");
                }
                else
                {
                    for (int i = 0; i < newTasks.Count; i++)
                    {
                        string status = newTasks[i].IsCompleted ? "Completed" : "Uncompleted";
                        Console.WriteLine($"{i + 1}.[{status}] {newTasks[i].TaskTitle}");
                    }

                }
            }
            else if (filterInput == "1")
            {
                bool hasResult = false;
                for (int i = 0; i < newTasks.Count; i++)
                {

                    if (newTasks[i].IsCompleted)
                    {
                        Console.WriteLine($"{i + 1}.{newTasks[i].TaskTitle}");
                        hasResult = true;
                    }
                }

                if (!hasResult)
                {
                    Console.WriteLine("No completed task.");
                }
            }
            else if (filterInput == "2")
            {
                bool hasResult = false;
                for (int i = 0; i < newTasks.Count; i++)
                {
                    if (!newTasks[i].IsCompleted)
                    {
                        Console.WriteLine($"{i + 1}.{newTasks[i].TaskTitle}");
                        hasResult = true;
                    }
                }

                if (!hasResult)
                {
                    Console.WriteLine("No uncompleted task.");
                }
            }
            else if (filterInput == "3")
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

                    for (int i = 0; i < newTasks.Count; i++)
                    {
                        string taskTitle = newTasks[i].TaskTitle.ToLower();

                        if (taskTitle.Contains(keyword))
                        {
                            string status = newTasks[i].IsCompleted ? "Completed" : "Uncompleted";
                            Console.WriteLine($"{i + 1}.[{status}] {newTasks[i].TaskTitle}");
                            hasResult = true;
                        }
                    }

                    if (!hasResult)
                    {
                        Console.WriteLine("No matching task.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
    }

    else if (input == "8")
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
                if (newTasks.Count == 0)
                {
                    Console.WriteLine("No task can clear.");
                }
                else
                {

                    newTasks.Clear();
                    SaveTasks(newTasks, filePath);
                    Console.WriteLine("Clear all tasks successfully!");
                }
            }
            else
            {
                Console.WriteLine("Clear cancelled");
            }
        }
    }

    else
    {
        Console.WriteLine("Invalid option.");
    }

    Console.WriteLine();
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
    Console.Clear();
}

static void SaveTasks(List<TodoItem> tasks, string filePath)
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

class TodoItem
{
    public string TaskTitle { get; set; } = "";
    public bool IsCompleted { get; set; }
}