using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace pADD
{
    class Program
    {
        static EnvironmentVariableTarget specifier = EnvironmentVariableTarget.Machine; //user or all users (local machine)
        static string sysVar = "PATH"; //system variable
        static string envVar = SecureEnvVar(); //enviroment variable

        static string SecureEnvVar()
        {
            if(Environment.GetEnvironmentVariable(sysVar, specifier) != null)
            {
                return Environment.GetEnvironmentVariable(sysVar, specifier);
            }
            else if(Environment.GetEnvironmentVariable(sysVar, specifier) == null)
            {
                return "";
            }
            else
            {
                throw new Exception();
            }
        }

        static void ListArguments()
        {
            Console.WriteLine("Format: [argument] [dir/path] [specifier] - [explanation]");
            Console.WriteLine("Use: \"\" to mark paths with spaces \n");
            Console.WriteLine("[help / h] - Lists all available commands");
            Console.WriteLine("[add / a] [dir path(s) or leave to use current dir path] [user / u or machine / m or leave blank to use default [v]] - Adds directory path to system enviorment PATH (use ; between each dir if using multiple dirs), specifier adds to current user or all users");
            Console.WriteLine("[remove / r] [dir path(s) or leave to use current dir path] - Removes directory path from system enviroments PATH (use ; between each dir if using multiple dirs), specifier removes to current user or all users");
            Console.WriteLine("[list / l] [user / u or machine / m or leave blank to use default [v]] - Lists variable folders, specifier lists current user or all users");
            Console.WriteLine("[change / c] [Variable name] [ user / u or machine / m or leave blank to use default] (check in your system enviorment variables) - Change where add command adds path (change system enviroment PATH)");
            Console.WriteLine("[verify / v] - Outputs variable and default specifier which you [add] to, change with [change]");
        }

        static void AddDir(string[] args)
        {
            if(args.Length == 1)
            {
                if (envVar.Contains(";" + Environment.CurrentDirectory))
                {
                    Console.WriteLine(sysVar + " already contains this directory");
                }
                else if (envVar.Contains(Environment.CurrentDirectory))
                {
                    Console.WriteLine(sysVar + " already contains this directory");
                }
                else if(!envVar.Contains(";" + Environment.CurrentDirectory))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar + ";" + Environment.CurrentDirectory, specifier);
                }
                else if (!envVar.Contains(Environment.CurrentDirectory))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar + Environment.CurrentDirectory, specifier);
                }
                else
                {
                    throw new Exception();
                }
            }
            else if (args.Length == 2)
            {
                if (envVar.Contains(";" + args[1]))
                {
                    Console.WriteLine(sysVar + " already contains this directory");
                }
                else if (envVar.Contains(args[1]))
                {
                    Console.WriteLine(sysVar + " already contains this directory");
                }
                else if (!envVar.Contains(args[1]))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar + args[1], specifier);
                }
                else if (!envVar.Contains(";" + args[1]))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar + ";" + args[1], specifier);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                Console.WriteLine("Too many commands, try help or h");
            }
        }

        static void RemoveDir(string[] args)
        {
            if (args.Length == 1)
            {
                if (envVar.Contains(";" + Environment.CurrentDirectory))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar.TrimStart((";" + Environment.CurrentDirectory).ToCharArray()), specifier);
                }
                else if (envVar.Contains(Environment.CurrentDirectory))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar.TrimStart((Environment.CurrentDirectory).ToCharArray()), specifier);
                }
                else if(!envVar.Contains(";" + Environment.CurrentDirectory))
                {
                    Console.WriteLine(sysVar + " doesn't contains this directory");
                }
                else if (!envVar.Contains(Environment.CurrentDirectory))
                {
                    Console.WriteLine(sysVar + " doesn't contains this directory");
                }
                else
                {
                    throw new Exception();
                }
            }
            else if (args.Length == 2)
            {
                if (envVar.Contains(";" + args[1]))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar.TrimStart((";" + args[1]).ToCharArray()), specifier);
                }
                else if (envVar.Contains(args[1]))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar.TrimStart((args[1]).ToCharArray()), specifier);
                }
                else if (!envVar.Contains(";" + args[1]))
                {
                    Console.WriteLine(sysVar + " doesn't contains this directory");
                }
                else if (!envVar.Contains(args[1]))
                {
                    Console.WriteLine(sysVar + " doesn't contains this directory");
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                Console.WriteLine("Too many commands, try help or h");
            }
        }

        static void ListEnvVar()
        {
            if (specifier == EnvironmentVariableTarget.User)
            {
                Console.WriteLine("Current user: " + Environment.GetEnvironmentVariable(sysVar, EnvironmentVariableTarget.User));
            }
            else if (specifier == EnvironmentVariableTarget.Machine)
            {
                Console.WriteLine("All users: " + Environment.GetEnvironmentVariable(sysVar, EnvironmentVariableTarget.Machine));
            }
            else
            {
                throw new Exception("EnvironmentVariableTarget error, no recognized specifier");
            }
        }

        static void ChangeSysVar()
        {

        }

        static void GetSysVar()
        {
            Console.WriteLine("variable: " + sysVar + ", " + "default specifier: " + specifier);
        }

        static void Main(string[] args)
        {
            if (args[0].ToLower() == "help" || args[0].ToLower() == "h")
            {
                ListArguments();
            }
            else if (args[0].ToLower() == "add" || args[0].ToLower() == "a")
            {
                AddDir(args);
            }
            else if (args[0].ToLower() == "remove" || args[0].ToLower() == "r")
            {
                RemoveDir(args);
            }
            else if (args[0].ToLower() == "list" || args[0].ToLower() == "l")
            {
                ListEnvVar();
            }
            else if (args[0].ToLower() == "change" || args[0].ToLower() == "c")
            {

            }
            else if (args[0].ToLower() == "verify" || args[0].ToLower() == "v")
            {
                GetSysVar();
            }
            else
            {
                Console.WriteLine("No matching argument, use help or h");
            }
        }
    }
}
