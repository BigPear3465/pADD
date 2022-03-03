using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;

namespace pADD
{
    class Program
    {
        static EnvironmentVariableTarget specifier = EnvironmentVariableTarget.Machine; //user or all users (local machine)
        static string sysVar = "PATH"; //system variable
        static string envVar = SecureEnvVar(); //enviroment variable
        static int length = 0; //Number of arguments inputed

        /// <summary>
        /// Makes sure envVar isnt empty and causes a crash
        /// </summary>
        /// <returns></returns>
        static string SecureEnvVar()
        {
            string getEnvVar = Environment.GetEnvironmentVariable(sysVar, specifier);
            if (getEnvVar != null)
            {
                return getEnvVar;
            }
            else
            {
                throw new Exception("No system variable PATH found");
            }
        }

        /// <summary>
        /// Lists all commands available
        /// </summary>
        static void ListArguments()
        {
            Console.WriteLine("Format: [argument] [dir/path] [specifier] - [explanation]");
            Console.WriteLine("Use: \"\" to mark paths with spaces \n");
            Console.WriteLine("[help / h] - Lists all available commands \n");
            Console.WriteLine("[add / a] [dir path(s) or leave to use current dir path] [user / u or alluser / a or leave blank to use default] - Adds directory path to system enviorment PATH (use ; after each dir), specifier adds to current user or all users \n");
            Console.WriteLine("[remove / r] [dir path(s) or leave to use current dir path] [user / u or alluser / a or leave blank to use default] - Removes directory path from system enviroments PATH (use ; after each dir), specifier removes to current user or all users \n");
            Console.WriteLine("[list / l] [user / u or alluser / a or leave blank to use default] - Lists variable folders, specifier lists current user or all users \n");
            Console.WriteLine("[change / c] [variable name] [ user or alluser or leave blank to use default] - Changes default system enviorment path which add, remove, list uses \n");
            Console.WriteLine("[verify / v] - Outputs default variable and default specifier, change with [change / c] \n");
        }

        /// <summary>
        /// Adds a directory to system variable PATH
        /// </summary>
        /// <param name="args"></param>
        static void AddDir(string[] args)
        {
            if(length == 1)
            {
                string currentDir = Environment.CurrentDirectory;
                if (envVar.Contains(currentDir + ";") || envVar.Contains(currentDir))
                {
                    Console.WriteLine(sysVar + " already contains this directory");
                }
                else if(!envVar.Contains(currentDir + ";") || !envVar.Contains(currentDir))
                {
                    Console.WriteLine("Loading...");

                    if (!envVar.EndsWith(";")) //Makes sure the envVar is prepared for another dir
                    {
                        envVar += ";";
                    }
                    if (!currentDir.EndsWith(";")) //Makes sure the dir ends with ;
                    {
                        currentDir += ";";
                    }
                    Environment.SetEnvironmentVariable(sysVar, envVar + currentDir, specifier);

                    Console.WriteLine("Done");
                }
                else
                {
                    throw new Exception();
                }
            }
            else if (length == 2)
            {
                if (envVar.Contains(args[1] + ";") || envVar.Contains(args[1]))
                {
                    Console.WriteLine(sysVar + " already contains this directory");
                }
                else if (!envVar.Contains(args[1] + ";"))
                {
                    Console.WriteLine("Loading...");

                    if (!envVar.EndsWith(";")) //Makes sure the envVar is prepared for another dir
                    {
                        envVar += ";";
                    }
                    if (!args[1].EndsWith(";")) //Makes sure the dir ends with ;
                    {
                        args[1] += ";";
                    }
                    Environment.SetEnvironmentVariable(sysVar, envVar + args[1], specifier);

                    Console.WriteLine("Done");
                }
                else
                {
                    throw new Exception();
                }
            }
            else if(length > 2)
            {
                Console.WriteLine("Too many commands, try help or h");
            }
            else
            {
                Console.WriteLine("[add / a] [dir path(s) or leave to use current dir path] [user / u or alluser / a or leave blank to use default] - Adds directory path to system enviorment PATH (use ; after each dir), specifier adds to current user or all users \n");
            }
        }

