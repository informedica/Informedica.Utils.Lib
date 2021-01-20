# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased] 
Note: a netcore only lib cannot be referenced using `#r "nuget: "` with VS. It works 
in VSCode.

### Changed
- namespace from Informedica.GenUtils.Lib to Informedica.Utils.Lib 

## [1.0.0] - 2021-01-19

### Changed
Completely moved to latest miniscaffold and net5.0. Will replace Informedica.GenUtils.Lib

## [0.5.0-beta]

* Switch to dotnet core

## [0.4.2-beta]

* Added missing assembly info

## [0.4.1-beta]

* Improved union case fromString function

## [0.4.0-beta]

* Added reflection functions for union case to and from string

## [0.3.0-beta]

* Added list replace and distinct functions
* Added safe null check
* Improved String startswith

## [0.2.1-beta]

* Added tests for BigRational functions
* Added exception types for String and BigRational functions
* Added continuation functions
* Updated documentation

## [0.1.1-beta]

* Added BCL (basic core library) functions for String and BigRational
* Finished basic project setup

## [0.0.1-alpha]

* Initial setup
[Unreleased]: https://github.com/informedica/Informedica.Utils.Lib/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/informedica/Informedica.Utils.Lib/compare/v0.5.0-beta...v1.0.0
