using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDD
{
    public class HotelDatabase
    {
        public static string ConnectionString = Path.Combine(AppContext.BaseDirectory, @"..\..\..\DoDoParipope.db");

        public static List<Rooms> GetRooms()
        {
            var rooms = new List<Rooms>();

            using var conn = new SQLiteConnection($"Data Source={ConnectionString};Version=3;");
            conn.Open();

            using var cmd = new SQLiteCommand("SELECT * FROM Room", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                rooms.Add(new Rooms
                {
                    Id = reader.GetInt32(0),
                    Number = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Layer = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    Category = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                    Locked = reader.IsDBNull(4) ? "" : reader.GetString(4)
                });
            }

            return rooms;
        }

        public static List<Guests> GetGuests()
        {
            var guests = new List<Guests>();

            using var conn = new SQLiteConnection($"Data Source={ConnectionString};Version=3;");
            conn.Open();

            using var cmd = new SQLiteCommand("SELECT * FROM Guest", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                guests.Add(new Guests
                {
                    Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Surname = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    LastName = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Phone = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Passport = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    RoomID = reader.IsDBNull(6) ? 0 : reader.GetInt32(6)
                });
            }

            return guests;
        }

        public static Guests AddGuest(Guests guest)
        {
            using var conn = new SQLiteConnection($"Data Source={ConnectionString};Version=3;");
            conn.Open();

            string query = @"
        INSERT INTO Guest (Name, Surname, Last_Name, Phone, Passport, RoomID) 
        VALUES (@name, @surname, @lastname, @phone, @passport, @roomid);
        SELECT last_insert_rowid();";

            using var cmd = new SQLiteCommand(query, conn);

            cmd.Parameters.AddWithValue("@name", guest.Name);
            cmd.Parameters.AddWithValue("@surname", guest.Surname);
            cmd.Parameters.AddWithValue("@lastname", guest.LastName);
            cmd.Parameters.AddWithValue("@phone", guest.Phone);
            cmd.Parameters.AddWithValue("@passport", guest.Passport);
            cmd.Parameters.AddWithValue("@roomid", guest.RoomID);

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                guest.Id = Convert.ToInt32(result);
            }

            return guest;
        }
    }
}