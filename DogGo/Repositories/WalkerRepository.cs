using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalkerRepository : IWalkerRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalkerRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walker> GetAllWalkers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.[Name], w.ImageUrl, w.NeighborhoodId, n.Name as NeighborhoodName
                        FROM Walker w
                        LEFT JOIN Neighborhood n on n.Id = w.NeighborhoodId
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walker> walkers = new List<Walker>();
                    while (reader.Read())
                    {
                        Neighborhood neighborhood = new Neighborhood
                        {
                            Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                        };
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Neighborhood = neighborhood
                        };

                        walkers.Add(walker);
                    }

                    reader.Close();

                    return walkers;
                }
            }
        }

        public Walker GetWalkerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.[Name], w.ImageUrl, w.NeighborhoodId, n.Name as NeighborhoodName
                        FROM Walker w
                        LEFT JOIN Neighborhood n on n.Id = w.NeighborhoodId
                        WHERE w.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Neighborhood neighborhood = new Neighborhood
                        {
                            Name = reader.GetString(reader.GetOrdinal("NeighborhoodName"))
                        };
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Neighborhood = neighborhood
                        };
                        reader.Close();
                        return walker;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }

        //public void AddOwner(Owner owner)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //            INSERT INTO Owner ([Name], Email, Phone, Address, NeighborhoodId)
        //            OUTPUT INSERTED.ID
        //            VALUES (@name, @email, @phoneNumber, @address, @neighborhoodId);
        //        ";

        //            cmd.Parameters.AddWithValue("@name", owner.Name);
        //            cmd.Parameters.AddWithValue("@email", owner.Email);
        //            cmd.Parameters.AddWithValue("@phoneNumber", owner.Phone);
        //            cmd.Parameters.AddWithValue("@address", owner.Address);
        //            cmd.Parameters.AddWithValue("@neighborhoodId", owner.NeighborhoodId);

        //            int id = (int)cmd.ExecuteScalar();

        //            owner.Id = id;
        //        }
        //    }
        //}

        //public void UpdateOwner(Owner owner)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                    UPDATE Walker
        //                    SET 
        //                        [Name] = @name, 
        //                        Email = @email, 
        //                        Address = @address, 
        //                        Phone = @phone, 
        //                        NeighborhoodId = @neighborhoodId
        //                    WHERE Id = @id";

        //            cmd.Parameters.AddWithValue("@name", owner.Name);
        //            cmd.Parameters.AddWithValue("@email", owner.Email);
        //            cmd.Parameters.AddWithValue("@address", owner.Address);
        //            cmd.Parameters.AddWithValue("@phone", owner.Phone);
        //            cmd.Parameters.AddWithValue("@neighborhoodId", owner.NeighborhoodId);
        //            cmd.Parameters.AddWithValue("@id", owner.Id);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public void DeleteWalker(int walkerId)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                    DELETE FROM Walker
        //                    WHERE Id = @id
        //                ";

        //            cmd.Parameters.AddWithValue("@id", walkerId);

        //            cmd.ExecuteNonQuery();
        //        }

    }
}