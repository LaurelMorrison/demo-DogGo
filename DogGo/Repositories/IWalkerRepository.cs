using DogGo.Controllers;
using DogGo.Models;
using DogGo.Repositories;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalkerRepository
    {
        List<Walker> GetAllWalkers();
        Walker GetWalkerById(int id);

        void AddWalker(Walker walker);

        void UpdateWalker(Walker Walker);

        void DeleteWalker(int walkerId);

        List<Walker> GetWalkersInNeighborhood(int neighborhoodId);

    }

}