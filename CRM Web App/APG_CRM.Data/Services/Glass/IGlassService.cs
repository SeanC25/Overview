using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;
using System.Collections.Generic; // To use List<T>

namespace APG_CRM.Data.Services
{
    public interface IGlassService
    {
        void Initialise();

        //*** ==================== Glass Entity Services Management  CRUD operations (Create, Read, Update, Delete),=========================
        Glass AddGlass(Glass g); //Create method
        Glass GetGlassById(int id); //read method
        Glass UpdateGlass(Glass g); //Update method
        bool DeleteGlass(int id); //Delete method

        //*** ==================== Glass Entity Services helper methods/Search Functions=========================

        //List<Glass> GetAllGlass();

        IQueryable<Glass> GetAllGlass();
        Glass GetGlassByName(string Name);
        Glass GetGlassByType(string Type);

        //Pagination
        Paged<Glass> GetAllGlassPaged(string searchTerm = "", string sortBy = "Id", string direction = "asc", int page = 1, int size = 20);

        // ====================Stock Entity Services Search Functions =========================
    }
}


// ====================Stock Entity Services Search Functions =========================
