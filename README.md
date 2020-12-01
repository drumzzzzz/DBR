Database Reflection (DBR) via SQLite Project Library 

     A builder pattern based library that provides an abstraction to accessing SQLite type databases
     created for UWGB Computer Science Fall semester, 2020.

The goal of this library is to reduce the hardcoding involved with SQLite database interaction
by providing returned row queries via object Reflection. This allows the use of simpler 
class models which only need a single boilerplate method that casts the returned object list.
This also helps reduce the dependence and overhead on larger database libraries (i.e. Entity Framework).
The reflection is provided via FastMember for optimal performance and caching. Abstractions for 
non-query and integer count return type commands are also implemented. 

How it Works

An example database model created with a boilerplate casting method:

public class MyClass
{
    public long MyClassId {get;set;}
    ...
}

public class MyClasses : List<MyClass>    
{
    // This method casts a reflected object list as the model type
    public MyClasses(IReadOnlyList<object> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Add((MyClass)objects[i]);
        }
    }
}

// Init the command context:
CommandContext commandContext = new CommandContext();

// Create and perform an example query:
QueryCommand querycmd = new QueryCommand("MyDatabaseFile", "MyClassTable", typeof(MyClass));
querycmd.SetCommand("SELECT (*) FROM MyClass");
MyClasses myClasses = new MyClasses(commandContext.Execute(querycmd));

// Create and perform an example non-query:
NonQueryCommand nonquerycmd = new NonQueryCommand("MyDatabaseFile", "MyClassTable");
nonquerycmd.SetCommand("INSERT INTO MyClassTable(SomeColumn1, SomeColumn2, ...) VALUES('Value1', 'Value2', ...)");

// Create and perform an example row count query:
QueryCountCommand querycountcmd = new QueryCountCommand(db_file, "MyClassTable");
querycountcmd.SetCommand("SELECT COUNT(*) FROM MyClassTable");
int record_count = commandContext.ExecuteCount(querycountcmd);


Development setup
    Visual Studio 2019 
    Net Framework 4.7.2
    x64 CPU Build*   
   
     
*An x86 build requires changing ~DBR\SQLite.Interop.dll and installation of x86 SQLite framework: 
https://system.data.sqlite.org/downloads/1.0.113.0/sqlite-netFx46-setup-x86-2015-1.0.113.0.exe

Credits
This software uses the following open source packages:

    SQLite 
    FastMember
    Chinook DB


Release History

    0.0.2
        Add opensource features

    0.0.1
        Work in progress

Meta

Nathaniel Kennis kennnl04@uwgb.edu

Distributed under the Apache license. See LICENSE for more information.

https://github.com/drumzzzzz/Encryption


Contributing

    Fork it (https://github.com/drumzzzzz/Encryption/fork)
    Create a new Pull Request


