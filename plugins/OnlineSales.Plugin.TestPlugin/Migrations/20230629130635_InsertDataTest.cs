﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;
using OnlineSales.Plugin.TestPlugin.TestData;

#nullable disable

namespace OnlineSales.Plugin.TestPlugin.Migrations
{
    /// <inheritdoc />
    public partial class InsertDataTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var insertData = new object[ChangeLogMigrationsTestData.InsertionData.Count, 8];
            for (int i = 0; i < ChangeLogMigrationsTestData.InsertionData.Count; i++)
            {
                insertData[i, 0] = i+1;
                insertData[i, 1] = DateTime.UtcNow;
                insertData[i, 2] = null;
                insertData[i, 3] = null;
                insertData[i, 4] = ChangeLogMigrationsTestData.InsertionData[i];
                insertData[i, 5] = null;
                insertData[i, 6] = null;
                insertData[i, 7] = null;
            }

#pragma warning disable SA1118 

            migrationBuilder.InsertData(
                table: "test_entity",
                columns: new[] { "id", "created_at", "created_by_ip", "created_by_user_agent", "string_field", "updated_at", "updated_by_ip", "updated_by_user_agent" },
                values: insertData);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // not needed
        }
    }
}
