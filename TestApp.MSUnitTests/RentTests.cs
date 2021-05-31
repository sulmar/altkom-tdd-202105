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


        // Method_Scenario_ExptectedBehavior

        [TestMethod]
        public void CanReturn_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            User rentee = new User();
            Rent rent = new Rent { Rentee = rentee };

            // Act
            bool result = rent.CanReturn(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanReturn_UserIsRentee_ReturnsTrue()
        {
            // Arrange
            User rentee = new User();
            Rent rent = new Rent { Rentee = rentee };

            // Act
            bool result = rent.CanReturn(rentee);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void CanReturn_UserIsNotRentee_ReturnsFalse()
        {
            // Arrange
            User rentee = new User();
            Rent rent = new Rent { Rentee = rentee };

            // Act
            bool result = rent.CanReturn(new User());

            // Assert
            Assert.IsFalse(result);
            
        }


    }
}
