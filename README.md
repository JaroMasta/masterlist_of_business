# masterlist_of_business

![Stonks](https://i.insider.com/601448566dfbe10018e00c5d?width=700)

ASP.NET Core MVC web application for managing user accounts, products, and transactions between users. It includes roles (Admin, Seller), secure password storage, inventory tracking, and transaction management.

##  Features

-  User management (Admin can create/remove users)
-  Password hashing using `BCrypt.Net`
-  Product inventory per account
-  Transaction system (each transaction decrements product quantity)
-  Role-based access (Admin vs Seller)
-  Entity Framework Core with SQLite

##  Technologies

- ASP.NET Core MVC (.NET 7/8)
- Entity Framework Core (Code First)
- SQLite
- Bootstrap
- BCrypt.Net for password hashing

##  Getting Started

### Prerequisites

- .NET 7 or newer
- SQLite
- Entity Framework Core CLI:  
dotnet tool install --global dotnet-ef



### Setup

1. **Clone the repository**
 ```bash
 git clone https://github.com/your-username/MasterlistOfBusiness.git
 cd MasterlistOfBusiness
 ```
Restore dependencies


dotnet restore
Apply database migrations


dotnet ef database update
Run the app


dotnet run
 Project Structure
Models/ — EF Core entity classes (Uzytkownik, Produkt, Transakcja, etc.)

Controllers/ — MVC Controllers for CRUD operations

Views/ — Razor views for UI rendering

Data/ — Application DB context and configuration

 Security
Passwords are not stored directly. Instead, we hash them with BCrypt before saving:


var hashedPassword = BCrypt.Net.BCrypt.HashPassword(uzytkownik.Haslo);
 Creating a Transaction
Each transaction:

Requires an existing Produkt and Konto

On save, automatically decreases Produkt.ilosc by 1

 Role-Based Access
Admin: full control (manage users)

Seller: can only create/view products and transactions linked to their account

 Database
Main entities:

Uzytkownik (login, hashed password, role)

Konto (account per seller)

Produkt (linked to Konto, includes ilosc)

Transakcja (links to a Produkt and a Konto)

 Future Improvements
JWT or cookie-based login

Product filtering/search

Better error handling and logging

Unit/integration tests

 License
MIT License. Free to use and modify.


---

##  Dokumentacja techniczna (PL)

###  Modele danych

#### `Uzytkownik`
- `login` (PK) – identyfikator użytkownika
- `HasloHash` – przechowywane hasło (zahashowane)
- `Haslo` (NotMapped) – wykorzystywane tylko przy tworzeniu
- `typ` – rola (`Admin` / `Sprzedawca`)

#### `Produkt`
- `id_produktu` (PK)
- `nazwa`, `cena`, `ilosc`
- `id_konta` – FK do konta właściciela

#### `Transakcja`
- `id_transakcji` (PK)
- `id_produktu` – FK do produktu
- `id_konta` – FK do konta kupującego

#### `Konto`
- `id_konta` (PK)
- `id_sprzedawcy` – FK do sprzedawcy
- lista produktów

---

### 
1. Użytkownik wybiera produkt z listy.
2. W `POST Transakcja/Create`:
   - Tworzona jest nowa `Transakcja`
   - Znajdowany jest `Produkt` i `ilosc` jest dekrementowana o 1
3. Zmiany zapisywane są do bazy danych

---

###  Hashowanie haseł

W kontrolerze `UzytkownikController.cs`, przed zapisaniem użytkownika:

```csharp
var hashedPassword = BCrypt.Net.BCrypt.HashPassword(uzytkownik.Haslo);
uzytkownik.HasloHash = hashedPassword;
Pole Haslo nie jest zapisywane do bazy ([NotMapped]).
 Autoryzacja
Widoki i akcje są filtrowane na podstawie zalogowanego użytkownika (User.Identity.Name)

Tylko Admin może dodawać/usunąć użytkowników

Sprzedawca widzi tylko swoje produkty

