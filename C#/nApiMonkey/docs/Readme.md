# Prerequisites:
1. Microsoft Visual Studio installed
2. Git installed
3. Set the PATH environment variable to point to *msbuild* and *mstest* exe locations.
(Usually *msbuild* is located inside *'Microsoft.NET\Framework\v4.0'* and *mstest* is located inside *'Microsoft Visual Studio 14.0\Common7\IDE'*. ) 
# Running instructions: 
1. Clone the [APIMonkey](http://https://github.com/alt-code/ApiMonkey) project
2. Go to folder *C#/ApiMonkey*.
3. Open *nApiMonkey.sln* in Visual Studio.
4. Open file *Program.cs* and modify values of following variables:

``

	string project_name = ""; 
	//Name of the project eg. "Microsoft.Bot.Builder"; Ususally same as name of main .sln file
    
	string project_path = @""; 
	//Path to the location where you want to clone original project eg. @"G:\samples_proj";

    string sandbox_path = @""; 
	//Path to sandbox folders eg. @"G:\nuget_sandbox_new\BotBuilder_new";

    string oldRootSol = @""; 
	//Path to the folder in original project where mail solution file is located. eg. @"G:\samples_proj\BotBuilder\CSharp" folder has Microsoft.Bot.Builder.sln file

    string testLocation = @"";
	//relative path to the folder where test dlls are located eg. @"Tests\Microsoft.Bot.Builder.Tests\bin\Debug\Microsoft.Bot.Builder.Tests.dll";
``

5. Set script paths- Currently hardcoded to the values inside *nApiMonkey\scripts* folder

``

    string git_script = @"G:\new_demo\ApiMonkey\C#\nApiMonkey\nApiMonkey\scripts\gitcmd.sh";
    
    string build_script = @"G:\new_demo\ApiMonkey\C#\nApiMonkey\nApiMonkey\scripts\build.bat";
    
    string copy_script = @"G:\new_demo\ApiMonkey\C#\nApiMonkey\nApiMonkey\scripts\script.bat";
``

6. Build the *nApiMonkey* solution and run it in visual studio.
7. At the end of the run, final report file will be generated at the location listed as project_path with the name as project_name.md eg. *G:\samples_proj\Microsoft.Bot.BuilderReport.md*
8. Also the individual build report and test reports can be found inside each sandbox folder. Following are the files generated by this tool:

Build Reports:

-  *msbuild1.log* : contains output of msbuild run
-  *BuildWarnings.log* : lists all warning generated during msbuild run
-  *BuildErrors.log* : lists all errors generated during msbuild run

Test run report:

-  *TestResults.trx* : contains test run result of unit tests written using visual studio.

Currently the tool only supports tests written by visual studio. Other testing frameworks can be added such as xunit, nunit etc.

## Problems that you might encounter ##
1. Some projects don't have */packages* folder pushed on the repository. In such cases, we first need to manually run *'nuget restore'* command to download all the required packages and then run the msbuild.
2. Different projects can have different structures. So pay attention to the naming conventions while setting the variables mentioned in step 4.
 
