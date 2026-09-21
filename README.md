
```markdown
# Notification & Payment Auditing Engine

A practical ASP.NET Core 8 Web API demonstrating deep architectural patterns around **Dependency Injection (IoC)**, service lifetime behaviors, keyed service resolutions, factory-based runtime instantiation, manual startup scopes, and clean modular configuration.

---

## 📌 Architecture & Design Highlights

This engine implements the core foundational patterns of modern .NET dependency injection:

1. **Service Lifetime Verification (Transient, Scoped, Singleton):**
   - Validates instance generation rules and state persistence across HTTP requests using specialized GUID service implementations (`IGuidService`).
   - Uses an aggregation service (`AuditService`) to benchmark instance equality and scope isolation.

2. **Clean Registration Extensions:**
   - Adheres to the **Single Responsibility Principle** by moving DI configuration out of `Program.cs` and encapsulating registrations inside dedicated extension methods within `Microsoft.Extensions.DependencyInjection`.

3. **Multiple Registrations & The Composite Pattern (`IEnumerable<T>`):**
   - Registers multiple channel implementations (`SmsNotificationSender`, `EmailNotificationSender`) under the unified `INotificationSender` contract.
   - Resolves all senders simultaneously inside `NotificationDispatcher` to broadcast messages across all active channels.

4. **Keyed Services & Dynamic Runtime Factory:**
   - Implements payment processing strategies (`StripeGateway`, `PayPalGateway`) registered via .NET 8 **Keyed Services** (`AddKeyedScoped`).
   - Leverages an `IServiceProvider` factory delegate that dynamically resolves the active payment gateway at runtime based on `DefaultPaymentProvider` configured in `appsettings.json`.

5. **Manual Startup Scope Execution:**
   - Demonstrates safe consumption of scoped services before the HTTP server starts listening by creating a manual boundary scope (`CreateScope()`) to run `DatabaseInitializer`.

---

## 📂 Project Structure

```text
Notification-Payment-Auditing-Engine/
│
├── Database_initializerTask/
│   └── DatabaseInitializer.cs            # Startup routine service
│
├── Keyed_Services/
│   ├── IPaymentGateway.cs                # Payment abstraction
│   ├── PayPalGateway.cs                  # Keyed implementation: "paypal"
│   ├── StripeGateway.cs                  # Keyed implementation: "stripe"
│   └── DependencyInjectionPayment.cs     # Keyed registrations & factory delegate
│
├── Multiple_Registeration_Task/
│   ├── INotificationSender.cs            # Notification abstraction
│   ├── EmailNotificationSender.cs        # Email delivery implementation
│   ├── SmsNotificationSender.cs          # SMS delivery implementation
│   ├── NotificationDispatcher.cs         # Composite dispatcher (IEnumerable<T>)
│   └── DependencyInjectionNotification.cs# Notification registrations
│
├── Guid_Lifetimes/ (DI Services)
│   ├── IGuidService.cs                   # GUID contract
│   ├── TransientGuidService.cs           # Instantiated per injection
│   ├── ScopedGuidService.cs              # Instantiated once per HTTP request
│   ├── SingletonGuidService.cs           # Instantiated once per app lifetime
│   ├── AuditService.cs                   # Aggregates GUID services for verification
│   └── DependencyInjectionExtensions.cs  # Lifetime registrations
│
├── appsettings.json                      # System & provider configuration
└── Program.cs                            # Application pipeline & Minimal API routes

```

---

## ⚙️ Configuration

Set your default payment gateway dynamically inside `appsettings.json`:

```json
{
  "DefaultPaymentProvider": "stripe",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

> Supported values for `DefaultPaymentProvider`: `"stripe"` | `"paypal"`

---

## 🚀 API Endpoints

### 1. Test Lifetimes

* **Endpoint:** `GET /testLifeTime`
* **Description:** Reads the generated GUIDs across the three lifetimes from `AuditService` to confirm instance recreation vs persistence.
* **Response Example:**
```text
9a6e1111-2345-4b11-92b0-8c9e0d1e2f3a Singleton 
4b3c2222-6789-4d22-83c1-9d8e1f2a3b4c Scoped 
7f8e3333-1011-4a33-74d2-0e9f2a3b4c5d Transient

```



### 2. Multi-Channel Notification Broadcast

* **Endpoint:** `GET /MultipleRegistrations/{msg}`
* **Description:** Iterates through every registered `INotificationSender` and returns delivery statuses.
* **Example Call:** `GET /MultipleRegistrations/OrderConfirmed`
* **Response Example:**
```json
[
  "SmsNotificationSender : OrderConfirmed",
  "EmailNotificationSender : OrderConfirmed"
]

```



### 3. Keyed Payment Gateway Processing

* **Endpoint:** `GET /keyedServices/{amount}`
* **Description:** Executes payment processing through the gateway resolved by the runtime factory based on `appsettings.json`.
* **Example Call:** `GET /keyedServices/250`
* **Response Example:**
```text
Processed 250 with StripeGateway

```



---

## 🛠️ How to Run Locally

1. **Clone the repository:**
```bash
git clone [https://github.com/AbdallahNasrat/Notification-Payment-Auditing-Engine.git](https://github.com/AbdallahNasrat/Notification-Payment-Auditing-Engine.git)
cd Notification-Payment-Auditing-Engine

```


2. **Restore dependencies & run:**
```bash
dotnet restore
dotnet run

```


3. **Verify startup execution:**
Check your console on application launch to see the scoped startup initializer output:
```text
Database and Seed Data Initialized Successfully
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000

```



---

## 💡 Key Architectural Takeaways

* **Captive Dependencies Prevention:** Ensuring services with shorter lifecycles (`Transient` / `Scoped`) are never captured inside `Singleton` classes, safeguarding against thread-safety bugs and data leaks.
* **Runtime Resolution:** Using factory delegates `(sp => ...)` to combine configuration checks with DI resolutions dynamically.
* **Native .NET 8 Features:** Using native Keyed Services (`AddKeyedScoped` and `GetRequiredKeyedService`) instead of relying on custom dictionary workarounds.

```
