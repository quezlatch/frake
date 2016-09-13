#r "./Tools/FAKE/tools/FakeLib.dll"

open Fake

Target "Clean" <| fun _ ->
  CleanDirs ["./bin/"; "./obj/"]

if not (DotNetCli.isInstalled()) then failwith "donet cli is not installed"

Target "Restore" <| fun _ ->
  DotNetCli.Restore (fun p -> { p with NoCache = true })

Target "Test" <| fun _ ->
  !!"test/*/project.json" |> DotNetCli.Test id

open System.Threading
Target "Start" <| fun _ ->
  fireAndForget <| fun info ->
    info.FileName <- "dotnet"
    info.Arguments <- "run -p src/ConsoleApplication/project.json"
  Thread.Sleep 3000

Target "Ping" <| fun _ ->
  let res = REST.ExecuteGetCommand "userName" "password" "http://localhost:5000"
  if not (res.Contains("\"status\": true")) then failwith "api request failed"

Target "Default" <| fun _ ->
  trace "all good"

"Clean"
  ==> "Restore"
  ==> "Test"
  ==> "Start"
  ==> "Ping"
  ==> "Default"

RunTargetOrDefault "Default"
