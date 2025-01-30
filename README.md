## ATM

### Functional Features
- Ability to create new accounts
- View the balance of an existing account
- Withdraw money from an account
- Deposit money into an account
- Access and view transaction history

### Operational Features
- Interactive console-based user interface
- Option to choose between two modes: user mode or administrator mode
    - In user mode, account details (account number, PIN) are required to proceed
    - In administrator mode, a system password is required
        - Incorrect password entry will terminate the system's operation
- System password can be configured dynamically
- Error messages are displayed for any invalid or prohibited operations
- Persistent data storage using a PostgreSQL database
- Application structure is built on hexagonal architecture principles
