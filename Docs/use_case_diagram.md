```mermaid
usecaseDiagram
    actor "Hospital Admin" as Admin

    rectangle "Hospital Management System" {
        Admin -- (Manage Patients)
        Admin -- (Manage Doctors)
        Admin -- (Manage Appointments)

        (Manage Patients) ..> (Register Patient) : <<include>>
        (Manage Patients) ..> (List Patients) : <<include>>
        (Manage Patients) ..> (Search Patient) : <<include>>
        (Manage Patients) ..> (Update Patient) : <<include>>
        (Manage Patients) ..> (Delete Patient) : <<include>>

        (Manage Doctors) ..> (Register Doctor) : <<include>>
        (Manage Doctors) ..> (List Doctors) : <<include>>
        (Manage Doctors) ..> (Search Doctor) : <<include>>
        (Manage Doctors) ..> (Update Doctor) : <<include>>
        (Manage Doctors) ..> (Delete Doctor) : <<include>>

        (Manage Appointments) ..> (Schedule Appointment) : <<include>>
        (Manage Appointments) ..> (List Appointments) : <<include>>
        (Manage Appointments) ..> (Cancel Appointment) : <<include>>

        (Register Patient) ..> (Send Welcome Email) : <<extend>>
        (Register Doctor) ..> (Send Welcome Email) : <<extend>>
    }
```
