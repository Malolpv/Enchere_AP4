using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enchere_AP4.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Enchere_AP4.Models;
using Xamarin.Forms.Maps;

namespace Enchere_AP4.ViewModels.Tests
{
    [TestClass()]
    public class MagasinMapViewModelTests
    {
        [TestMethod()]
        public void LoadPinsMagasinsTest()
        {
            //preparation du jeu d'essai

            ObservableCollection<Magasin> collMagasins = new ObservableCollection<Magasin>() { };

            collMagasins.Add(new Magasin()
            {
                Nom = "Test 1",
                Adresse = "rue des cordiers",
                Ville = "Lannion",
                CodePostal = 22520,
                Id = 1,
                Latitude = 48.731151,
                Longitude = -3.458822
            });

            collMagasins.Add(new Magasin()
            {
                Nom = "Test 2",
                Adresse = "rue des cordiers",
                Ville = "Lannion",
                CodePostal = 22520,
                Id = 2,
                Latitude = 49.731151,
                Longitude = -2.458822
            });

            collMagasins.Add(new Magasin()
            {
                Nom = "Test 3",
                Adresse = "rue des cordiers",
                Ville = "Lannion",
                CodePostal = 22520,
                Id = 3,
                Latitude = 50.731151,
                Longitude = -5.458822
            });

            MagasinMapViewModel viewModel = new MagasinMapViewModel();


            ObservableCollection<Pin> expected = new ObservableCollection<Pin>();

            expected.Add(new Pin()
            {
                Address = "rue des cordiers" + " " + 22520 + " " + "Lannion",
                Label = "Test 1",
                Position = new Position(48.731151, -3.458822)
            });

            expected.Add(new Pin()
            {
                Address = "rue des cordiers" + " " + 22520 + " " + "Lannion",
                Label = "Test 2",
                Position = new Position(49.731151, -2.458822)
            });

            expected.Add(new Pin()
            {
                Address = "rue des cordiers" + " " + 22520 + " " + "Lannion",
                Label = "Test 3",
                Position = new Position(50.731151, -5.458822)
            });

            Assert.Equals(expected,viewModel.LoadPinsMagasins(collMagasins));

        }
    }
}