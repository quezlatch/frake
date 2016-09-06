#! /bin/bash

nuget install FAKE -OutputDirectory Tools -ExcludeVersion
mono ./Tools/FAKE/tools/FAKE.exe
