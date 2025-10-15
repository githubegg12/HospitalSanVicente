# Hospital San Vicente - Management System

A console-based application for managing patients, doctors, and appointments at the San Vicente Hospital. It features user registration with automatic email notifications.

## Features

- **Patient Management**: Register, list, search, update, and delete patients.
- **Doctor Management**: Register, list, search, update, and delete doctors.
- **Appointment Management**: Schedule, list, and cancel appointments.
- **Email Notifications**: Automatically sends a welcome email upon successful registration of a new patient or doctor.
- **Interactive Console Menu**: An easy-to-navigate command-line interface for all operations.
- **Data Persistence**: Uses a JSON file-based database to store information.

## Technologies Used

- .NET 8
- C#

## Setup and Configuration

Follow these steps to get the project up and running on your local machine.

### 1. Prerequisites

- Make sure you have the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed.

### 2. Clone the Repository

```sh
git clone https://github.com/githubegg12/HospitalSanVicente.git
cd HospitalSanVicente
```

### 3. Restore Dependencies

Open a terminal in the project's root directory and run the following command to restore the necessary NuGet packages:

```sh
dotnet restore
```

### 4. Configure the Email Service

The application uses **Gmail's SMTP server** to send email notifications. You need to configure your Gmail account and the project to allow this.

**A. On your Google Account:**

1.  **Enable 2-Step Verification**: Go to your Google Account settings, navigate to the "Security" tab, and enable 2-Step Verification. This is mandatory for generating an App Password.
2.  **Generate an App Password**: On the same "Security" page, find and click on "App passwords". Generate a new password for a custom app (e.g., "HospitalSanVicenteApp"). Google will provide a **16-digit password**. Copy it, as you will not be able to see it again.

**B. In the Project:**

1.  Open the `appsettings.json` file.
2.  Update the `SmtpSettings` section with your Gmail account details and the App Password you just generated.

```json
{
  "SmtpSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-16-digit-app-password",
    "EnableSsl": true,
    "FromEmail": "your-email@gmail.com"
  }
}
```

## How to Run the Application

Once the configuration is complete, run the application from the terminal:

```sh
dotnet run
```

The main menu will appear in the console, and you can start using the application.

## Detailed Project Structure

The project follows a layered architecture to separate concerns, making it easier to maintain and scale.

-   **`Program.cs`**: The main entry point of the application. It is responsible for building the configuration from `appsettings.json`, initializing the `EmailService`, and starting the main application loop by calling `MainMenu.Run()`.

-   **`appsettings.json`**: The configuration file for the application. It stores external settings, such as the SMTP credentials for the email service. It is configured to be copied to the output directory on build.

-   **`/Models`**: Contains the Plain Old C# Object (POCO) classes that represent the core entities of the application.
    -   `User.cs`: An abstract base class with common properties like `Id`, `FirstName`, `LastName`, etc.
    -   `Patient.cs`: Represents a patient, inheriting from `User`.
    -   `Doctor.cs`: Represents a doctor, inheriting from `User` and adding a `Specialty`.
    -   `Appointment.cs`: Represents an appointment, linking a `Patient` and a `Doctor`.

-   **`/Data`**: Manages the physical data storage.
    -   `Database.cs`: A generic class that handles the serialization and deserialization of data to and from a `database.json` file. It acts as a simple JSON-based database.

-   **`/Interfaces`**: Defines the contracts (abstractions) for the repositories. This allows for decoupling the business logic from the data access implementation.
    -   `IRepository.cs`: A generic interface defining basic CRUD (Create, Read, Update, Delete) operations.

-   **`/Repositories`**: Contains the concrete implementations of the repository interfaces. They are responsible for interacting directly with the `Database` class to manage data.
    -   `PatientRepository.cs`: Implements `IRepository<Patient>` for patient data operations.
    -   `DoctorRepository.cs`: Implements `IRepository<Doctor>` for doctor data operations.

-   **`/Services`**: Contains the application's business logic. These classes orchestrate operations, using repositories to access data and performing tasks like validation and sending notifications.
    -   `PatientService.cs`: Manages all operations related to patients (registration, listing, etc.).
    -   `DoctorService.cs`: Manages all operations related to doctors.
    -   `AppointmentService.cs`: Manages appointment scheduling and cancellation.
    -   `EmailService.cs`: Handles the sending of emails via SMTP. It is configured at startup with settings from `appsettings.json`.
    -   `MainMenu.cs`, `MenuPatient.cs`, `MenuDoctor.cs`, `MenuAppointment.cs`: These classes manage the application's flow based on user input from the console menus.

-   **`/Utils`**: Contains utility and helper classes.
    -   `MainMenuView.cs`, `MenuPatientView.cs`, etc.: Static classes responsible for displaying the different console menus and UI elements to the user.
    -   `InputValidator.cs`: Provides static methods for reading and validating user input from the console, ensuring data integrity.

-   **`HospitalSanVicente.csproj`**: The MSBuild project file. It defines project properties, target framework, and dependencies, including the NuGet packages and the rule to copy `appsettings.json` to the output directory.

## Author

- **David Felipe Vargas Varela**
- **ID**: 1140893306
- **Clan**: Caiman
- **Email**: davidvargas1224@gmail.com
