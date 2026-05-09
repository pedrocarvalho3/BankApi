using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApi.Infrastructure.Migrations
{
    public partial class RenameCustomersToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM information_schema.tables
                        WHERE table_schema = 'public'
                          AND table_name = 'Customers'
                    ) THEN
                        ALTER TABLE "Customers" RENAME TO "Users";
                    END IF;
                END
                $$;
                """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1
                        FROM information_schema.tables
                        WHERE table_schema = 'public'
                          AND table_name = 'Users'
                    ) THEN
                        ALTER TABLE "Users" RENAME TO "Customers";
                    END IF;
                END
                $$;
                """);
        }
    }
}
