<div id="top"></div>

<div id="title"><h1>Server+Client Implementation - Chats App</h1><div>
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

![Screenshot 2022-05-24 at 15-55-01 Whatsapp Web Clone](https://user-images.githubusercontent.com/72495653/170186548-6ec1c18c-2215-45d8-a8e8-1fd32fa137d4.png)


- This is the second part out of four in the project for _Advanced Programming 2_ course at Bar Ilan University.
- In this part we created a web server using asp.net, which implemets the API provided.We migrated and adjusted the first part of the project (https://github.com/TomerPardi/Whatsapp-Web-Clone) to work with the server using API calls. In its current state the app supports only text communication.
We used JWT's to distinguish and differentiate between connected users, and we identify user requests by the tokens embedded in the requests.
Real time communications between users is implemented using SignalR hub in the server.
Additionally, the repository contains a rating page created using asp.net MVC, which is linked to the chat login page. 
- Authors: **Daniel Bronfman** (ID: 315901173) & **Tomer Pardilov** (ID: 316163922).

<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [React.js](https://reactjs.org/)
* [Bootstrap](https://getbootstrap.com)
* [asp.net](https://dotnet.microsoft.com/en-us/apps/aspnet)
* [SignalR](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr)
* [node.js](https://nodejs.org/en/)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

After cloning the repository you will have two folder containing the 3 parts of the project.
- The WebApplication folder contains the asp.net backend and the react on node.js frontend in the ClientApp folder.
- The RatingApp contains the asp.net MVC code of the rating page.
- To fully launch the project you need to have 2 asp.net servers running, and node.js for the react client.

### Prerequisites

Make sure to get all the dependencies for the react webclient.
* npm
  ```sh
  npm install
  ```
* Ports:
Please make sure that the rating app server is listeting on port 5081, and that nodejs listens on port 3000.



<!-- USAGE EXAMPLES -->
## Usage

We provide as batch script to launch the servers simultaneously, launch.bat located in the root folder.
 ![launch](https://user-images.githubusercontent.com/72495653/170070632-87547af1-7984-4f69-bb76-849578acb31d.gif)

Otherwise launch the asp.net servers and run npm start from the ClientApp folder.
 Rating app demo:  
![anim-opt](https://user-images.githubusercontent.com/72495653/170073636-85ecb660-0df5-44b9-b496-6eda0d9cc070.gif)
  
  
The default username/passwords are:
Username | Password
-------------------
  alice  | 123
  bob    | 456
  dan    | 123
  peter  | 123

* Limitations:
Due to the authentication using JWT and cookies only one client can be connected per browser (2 if using one incognito browser.)
For best results use Google Chrome.

<p align="right">(<a href="#top">back to top</a>)</p>




<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[product-screenshot]: images/screenshot.png
