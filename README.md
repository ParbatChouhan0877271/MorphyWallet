# MorphyWallet
Course CYB206-25S-001 WEB APPLICATION SECURITY Spring 2025 - 001
Team : Delta
Parbat Singh Chouhan – CEO & Lead Software Engineer
Sahal Mohammad – Quality Assurance Tester
Chibundu Onyekwere – Deployment & DevOps Engineer

Wallet Table
| Column Name | Type      | Description                                      |
| ----------- | --------- | ------------------------------------------------ |
| `Id`        | `int`     | Primary Key (auto-increment)                     |
| `UserId`    | `string`  | Foreign Key to `ApplicationUser` (Identity User) |
| `Balance`   | `decimal` | Current wallet balance                           |
| `User`      | `object`  | Navigation property to linked `ApplicationUser`  |


WalletTransaction Table
| Column Name       | Type       | Description                                               |
| ----------------- | ---------- | --------------------------------------------------------- |
| `Id`              | `int`      | Primary Key (auto-increment)                              |
| `WalletId`        | `int`      | Foreign Key to `Wallet`                                   |
| `TransactionType` | `enum`     | Enum: `Credit` or `Debit`                                 |
| `Amount`          | `decimal`  | Amount involved in the transaction                        |
| `Timestamp`       | `DateTime` | Auto-set to `DateTime.UtcNow` when transaction is created |
| `Wallet`          | `object`   | Navigation property to linked `Wallet`                    |


MorphyWallet/
│
├── Controllers/
│   ├── HomeController.cs
│   ├── WalletsController.cs          ← Scaffolded or custom controller
│   └── WalletTransactionsController.cs

├── Data/
│   └── ApplicationDbContext.cs       ← Includes DbSet<Wallet> and DbSet<WalletTransaction>

├── Models/
│   ├── ApplicationUser.cs            ← Inherits from IdentityUser
│   ├── Wallet.cs                     ← Wallet model linked to user
│   └── WalletTransaction.cs         ← Enum-based transaction record


├── Views/
│   ├── Home/
│   │   ├── Index.cshtml
│   │   └── AboutUs.cshtml            ← Fictional company info
│   │
│   ├── Wallets/
│   │   ├── Index.cshtml              ← List user wallets
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   ├── Delete.cshtml
│   │   └── Catalogue.cshtml          ← Product catalogue of wallet plans
│   │
│   ├── WalletTransactions/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   └── Delete.cshtml
│
├── wwwroot/
│   └── images/
│       └── morphy-logo.jpg           ← Logo shown in navbar
│
├── appsettings.json
├── Program.cs
├── README.md                         ← External project summary
├── README.txt                        ← Internal dev log (time-stamped)
