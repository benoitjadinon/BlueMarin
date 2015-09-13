#!/bin/bash

ProjectPath="/Users/bja/Workspaces/Xamarin/mobilite/Mobilite.Android/Mobilite.Android.csproj"

AndroidSDKPath="/Users/bja/SDKs/android-sdk-macosx"

mono tools/Xamaridea/Xamaridea.Console.exe -p=$ProjectPath -s=$AndroidSDKPath