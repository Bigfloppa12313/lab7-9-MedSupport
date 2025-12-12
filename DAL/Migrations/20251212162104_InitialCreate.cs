using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    reportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: true),
                    generatedDate = table.Column<DateTime>(nullable: false),
                    summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.reportId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 200, nullable: true),
                    email = table.Column<string>(maxLength: 200, nullable: true),
                    UserType = table.Column<string>(nullable: false),
                    employeeId = table.Column<int>(nullable: true),
                    position = table.Column<string>(nullable: true),
                    department = table.Column<string>(nullable: true),
                    MedicalStaff_position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    recordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeId = table.Column<int>(nullable: false),
                    history = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    deseaseType = table.Column<string>(nullable: true),
                    lastCheckup = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.recordId);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Users_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    scheduledId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctorId = table.Column<int>(nullable: false),
                    dateTime = table.Column<DateTime>(nullable: false),
                    available = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.scheduledId);
                    table.ForeignKey(
                        name: "FK_Schedules_Users_doctorId",
                        column: x => x.doctorId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalCheckups",
                columns: table => new
                {
                    checkupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(nullable: false),
                    doctor = table.Column<string>(nullable: true),
                    conclusion = table.Column<string>(nullable: true),
                    recordId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCheckups", x => x.checkupId);
                    table.ForeignKey(
                        name: "FK_MedicalCheckups_MedicalRecords_recordId",
                        column: x => x.recordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "recordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    notificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctorId = table.Column<int>(nullable: false),
                    dateTime = table.Column<DateTime>(nullable: false),
                    available = table.Column<bool>(nullable: false),
                    message = table.Column<string>(nullable: true),
                    scheduleId = table.Column<int>(nullable: true),
                    EmployeeuserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.notificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_EmployeeuserId",
                        column: x => x.EmployeeuserId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_doctorId",
                        column: x => x.doctorId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Schedules_scheduleId",
                        column: x => x.scheduleId,
                        principalTable: "Schedules",
                        principalColumn: "scheduledId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCheckups_recordId",
                table: "MedicalCheckups",
                column: "recordId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_employeeId",
                table: "MedicalRecords",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_EmployeeuserId",
                table: "Notifications",
                column: "EmployeeuserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_doctorId",
                table: "Notifications",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_scheduleId",
                table: "Notifications",
                column: "scheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_doctorId",
                table: "Schedules",
                column: "doctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalCheckups");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
