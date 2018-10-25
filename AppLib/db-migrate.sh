#!/usr/bin/env bash

name=$1

if [ "$name" = "" ]; then
    echo -e "Must specify migration name"
    exit 1
fi

dotnet ef --startup-project ../AppBackend/ migrations add $name