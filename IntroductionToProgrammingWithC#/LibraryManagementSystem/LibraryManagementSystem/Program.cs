using System;

namespace LibraryManagementSystem
{
    class Program
    {
        /// <summary>
        /// List of available books in the library.
        /// </summary>
        static List<string> availableBooks = new List<string>();
        /// <summary>
        /// List of books that are currently checked out.
        /// </summary>
        static List<string> checkedOutBooks = new List<string>();
        /// <summary>
        /// Dictionary mapping borrowers to the list of books they have borrowed.
        /// </summary>
        static Dictionary<string, List<string>> borrowerBookBorrowedMap = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Library Management System");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Remove Book");
                Console.WriteLine("3. Display Books");
                Console.WriteLine("4. Search Book");
                Console.WriteLine("5. Borrow Book");
                Console.WriteLine("6. Return Book");
                Console.WriteLine("7. Exit");
                string choice = GetValidInput("Choose an option: ");

                switch (choice)
                {
                    // Adds book to available inventory if it doesn't already exist
                    case "1":
                        string bookToAdd = GetValidInput("Enter book title: ");

                        if (IsBookCheckedOut(bookToAdd))
                        {
                            Console.WriteLine("Book already exist and is checked out.");
                            break;
                        }

                        if (IsBookAvailable(bookToAdd))
                        {
                            Console.WriteLine("Book already exists in the available books list.");
                            break;
                        }

                        availableBooks.Add(bookToAdd);
                        Console.WriteLine("Book added to available books.");
                        break;

                    // Removes book from available inventory if it exist
                    case "2":
                        string bookToRemove = GetValidInput("Enter book title to remove: ");

                        if (IsBookCheckedOut(bookToRemove))
                        {
                            Console.WriteLine("Book is checked out.");
                            break;
                        }

                        if (availableBooks.Remove(bookToRemove))
                        {
                            Console.WriteLine("Book removed from available books.");
                        }
                        else
                        {
                            Console.WriteLine("Book not found in the available books list.");
                        }
                        break;

                    // Displays all existing books with current status and borrower information
                    case "3":
                        Console.WriteLine("Book Inventory:");
                        foreach (string book in availableBooks)
                        {
                            Console.WriteLine($"{book} | Available");
                        }
                        foreach (KeyValuePair<string, List<string>> entry in borrowerBookBorrowedMap)
                        {
                            string borrowerName = entry.Key;
                            List<string> checkedOutBooks = entry.Value;
                            if (checkedOutBooks.Count == 0)
                            {
                                continue;
                            }
                            foreach (string book in checkedOutBooks)
                            {
                                Console.WriteLine($"{book} | Checked Out | {borrowerName}");
                            }
                        }
                        break;

                    // Searches for a book with given title if it exist
                    case "4":
                        string bookToSearch = GetValidInput("Enter book title to search: ");
                        if (IsBookAvailable(bookToSearch))
                        {
                            Console.WriteLine("Book is available.");
                            break;
                        }

                        if (IsBookCheckedOut(bookToSearch))
                        {
                            Console.WriteLine("Book is checked out.");
                        }
                        else
                        {
                            Console.WriteLine("Book doesn't exist.");
                        }
                        break;

                    // Sets book as borrowed to a particular user if the book exist
                    case "5":
                        string bookToBorrow = GetValidInput("Enter book title to borrow: ");

                        if (IsBookCheckedOut(bookToBorrow))
                        {
                            Console.WriteLine("Book is not available.");
                            break;
                        }

                        if (IsBookAvailable(bookToBorrow))
                        {
                            string borrowerName = GetValidInput("Enter borrower's name: ");
                            if (borrowerBookBorrowedMap.TryGetValue(borrowerName, out List<string>? borrowedBooks) && borrowedBooks != null)
                            {
                                if (borrowedBooks.Count >= 3)
                                {
                                    Console.WriteLine("Borrow limit reached.");
                                    break;
                                }
                                else
                                {
                                    borrowerBookBorrowedMap[borrowerName].Add(bookToBorrow);
                                }
                            }
                            else
                            {
                                borrowerBookBorrowedMap.Add(borrowerName, new List<string> { bookToBorrow });
                            }
                            availableBooks.Remove(bookToBorrow);
                            checkedOutBooks.Add(bookToBorrow);
                        }
                        else
                        {
                            Console.WriteLine("Book doesn't exist.");
                        }
                        break;

                    // Sets book as returned by borrower and updates borrower information
                    case "6":
                        string bookToReturn = GetValidInput("Enter book title to return: ");

                        if (IsBookAvailable(bookToReturn))
                        {
                            Console.WriteLine("Book is not checked out.");
                            break;
                        }

                        if (IsBookCheckedOut(bookToReturn))
                        {
                            string borrowerName = GetValidInput("Enter borrower's name: ");
                            if (borrowerBookBorrowedMap.TryGetValue(borrowerName, out List<string>? borrowedBooks) && borrowedBooks != null)
                            {
                                if (borrowerBookBorrowedMap[borrowerName].Remove(bookToReturn))
                                {
                                    checkedOutBooks.Remove(bookToReturn);
                                    availableBooks.Add(bookToReturn);
                                    Console.WriteLine($"Book checked out to {borrowerName}.");
                                }
                                else
                                {
                                    Console.WriteLine($"Book is not checked out under {borrowerName}.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Book doesn't exist.");
                        }
                        break;

                    case "7":
                        Environment.Exit(0);
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

            /// <summary>
            /// Prompts the user with the specified message and retrieves a valid input.
            /// </summary>
            /// <param name="prompt">The message to display to the user.</param>
            /// <returns>A non-empty string input from the user.</returns>
            static string GetValidInput(string prompt)
            {
                string input;
                do
                {
                    Console.Write(prompt);
                    input = Console.ReadLine() ?? string.Empty;
                } while (string.IsNullOrEmpty(input));
                return input;
            }

            static bool IsBookAvailable(string bookTitle)
            {
                return SearchBook(bookTitle, availableBooks);
            }

            static bool IsBookCheckedOut(string bookTitle)
            {
                return SearchBook(bookTitle, checkedOutBooks);
            }

            /// <summary>
            /// Searches for a book with the given title in the specified book list.
            /// </summary>
            /// <param name="bookToSearch">The title of the book to search for.</param>
            /// <param name="bookList">The list of books to search within.</param>
            /// <returns>
            /// True if the book is found in the list; otherwise, false.
            /// </returns>
            static bool SearchBook(string bookToSearch, List<string> bookList)
            {
                if (bookList.Count == 0 || string.IsNullOrEmpty(bookToSearch))
                {
                    return false;
                }

                foreach (string bookTitle in bookList)
                {
                    if (bookTitle.Equals(bookToSearch, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}