using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTicManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class TimetableController
    {
        public static List<TimetableModel> GetAllTimetables()
        {
            List<TimetableModel> list = new List<TimetableModel>();

            using (var conn = DbConfig.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT t.TimetableID, c.CourseName, s.SubjectName, t.RoomType, t.TimeSlot
                                    FROM Timetables t
                                    JOIN Subjects s ON t.SubjectID = s.SubjectID
                                    JOIN Courses c ON s.CourseID = c.CourseID";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TimetableModel
                        {
                            Id = Convert.ToInt32(reader["TimetableID"]),
                            Course = reader["CourseName"].ToString(),
                            Subject = reader["SubjectName"].ToString(),
                            RoomType = reader["RoomType"].ToString(),
                            TimeSlot = reader["TimeSlot"].ToString()
                        });
                    }
                }
            }

            return list;
        }

// ==============================================================================================================================================================================
        public static void AddTimetable(int subjectId, string roomType, string timeSlot)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Timetables (SubjectID, RoomType, TimeSlot) VALUES (@s, @r, @t)";
                cmd.Parameters.AddWithValue("@s", subjectId);
                cmd.Parameters.AddWithValue("@r", roomType);
                cmd.Parameters.AddWithValue("@t", timeSlot);
                cmd.ExecuteNonQuery();
            }
        }

// ==============================================================================================================================================================================
        public static void UpdateTimetable(int id, int subjectId, string roomType, string timeSlot)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE Timetables SET SubjectID = @s, RoomType = @r, TimeSlot = @t WHERE TimetableID = @id";
                cmd.Parameters.AddWithValue("@s", subjectId);
                cmd.Parameters.AddWithValue("@r", roomType);
                cmd.Parameters.AddWithValue("@t", timeSlot);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

// ==============================================================================================================================================================================
        public static void DeleteTimetable(int id)
        {
            using (var conn = DbConfig.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Timetables WHERE TimetableID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
