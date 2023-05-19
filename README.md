# 245Final

This is my final project for my Web Development class.

The project is made from an ASP.Net Core Web App initialized from Visual Studio. The Identity and CRUD pages have been initialized with Visual Studio's scaffolding process.
Please note that the database is stored locally, as the intent of this class was not to cover the topic of databases, therefore the only data that remains is within the **'wwwroot/images'** folder.

This is a mock website created to showcase a shop page with Admin and User roles and features. 
An Admin account allows for CRUD interactions with the database in the 3 models through the website while a User account allows for common customer interactions. The cart is created and used with Session state variables and is lost upon user logout.
An Admin account is only createable by uncommenting a line in the **'Register.cshtml.cs'** file, otherwise it is defaulted to create a User account upon account creation. 
