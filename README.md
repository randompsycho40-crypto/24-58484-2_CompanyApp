# 24-58484-2_CompanyApp

## Lab 2 — Merging Login/Register and Employee CRUD into One App

This project is the result of merging two previously separate Windows Forms applications into one unified C# application with one SQL Server LocalDB database.

The original applications were:

* **Login-and-Register** — handled user registration and login using Microsoft Access (`db_users.mdb`) through `System.Data.OleDb`.
* **EmployeeDetails** — handled Employee CRUD operations using SQL Server LocalDB through `System.Data.SqlClient`.

The final application is named **CompanyApp** and provides a complete flow:

**Login → Dashboard → Employee CRUD → Logout → Login**

The application uses one SQL Server LocalDB database named **`dbCompanyApp`**.

---

# 1. Before and After

## Before the Merge

There were two independent applications.

### Application 1 — Login-and-Register

* `frmLogin`
* `frmRegister`
* `frmDashboard`
* Microsoft Access database: `db_users.mdb`
* Data provider: `System.Data.OleDb`
* Older .NET Framework version: 4.7.2
* Separate `Program.cs` and application entry point

### Application 2 — EmployeeDetails

* Employee CRUD form
* `Employee.cs`
* SQL Server LocalDB
* Data provider: `System.Data.SqlClient`
* .NET Framework 4.8
* Separate `Program.cs` and application entry point

Because the two applications used different databases, namespaces, providers, framework versions and entry points, they could not simply be copied together.

## After the Merge

The final application is:

**CompanyApp**

It contains:

* `frmLogin`
* `frmRegister`
* `frmDashboard`
* `frmEmployee`
* `User.cs`
* `Session.cs`
* `Employee.cs`
* One `Program.cs`
* One `App.config`
* One SQL Server LocalDB database: `dbCompanyApp`

The final flow is:


Application Start
       ↓
   frmLogin
       ↓
Validate Username + Password
       ↓
   frmDashboard
       ↓
 Manage Employees
       ↓
   frmEmployee
       ↓
Employee CRUD
       ↓
    Logout
       ↓
 New frmLogin


---

# 2. The Six Conflicts and How They Were Solved

The assignment identified six major conflicts that had to be resolved before merging the projects.

## Conflict 1 — Different Namespaces

The Login-and-Register project used:


Login_and_Register


while the EmployeeDetails project used:


EmployeeDetails


A Windows Form consists of partial class declarations across multiple files. Therefore, simply copying the forms without changing their namespaces would cause compilation errors.

### Solution

The imported Login/Register form files were changed to use the host project's namespace:


EmployeeDetails


This was done for both the main `.cs` file and its corresponding `.Designer.cs` file for each imported form.

The root namespace was intentionally left as `EmployeeDetails` according to the assignment requirement, while the project, folder and assembly were renamed to **CompanyApp**.

---

## Conflict 2 — Different Data Providers

The Login-and-Register application used:


System.Data.OleDb


because it connected to Microsoft Access.

The EmployeeDetails application used:


System.Data.SqlClient


because it connected to SQL Server LocalDB.

`OleDbConnection` could not be used to communicate with the SQL Server database in the final application.

### Solution

The Login/Register database access code was completely migrated from **OleDb to SqlClient**.

The final application uses:


SqlConnection
SqlCommand
SqlDataReader
SqlDataAdapter


and SQL Server named parameters such as:


@username
@password


instead of Access/OleDb positional `?` parameters.

The old `.mdb` database and OleDb dependency were removed from the final application.

---

## Conflict 3 — Two Databases

Originally there were two separate data stores:


db_users.mdb
        +
SQL Server Employee database


These databases could not directly provide the unified relationship required by the assignment.

### Solution

A single SQL Server LocalDB database was created:


dbCompanyApp


It contains both:


dbo.Users
dbo.Emp_details


The two tables are connected through the `CreatedBy` foreign key.

This allows the authenticated user from the Login/Register part of the application to be connected with employees created through the Employee CRUD part.

---

## Conflict 4 — Different Framework Versions

The Login-and-Register project targeted:


.NET Framework 4.7.2


while EmployeeDetails targeted:


.NET Framework 4.8


### Solution

The EmployeeDetails project was selected as the host project because it already targeted **.NET Framework 4.8**.

The older Login/Register forms were imported into this newer host project.

