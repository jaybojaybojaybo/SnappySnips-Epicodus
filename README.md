
# Snappy Snips 
#### By Jasun Feddema

## Description
Hair Salon: keep track of your stylists and their clients

## Specifications
* Take input from the user(employee) for a Stylist in a single string.
  - example input: "Kim Johnson"
  - example output: New Stylist: "Kim Johnson"

* When a Stylist is entered, a Stylist object is created.
  - example input: "Kim Johnson"
  - example output:
    id: 1
    Stylist: Kim Johnson

* Once the Stylist is created, take employee input to enter a client via a form on the Stylist's client creation page.
  Clients can only be entered on a Stylist's client creation page.
  - example input: "Client Von Clientele"
  - example output: "Client Von Clientele"

  - example input: "Yu Zerbase"
  - example output: "Yu Zerbase"

* When a client is entered, a full client object is created.
  - example input: "Client Von Clientele"
  - example output:
    id: 1
    Client: "Client von Clientele"
    Stylist: 1

  - example input: "Yu Zerbase"
  - example output:
    id: 2
    Client: "Yu Zerbase"
    Stylist: 1

* Take input from the employee for a second Stylist in a single string.
  - example input: "John Johns"
  - example output:
    id: 2
    Stylist: "John Johns"

* Once the Stylist is created, take employee input to enter a client via a form on the Stylist's page.
  - example input (while on John Johns' Stylist Page): "Marsha Martia-Martsché"
  - example output:
    id: 3
    Client: "Marsha Martia-Martsché"
    Stylist: 2    

* Stylist List page will have the following Stylists and associated links:
  - "Kim Johnson"
  - "John Johns"

* Clicking on "Kim Johnson" will show the correlated client list:
  - "Client Von Clientele"
  - "Yu Zerbase"

* Clicking on "John Johns" will show the correlated client list:
  - "Marsha Martia-Martsché"

* User can access list of all Clients via a link on the HairSalon summary page and see their associated Stylists:
  - example display:
   Clients: "Client Von Clientele", "Yu Zerbase", "Marsha Martia-Martsché"
   Stylists: "Kim Johnson", "Kim Johnson", "John Johns"

## Setup/Installation Requirements

* Clone the git repository from 'https://github.com/jaybojaybojaybo/HairSalon-Epicodus'.
* Run the command 'dotnet restore' in the HairSalon folder to download the necessary packages.
* While still in the WordCounter folder, run the command 'dotnet run' to build and run the server on localhost.
* Use your preferred web browser to navigate to localhost:5000
* Follow instructions on webpage for the Word Counter experience.


## Support and contact details

* contact the author at jasun.feddema@gmail.com

## Technologies Used

* C#
* Asp .NET Core MVC
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
