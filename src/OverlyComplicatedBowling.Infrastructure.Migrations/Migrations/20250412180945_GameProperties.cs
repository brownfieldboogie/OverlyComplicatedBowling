﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OverlyComplicatedBowling.Infrastructure.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class GameProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalScore",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "Games");
        }
    }
}
