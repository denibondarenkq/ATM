using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        CREATE TABLE IF NOT EXISTS Admins
        (
            AdminID bigint PRIMARY KEY generated always as identity,
            Username VARCHAR(256) UNIQUE NOT NULL,
            Password TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS Customers
        (
            CustomerID bigint PRIMARY KEY generated always as identity,
            Username VARCHAR(256) UNIQUE NOT NULL,
            Password TEXT NOT NULL,
            Balance DECIMAL(18, 2) NOT NULL DEFAULT 0.00
        );

        CREATE TABLE IF NOT EXISTS Transactions
        (
            TransactionID bigint PRIMARY KEY generated always as identity,
            Username VARCHAR(256) NOT NULL REFERENCES Customers(Username),
            Type VARCHAR(256) NOT NULL,
            Amount DECIMAL(18, 2) NOT NULL
        );
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        DROP TABLE IF EXISTS Transactions;
        DROP TABLE IF EXISTS Admins;
        DROP TABLE IF EXISTS Customers;
        """;
}