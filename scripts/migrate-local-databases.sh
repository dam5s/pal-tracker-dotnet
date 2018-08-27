#!/usr/bin/env bash

set -e
set -v

flyway -url="jdbc:mysql://localhost:3306/tracker_dotnet_dev" -user=tracker_dotnet -password=password -locations=filesystem:databases/tracker migrate
flyway -url="jdbc:mysql://localhost:3306/tracker_dotnet_test" -user=tracker_dotnet -password=password -locations=filesystem:databases/tracker migrate

