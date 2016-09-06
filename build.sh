#! /bin/bash

[[ -d ./Tools/FAKE ]] ||nuget install FAKE -OutputDirectory Tools -ExcludeVersion
mono ./Tools/FAKE/tools/FAKE.exe
