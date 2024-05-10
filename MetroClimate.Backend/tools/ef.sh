#!/usr/bin/env bash
cd "$(dirname "$0")"
~/.dotnet/tools/dotnet-ef "$@" --project ../MetroClimate.Data --startup-project ../MetroClimate.Api
