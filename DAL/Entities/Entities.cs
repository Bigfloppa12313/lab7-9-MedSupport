using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Catalog.DAL.Entities
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        private string password { get; set; }
        private string login { get; set; }

        public virtual bool loginUser(string _login, string _password)
        {
            return login == _login && password == _password;
        }
        public virtual void logout() { }
    }

    public class Admin : User
    {
        public void manageUsers() { }
        public void setSchedule() { }
        public void backupData() { }
        public void generateReports() { }
    }

    public class Employee : User
    {
        public int employeeId { get; set; }
        public string position { get; set; }
        public string department { get; set; }

        public void registerForCheckup() { }
        public void viewownHistory() { }
        public void receiveNotification() { }

        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }

    public class MedicalStaff : User
    {
        private int doctorId { get; set; }
        public string position { get; set; }

        public void viewHistory() { }
        public void receiveNotification() { }
        public void acceptCheckup() { }
        public void denyCheckup() { }
        public void completeCheckup() { }

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }

    public class Schedule
    {
        [Key]
        public int scheduledId { get; set; }
        public int doctorId { get; set; }
        public DateTime dateTime { get; set; }
        public bool available { get; set; }

        public void reserveSlot() { }
        public void cancelSlot() { }

        [ForeignKey("doctorId")]
        public virtual MedicalStaff Doctor { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }

    public class Report
    {
        [Key]
        public int reportId { get; set; }
        public string type { get; set; }
        public DateTime generatedDate { get; set; }
        public string summary { get; set; }

        public void generateReport() { }
        public void analyzeTrends() { }
    }

    public class Notification
    {
        [Key]
        public int notificationId { get; set; }
        public int doctorId { get; set; }
        public DateTime dateTime { get; set; }
        public bool available { get; set; }
        public string message { get; set; }

        public int? scheduleId { get; set; }

        [ForeignKey("doctorId")]
        public virtual MedicalStaff Doctor { get; set; }
        [ForeignKey("scheduleId")]
        public virtual Schedule Schedule { get; set; }
    }

    public class MedicalRecord
    {
        [Key]
        public int recordId { get; set; }
        public int employeeId { get; set; }
        public string history { get; set; }
        public string type { get; set; }
        public string deseaseType { get; set; }
        public DateTime lastCheckup { get; set; }

        [ForeignKey("employeeId")]
        public virtual Employee Employee { get; set; }
        public virtual ICollection<MedicalCheckup> MedicalCheckups { get; set; }

        public string getHistory() => history;
    }

    public class MedicalCheckup
    {
        [Key]
        public int checkupId { get; set; }
        public DateTime date { get; set; }
        public string doctor { get; set; }
        public string conclusion { get; set; }

        public int? recordId { get; set; }
        [ForeignKey("recordId")]
        public virtual MedicalRecord MedicalRecord { get; set; }
    }
}
