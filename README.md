# Hair Salon
###### By Miranda Keith

## Setup instructions
1. Clone the repo from [here](https://github.com/gold-mir/hair-salon) using `git clone https://github.com/gold-mir/hair-salon`.
1. Setup database in MySQL using the following commands:
```
> CREATE DATABASE miranda_keith;
> USE miranda_keith;
> CREATE TABLE stylists (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL);
> CREATE TABLE clients (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    stylist_id INT NOT NULL,
    FOREIGN KEY (stylist_id) REFERENCES stylists(id));
> CREATE TABLE specialties (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL);
> CREATE TABLE stylists_specialties (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    stylist_id INT NOT NULL,
    specialty_id INT NOT NULL,
    FOREIGN KEY (stylist_id) REFERENCES stylists(id) ON DELETE CASCADE,
    FOREIGN KEY (specialty_id) REFERENCES specialties(id) ON DELETE CASCADE);
```
2. Repeat this process for database `miranda_keith_test` to create test database.
3. Run `dotnet restore HairSalon/HairSalon.csproj` and `dotnet restore HairSalon.Tests/Test.csproj` to get the necessary packages.
4. Start the server with `dotnet run`.

## Description
This app allows a user to keep track of various hair stylists and their clients.

> © Miranda Keith, 2018
