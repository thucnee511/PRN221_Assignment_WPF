﻿using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE171089_Daos
{
    public class RentDao : IDao<Rent>
    {
        private static RentDao instance = null;
        private LibraryManagementContext context = null;
        private RentDao()
        {
            context = new LibraryManagementContext();
        }
        public static RentDao Instance
        {
            get
            {
                instance ??= new RentDao();
                return instance;
            }
        }
        public List<Rent> GetList() => context.Rents.Include(r => r.RentDetails).Include(r => r.User).ToList();
        public Rent GetItem(int id) => context.Rents.Include(r => r.RentDetails).Include(r => r.User).SingleOrDefault(rent => rent.Id == id);
        public Rent Insert(Rent item)
        {
            context.Rents.Add(item);
            context.SaveChanges();
            //get the lastest rent
            item = context.Rents.OrderByDescending(r => r.Id).FirstOrDefault();
            return item;
        }
        public Rent Update(Rent item)
        {
            context.Rents.Update(item);
            context.SaveChanges();
            return item;
        }
        public Rent Delete(int id)
        {
            Rent item = GetItem(id);
            context.Rents.Remove(item);
            context.SaveChanges();
            return item;
        }
    }
}
