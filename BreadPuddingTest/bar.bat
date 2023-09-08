@echo off
cd..
cd BreadPuddingCore
call build
cd..
cd BreadPuddingTest
dotnet build
cls
dotnet run
@echo on