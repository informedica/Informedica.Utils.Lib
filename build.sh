#!/usr/bin/env bash
# to properly set Travis permissions: https://stackoverflow.com/questions/33820638/travis-yml-gradlew-permission-denied
# git update-index --chmod=+x fake.sh
# git commit -m "permission access for travis"

set -eu
set -o pipefail

OS=${OS:-"unknown"}

function run() {
  if [[ "$OS" != "Windows_NT" ]]
  then
    mono "$@"
  else
    "$@"
  fi
}

#Only run the bootstrapper if no paket.exe
if [ ! -e .paket/paket.exe ]
	then
		run .paket/paket.bootstrapper.exe
fi

dotnet restore build.proj


fake build --target $@