The final merged project therefore uses one framework version rather than maintaining two separate project targets.

---

## Conflict 5 — Two `Program.cs` / `Main()` Methods

Each original project had its own `Program.cs` and its own `Main()` method.

A single executable cannot have two competing entry points.

### Solution

The EmployeeDetails project was used as the host.

The imported Login/Register `Program.cs` was not copied.

Only one `Program.cs` remains in the final project, with the application starting from:


Application.Run(new frmLogin());


Therefore, the application starts with the Login form instead of directly opening the registration or employee form.

---

## Conflict 6 — Hidden `.mdb` File Dependency

The original Login/Register project depended on:


db_users.mdb


which existed under `bin\Debug` and was not properly part of the project structure.

This meant that cleaning the solution could remove the file and break the login system.

### Solution

The Access database dependency was completely removed.

The Access user records were migrated into:


dbCompanyApp
    └── dbo.Users


The final application therefore does not depend on an `.mdb` file.

---

# 3. Unified Database Design

The final database is:


dbCompanyApp


The database contains two main tables.

## `dbo.Users`

The Users table stores registered accounts.

Important columns:

| Column      | Purpose                           |
| ----------- | --------------------------------- |
| `UserID`    | Primary key generated by IDENTITY |
| `Username`  | Unique login username             |
| `Password`  | Stored password value             |
| `CreatedAt` | Account creation time             |

The primary key is:


UserID


and `Username` has a unique constraint.

## `dbo.Emp_details`

The employee table stores employee information.

Important columns:

| Column       | Purpose                                     |
| ------------ | ------------------------------------------- |
| `EmpId`      | Employee primary key                        |
| `EmpName`    | Employee name                               |
| `EmpAge`     | Employee age                                |
| `EmpContact` | Contact information                         |
| `EmpGender`  | Gender                                      |
| `CreatedBy`  | UserID of the user who created the employee |

The relationship is:


Users.UserID
      ↓
Emp_details.CreatedBy


The foreign key is:


FK_Emp_CreatedBy


`CreatedBy` is nullable because employees migrated from the old database do not necessarily have a known creator.

The complete database creation script is included in the repository as:


Schema.sql


---

# 4. Access Data Migration

The original Login/Register application stored user accounts in:


db_users.mdb


The accounts were migrated into:


dbo.Users


The migration used INSERT statements such as:


INSERT INTO dbo.Users (Username, Password)
VALUES (...);


`UserID` was intentionally not supplied during migration.

This allowed SQL Server's:


IDENTITY(1,1)


column to automatically generate the new `UserID`.

This was important because `UserID` is later used by `Emp_details.CreatedBy`.

After migration, the existing Login/Register accounts were available from the unified SQL Server database.

---

# 5. Importing the Forms — The Three-File Rule

A Windows Forms form is not only one `.cs` file.

Each form consists of three related files:


frmLogin.cs
frmLogin.Designer.cs
frmLogin.resx


The same structure applies to the other imported forms.

Therefore, the forms were imported using the **three-file rule**.

For example:


frmLogin.cs
frmLogin.Designer.cs
frmLogin.resx


were kept together.

Only the `.cs` files were added through Visual Studio's **Add → Existing Item** process because Visual Studio can automatically associate/nest the corresponding Designer and resource files when the project structure is correct.

The second `.csproj`, `Program.cs`, `App.config`, `Properties` folder and unrelated project files were not imported.

This ensured that the final solution remained:


ONE PROJECT
ONE EXE
ONE ENTRY POINT


rather than becoming two separate applications.

---

# 6. Namespace Fixing

After importing the forms, the Login/Register files still contained the original namespace:


namespace Login_and_Register


This had to be changed to the host project's namespace:


namespace EmployeeDetails


This change was made consistently across the imported form files.

The `.Designer.cs` files were especially important because they are partial declarations of the same form class.

If one part used a different namespace, the partial class declarations would no longer belong to the same class.

After correcting the namespaces, the project was rebuilt to verify that the imported forms compiled correctly.

---

# 7. OleDb → SqlClient Migration

The Login/Register portion was originally written for Microsoft Access.

The old approach used:


System.Data.OleDb


and an Access connection string.

The final application uses:


System.Data.SqlClient


with SQL Server LocalDB.

The connection is based on the unified database:


dbCompanyApp


