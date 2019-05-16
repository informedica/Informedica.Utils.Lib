# GenUtils
Library with utility functions

## Build Status

Mono | .NET | NuGet|
---- | ---- | ---- |
[![Mono CI Build Status](https://img.shields.io/travis/halcwb/GenUtils/master.svg)](https://travis-ci.org/halcwb/GenUtils) | [![.NET Build Status](https://img.shields.io/appveyor/ci/halcwb/GenUtils/master.svg)](https://ci.appveyor.com/project/halcwb/GenUtils)| [![NuGet Status](http://img.shields.io/nuget/v/Informedica.GenUtils.Lib.svg?style=flat)](https://www.nuget.org/packages/Informedica.GenUtils.Lib/)


# Background
The main purpose of this library is to 'functionalize' existing dotnet libraries and provide common utility functions

# Libray design
This repository uses an explicit opt-in `.gignore` strategy, meaning that all files are excluded unless specifically included via the `.gitignore` file.

# Build

The library can be build by using the `build.cmd` or `build.sh`.

## Usage of FAKE
As I always forget the usefull fake commands, here is a list:

* To build use: `fake run build.fsx`
* To list targets: `fake run build.fsx --list`
* To run a specific target: `fake run build.fsx --target Clean`

Another thing: The build scripts keeps track of its own dependencies, these are locked in the `build.fsx.lock` file. However, when updating FAKE references, you'll need to remove the `build.fsx.lock` file first.