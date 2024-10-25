# Blazor Web App with Multi-Tab Login

This project demonstrates the simplest way to implement login functionality across multiple tabs in a Blazor web app with interactive server mode. The username is `user` and the password is `password`.

## What This App Does

This Blazor web app allows users to log in and maintain their authentication state across multiple browser tabs. When a user logs in or logs out in one tab, the authentication state is synchronized across all open tabs. This ensures a consistent user experience and enhances security by preventing unauthorized access in other tabs after logging out.

## Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or later

## Project Structure

- `Program.cs`: Configures services and middleware for the Blazor app.
- `CustomAuthenticationStateProvider.cs`: Custom authentication state provider using `ProtectedSessionStorage`.
- `Login.razor`: Login component for user authentication.

## Usage

1. Open the application in your browser.
2. Navigate to the `/login` page.
3. Enter the username `user` and the password `password`.
4. You will be logged in and redirected to the home page.
5. Open a new tab and navigate to the application. You will be logged in automatically.
6. To log out, navigate to `/logout` in any tab. This will log you out from all tabs.