        /// <summary>
        /// Removes a directory from system variable PATH
        /// </summary>
        /// <param name="args"></param>
        static void RemoveDir(string[] args)
        {
            if (length == 1)
            {
                string currentDir = Environment.CurrentDirectory;
                if (envVar.Contains(currentDir + ";"))
                {
                    Console.WriteLine("Loading...");

                    Environment.SetEnvironmentVariable(sysVar, envVar.TrimStart((currentDir + ";").ToCharArray()), specifier);

                    Console.WriteLine("Done");
                }
                else if (envVar.Contains(currentDir))
                {
                    Console.WriteLine("Loading...");

                    Environment.SetEnvironmentVariable(sysVar, envVar.TrimStart((currentDir).ToCharArray()), specifier);

                    Console.WriteLine("Done");
                }
                else if(!envVar.Contains(currentDir + ";") || !envVar.Contains(currentDir))
                {
                    Console.WriteLine(sysVar + " doesn't contains this directory");
                }
                else
                {
                    throw new Exception();
                }
            }
            else if (length == 2)
            {
                if (envVar.Contains(args[1] + ";"))
                {
                    Console.WriteLine("Loading...");

                    string tmp = envVar.Trim((args[1] + ";").ToCharArray());
                    Environment.SetEnvironmentVariable(sysVar, tmp, specifier);

                    Console.WriteLine("Done");
                }
                else if (envVar.Contains(args[1]))
                {
                    Console.WriteLine("Loading...");

                    string tmp = envVar.Trim((args[1]).ToCharArray());
                    Environment.SetEnvironmentVariable(sysVar, tmp, specifier);

                    Console.WriteLine("Done");
                }
                else if (!envVar.Contains(args[1] + ";") || !envVar.Contains(args[1]))
                {
                    Console.WriteLine(sysVar + " doesn't contains this directory");
                }
                else
                {
                    throw new Exception();
                }
            }
            else if(length > 2)
            {
                Console.WriteLine("Too many commands, try help or h");
            }
            else
            {
                Console.WriteLine("[remove / r] [dir path(s) or leave to use current dir path] [user / u or alluser / a or leave blank to use default] - Removes directory path from system enviroments PATH (use ; after each dir), specifier removes to current user or all users \n");
            }
        }

        /// <summary>
        /// Lists all folders contained in system enviroment variable
        /// </summary>
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

        /// <summary>
        /// Function to change default system variable and specifier
        /// </summary>
        static void ChangeSysVar(string[] args)
        {
            if (length == 1 && (args[1] != null || args[1] != ""))
            {
                Properties.Settings.Default.sysVar = args[1];

                if (length == 2 && (args[2] != null || args[2] != ""))
                {
                    switch (args[2])
                    {
                        case "user": Properties.Settings.Default.specifier = "user"; break;
                        case "alluser": Properties.Settings.Default.specifier = "alluser"; break;
                        case "u": Properties.Settings.Default.specifier = "user"; break;
                        case "a": Properties.Settings.Default.specifier = "alluser"; break;
                        default: Properties.Settings.Default.specifier = "alluser"; Console.WriteLine("specifier error, set to defualt"); break;
                    }
                    Properties.Settings.Default.Save();
                    Console.WriteLine("Done");
                }
                else
                {
                    Properties.Settings.Default.Save();
                    Console.WriteLine("Done");
                }
            }
            else
            {
                Console.WriteLine("[change / c] [variable name] [ user or alluser or leave blank to use default] - Changes default system enviorment path which add, remove, list uses \n");
            }
        }

        /// <summary>
        /// Outputs default system variable and default specifier
        /// </summary>
        static void GetSysVar()
        {
            Console.WriteLine("variable: " + sysVar + ", " + "default specifier: " + specifier);
        }

        /// <summary>
        /// Config file
        /// </summary>
        static void Config()
        {
            if (File.Exists("pADD.exe.config"))
            {
                sysVar = Properties.Settings.Default.sysVar;
                string localSpecifier = Properties.Settings.Default.specifier;

                switch (localSpecifier)
                {
                    case "user": specifier = EnvironmentVariableTarget.User; break;
                    case "alluser": specifier = EnvironmentVariableTarget.Machine; break;
                    case "u": specifier = EnvironmentVariableTarget.User; break;
                    case "a": specifier = EnvironmentVariableTarget.Machine; break;
                    default: specifier = EnvironmentVariableTarget.Machine; Console.WriteLine("specifier error, set to defualt"); break;
                }
            }
            else
            {
                Console.WriteLine("Config file not found (redownload config file)");
            }
        }

        static void Main(string[] args)
        {
            length = args.Length; //number of arguments inputed
            Config(); //Loads config

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
                ChangeSysVar(args);
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
