using System;
using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTicManagementSystem.Repositories;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Controllers
{
    internal class StudentController

    {
        //  ====================================================================== Add =================================================================================
        public string AddStudentWithUser(Student student)
        {
            using (var conn = new SQLiteConnection(@"Data Source=UnicomTic.db;Version=3;"))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        
                        var userCmd = new SQLiteCommand("INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role);", conn);
                        userCmd.Parameters.AddWithValue("@Username", student.Username);
                        userCmd.Parameters.AddWithValue("@Password", student.Password);
                        userCmd.Parameters.AddWithValue("@Role", student.Role);
                        userCmd.ExecuteNonQuery();

                        
                        student.UserId = (int)conn.LastInsertRowId;

                       
                        var studentCmd = new SQLiteCommand("INSERT INTO Students (Name, Address, Age, CourseID, UserID) VALUES (@Name, @Address, @Age, @CourseID, @UserID);", conn);
                        studentCmd.Parameters.AddWithValue("@Name", student.Name);
                        studentCmd.Parameters.AddWithValue("@Address", student.Address);
                        studentCmd.Parameters.AddWithValue("@Age", student.Age);
                        studentCmd.Parameters.AddWithValue("@CourseID", student.CourseId);
                        studentCmd.Parameters.AddWithValue("@UserID", student.UserId);
                        studentCmd.ExecuteNonQuery();

                        transaction.Commit();
                        return "Student and user added successfully.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return $"Error: {ex.Message}";
                    }
                }
            }
        }



        //============================================================================ GET STUDENT =========================================================================================

        public List<Student> GetStudents()
        {
            var students = new List<Student>();
            using (var conn = new SQLiteConnection(@"Data Source=UnicomTic.db;Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT s.StudentID, s.Name, s.Address, s.Age, s.CourseID, u.UserID, u.Username, u.Password, u.Role FROM Students s JOIN Users u ON s.UserID = u.UserID;", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Address = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Age = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                            CourseId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                            UserId = reader.GetInt32(5),
                            Username = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Password = reader.IsDBNull(7) ? null : reader.GetString(7),
                            Role = reader.IsDBNull(8) ? null : reader.GetString(8)
                        });

                    }
                }
            }
            return students;
        }

        // ======================================================================== Update ==========================================================================

        public string UpdateStudent(Student student)
        {
            using (var conn = new SQLiteConnection(@"Data Source=UnicomTic.db;Version=3;"))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                       
                        var userCmd = new SQLiteCommand("UPDATE Users SET Username = @Username, Password = @Password, Role = @Role WHERE UserID = @UserID;", conn);
                        userCmd.Parameters.AddWithValue("@Username", student.Username);
                        userCmd.Parameters.AddWithValue("@Password", student.Password);
                        userCmd.Parameters.AddWithValue("@Role", student.Role);
                        userCmd.Parameters.AddWithValue("@UserID", student.UserId);
                        userCmd.ExecuteNonQuery();

                      
                        var studentCmd = new SQLiteCommand("UPDATE Students SET Name = @Name, Address = @Address, Age = @Age, CourseID = @CourseID WHERE StudentID = @StudentID;", conn);
                        studentCmd.Parameters.AddWithValue("@Name", student.Name);
                        studentCmd.Parameters.AddWithValue("@Address", student.Address);
                        studentCmd.Parameters.AddWithValue("@Age", student.Age);
                        studentCmd.Parameters.AddWithValue("@CourseID", student.CourseId);
                        studentCmd.Parameters.AddWithValue("@StudentID", student.Id);
                        studentCmd.ExecuteNonQuery();

                        transaction.Commit();
                        return "Student updated successfully.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return $"Error: {ex.Message}";
                    }
                }
            }
        }

        // ============================================================== Delete =====================================================================================

        public string DeleteStudent(int studentId)
        {
            using (var conn = new SQLiteConnection(@"Data Source=UnicomTic.db;Version=3;"))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                      
                        var studentCmd = new SQLiteCommand("DELETE FROM Students WHERE StudentID = @StudentID;", conn);
                        studentCmd.Parameters.AddWithValue("@StudentID", studentId);
                        studentCmd.ExecuteNonQuery();

                      
                        var userCmd = new SQLiteCommand("DELETE FROM Users WHERE UserID = (SELECT UserID FROM Students WHERE StudentID = @StudentID);", conn);
                        userCmd.Parameters.AddWithValue("@StudentID", studentId);
                        userCmd.ExecuteNonQuery();

                        transaction.Commit();
                        return "Student and associated user deleted successfully.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return $"Error: {ex.Message}";
                    }
                }
            }
        }



    }
}
    
          
    

