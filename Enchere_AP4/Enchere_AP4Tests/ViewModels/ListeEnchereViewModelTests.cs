using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enchere_AP4.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enchere_AP4.ViewModels.Tests
{
    [TestClass()]
    public class ListeEnchereViewModelTests
    {
        [TestMethod()]
        public void GetEnchereEnCoursTest()
        {


            ListeEnchereViewModel viewModel = new ListeEnchereViewModel();
            viewModel.GetEnchereEnCours();
            Assert.Fail();
        }
    }
}