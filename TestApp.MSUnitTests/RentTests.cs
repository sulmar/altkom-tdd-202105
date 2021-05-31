using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.MSUnitTests
{
    [TestClass]
    public class RentTests
    {

        // 1. Czy użytkownik jest adminem -> true
        // 2. Czy użytkownik jest wynajmującym -> true
        // 3. Czy użytkownik jest inny niż ten, który wynajął -> false


        private User rentee;
        private Rent rent;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            rentee = new User();
            rent = new Rent { Rentee = rentee };

        }


        // Method_Scenario_ExptectedBehavior

        [TestMethod]
        public void CanReturn_UserIsAdmin_ReturnsTrue()
        {
            // Act
            bool result = rent.CanReturn(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanReturn_UserIsRentee_ReturnsTrue()
        {
            // Act
            bool result = rent.CanReturn(rentee);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void CanReturn_UserIsNotRentee_ReturnsFalse()
        {
            // Act
            bool result = rent.CanReturn(new User());

            // Assert
            Assert.IsFalse(result);
            
        }

        //[TestMethod]
        //public void CanReturn_UserIsEmpty_ReturnsFalse()
        //{
        //    // Act
        //    bool result = rent.CanReturn(null);

        //    // Assert
        //    Assert.IsFalse(result);
        //}

        // ArgumentNullException

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanReturn_UserIsEmpty_ThrowsArgumentNullException()
        {
            // Act
            bool result = rent.CanReturn(null);

            // Assert

        }


    }
}
