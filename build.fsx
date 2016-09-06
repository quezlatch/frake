#r "./Tools/FAKE/tools/FakeLib.dll"

open Fake

Target "Clean"( fun _ ->
  CleanDirs ["./bin/"; "./obj/"]
)

if not (DotNetCli.isInstalled()) then failwith "donet cli is not installed"

Target "Restore"( fun _ ->
  DotNetCli.Restore (fun p -> { p with NoCache = true })
)

Target "Start"( fun _ ->
  DotNetCli.RunCommand id "run"
)

Target "Default"( fun _ ->
  trace "all good"
)

"Clean"
  ==> "Restore"
  ==> "Start"
  ==> "Default"


RunTargetOrDefault "Default"
