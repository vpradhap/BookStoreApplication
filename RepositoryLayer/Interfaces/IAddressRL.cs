using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        public string AddAddress(AddressModel address, int userId);
        public AddressModel UpdateAddress(AddressModel address, int userId);
        public List<AddressModel> GetAllAddress(int userId);
    }
}