The SQL queries were also changed from Access/OleDb style to SQL Server style.

For example, named parameters are used:


@username
@password


instead of positional:


?


The Login and Register database operations were moved into `User.cs`, following the same general data-access style used by `Employee.cs`.

---

# 8. `User.cs`

A separate `User.cs` class was used to keep Login/Register database operations organized.

Its main responsibilities are:

### `ValidateLogin()`

Checks the supplied username and password against the Users table.

On successful login, it returns the corresponding:


UserID


instead of only returning `true` or `false`.

Returning the `UserID` is important because the Employee CRUD section needs to know which user created an employee.

### `UsernameExists()`

Uses a database query with `ExecuteScalar()` to determine whether a username is already registered.

### `RegisterUser()`

Inserts a new account into:


dbo.Users


This keeps database operations separate from the form UI logic.

---

# 9. `Session.cs`

A small static `Session` class was introduced to keep track of the currently logged-in user.

It stores:


UserID
Username


The basic idea is:


Successful Login
      ↓
Session.UserID = logged-in UserID
Session.Username = logged-in Username


The session can then be accessed by the Employee CRUD form.

A `Clear()` method is also used during logout so that the previous user's information is removed.

This allows the Employee form to identify the current user without asking for the username again.

---

# 10. Login → Dashboard → CRUD → Logout Flow

The final application uses the following navigation.

## Step 1 — Application Start

The only entry point starts:


frmLogin


The user cannot directly start at the Employee CRUD screen.

## Step 2 — Login

The user enters:


Username
Password


`ValidateLogin()` checks the credentials.

If valid:


Session.UserID
Session.Username


are populated.

Then:


frmDashboard


is shown and the Login form is hidden.

## Step 3 — Dashboard

The Dashboard contains the option to manage employees.

The **Manage Employees** button opens:


frmEmployee


## Step 4 — Employee CRUD

The user can perform the Employee CRUD operations through the Employee form.

When a new employee is added, the application sets:


employee.CreatedBy = Session.UserID;


This connects the employee record with the currently logged-in user.

## Step 5 — Logout

Logout does not terminate the entire application.

Instead:

1. A confirmation dialog is displayed.
2. The session is cleared.
3. A new Login form is shown.
4. The Dashboard is closed.

The Login form also handles its closing behavior so that the application process does not remain running invisibly.

The result is:


Login
  ↓
Dashboard
  ↓
Employee CRUD
  ↓
Logout
  ↓
Login


---

# 11. How `CreatedBy` Works

`CreatedBy` connects the two originally separate applications.

When a user logs in:


Users.UserID
      ↓
Session.UserID


When that user adds an employee:


Session.UserID
      ↓
employee.CreatedBy
      ↓
Emp_details.CreatedBy


For example:


UserID = 3
Username = admin


If that user creates an employee:


Emp_details.CreatedBy = 3


The database therefore knows which registered user created the employee.

This is better than storing the username directly because `UserID` is a stable integer primary key and is specifically designed to be referenced by the foreign key.

---

# 12. Why `LEFT JOIN` Instead of `JOIN`

The Employee grid needs to display the username of the person who created each employee.

The relationship is:


Emp_details.CreatedBy
        ↓
Users.UserID


The query uses:


LEFT JOIN Users u
    ON e.CreatedBy = u.UserID


A `LEFT JOIN` was selected instead of an inner `JOIN` because `CreatedBy` is nullable.

Some employees may have been migrated from the previous system without a known creator.

For those rows:


CreatedBy = NULL


An inner join would remove those employees from the result.

A `LEFT JOIN` keeps every employee record and displays the creator's username when a matching user exists.

The result is exposed to the grid using:


u.Username AS CreatedBy


This also allows the grid to display the creator while keeping the database relationship based on the numeric `UserID`.

---

# 13. Real Build Issue Encountered

During the form-import process, one important problem was related to the relationship between the Windows Forms code file and its Designer file.

The form code depends on controls declared in:

```text
frmLogin.Designer.cs
```

For example, controls such as the username textbox are generated and declared in the Designer file.

When the Designer file is not correctly associated with the main form file, Visual Studio can produce errors such as:

```text
The name 'txtUsername' does not exist in the current context
```

The problem was fixed by ensuring that the form's three files were correctly brought into the host project and that the `.cs` and `.Designer.cs` files used the same namespace and partial class.

