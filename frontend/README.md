# Healthcare Appointment Management System - Frontend

## Overview

This project represents the frontend component of the Healthcare Appointment Management System. It is developed using Next.js 14, TypeScript, and shadcn for UI components. The system offers functionalities for user authentication, appointment management, and visualizing data through charts to monitor appointments and recent activities.

## Project Structure

app
├── components
├── context
├── hoc
├── hooks
├── lib
├── public
├── stores
├── styles
└── types

## Key Components

### 1. Overview

Central hub for accessing and managing appointments.

### 2. Recent Appointments

Lists recent appointments with doctor details and appointment times.

### 3. Appointment Form

Form for booking and managing appointments.

### 4. Doctor Availability

Displays available time slots for doctors.

### 5. Doctor Detail

Detailed information about specific doctors.

### 6. User Authentication

Components for user login and registration.

## Dependencies

- Next.js 14
- TypeScript
- SWR for data fetching
- shadcn for UI components
- Axios for HTTP requests
- Tailwind CSS for styling

## Environment Variables

Create a .env file in the root of your project with:

```bash
NEXT_PUBLIC_API_URL=<Your Backend API URL>
```

## Getting Started

1. **Clone the repository:**

```bash
git clone <repository-url>
```

2. **Install dependencies:**

```bash
npm install
```

3. **Run the development server:**

```bash
npm run dev
```

4. **Build the project for production:**

```bash
npm run build
```

5. **Run the production server:**

```bash
npm start
```
