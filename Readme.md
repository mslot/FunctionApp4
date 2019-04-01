# Welcome to FunctionApp4
This name isn't random but reflects my urge to get it out on the net. I just found out that Azure Functions supports:

1. Real IoC
2. Run from commandline

Technically I did know 2), but hey it sounds more Matrix to say two things (this project covers both).

I have written a fast and poor example how to do real IoC with the new 1.0.26 Azure Funtions SDK. It is super easy to setup, just add a Startup class (here it is named StartUp, with capital U because I can't be more exicted):

```csharp
using FunctionApp4;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(StartUp))]
namespace FunctionApp4
{
    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddTransient<ILogic, Logic>();
        }
    }
}
```

Two things to notice here:

1. I have included the usings - SO YOU CAN SEE WHAT MY INTENTIONS ARE. Always include those in examples
2. the [assembly:] tag on namespace

There you go. Remove the statics from your functions, and inject the interfaces in your constructor ... BOOOOOM!!!

# Run multiple Azure functions at once
I often create multiple AzureFunctions that I want to run, just to test things. The Functions is spread out in multiple repositories, and it is cumbersome to open each solution just to test each step (I tend to use storage queues to link the functions together). I have recently begun to include a simple scripts folder with one script in it, RunLocally.ps1 that contains:

```powershell

param(
    [string] $Name
)

func host start --script-root "..\$Name" --port 5005

```

I include this for each set of functions. To start it all of i then create a RunAll.ps1 script that is a sibling to this AzureFunctions and start each RunLocally.ps1 script in a Start-Process:

```powershell
Start-Process powershell.exe -ArgumentList "-file RunLocally.ps1 -Name FunctionApp3" -WorkingDirectory "FunctionApp3\scripts"
Start-Process powershell.exe -ArgumentList "-file RunLocally.ps1 -Name FunctionApp4" -WorkingDirectory "FunctionApp4\scripts"
```

Here I have two set of functions that I want to run. The file structure is something alike:

* FunctionApp3\
* * scripts\RunLocally.ps1
* * FunctionApp3\host.json
* FunctionApp4\
* * scripts\RunLocally.ps1
* * FunctionApp4\host.json
* RunAll.ps1

Remember to assign an unused port in each RunLocally.ps1.