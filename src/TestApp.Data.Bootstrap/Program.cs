using CommandLine;
using System.Text;
using TestApp.Data.Bootstrap.Commands;

Console.OutputEncoding = Encoding.UTF8;

var parser = Parser.Default
    .ParseArguments<InitCommand>(args)
    .WithParsed(action => action.Execute())
    .WithNotParsed(errors =>
     {
         
     });