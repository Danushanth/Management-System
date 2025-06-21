using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTicManagementSystem.Repositories;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Controllers
{
    public class CourseController
    {
        public List<Course> GetAllCourses()
        {
            var courseList = new List<Course>();

            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("SELECT CourseID, CourseName FROM Courses", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    courseList.Add(new Course
                    {
                        CourseId = reader.GetInt32(0),
                        CourseName = reader.GetString(1)
                    });
                }
            }

            return courseList;
        }

// =============================================================================================================================================================================

        public void AddCourse(string courseName)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("INSERT INTO Courses (CourseName) VALUES (@CourseName)", conn))
            {
                cmd.Parameters.AddWithValue("@CourseName", courseName);
                cmd.ExecuteNonQuery();
            }
        }

// =============================================================================================================================================================================

        public void UpdateCourse(int courseId, string courseName)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("UPDATE Courses SET CourseName = @CourseName WHERE CourseID = @CourseID", conn))
            {
                cmd.Parameters.AddWithValue("@CourseName", courseName);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                cmd.ExecuteNonQuery();
            }
        }

// ==============================================================================================================================================================================

        public void DeleteCourse(int courseId)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = new SQLiteCommand("DELETE FROM Courses WHERE CourseID = @CourseID", conn))
            {
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
