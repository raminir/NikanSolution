using Models;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.Repositories
{
    public class AdoTicketRepository : ITicketRepository
    {
        private readonly string _connectionString;

        public AdoTicketRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Ticket CreateTicket(Ticket model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"INSERT INTO {nameof(Ticket)} ({nameof(Ticket.TicketNumber)}, {nameof(Ticket.CreatedAt)}) VALUES ({model.TicketNumber}, {model.CreatedAt})", connection);
                command.ExecuteNonQuery();
            }
            return model;
        }

        public List<TicketInRooms> GetAll()
        {
            var ticketInRooms = new List<TicketInRooms>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT * FROM {nameof(TicketInRooms)}", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ticketInRooms.Add(new TicketInRooms
                        {
                            Id = (int)reader[$"{nameof(TicketInRooms.Id)}"],
                        });
                    }
                }
            }
            return ticketInRooms;
        }

        public int GetLastTicketNumber(DateTime date)
        {
            int ticketNumber = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT * FROM Tickets WHERE {nameof(Ticket.CreatedAt)} = {date} ", connection);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ticketNumber = (int)reader["Id"];
                    }
                }
            }
            return ticketNumber;
        }

        public void UpdateStatus(TicketInRooms model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"UPDATE {nameof(TicketInRooms)} SET {nameof(TicketInRooms.StatusId)} = {model.StatusId} WHERE {nameof(TicketInRooms.Id)} = {model.Id}", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
