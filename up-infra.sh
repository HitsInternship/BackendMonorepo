#!/bin/bash

# shellcheck disable=SC2162
echo "Launching infrastructure"

docker-compose -f docker-compose-dev.yml up -d
