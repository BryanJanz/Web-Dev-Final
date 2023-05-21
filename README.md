# Web Development Final

This is my final project for my Web Development class. This is a mock website created to showcase a shop page with Admin and User roles and features. 

The project is made from an ASP.Net Core Web App initialized from Visual Studio. The Identity and CRUD pages have been initialized with Visual Studio's scaffolding process.
Please note that the database is stored locally, as the intent of this project was not to cover the topic of databases but the functionality of Razor Pages with C# code-behind. The data is stored and queried with SQL and LINQ.

An Admin account allows for CRUD interactions with the database in the 3 models through the website while a User account allows for common customer interactions. The cart is created and used with Session state variables and is lost upon user logout.
An Admin account is only createable by uncommenting a line in the **'Register.cshtml.cs'** file, otherwise it is defaulted to create a User account upon account creation. 

As Admin, you are able to create new Genres and Platforms that are used in the Games creation. When a new Genre or Platform is created it is automatically added to the respective dropdown menu in the Games creation or edit page. Editing a Genre or Platform updates all Games that contain that data as well.

As User, you are able to add or remove items to/from your cart. If an item has 0 quantity it is removed entirely. When proceeding to checkout, the current cart items are displayed in the top right with quantity and cost. 
Shipping and customer information is required to finalize the transaction, complete with regex formulas to ensure proper information is enterred. As this is a mock website, there is no functionality to the payment system itself and proper information entered simply takes you to a Thank You page.
