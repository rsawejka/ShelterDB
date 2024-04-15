using ShelterDB.Models;

namespace ShelterDB.Services
{
    public interface AnimalDataService
    {
        List<AnimalModel> GetAllAvailableAnimals();
        List<AnimalModel> GetAllAvailableAnimalsByName();
        List<AnimalModel> GetAllVetCareAnimals();
        List<AnimalModel> GetAllVetCareAnimalsByName();
        List<AnimalModel> GetAllAnimals();

        List<AnimalModel> SearchAnimals(string searchName, string searchStatus);

        AnimalModel GetAnimalById(int id);

        int InsertAnimal(AnimalModel animal);
        int DeleteAnimal(AnimalModel animal);
        int ProcessEditAnimal(AnimalModel animal);

        int GetLastAnimalId();

        bool SeeIfExists(int id);
    }
}