After correcting the project structure and namespaces, the solution was rebuilt and the forms compiled correctly.

This demonstrated why the three-file rule and namespace consistency are important when importing Windows Forms from another project.

---

# 14. Why One Database Is Better Than Two

Using one database makes the two parts of the application consistent and allows them to share relationships. With two separate databases, the Login system would know about users while the Employee system would maintain employee data independently, making it difficult to reliably identify which user created an employee. The unified database solves this through the `Users.UserID` and `Emp_details.CreatedBy` foreign-key relationship. The `LEFT JOIN` is a direct example of this benefit because employee records can be displayed together with their creator's username from the same database. It also eliminates the old Access `.mdb` dependency and allows the whole application to use one SQL Server data provider and one connection string.

---

# 15. Single Connection String

The final application uses one connection string in:


App.config


The connection points to:


(localdb)\MSSQLLocalDB


and:


dbCompanyApp


The application accesses it through:


ConfigurationManager


Hard-coded connection strings were removed from the application code.

The project was also checked for old Access/OleDb connection information so that the final application does not accidentally depend on the old database.



# 16. Final Project Structure

The important structure of the merged application is:


24-58484-2_CompanyApp
│
├── App.config
├── Program.cs
├── User.cs
├── Session.cs
├── Employee.cs
│
├── frmLogin.cs
├── frmLogin.Designer.cs
├── frmLogin.resx
│
├── frmRegister.cs
├── frmRegister.Designer.cs
├── frmRegister.resx
│
├── frmDashboard.cs
├── frmDashboard.Designer.cs
├── frmDashboard.resx
│
├── frmEmployee.cs
├── frmEmployee.Designer.cs
├── frmEmployee.resx
│
├── Schema.sql
├── README.md
└── .gitignore


Generated folders such as:


bin/
obj/
.vs/


are excluded from the GitHub submission.

The old:


db_users.mdb


and `OleDb` dependency are also not part of the final application.

---

# 17. Screenshots

The following screenshots document the completed implementation.

## 17.1 Unified Database — Object Explorer

Show:

```text
dbCompanyApp
├── Tables
│   ├── dbo.Users
│   └── dbo.Emp_details
```

**Screenshot:**
`[Insert screenshot here]`

---

## 17.2 Users Table — View Data

Show the migrated Login/Register accounts inside:

```text
dbo.Users
```

**Screenshot:**
`[Insert screenshot here]`

---

## 17.3 Solution Explorer — Imported Forms

Show the project with the form files correctly associated/nested:

```text
frmLogin.cs
    ├── frmLogin.Designer.cs
    └── frmLogin.resx

frmRegister.cs
    ├── frmRegister.Designer.cs
    └── frmRegister.resx

frmDashboard.cs
    ├── frmDashboard.Designer.cs
    └── frmDashboard.resx
```

**Screenshot:**
`[Insert screenshot here]`

---

## 17.4 Login → Dashboard Flow

Show the working application flow:

```text
Login
  ↓
Dashboard
```

**Screenshot:**
`[Insert screenshot here]`

---

## 17.5 Employee CRUD

Show the Employee management screen with working CRUD functionality.

**Screenshot:**
`[Insert screenshot here]`

---

## 17.6 Creator Display

Show the Employee grid displaying the creator through the `CreatedBy` relationship.

Example:

```text
EmpId | EmpName | EmpAge | ... | CreatedBy
------------------------------------------------
E001  | ...     | ...    | ... | admin
```

**Screenshot:**
`[Insert screenshot here]`

---

## 17.7 Logout Flow

Show that selecting Logout returns the user to a new Login screen rather than terminating the application unexpectedly.

**Screenshot:**
`[Insert screenshot here]`

---

# 18. Conclusion

The original Login/Register and EmployeeDetails applications were successfully combined into one Windows Forms application.

The final **CompanyApp** uses:

* One project
* One executable
* One entry point
* One SQL Server LocalDB database
* One connection string
* `System.Data.SqlClient`
* A unified `Users` and `Emp_details` database design
* Session-based user identification
* `CreatedBy` foreign-key relationship
* Login, Register, Dashboard, Employee CRUD and Logout flow

The merge removed the dependency on the old Access `.mdb` database and allowed the authentication and Employee management systems to work together as a single application.
