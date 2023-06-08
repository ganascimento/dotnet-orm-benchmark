# Dotnet ORM Benchmark

## About

This project was developed to test the performance between different ways of accessing the database
For the test, some methods were executed:

- Insert many cars
- Get cars by id 5 times
- Get all cars 5 times
- Get by branch 5 times

All times are the result of all executions
See the results below

### ADO

- Insert: 2864 ms
- GetByIdAsync: 11 ms
- GetAllAsync: 34 ms
- GetByBrandAsync: 32 ms

### Dapper

- Insert: 3158 ms
- GetByIdAsync: 20 ms
- GetAllAsync: 35 ms
- GetByBrandAsync: 36 ms

### EF Core

- Insert: 9360 ms
- GetByIdAsync: 20 ms
- GetAllAsync: 51 ms
- GetByBrandAsync: 52 ms

### EF Core - Single Save Changes on insert

- Insert: 589 ms
- GetByIdAsync: 20 ms
- GetAllAsync: 51 ms
- GetByBrandAsync: 53 ms

### EF Core No Tracking

- Insert: 9362 ms
- GetByIdAsync: 25 ms
- GetAllAsync: 43 ms
- GetByBrandAsync: 45 ms

### EF Core Compiled Query

- Insert: 9354 ms
- GetByIdAsync: 19 ms
- GetAllAsync: 28 ms
- GetByBrandAsync: 38 ms
