List<string> tasks = new List<string>();
// 建立一個可以存放很多 string 的清單，名字叫Tasks

while (true)
// 永遠重複執行這行程式，直到遇到break
{
    Console.WriteLine("=== Todo List ===");
    Console.WriteLine("1. Show All Taks");
    Console.WriteLine("2. Add New Task");
    Console.WriteLine("3. Mark As Completed");
    Console.WriteLine("4. Delete Task");
    Console.WriteLine("5. Exit");
    Console.Write("Please Select An Option:");

    String input = Console.ReadLine();


    if (input == "1")
    {
        Console.WriteLine("=== Show All Tasks ===");
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}.{tasks[i]}");
        }
    }
    else if (input == "2")
    {
        Console.WriteLine("=== Add New Task ===");
        Console.WriteLine("Enter task:");
        String task = Console.ReadLine();
        if (string.IsNullOrEmpty(task))
        {
            Console.WriteLine("Task cannot be empty.");
        }
        else
        {
            tasks.Add(task);
            Console.WriteLine("Successfully add task.");
        }
    }
    else if (input == "3")
    {
        Console.WriteLine("=== Mark As Completed ===");
    }
    else if (input == "4")
    {
        Console.WriteLine("=== Delete Task ===");
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks can delete.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{tasks[i]}");
            }

            Console.WriteLine("Enter task number to delete:");
            String deleteInput = Console.ReadLine();

            if (int.TryParse(deleteInput, out int taskNumber))
            // TryParse => 嘗試將字串轉成指定型態，成功回傳 true，失敗回傳 false。若轉換失敗不會讓程式當掉
            // [型態].TryParse(要轉換的字串, out 轉換成功後存放結果的變數)
            {
                int index = taskNumber - 1;

                if (index >= 0 && index < tasks.Count)
                {
                    tasks.RemoveAt(index);
                    Console.WriteLine($"Remove {deleteInput} success!");
                }
                else
                {
                    Console.WriteLine("Task Number doesn't exit or invalid insert.");
                }
            }
        }
    }
    else if (input == "5")
    {
        Console.WriteLine("Successfully exited.");
        break;
    }
    else
    {
        Console.WriteLine("Invalid option.");
    }
}