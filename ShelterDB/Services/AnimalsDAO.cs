using ShelterDB.Models;
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

            string sqlStatement = "SELECT * FROM dbo.Animals";

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
        
        public AnimalModel GetAnimalById(int id)
        {
            AnimalModel foundAnimal = null;

            string sqlStatement = "SELECT * FROM dbo.Animals WHERE Id = @Id";

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

                        var AnimalDobReader = (DateTime) reader[8];
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
                 sqlStatement = "SELECT * FROM dbo.Animals WHERE Name LIKE @Name";
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
                 sqlStatement = "SELECT * FROM dbo.Animals WHERE Name LIKE @Name AND AnimalStatus = @Status";
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

            string sqlStatement = "SELECT * FROM dbo.Animals WHERE Id = @Id";

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
    }
}
