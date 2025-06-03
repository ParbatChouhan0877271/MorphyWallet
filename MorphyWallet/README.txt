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

1130  
Updated ApplicationDbContext.cs to include DbSet<Wallet> and DbSet<WalletTransaction>.

1150  
Created a migration using Package Manager Console:
powershell
Add-Migration InitialCreate
Update-Database

Tested and confirmed tables (AspNetUsers, Wallets, WalletTransactions) created successfully.

1230  
Used Visual Studio’s Scaffolding to generate controllers and views for Wallet and WalletTransaction models with EF.

1300  
Tested /Wallets and /WalletTransactions on localhost — CRUD pages working as expected.