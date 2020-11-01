using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryDate.Models
{
    public class Patron
    {
        #region properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TelephoneNumber { get; set; }
        #endregion

        //public virtual LibraryCard LibraryCard { get; set; }
    }
}
