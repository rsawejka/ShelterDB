﻿using ShelterDB.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShelterDB.Services
{
    public class AnimalsDAO : AnimalDataService
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public int DeleteAnimal(AnimalModel animal)
        {
            int newIdNumber = -1;

            string sqlStatement = "DELETE FROM dbo.Animals WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@Id", animal.Id);

                try
                {
                    connection.Open();
                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return newIdNumber;
        }

        public int ProcessEditAnimal(AnimalModel animal)
        {
            int newIdNumber = -1;

            string sqlStatement = "UPDATE dbo.Animals SET Name = @Name, Breed = @Breed, Color = @Color, Microchip = @Microchip, AnimalImg = @AnimalImg, AnimalStatus = @AnimalStatus, Type = @Type, AnimalDob = @AnimalDob WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Breed", animal.Breed);
                command.Parameters.AddWithValue("@Color", animal.Color);
                command.Parameters.AddWithValue("@Microchip", animal.Microchip);
                command.Parameters.AddWithValue("@AnimalImg", animal.AnimalImg);
                command.Parameters.AddWithValue("@AnimalStatus", animal.AnimalStatus);
                command.Parameters.AddWithValue("@Type", animal.Type);
                command.Parameters.AddWithValue("@AnimalDob", animal.AnimalDob);
                command.Parameters.AddWithValue("@Id", animal.Id);

                try
                {
                    connection.Open();
                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return newIdNumber;
        }

        public List<AnimalModel> GetAllAvailableAnimals()
        {
            throw new NotImplementedException();
        }
        public List<AnimalModel> GetAllVetCareAnimals()
        {
            throw new NotImplementedException();
        }
        public List<AnimalModel> GetAllAnimals()
        {
            List<AnimalModel> foundAnimals = new List<AnimalModel>();

            string sqlStatement = "SELECT Id, Name, Breed, Color, Microchip, AnimalImg, AnimalStatus, Type, AnimalDob FROM dbo.Animals";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var AnimalDobReader = (DateTime)reader[8];
                        var AnimalDob = AnimalDobReader.Date.ToShortDateString();
                        foundAnimals.Add(new AnimalModel
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Breed = (string)reader[2],
                            Color = (string)reader[3],
                            Microchip = (string)reader[4],
                            AnimalImg = (string)reader[5],
                            AnimalStatus = (string)reader[6],
                            Type = (string)reader[7],
                            AnimalDob = AnimalDob,
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return foundAnimals;
        }
        public List<AllVetTreatmentsModel> GetAllAnimalVetTreatments(int id)
        {
            List<AllVetTreatmentsModel> foundVetTreatments = new List<AllVetTreatmentsModel>();

            string sqlStatement = @"Select 
                                    dbo.Animals.Id, 
                                    dbo.Animals.Name, 
                                    dbo.Animals.Breed,
                                    dbo.Animals.Color, 
                                    dbo.Animals.Microchip, 
                                    dbo.Animals.AnimalImg, 
                                    dbo.Animals.AnimalStatus, 
                                    dbo.Animals.Type, 
                                    dbo.Animals.AnimalDob, 
                                    dbo.VetTreatments.Id, 
                                    dbo.VetTreatments.Type,
                                    dbo.Manufacturer.Id,
                                    dbo.Manufacturer.name,
                                    dbo.Manufacturer.type
                                    FROM dbo.Animals 
                                    INNER JOIN dbo.VetTreatments ON dbo.Animals.Id = dbo.VetTreatments.AnimalId 
                                    LEFT JOIN dbo.Manufacturer On dbo.VetTreatments.ManId = dbo.Manufacturer.Id
                                    WHERE dbo.Animals.Id = @animalId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@animalId", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //var AnimalDateGivenReader = (DateTime)reader[2];
                        // var DateGiven = AnimalDateGivenReader.Date.ToShortDateString();
                        //var AnimalDateDueReader = (DateTime)reader[3];
                        // var DateDue = AnimalDateDueReader.Date.ToShortDateString();
                          var VetTreatmentId = Convert.ToInt32(reader[9]);
                        var manId = 0;
                        if(reader[11] == null)
                        {
                            manId = 0;
                        }
                        else
                        {
                            manId = Convert.ToInt32(reader[11]);
                        }
                        
                        foundVetTreatments.Add(new AllVetTreatmentsModel
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Breed = (string)reader[2],
                            Color = (string)reader[3],
                            Microchip = (string)reader[4],
                            AnimalImg = (string)reader[5],
                            AnimalStatus = (string)reader[6],
                            Type = (string)reader[7],
                            VetTreatmentId = VetTreatmentId,
                            VetTreatmentType = (string)reader[10],
                            ManId = manId,
                            ManName = (string)reader[12],
                            ManType = (string)reader[13],


                            // DateGiven = DateGiven,
                            // DateDue = DateDue,
                            //  AnimalId = AnimalId,

                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return foundVetTreatments;
        }       
        public List<AllVetTreatmentsModel> GetAllAnimalVetTreatmentsByDueDate(string DateDue)
        {
            List<AllVetTreatmentsModel> foundVetTreatments = new List<AllVetTreatmentsModel>();

            string sqlStatement = "Select * From dbo.Animals INNER JOIN dbo.VetTreatments ON dbo.Animals.Id = dbo.VetTreatments.AnimalId WHERE dbo.VetTreatments.DateDue = @DueDate";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@DueDate", DateDue);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                       var AnimalDateGivenReader = (DateTime)reader[11];
                       var DateGiven = AnimalDateGivenReader.Date.ToShortDateString();
                        var AnimalDateDueReader = (DateTime)reader[12];
                        var VetTreatmentDateDue = AnimalDateDueReader.Date.ToShortDateString();
                        var AnimalId = Convert.ToInt32(reader[13]);
                        foundVetTreatments.Add(new AllVetTreatmentsModel
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Breed = (string)reader[2],
                            Color = (string)reader[3],
                            Microchip = (string)reader[4],
                            AnimalImg = (string)reader[5],
                            AnimalStatus = (string)reader[6],
                            Type = (string)reader[7],
                            AnimalDob = reader[8].ToString(),
                            VetTreatmentId = (int)reader[9],
                            VetTreatmentType = (string)reader[10],
                            DateGiven = DateGiven,
                            DateDue = VetTreatmentDateDue,
                            AnimalId = AnimalId,

                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return foundVetTreatments;
        }

        public AnimalModel GetAnimalById(int id)
        {
            AnimalModel foundAnimal = null;

            string sqlStatement = "SELECT Id, Name, Breed, Color, Microchip, AnimalImg, AnimalStatus, Type, AnimalDob FROM dbo.Animals WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        var AnimalDobReader = (DateTime)reader[8];
                        var AnimalDob = AnimalDobReader.Date.ToShortDateString();

                        foundAnimal = new AnimalModel
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Breed = (string)reader[2],
                            Color = (string)reader[3],
                            Microchip = (string)reader[4],
                            AnimalImg = (string)reader[5],
                            AnimalStatus = (string)reader[6],
                            Type = (string)reader[7],
                            AnimalDob = AnimalDob,

                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return foundAnimal;
        }


        public int InsertAnimal(AnimalModel animal)
        {
            List<AnimalModel> newAnimal = new List<AnimalModel>();
            int newIdNumber = -1;

            string sqlStatement = "INSERT INTO dbo.Animals (Name, Breed, Color, Microchip, AnimalImg, AnimalStatus, Type, AnimalDob) VALUES (@Name, @Breed, @Color, @Microchip, @AnimalImg, @AnimalStatus, @Type, @AnimalDob)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Breed", animal.Breed);
                command.Parameters.AddWithValue("@Color", animal.Color);
                command.Parameters.AddWithValue("@Microchip", animal.Microchip);
                command.Parameters.AddWithValue("@AnimalImg", animal.AnimalImg);
                command.Parameters.AddWithValue("@AnimalStatus", animal.AnimalStatus);
                command.Parameters.AddWithValue("@Type", animal.Type);
                command.Parameters.AddWithValue("@AnimalDob", animal.AnimalDob);
                command.Parameters.AddWithValue("@Id", animal.Id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        newAnimal.Add(new AnimalModel
                        {
                            Id = (int)reader[0],
                            Name = (string)reader[1],
                            Breed = (string)reader[2],
                            Color = (string)reader[3],
                            Microchip = (string)reader[4],
                            AnimalImg = (string)reader[5],
                            AnimalStatus = (string)reader[6],
                            Type = (string)reader[7],
                            AnimalDob = (string)reader[8],
                            


                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return newIdNumber;
        }

        public List<AnimalModel> SearchAnimals(string searchName, string searchStatus)
        {
            List<AnimalModel> foundAnimals = new List<AnimalModel>();

            string sqlStatement = "";

            if(searchStatus == null) 
            {
                 sqlStatement = "SELECT Id, Name, Breed, Color, Microchip, AnimalImg, AnimalStatus, Type, AnimalDob FROM dbo.Animals WHERE Name LIKE @Name";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    command.Parameters.AddWithValue("@Name", '%' + searchName + '%');

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var AnimalDobReader = (DateTime)reader[8];
                            var AnimalDob = AnimalDobReader.Date.ToShortDateString();
                            foundAnimals.Add(new AnimalModel
                            {
                                Id = (int)reader[0],
                                Name = (string)reader[1],
                                Breed = (string)reader[2],
                                Color = (string)reader[3],
                                Microchip = (string)reader[4],
                                AnimalImg = (string)reader[5],
                                AnimalStatus = (string)reader[6],
                                Type = (string)reader[7],
                                AnimalDob = AnimalDob,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return foundAnimals;
            }
            else
            {
                 sqlStatement = "SELECT Id, Name, Breed, Color, Microchip, AnimalImg, AnimalStatus, Type, AnimalDob FROM dbo.Animals WHERE Name LIKE @Name AND AnimalStatus = @Status";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sqlStatement, connection);
                    command.Parameters.AddWithValue("@Name", '%' + searchName + '%');
                    command.Parameters.AddWithValue("@Status", searchStatus);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var AnimalDobReader = (DateTime)reader[8];
                            var AnimalDob = AnimalDobReader.Date.ToShortDateString();
                            foundAnimals.Add(new AnimalModel
                            {
                                Id = (int)reader[0],
                                Name = (string)reader[1],
                                Breed = (string)reader[2],
                                Color = (string)reader[3],
                                Microchip = (string)reader[4],
                                AnimalImg = (string)reader[5],
                                AnimalStatus = (string)reader[6],
                                Type = (string)reader[7],
                                AnimalDob = AnimalDob,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return foundAnimals;
            }

           

            
        }
        //USED FOR WHEN ADDING ANIMALS
        public int GetLastAnimalId()
        {
            var lastAnimalId = 0;

         

            string sqlStatement = "SELECT top 1 Id from dbo.Animals ORDER BY Id DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        lastAnimalId = (int)reader[0];
                    }
                        
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return lastAnimalId;
        }

        public List<AnimalModel> GetAllAvailableAnimalsByName()
        {
            throw new NotImplementedException();
        }

        public List<AnimalModel> GetAllVetCareAnimalsByName()
        {
            throw new NotImplementedException();
        }

        public bool SeeIfExists(int id)
        {
            var animalExists = false;

            string sqlStatement = "SELECT Id, Name, Breed, Color, Microchip, AnimalImg, AnimalStatus, Type, AnimalDob FROM dbo.Animals WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var animalName = (string)reader[1];
                        if(animalName == null)
                        {
                            animalExists = false;
                        }
                        else
                        {
                            animalExists = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return animalExists;
        }

        public int InsertVetTreatments(VetTreatmentModel vetTreatment)
        {
            List<VetTreatmentModel> newVetTreatment = new List<VetTreatmentModel>();
            int newIdNumber = -1;

            string sqlStatement = "INSERT INTO dbo.VetTreatments (Type, DateGiven, DateDue, AnimalId) VALUES (@Type, @DateGiven, @DateDue, @AnimalId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Type", vetTreatment.Type);
                command.Parameters.AddWithValue("@DateGiven", vetTreatment.DateGiven);
                command.Parameters.AddWithValue("@DateDue", vetTreatment.DateDue);
                command.Parameters.AddWithValue("@AnimalId", vetTreatment.AnimalId);


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        newVetTreatment.Add(new VetTreatmentModel
                        {
                            Id = (int)reader[0],
                            Type = (string)reader[1],
                            DateGiven = (string)reader[2],
                            DateDue = (string)reader[3],
                            AnimalId = (int)reader[4],




                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return newIdNumber;
        }
    }

}

