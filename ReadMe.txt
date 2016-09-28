This application was created with:
-	MS Visual Studio 2013
-	MVC 5
-	Entity Framework Code First
-	SQL Server 2008

Application's config file web.config contains setting 'PreCreateUsers' which value is 'true' by default,
what invokes creation of three demo clients and related objects.

Instructions:
There are two navigation items: 'Clients' and 'Orders'.
'Clients' obviously displays the list of clients existing in DB. Application user can search client by first or last name by filling the field and pressing button 'Search'.
There are several actions on the view which allow to create new client, delete or edit existing client. It also possible to proceed to client's card by pressing corresponding
button opposite needed client. Primary application operations are mainly avalaible in client card. Application user have to create new car and then he'll be able to create new order
by pressing corresponding button opposite created car. Or, he may proceed to the car detailed card and create order from there.
'Orders' navigation item displays the list of all orders. User can filter orders by their status. This navigation item mainly used to do quick operations with an order, avoiding client card,
such as changing it's status or something else. But creation of a new order is only available in a car or client card.


