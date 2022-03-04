# p - ADD
CLI tool for adding folders to system enviroment PATH

If you use the tool incorrectly you can fuck up you PATH folder

I AM NOT RESPONSIBLE FOR THOSE OCCURRENCES 

Format: [argument] [dir / path] [specifier] - [explanation]
Use: \"\" to mark paths with spaces

Commands:

[help / h] - Lists all available commands
[add / a] [dir path(s) or leave to use current dir path] [user / u or alluser / a or leave blank to use default] - Adds directory path to system enviorment PATH (use ; after each dir), specifier adds to current user or all users
[remove / r] [dir path(s) or leave to use current dir path] [user / u or alluser / a or leave blank to use default] - Removes directory path from system enviroments PATH (use ; after each dir), specifier removes to current user or all users
[list / l] [user / u or alluser / a or leave blank to use default] - Lists variable folders, specifier lists current user or all users
[change / c] [variable name] [ user or alluser or leave blank to use default] - Changes default system enviorment path which add, remove, list uses
[verify / v] - Outputs default variable and default specifier, change with [change / c]
