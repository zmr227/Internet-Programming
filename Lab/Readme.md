## Contents

### Lab 2 - Static Pages
- Create three HTML5 Pages: Home, Sitemap, Labs using new features of HTML5.
- Create Javascript page with code to apply theme(light/dark) to a page and design corresponding CSS style sheet.
- Provide links on each page to jump to the others.
- Add three check boxes for lightTheme, darkTheme and pageTheme:
  - The first two are used to change the theme, ignoring the divs with theme classes. So all pages will have the same theme. 
  - The third checkbox supports reverting to page themes. Save the state of the buttons in local storage, and retrieve from local storage on page load.

### Lab 3 - Styling with CSS
- Use CSS to build an image slide show using CSS and as little JavaScript as possible.
- Extend that to things other than images, e.g., blocks of text or web pages using an iframe.
- Use a flexbox to display the slide show on the left and text to its right. 

### Lab 4 - Javascript
Build a resizable web page previewer:
- Starting with an empty html file, add html document structure, a heading, and a suitably-placed iframe.
- Add hide, minus, and plus buttons just above the iframe, similar to the image-sizer demonstration in class.
- Add, to the right of the buttons, a textbox that allows entering a page url to preview in the iframe.
- Define, and support a default url for the previewer.
- Provide JavaScript to change the src parameter of iframe. (sites can block loads into an iframe)

### Lab 5 - React and Vue
Using React to build an image resizer component (install node.js to implement this lab):
- Build a component that can be placed on a webpage to display the image and resize it. 
- Include a script block at the end of your HTML file to trigger the Babel translation.
- Image resizer has three buttons, +, -, and H, for making a specified image larger, smaller, or toggling its visibility.
- Use React with JSX to implement the resizer component, combining JavaScript and markup, as the implementation medium. (use the Babel library to translate JSX)

### Lab 6 - Asp.Net Core
Build a server to host three static pages:
- Create a web application with dotnet new web.
- Add a service to host static pages.
- Create a Home page, Site map, and About page.
- Provide an error return if given a url that isn't matched by any file in wwwroot.
- Provide links to navigate between pages.

### Lab 7 - Asp.Net MVC and Razor Pages
Build an Asp.Net MVC basic application:
- Starting from an empty Asp.Net Core Web application
- Provide an index view and views for about and Labs.
- Provide all the styling from a single self-designed css stylesheet.

### Lab 8 - SQL Server
Build an application using SQL Server and data models with (at least) two tables that are relevant to the final project:
- Draw a database diagram the final project.
- Create a list of Views that support viewing and editing data in tables, as needed for final project.
- Define all the data models necessary for viewing and editing the project's data. For a one to many relationship between two tables, define one model for each table AND a model and view that supports selecting data from a table to add items needed for the relationship2 with the other table.
- Draw a sketch of each view from list of Views, emphasizing editing and displaying item details.

### Lab 9 - Mvc Models
Build models for the Final Project:
- Create mock data models for the final project. Each model class supports a getInstance() method that maintains an instance of the model from request to request. Note that it does not attempt to persist data on application shutdown. Instead, it seeds the model with a small amount of test data.
- Construct an Mvc application that uses the model data to populate views.
- Construct two views to demonstrate that the mock data processing works.

### Lab 10 - Entity Framework Core
Link data models to the Entity Framework:
- Create an Asp.Net Core Mvc application using single user accounts authentication.
- Define two tables linked by a one-to-many relationship.
- Create views that allow your to create entries for each table, view them, and edit them.

### Lab 11 - Authorization
Partition final project into public and private areas:
- Extend Lab 10 solution by adding Authentication and Authorization.
- Define an admin role and require that role to delete information from application table(s)
- For non-admin users hide the delete action link in your view(s)

### Lab 12 - Web Service
Build Web API site that displays files in a site storage area. Define an API that:
- Returns a list of files in a storage area. Implement this and view in Postman.
- Define methods for uploading, downloading, deleting, and modifying JSON meta data about files, based on GET, POST, and DELETE.
- Implement the action for downloading metadata, and view in Postman.

### Lab 13 - Web API and Web Client
Build Files Web Api as needed for the final project and integrate with main site controller:
- Add the Files Web Api to the final project.
- Test by uploading and displaying files on the site, using the console client from the Files Web Api demo.
