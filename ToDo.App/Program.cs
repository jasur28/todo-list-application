﻿using System;
using System.Linq;
using System.Collections.Generic;
using ToDo.BLL.Operations;
using ToDo.BLL.Entity;

namespace ToDo.App
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("\tWelcome to ToDoList Application!");

            Menu();
        }

        static void DisplayList(TODOList item)
        {
            Console.WriteLine("List:\n\t ID: {0}\tName: {1}\tVisible: {2}", item.id, item.name, item.isVisible);
        }

        static void CreateList()
        {
            string choice = "y";
            string listName;
            while (choice != "n")
            {
                Console.WriteLine("Enter a list name:");
                listName = Console.ReadLine();
                TODOList newList = new ToDoListOperations().Create(listName);
                DisplayList(newList);
                Console.Write("Do you want enter more lists (y/n)");
                choice = Console.ReadLine();
            }
        }

        static void RemoveList()
        {
            string choice = "y";
            int listId;
            while (choice != "n")
            {
                Console.WriteLine("Enter ID of a list:");
                listId = Convert.ToInt32(Console.ReadLine());
                var removeList = new ToDoListOperations().Remove(listId);
                if (removeList > 0)
                {
                    Console.WriteLine("List with ID: {0} successfully removed ", listId);
                }
                else
                {
                    Console.WriteLine("Sorry, list with ID: {0} unsuccessfully removed ", listId);
                }

                Console.Write("Do you want to remove more lists (y/n)");
                choice = Console.ReadLine();
            }
        }

        static void UpdateList()
        {
            Console.WriteLine("Enter a list ID to modify");
            int listId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter a new name for the list");
            string modifyName = Console.ReadLine();
            Console.WriteLine("Do you want to hide a list from the list view(y/n)");
            string resultHide = Console.ReadLine();
            bool isVisible;

            if (resultHide == "y")
            {
                isVisible = false;
            }
            else
            {
                isVisible = true;
            }

            TODOList modifyList = new TODOList()
            {
                id = listId,
                name = modifyName,
                isVisible = isVisible
            };

            modifyList = new ToDoListOperations().Update(modifyList);
            DisplayList(modifyList);
        }

        static void GetAllListData()
        {
            Console.WriteLine();
            List<TODOList> lists = new ToDoListOperations().GetAll();

            foreach (var list in lists)
            {
                Console.WriteLine("List:\n\t ID: {0}\tName: {1}\tVisible: {2}", list.id, list.name, list.isVisible);
            }
        }

        static void GetList()
        {
            Console.WriteLine("Enter ID of a list:");
            int listId = int.Parse(Console.ReadLine());

            TODOList list = new ToDoListOperations().Get(listId);
            DisplayList(list);
        }

        static void CreateEntry()
        {
            Console.WriteLine("Enter an list ID");
            int listId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter title for entry:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter description for entry :");
            string description = Console.ReadLine();

            Console.WriteLine("Enter due date for entry(example “MM/dd/yyyy”):");
            string pattern = "MM/dd/yyyy";
            DateTime date = DateTime.ParseExact(Console.ReadLine(), pattern, null);

            Console.WriteLine("Does the task is finished(y/n):");
            string statusChar = Console.ReadLine();
            bool statusString;
            if (statusChar == "y")
            {
                statusString = true;
            }
            else
            {
                statusString = false;
            }

            TODOEntry newentity = new TODOEntry()
            {
                title = title,
                description = description,
                dueDate = date,
                isDone = statusString,
                listid = listId
            };

            var result = new ToDoEntryOperations().Create(newentity);
            Console.WriteLine("You added entry:\n\t ID: {0} Title: {1} Description: {2} Due date: {3} Completed: {4}",
                result.id, result.title,
                result.description, result.dueDate, result.isDone);
        }

        /// <summary>
        /// Remove Entry
        /// </summary>
        static void RemoveEntry()
        {
            string choice = "y";
            int entryId;
            while (choice != "n")
            {
                Console.WriteLine();
                Console.WriteLine("Enter ID of a entry:");
                entryId = Convert.ToInt32(Console.ReadLine());
                var removeList = new ToDoEntryOperations().Remove(entryId);
                if (removeList > 0)
                {
                    Console.WriteLine("List with ID: {0} successfully removed ", entryId);
                }
                else
                {
                    Console.WriteLine("Sorry, list with ID: {0} unsuccessfully removed ", entryId);
                }

                Console.Write("Do you want to remove more entries (y/n)");
                choice = Console.ReadLine();
                Console.WriteLine();
            }
        }

        static void GetAllEntryData()
        {
            Console.WriteLine();
            List<TODOEntry> entries = new ToDoEntryOperations().GetAll();

            foreach (var entry in entries)
            {
                Console.WriteLine("Entry is:\n\t ID: {0} Title: {1} Description: {2} Due date: {3} Completed: {4}",
                entry.id, entry.title, entry.description, entry.dueDate, entry.isDone);
            }
        }

        static void GetEntry()
        {
            Console.WriteLine("Enter ID of a list:");
            int entryId = Convert.ToInt32(Console.ReadLine());

            TODOEntry entry = new ToDoEntryOperations().Get(entryId);
            Console.WriteLine("Entry is:\n\t ID: {0} Title: {1} Description: {2} Due date: {3} Completed: {4}",
                entry.id, entry.title, entry.description, entry.dueDate, entry.isDone);
        }

        /// <summary>
        /// Update Entry
        /// </summary>
        static void UpdateEntry()
        {
            Console.WriteLine("Please enter id of entry to modify: ");
            int entryId = Convert.ToInt32(Console.ReadLine());

            TODOEntry entry = new ToDoEntryOperations().Get(entryId);

            Console.WriteLine("1-Title");
            Console.WriteLine("2-Description");
            Console.WriteLine("3-Due date");
            Console.WriteLine("4-Completed");
            Console.Write("What you want to modify choose 1-4:");
            int x = Convert.ToInt32(Console.ReadLine());

            switch (x)
            {
                case 1:
                    Console.WriteLine("Enter a new name for the title");
                    string titleName = Console.ReadLine();
                    entry.title = titleName;
                    break;
                case 2:
                    Console.WriteLine("Enter a new name for the description");
                    string descriptionName = Console.ReadLine();
                    entry.description = descriptionName;
                    break;
                case 3:
                    Console.WriteLine("Enter due date for entry(example “MM/dd/yyyy”):");
                    string pattern = "MM/dd/yyyy";
                    DateTime date = DateTime.ParseExact(Console.ReadLine(), pattern, null);
                    entry.dueDate = date;
                    break;
                case 4:
                    Console.WriteLine("Does the task is finished(true/false):");
                    bool status = Convert.ToBoolean(Console.ReadLine());
                    entry.isDone = status;
                    break;
            }

            var result = new ToDoEntryOperations().Update(entry);

            Console.WriteLine("Entry is:\n\t ID: {0} Title: {1} Description: {2} Due date: {3} Completed: {4}",
                result.id, result.title, result.description, result.dueDate, result.isDone);
        }

        static void Menu()
        {
            Console.WriteLine("Menu Driven Program for ToDo List");
            while (true)
            {
                Console.WriteLine("  1. Create a list");
                Console.WriteLine("  2. Get all list data");
                Console.WriteLine("  3. Get one list");
                Console.WriteLine("  4. Modify a ist");
                Console.WriteLine("  5. Remove a list");
                Console.WriteLine("  6. Create an entry");
                Console.WriteLine("  7. Remove an entry");
                Console.WriteLine("  8. Get one entry");
                Console.WriteLine("  9. Get all entry data");
                Console.WriteLine("  10. Set status of an entry");
                Console.WriteLine("  11. Modify an entry");
                Console.Write("Choose one (q to quit): ");

                string choice = Console.ReadLine();
                if (choice == "q")
                    break;
                switch (choice)
                {
                    case "1":
                        Console.WriteLine();
                        CreateList();
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine();
                        GetAllListData();
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine();
                        GetList();
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine();
                        UpdateList();
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.WriteLine();
                        RemoveList();
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.WriteLine();
                        CreateEntry();
                        Console.WriteLine();
                        break;
                    case "7":
                        Console.WriteLine();
                        RemoveEntry();
                        Console.WriteLine();
                        break;
                    case "8":
                        Console.WriteLine();
                        GetEntry();
                        Console.WriteLine();
                        break;
                    case "9":
                        Console.WriteLine();
                        GetAllEntryData();
                        Console.WriteLine();
                        break;
                    case "10":
                        Console.WriteLine();
                        SetStatusEntry();
                        Console.WriteLine();
                        break;
                    case "11":
                        Console.WriteLine();
                        UpdateEntry();
                        Console.WriteLine();
                        break;
                }
            }
        }

    }
}
