using static system_code.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace interface_code
{
    public class Interface
    {
        public static List<User>? userList;
        public static string? currentState;
        public static User? currentUser;

        public Interface(List<User> users)
        {
            userList = users;
        }

        /* private void abortCheck()
        {
            if (pressed.Key == ConsoleKey.Escape)
            {
                switch (currentState)
                {
                    case "login":
                        Menu();
                        break;
                    case "create":
                        Menu();
                        break;
                    case "changename":
                        Profile(currentUser);
                        break;
                    case "changepassword":
                        Profile(currentUser);
                        break;
                    default:
                        break;
                }
            }
        } */

        internal void Menu()
        {
            currentState = "menu";
            string[] actions = { "L", "C", "P", "Q" };
            Console.WriteLine(" ~~~~~~ Menu ~~~~~~~");
            Console.WriteLine("* Login - L");
            Console.WriteLine("* Create - C");
            Console.WriteLine("* List Profiles - P");
            Console.WriteLine("* Quit - Q");
            Console.WriteLine(" ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
            string awnser = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            while (!actions.Contains(awnser))
            {
                awnser = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            }
            switch (awnser)
            {
                case "L":
                    Console.Clear();
                    Login();
                    break;
                case "C":
                    Console.Clear();
                    Create();
                    break;
                case "P":
                    Console.Clear();
                    View();
                    break;
                case "Q":
                    Console.Write("You sure you want to quit? (Y/N)");
                    string quitConfirm = Console.ReadKey(true).KeyChar.ToString().ToUpper();
                    if (quitConfirm == "Y")
                    {
                        Quit();
                    }
                    else
                    {
                        Console.Clear();
                        Menu();
                    }
                    break;
            }
        }

        internal void Login()
        {
            currentState = "login";
            Console.WriteLine(" ~~~~~ Login ~~~~~ ");
            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();
            var usr = userList.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (usr != null)
            {
                Console.WriteLine(" ~ ~ ~ ~ ~ ~ ~ ~ ~ ");
                Console.Write("Loging in");
                Delay(200, 5);
                Console.Clear();
                Profile(usr);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect Cridentials");
                Login();
            }
        }

        internal void Profile(User user)
        {
            currentState = "profile";
            currentUser = user;
            string[] actions = { "N", "P", "L", "D", "Q" };
            Console.Clear();
            Console.Write(" ~~~~~~~~~~~  Welcome  ~~~~~~~~~~~ ");
            if (user.Firstname != null && user.Firstname != "")
            {
                Console.Write("\n       " + user.Firstname + " ");
            }
            if (user.Lastname != null && user.Lastname != "")
            {
                Console.Write(user.Lastname);
            }
            Console.WriteLine("\n* Add/Change first & lastname - N");
            Console.WriteLine("* Change Password - P");
            Console.WriteLine("* Logout - L");
            Console.WriteLine("* Delete Profile - D");
            Console.WriteLine("* Quit - Q");
            Console.WriteLine(" ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ");
            string awnser = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            while (!actions.Contains(awnser))
            {
                awnser = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            }
            switch (awnser)
            {
                case "N":
                    Console.Clear();
                    ChangeName(user);
                    break;
                case "P":
                    Console.WriteLine("Input Current Password to continue:");
                    string password = Console.ReadLine();
                    if (password == user.Password)
                    {
                        Console.Clear();
                        ChangePassword(user);
                    }
                    else
                    {
                        Profile(user);
                    }
                    break;
                case "L":
                    Console.Write("Loging out");
                    Delay(200, 5);
                    Console.Clear();
                    currentUser = null;
                    Menu();
                    break;
                case "D":
                    Delete(user);
                    break;
                case "Q":
                    Quit();
                    break;
            }
        }

        internal void Create()
        {
            currentState = "create";
            Console.WriteLine("   ~~~ Profile Creation ~~~ ");
            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();
            if (username != "" && password.Length >= 6)
            {
                var newUser = AddUser(username, password);
                if (newUser != null)
                {
                    Console.WriteLine(" ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
                    Console.Write("Creating Profile");
                    Delay(700, 3);
                    Profile(newUser);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid Password or Username");
                Create();
            }
        }

        internal void ChangeName(User user)
        {
            currentState = "changename";
            Console.WriteLine(" ~~~~~~~ Change Name ~~~~~~~ ");
            Console.WriteLine("Firstname: ");
            string newFirst = Console.ReadLine();
            Console.WriteLine("Lastname: ");
            string newLast = Console.ReadLine();
            Console.Write("Confirm Name Change (Y/N)");
            string Confirm = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            if (Confirm == "Y")
            {
                Console.WriteLine("\n ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
                Console.Write("Changing Name");
                Delay(250, 4);
                editName(user, newFirst, newLast);
            }
            else
            {
                Console.WriteLine("\nTry Again? (Y/N)");
                string tryAgain = Console.ReadKey(true).KeyChar.ToString().ToUpper();
                if (tryAgain == "Y")
                {
                    Console.Clear();
                    ChangeName(user);
                }
                else
                {
                    Profile(user);
                }
            }
        }

        internal void ChangePassword(User user)
        {
            currentState = "changepassword";
            Console.WriteLine("   ~~~ Change Password ~~~ ");
            Console.WriteLine("Password: ");
            string newPassword = Console.ReadLine();
            Console.WriteLine("Confirm Pass: ");
            string confirmation = Console.ReadLine();
            if (newPassword == confirmation && newPassword.Length >= 6)
            {
                Console.Write("Confirm Password (Y/N)");
                string Confirm = Console.ReadKey(true).KeyChar.ToString().ToUpper();
                if (Confirm == "Y")
                {
                    Console.WriteLine(" ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
                    Console.Write("Changing Password");
                    Delay(250, 4);
                    editPassword(user, newPassword);
                }
                else
                {
                    Console.WriteLine("\nTry Again? (Y/N)");
                    string tryAgain = Console.ReadKey(true).KeyChar.ToString().ToUpper();
                    if (tryAgain == "Y")
                    {
                        Console.Clear();
                        ChangePassword(user);
                    }
                    else
                    {
                        Profile(user);
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.Write("Invalid Password");
                ChangePassword(user);
            }
        }

        internal void Delete(User user)
        {
            currentState = "delete";
            Console.WriteLine("Input Current Password to continue:");
            string password = Console.ReadLine();
            if (password == user.Password)
            {
                Console.Write("Deleting Profile");
                Delay(700, 3);
                DeleteUser(user);
                currentUser = null;
            }
            else
            {
                Console.Clear();
                Profile(user);
            }
        }

        internal void View()
        {
            currentState = "view";
            string[] actions = { "R", "Q" };
            Console.Clear();
            Console.WriteLine("   ~~~ Pofiles ~~~ ");
            Console.WriteLine("---------------------------");
            Console.Write("Loading Profiles");
            Delay(400, 4);
            Console.Clear();
            Console.WriteLine("   ~~~ Pofiles ~~~ ");
            Console.WriteLine("---------------------------");
            ListUsers();
            Console.WriteLine("* Return - R");
            Console.WriteLine("* Quit - Q");
            string awnser = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            while (!actions.Contains(awnser))
            {
                awnser = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            }
            switch (awnser)
            {
                case "R":
                    Console.Clear();
                    Menu();
                    break;
                case "Q":
                    Quit();
                    break;
            }
        }

        internal void Delay(int delay, int repititions)
        {
            Thread.Sleep(delay);
            for (int x = 0; x < repititions; x++)
            {
                Console.Write(".");
                Thread.Sleep(delay);
            }
        }

        internal void Quit()
        {
            Console.Write("Have a good day! (Press Any Key to Exit)");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
