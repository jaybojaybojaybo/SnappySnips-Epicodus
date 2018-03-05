
# Snappy Snips
#### By Jasun Feddema

## Description
Snappy Snips: keep track of your stylists, their specialties, and their clients.

## Specifications
* Take input from the user(employee) for a Stylist in a single string and a datetime to create new Stylist object in stylists database.
  - example input:
  name: "Kim Johnson
  hire date: "03/02/2018"
  - example output:
    id: 1
    name: "Kim Johnson"
    hireDate: "03/02/2018"    

* Once stylist is created, take input from the user(employee) for a Client in a single string to create new Client object in clients database.
  - example input: "Client von Clientele"
  - example output:
    id: 1
    name: "Client von Clientele"

* Once stylist is created, take input from the user(employee) for a Specialty in a single string to create new Specialty object in specialtys database.
  - example input: "Coloring"
  - example output:
    id: 1
    name: "Coloring"

* From a Stylist's profile page, user can assign a specialty to the stylist.
  - example input:
    Stylist: "Kim" (id:1)
    Specialty: "Cutting" (id:3)

  - example output (on join table):
    id: 1
    stylist_id: 1
    specialty_id: 3

* From a Stylist's profile page, user can create a Client that associates them to that stylist automatically.
  - example input (from Stylist Kim's profile page):
    Client Name: "Yu Zerbase" (id: 4)

  - example output (on join table):
    id: 1
    stylist_id: 1
    client_id: 4


* From the Clients Index page, user can create a new Client by entering the client's name and choosing a stylist from a dropdown menu.
- example input:
  Client Name: "Polly Anna"
  Stylist: "Kim Johnson"

- example output (on join table):
  id: 2
  stylist_id: 1
  client_id: 5

* From Stylists Index page, user can delete a Stylist, which would delete that stylist and all their clients.

* From Clients Index page, user can delete a Client, which would delete that client.

* From Specialties Index page, user can delete a Specialty, which would delete that specialty.

* All clients, stylists, and specialty names can be updated.

* Stylist's hire dates can be updated.


## Setup/Installation Requirements

* Make sure all necessary software is installed: 
* Clone the git repository from 'https://github.com/jaybojaybojaybo/SnappySnips-Epicodus'.
* Navigate to the SnappySnips.Tests folder and run the command "dotnet resore".
* In the SnappySnips folder, run the following command: "dotnet add package MySQLConnector"
* Run the command 'dotnet restore' in the SnappySnips folder to download the necessary packages.
* Using MAMP, setup database to port 8889.
* Navigate to myPHPadmin from the MAMP online start page.
* While in myPHPadmin, select import to import the database - navigate to the Solution folder of the SnappySnips project and select the "jasun_feddema" and "jasun_feddema_tests" databases.
* While in the SnappySnips folder, run the command 'dotnet run' to run the app.
* Use your preferred web browser to navigate to localhost:5000
* Follow instructions on webpage for the SnappySnips app experience.


## Support and contact details

* contact the author at jasun.feddema@gmail.com

## Technologies Used

* C#
* Asp .NET Core MVC
* MAMP
* MySql
* HTML
* CSS
* Javascript
* Bootstrap
* JQuery

### License

Copyright (c) 2018 Jasun Feddema

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
