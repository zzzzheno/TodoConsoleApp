using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

List<string> tasks = new List<string>();
// 建立一個可以存放很多 string 的清單，名字叫Tasks
List<bool> completed = new List<bool>();

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
    Console.Write("Please Select An Option:");

    string input = Console.ReadLine() ?? "";


    if (input == "0")
    {
        Console.WriteLine("Successfully existed.");
        break;
    }

    else if (input == "1")
    {
        Console.WriteLine("=== Show All Tasks ===");

        if (tasks.Count == 0)
        {
            Console.WriteLine("No task.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            // 設定 i = 0，只要 i < tasks.Count 成立，就執行迴圈內容；每執行完一輪，i 就加 1。
            {
                string status = completed[i] ? "Completed" : "Uncompleted";
                Console.WriteLine($"{i + 1}.[{status}] {tasks[i]}");
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
            tasks.Add(task);
            completed.Add(false);
            Console.WriteLine("Add task successfully.");
        }
    }

    else if (input == "3")
    {
        Console.WriteLine("=== Change Status ===");

        if (tasks.Count == 0)
        {
            Console.WriteLine("No task.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                string status = completed[i] ? "Completed" : "Uncompleted";
                Console.WriteLine($"{i + 1}.[{status}] {tasks[i]}");
            }

            Console.WriteLine("Enter task number to change status:");
            string changeInput = Console.ReadLine() ?? "";
            if (int.TryParse(changeInput, out int taskNumber))
            {
                int index = taskNumber - 1;
                if (index >= 0 && index < tasks.Count)
                {
                    // if (completed[index] == true)
                    // {
                    //     completed[index] = false;
                    //     Console.WriteLine($"Set {completed[index]} uncompleted.");
                    // }
                    // else
                    // {
                    //     completed[index] = true;
                    //     Console.WriteLine($"Set {completed[index]} completed.");
                    // }
                    completed[index] = !completed[index];
                    // 把目前的bool值反過來

                    string status = completed[index] ? "completed" : "uncompleted";
                    Console.WriteLine($"{tasks[index]} is now {status}.");
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
        if (tasks.Count == 0)
        {
            Console.WriteLine("No task.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                string status = completed[i] ? "Completed" : "Uncompleted";
                Console.WriteLine($"{i + 1}.[{status}] {tasks[i]}");
            }

            Console.WriteLine("Enter task number to delete:");
            string deleteInput = Console.ReadLine() ?? "";

            if (int.TryParse(deleteInput, out int taskNumber))
            // TryParse => 嘗試將字串轉成指定型態，成功回傳 true，失敗回傳 false。若轉換失敗不會讓程式當掉
            // [型態].TryParse(要轉換的字串, out 轉換成功後存放結果的變數)
            {
                int index = taskNumber - 1;

                if (index >= 0 && index < tasks.Count)
                {
                    string removedTask = tasks[index];
                    tasks.RemoveAt(index);
                    completed.RemoveAt(index);
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
        if (tasks.Count == 0)
        {
            Console.WriteLine("No task.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                string status = completed[i] ? "Completed" : "Uncompleted";
                Console.WriteLine($"{i + 1}.[{status}] {tasks[i]}");
            }

            Console.WriteLine("Enter task number to edit:");
            string editInput = Console.ReadLine() ?? "";

            if (int.TryParse(editInput, out int taskNumber))
            {
                int index = taskNumber - 1;

                if (index >= 0 && index < tasks.Count)
                {
                    Console.WriteLine("Enter new title:");
                    string newTitle = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(newTitle))
                    {
                        Console.WriteLine("New Title can't be empty.");
                    }
                    else
                    {
                        string editTask = tasks[index];
                        tasks[index] = newTitle;
                        Console.WriteLine($"Edit sussessfully.");
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

        int totalCount = tasks.Count;
        int completedCount = 0;
        int uncompletedCount;
        int completionRate;

        for (int i = 0; i < completed.Count; i++)
        {
            if (completed[i])
            {
                completedCount++;
            }
        }

        Console.WriteLine($"Total Tasks:{totalCount} \t Completed: \t Uncompleted: \t Completion Rate: %");
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