#!/bin/sh
set -e

cd ./Trappist/src/Promact.Trappist.Web
dotnet ef migrations add prod
dotnet publish -o published -v d
ls -la wwwroot
ls published/wwwroot
ls -la published/wwwroot
sleep 10
echo "again"
ls -la published/wwwroot
echo $PWD
cd ../../../
docker build -t promact/trappist:$TRAVIS_BRANCH .
docker login -u=$DOCKER_USERNAME -p=$DOCKER_PASSWORD
docker push promact/trappist:$TRAVIS_BRANCH
