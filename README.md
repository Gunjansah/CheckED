# CheckED Event Planner

CheckED is a cross-platform mobile event planning application developed using .NET MAUI (Multi-platform App UI). It aims to streamline event organization and enhance user access to event information. The application caters to two primary user roles: general users and event organizers (admins).

## Features

*   📱 Cross-platform compatibility (iOS, Android, macOS, Windows)
*   👤 User authentication (registration, login, logout)
*   📅 Event creation, editing, and management
*   ✉️ RSVP management and guest list tracking
*   🔔 Notifications and reminders (push and potentially email)
*   🎫 Guest check-in via QR code scanning
*   💰 Event budget planning and tracking
*   💾 Offline data persistence using SQLite

## Technologies

*   **.NET MAUI:** Cross-platform UI framework
*   **C#:** Programming language
*   **SQLite:** Local database for data storage
*   **QR Code Library:** (Specific library to be determined)
*   **Notification Service:** (Platform-specific or third-party, e.g., Firebase Cloud Messaging)

## Architecture

CheckED utilizes a modular architecture with the following key components:

*   **Data Model:**
    *   **User:** Stores user information (email, password, role).
    *   **Event:** Stores event details (name, date, time, location, description).
    *   **Guest:** Stores guest information (name, contact details).
    *   **RSVP:** Tracks guest RSVPs (status, preferences).
    *   **Budget:** Stores budget information (categories, expenses).
*   **User Authentication Module:** Handles user registration, login, and secure credential storage (hashed passwords).
*   **Event Management Module:** Provides UI for event organizers to manage events, RSVPs, and guest lists.
*   **Notification Module:** Implements notifications and reminders using platform-specific or third-party services.
*   **QR Code Module:** Generates QR codes for events and implements QR code scanning for guest check-in.

## Technical Details

*   **Data Persistence:** SQLite is used for local data storage, ensuring offline availability.
*   **User Interface:** .NET MAUI's UI controls (Labels, Entry fields, Buttons, Lists, CollectionView) and layout panels (Grid, StackLayout) are used to create a user-friendly interface.
*   **Security:** User passwords are securely stored using hashing.
*   **Notifications:** Platform-specific APIs (e.g., Firebase Cloud Messaging) or a cross-platform notification service will be used.
*   **QR Code Functionality:** A suitable .NET library will be integrated for QR code generation and scanning.

## Development Environment

*   Visual Studio (or Visual Studio Code for Mac)
*   .NET SDK 8.0

## Installation and Running

To build and run CheckED, follow these steps:

1.  **Install the .NET SDK 8.0:** Download and install the latest .NET SDK 8.0 from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).

2.  **Install .NET MAUI Workloads:** Open a terminal or command prompt and run the following command:

    ```bash
    dotnet workload install maui
    ```

3.  **Clone the Repository:** Clone the CheckED repository to your local machine:

    ```bash
    git clone https://github.com/Gunjansah/CheckED.git
    ```

4.  **Navigate to the Project Directory:** Change to the directory containing the project's `.csproj` file:

    ```bash
    cd CheckED # If you cloned directly into the current directory
    ```

5.  **Build the Project:** Build the project using the .NET CLI:

    ```bash
    dotnet build -t:Run -f net8.0-android # For Android
    dotnet build -t:Run -f net8.0-ios # For iOS (requires a Mac)
    dotnet build -t:Run -f net8.0-windows10.0.19041.0 # For Windows
    dotnet build -t:Run -f net8.0-maccatalyst # For macOS (requires a Mac)
    ```
    Choose the target framework (Android, iOS, Windows, or macOS). Note that building for iOS and macOS requires a Mac with Xcode installed.

6. **Alternatively, Build and run with Visual Studio:** Open the solution file (.sln) in Visual Studio and select the target platform in the debug dropdown menu and press the Run button.

## Usage (Planned)

*   Users can register and log in to the application.
*   Event organizers can create, edit, and manage events.
*   Users can view event details and RSVP.
*   Users will receive notifications and reminders about events.
*   Event organizers can use QR code scanning for guest check-in.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

(Add License Information Here - e.g., MIT License)

## Acknowledgements

*   .NET MAUI team
*   (Add any other acknowledgements)
