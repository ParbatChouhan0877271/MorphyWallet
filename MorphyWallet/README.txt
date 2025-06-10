MorphyWallet Development Log (ISO 8601) 


2025-06-03
1000  
Created a new .NET 8.0 MVC project using Visual Studio with Individual Accounts authentication.  
Localhost URL: https://localhost:xxxx/  

1015  
Verified that the project builds and runs successfully in development mode.

1030  
Reviewed the auto-generated ApplicationDbContext, Identity setup, and confirmed the database connection string is using LocalDB.

1050  
Added a new model class ApplicationUser inheriting from IdentityUser.

1110  
Created two new model classes Wallet and WalletTransaction. Established a relationship between ApplicationUser and Wallet using foreign key UserId.

1120  
Updated ApplicationDbContext.cs to include DbSet<Wallet> and DbSet<WalletTransaction>.

1130  
Created a migration using Package Manager Console:
powershell
Add-Migration InitialCreate
Update-Database

Tested and confirmed tables (AspNetUsers, Wallets, WalletTransactions) created successfully.

1140 
Used Visual Studio’s Scaffolding to generate controllers and views for Wallet and WalletTransaction models with EF.

1150  
Tested /Wallets and /WalletTransactions on localhost — CRUD pages working as expected.

1155
Created a new Catalogue() action in WalletsController to simulate 5 wallet plans.

1200
Built a new view Catalogue.cshtml displaying a table with 7 aligned fields: Plan ID, Name, Balance, Limit, Description, Status, Created Date.

1205
Tested catalogue display, styling, and responsiveness with Bootstrap — works smoothly.

1210
Added a new controller action AboutUs() in HomeController.cs.

1215
Created AboutUs.cshtml with fictional company info:

Company: Morphy

CEO: Parbat Singh Chouhan

Tester: Sahal Mohammad

Deployment Dev: Chibundu Onyekwere

HQ: Jodhpur, Rajasthan, India

1220
Updated _Layout.cshtml to include links to "Product Catalogue" and "About Us".

1225
Uploaded logo file (morphy-logo.jpg) to wwwroot/images/.

1230
Updated _Layout.cshtml to display logo next to brand name using:

html
Copy
<img src="~/morphy-logo.jpg" style="height: 40px;" />

1235
Tested all updates — logo, catalogue, About Us — working successfully. 

1240
Add th README.md file in github

1243
Ready to Submit.....
..