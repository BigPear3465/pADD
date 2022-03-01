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

        /// <summary>
        /// Makes sure envVar isnt empty and causes a crash
        /// </summary>
        /// <returns></returns>
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
            Console.WriteLine("[list / l] [user / u or machine / m or leave blank to use default [v]] - Lists variable folders, specifier lists current user or all users \n");
            Console.WriteLine("[change / c] [variable name] [ user or alluser or leave blank to use default] - Changes default system enviorment path which add, remove, list uses \n");
            Console.WriteLine("[verify / v] - Outputs default variable and default specifier, change with [change / c] \n");
        }

        /// <summary>
        /// Adds a directory to system variable PATH
        /// </summary>
        /// <param name="args"></param>
        static void AddDir(string[] args)
        {
            if(args.Length == 1)
            {
                if (envVar.Contains(Environment.CurrentDirectory + ";"))
                {
                    Console.WriteLine(sysVar + " already contains this directory");
                }
                else if (envVar.Contains(Environment.CurrentDirectory))
                {
                    Console.WriteLine(sysVar + " already contains this directory");
                }
                else if(!envVar.Contains(Environment.CurrentDirectory + ";"))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar + Environment.CurrentDirectory + ";", specifier);
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
                if (envVar.Contains(args[1] + ";"))
                {
                    Console.WriteLine(sysVar + " already contains this directory");
                }
                else if (envVar.Contains(args[1]))
                {
                    Console.WriteLine(sysVar + " already contains this directory");
                }
                else if (!envVar.Contains(args[1] + ";"))
                {
                    if (!envVar.EndsWith(";"))
                    {
                        envVar += ";";
                    }
                    Environment.SetEnvironmentVariable(sysVar, envVar + args[1] + ";", specifier);
                }
                else if (!envVar.Contains(args[1]))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar + args[1], specifier);
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

        /// <summary>
        /// Removes a directory from system variable PATH
        /// </summary>
        /// <param name="args"></param>
        static void RemoveDir(string[] args)
        {
            if (args.Length == 1)
            {
                if (envVar.Contains(Environment.CurrentDirectory + ";"))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar.TrimStart((Environment.CurrentDirectory + ";").ToCharArray()), specifier);
                }
                else if (envVar.Contains(Environment.CurrentDirectory))
                {
                    Environment.SetEnvironmentVariable(sysVar, envVar.TrimStart((Environment.CurrentDirectory).ToCharArray()), specifier);
                }
                else if(!envVar.Contains(Environment.CurrentDirectory + ";"))
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
                if (envVar.Contains(args[1] + ";"))
                {
                    string tmp = envVar.Trim((args[1] + ";").ToCharArray());
                    Environment.SetEnvironmentVariable(sysVar, tmp, specifier);
                }
                else if (envVar.Contains(args[1]))
                {
                    string tmp = envVar.Trim((args[1]).ToCharArray());
                    Environment.SetEnvironmentVariable(sysVar, tmp, specifier);
                }
                else if (!envVar.Contains(args[1] + ";"))
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
            sysVar = ConfigurationManager.AppSettings.Get("sysVar");
            envVar = ConfigurationManager.AppSettings.Get("specifier");

            if(args[1] != null)
            {
                ConfigurationManager.AppSettings.Add("sysVar", args[1].ToLower());
                if (args[2] != null)
                {
                    ConfigurationManager.AppSettings.Add("specifier", args[2].ToLower());
                }
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
            if (!ConfigurationManager.AppSettings.HasKeys())
            {
                Console.WriteLine("Config file not found, running on default integrated settings (redownload config file if empty)");
            }
            else
            {
                sysVar = ConfigurationManager.AppSettings.Get("sysVar");
                string localSpecifier = ConfigurationManager.AppSettings.Get("specifier");

                switch (localSpecifier)
                {
                    case "user": specifier = EnvironmentVariableTarget.User; break;
                    case "alluser": specifier = EnvironmentVariableTarget.Machine; break;
                    default: specifier = EnvironmentVariableTarget.Machine; break;
                }
            }
        }

        static void Main(string[] args)
        {
            Config(); //Loads config

            if (args[0].ToLower() == "help" || args[0].ToLower() == "h")
            {
                ListArguments();
            }
            else if (args[0].ToLower() == "add" || args[0].ToLower() == "a")
            {
                AddDir(args);
                Console.WriteLine("Done");
            }
            else if (args[0].ToLower() == "remove" || args[0].ToLower() == "r")
            {
                RemoveDir(args);
                Console.WriteLine("Done");
            }
            else if (args[0].ToLower() == "list" || args[0].ToLower() == "l")
            {
                ListEnvVar();
            }
            else if (args[0].ToLower() == "change" || args[0].ToLower() == "c")
            {
                ChangeSysVar(args);
                Console.WriteLine("Done");
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
