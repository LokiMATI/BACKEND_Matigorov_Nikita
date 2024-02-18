namespace Homework;

using System.Data.SQLite;
using System.Collections.Generic;

public class SQLiteLectureRepository : ILectureRepository
{
    private string connectionBD;
    private List<Lecture> lectures = new List<Lecture>();
    private const string CreateTableIfExists = @"CREATE TABLE IF NOT EXISTS Lectures (Id INTEGER PRIMARY KEY, Theme TEXT NOT NULL, Date TEXT NOT NULL, Text TEXT)";

    public SQLiteLectureRepository(string _connectionBD)
    {
        connectionBD = _connectionBD;
        CreateDatabase();
        ReadDataFromBD();
    }

    private void CreateDatabase()
    {
        SQLiteConnection connection = new SQLiteConnection(connectionBD);
        Console.WriteLine($"BD: {connectionBD} created.");
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(CreateTableIfExists, connection);
        command.ExecuteNonQuery();
    }

    private void ReadDataFromBD()
    {
        List<GALecture> GALectures = GetAllLectures();
        foreach (GALecture obj in GALectures)
        {
            lectures.Add(obj._lecture);
        }
    }

    public List<GALecture> GetAllLectures()
    {
        List<GALecture> GALectures = new List<GALecture>();
        using (SQLiteConnection connection = new SQLiteConnection(connectionBD))
        {
            connection.Open();
            string query = "SELECT * FROM Lectures";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Lecture lecture = new Lecture(reader["Theme"].ToString(), reader["Text"].ToString(), reader["Date"].ToString());
                        GALecture gaLecture = new GALecture(Convert.ToInt32(reader["Id"]), lecture);
                        GALectures.Add(gaLecture);
                    }
                }
            }
        }
        return GALectures;
    }

    public void AddLecture(Lecture lecture)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionBD))
        {
            connection.Open();
            string query = "INSERT INTO Lectures (Theme, Date, Text) VALUES (@Theme, @Date, @Text)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Theme", lecture.Theme);
                command.Parameters.AddWithValue("@Date", lecture.Date);
                command.Parameters.AddWithValue("@Text", lecture.Text);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteLecture(int id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionBD))
        {
            connection.Open();
            string query = "DELETE FROM Lectures WHERE Id = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}