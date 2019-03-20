## Digispace Developer Refactoring Excercise Briefing

Hello, You are about to participate in a Digispace technical screening. This codebase forms the baseline for this.

Please familiarize Yourself with the code before the interview and think about the questions posed in the header of Application.cs. There is an OutofScope... class in this codebase. That class is not supposed to be refactored, it just simulates accessing a database. Looking at it will let You understand what sort of data You will be dealing with. Since the application we are refactoring is a reporting application we will not be wasting any time thinking about how the data gets into the database. We just read it into our app. 😃

A couple of key points:

This is not a Test! There is nothing speciffic You have to say to pass. There also isn't something You might say that will cause You to fail. It is the overall picture of You working with code and others that will determine the next steps.
This is supposed to be done in casual conversation, relax and tell us Your opinion about the questions.
It is recieved positively if You produce some code during the refactoring discussion, so think about what You would like to change in the sourcecode of Application.cs and SqlRequest.cs. The main focus here would be to make the logic of the application testable with Unit Tests.
We do a lot of pair programming here and this interview is not different so it might be that someone else is typing in the changes. At some point You should hit the keyboard though.
If possible, bring a laptop. Otherwise You might be typing in a foreign IDE/editor.


## Build and Run

This project is a .net core 2.1 console application.

To build and run this project, you can choose one of the following options:

A. Open this folder in Visual Studio Code
B. Use the CLI: dotnet build && dotnet run

The corresponding .csproj file can be found in the root folder.