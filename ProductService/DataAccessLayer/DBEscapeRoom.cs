﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ModelLayer;

namespace ProductService.DataAccessLayer {
    class DBEscapeRoom : IESCAPEROOM<EscapeRoom> {

        private string _connectionString;
        public void Create(EscapeRoom entity) {
            throw new NotImplementedException();
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }

        public EscapeRoom GetForOwner(int ER_ID) {
            EscapeRoom escapeRoom = new EscapeRoom();
            String tempCheck;

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdReadERs = connection.CreateCommand()) {

                    cmdReadERs.CommandText = "SELECT EscapeRoom.*, CheckList.CheckList FROM EscapeRoom WHERE EscapeRoom.EscapeRoomID = @EscapeRoom.EscapeRoomID" +
                        " LEFT JOIN CheckList ON EscapeRoom.EscapeRoomID = CheckList.EscapeRoomID ";
                    cmdReadERs.Parameters.AddWithValue("EscapeRoom.EscapeRoomID", ER_ID);
                    SqlDataReader reader = cmdReadERs.ExecuteReader();
                    if (reader.Read()) {

                        escapeRoom.escapeRoomID = reader.GetInt32(reader.GetOrdinal("EscapeRoomID"));
                        escapeRoom.name = reader.GetString(reader.GetOrdinal("EsName"));
                        escapeRoom.price = reader.GetDecimal(reader.GetOrdinal("Price"));
                        escapeRoom.maxClearTime = reader.GetInt32(reader.GetOrdinal("MaxClearTime"));
                        escapeRoom.cleanTime = reader.GetInt32(reader.GetOrdinal("CleanTime"));
                        escapeRoom.description = reader.GetString(reader.GetOrdinal("EsDescription"));
                        escapeRoom.rating = reader.GetDecimal(reader.GetOrdinal("Rating"));

                        int i = 0;

                        while (reader.GetString(reader.GetOrdinal("CheckList")).Length > i) {
                            tempCheck = reader.GetString(reader.GetOrdinal("CheckList"));
                            escapeRoom.AddToList(tempCheck);
                            i++;
                        }

                    }
                }
            }
            return escapeRoom;
        }

        public IEnumerable<EscapeRoom> GetAllForOwner() {
            List<EscapeRoom> EscapeRooms = new List<EscapeRoom>();
            EscapeRoom tempER;
            String tempCheck;

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand cmdReadAllEs = connection.CreateCommand()) {

                    cmdReadAllEs.CommandText = "SELECT EscapeRoom.*, CheckList.CheckList FROM EscapeRoom LEFT JOIN CheckList ON EscapeRoom.EscapeRoomID = CheckList.EscapeRoomID ";
                   SqlDataReader reader =  cmdReadAllEs.ExecuteReader();

                    while (reader.Read()) {
                        tempER = new EscapeRoom();
                        tempER.escapeRoomID = reader.GetInt32(reader.GetOrdinal("EscapeRoomID"));
                        tempER.name = reader.GetString(reader.GetOrdinal("EsName"));
                        tempER.price = reader.GetDecimal(reader.GetOrdinal("Price"));
                        tempER.maxClearTime = reader.GetInt32(reader.GetOrdinal("MaxClearTime"));
                        tempER.cleanTime = reader.GetInt32(reader.GetOrdinal("CleanTime"));
                        tempER.description = reader.GetString(reader.GetOrdinal("EsDescription"));
                        tempER.rating = reader.GetDecimal(reader.GetOrdinal("Rating"));

                        int i = 0;

                        while (reader.GetString(reader.GetOrdinal("CheckList")).Length > i) {
                            tempCheck = reader.GetString(reader.GetOrdinal("CheckList"));
                            tempER.AddToList(tempCheck);
                            i++;
                        }

                        EscapeRooms.Add(tempER);
                    }

                }
               }
            return EscapeRooms;
        }

        public void Update(EscapeRoom entity) {


            throw new NotImplementedException();
        }
    }
}
